using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Sonigon;
using Sonigon.Internal;
using SoundImplementation;
using UnboundLib;
using UnboundLib.Utils;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000083 RID: 131
	public class TossMono : MonoBehaviour
	{
		// Token: 0x0600034D RID: 845 RVA: 0x00019818 File Offset: 0x00017A18
		private void Start()
		{
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.data = base.GetComponent<CharacterData>();
			this.gun = base.GetComponent<Gun>();
			this.ResetEffectTimer();
			this.ResetTimer();
			this.canTrigger = true;
			this.hasTriggered = false;
			this.basic = this.block.BlockAction;
			if (this.block)
			{
				this.gravy = new Action<BlockTrigger.BlockTriggerType>(this.GetDoBlockAction(this.player, this.block).Invoke);
				this.block.BlockAction = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(this.block.BlockAction, this.gravy);
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000198D6 File Offset: 0x00017AD6
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000198DE File Offset: 0x00017ADE
		public void OnDestroy()
		{
			this.block.BlockAction = this.basic;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x000198F4 File Offset: 0x00017AF4
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Toss")
					{
						this.numcheck++;
					}
				}
				if (this.numcheck > 0)
				{
					this.ResetTimer();
					if (Time.time >= this.timeOfLastEffect + this.effectCooldown)
					{
						this.canTrigger = true;
						return;
					}
					this.canTrigger = false;
					return;
				}
				else
				{
					Object.Destroy(this);
				}
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000199A8 File Offset: 0x00017BA8
		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block)
		{
			return delegate(BlockTrigger.BlockTriggerType trigger)
			{
				if (trigger != 1)
				{
					if (!TossMono.fieldsound)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("toss");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						TossMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						TossMono.fieldsound.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity.intensity = this.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
					SoundManager.Instance.Play(TossMono.fieldsound, this.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
					Object.Instantiate<GameObject>(TossMono.tossVisual, this.player.transform.position, Quaternion.identity);
					Vector2 vector = block.transform.position;
					Player[] array = PlayerManager.instance.players.ToArray();
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].teamID != player.teamID && (Vector2.Distance(vector, array[i].transform.position) < this.maxDistance && PlayerManager.instance.CanSeePlayer(player.transform.position, array[i]).canSee))
						{
							HealthHandler component = array[i].transform.GetComponent<HealthHandler>();
							if (this.canTrigger)
							{
								component.TakeDamage(25f * Vector2.down, array[i].transform.position, this.player.data.weaponHandler.gameObject, this.player, true, false);
								array[i].data.jump.Jump(true, 3f);
								component.gameObject.AddComponent<TossEffect>();
								this.hasTriggered = true;
							}
						}
					}
					if (this.hasTriggered)
					{
						this.hasTriggered = false;
						this.canTrigger = false;
						this.ResetEffectTimer();
					}
				}
			};
		}

		// Token: 0x06000352 RID: 850 RVA: 0x000199CF File Offset: 0x00017BCF
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x000199E3 File Offset: 0x00017BE3
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000354 RID: 852 RVA: 0x000199F0 File Offset: 0x00017BF0
		// (set) Token: 0x06000355 RID: 853 RVA: 0x00019C27 File Offset: 0x00017E27
		public static GameObject tossVisual
		{
			get
			{
				GameObject result;
				if (TossMono.tossvisual != null)
				{
					result = TossMono.tossvisual;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					TossMono.tossvisual = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					TossMono.tossvisual.name = "E_Toss";
					Object.DontDestroyOnLoad(TossMono.tossvisual);
					ParticleSystem[] componentsInChildren = TossMono.tossvisual.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(180f, 30f, 180f);
					}
					TossMono.tossvisual.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(180f, 30f, 180f), 0.3f)
					};
					Object.Destroy(TossMono.tossvisual.transform.GetChild(2).gameObject);
					TossMono.tossvisual.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					TossMono.tossvisual.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(TossMono.tossvisual.GetComponent<FollowPlayer>());
					TossMono.tossvisual.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(TossMono.tossvisual.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(TossMono.tossvisual.GetComponent<Explosion>());
					Object.Destroy(TossMono.tossvisual.GetComponent<Explosion_Overpower>());
					Object.Destroy(TossMono.tossvisual.GetComponent<RemoveAfterSeconds>());
					TossMono.tossvisual.AddComponent<TossMono.TossSpawner>();
					result = TossMono.tossvisual;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x04000439 RID: 1081
		private readonly float maxDistance = 8f;

		// Token: 0x0400043A RID: 1082
		public Block block;

		// Token: 0x0400043B RID: 1083
		public Player player;

		// Token: 0x0400043C RID: 1084
		public CharacterData data;

		// Token: 0x0400043D RID: 1085
		public Gun gun;

		// Token: 0x0400043E RID: 1086
		private Action<BlockTrigger.BlockTriggerType> gravy;

		// Token: 0x0400043F RID: 1087
		private Action<BlockTrigger.BlockTriggerType> basic;

		// Token: 0x04000440 RID: 1088
		private static GameObject tossvisual;

		// Token: 0x04000441 RID: 1089
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000442 RID: 1090
		private readonly float effectCooldown;

		// Token: 0x04000443 RID: 1091
		private float startTime;

		// Token: 0x04000444 RID: 1092
		private float timeOfLastEffect;

		// Token: 0x04000445 RID: 1093
		private bool canTrigger;

		// Token: 0x04000446 RID: 1094
		private bool hasTriggered;

		// Token: 0x04000447 RID: 1095
		public int numcheck;

		// Token: 0x04000448 RID: 1096
		public static SoundEvent fieldsound;

		// Token: 0x04000449 RID: 1097
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.3f, 0);

		// Token: 0x0200014E RID: 334
		private class TossSpawner : MonoBehaviour
		{
			// Token: 0x06000981 RID: 2433 RVA: 0x0002A528 File Offset: 0x00028728
			private void Start()
			{
				base.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				base.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 1f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "inited", false);
				typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 4.4f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "startWidth", 1.4f);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
			}
		}
	}
}
