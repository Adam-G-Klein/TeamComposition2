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
	// Token: 0x02000081 RID: 129
	public class CrushMono : MonoBehaviour
	{
		// Token: 0x0600033C RID: 828 RVA: 0x00019230 File Offset: 0x00017430
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

		// Token: 0x0600033D RID: 829 RVA: 0x000192EE File Offset: 0x000174EE
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000192F6 File Offset: 0x000174F6
		public void OnDestroy()
		{
			this.block.BlockAction = this.basic;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0001930C File Offset: 0x0001750C
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Crush")
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

		// Token: 0x06000340 RID: 832 RVA: 0x000193C0 File Offset: 0x000175C0
		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block)
		{
			return delegate(BlockTrigger.BlockTriggerType trigger)
			{
				if (trigger != 1)
				{
					if (!CrushMono.fieldsound)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("crush");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						CrushMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						CrushMono.fieldsound.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity.intensity = this.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
					SoundManager.Instance.Play(CrushMono.fieldsound, this.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
					Object.Instantiate<GameObject>(CrushMono.crushVisual, this.player.transform.position, Quaternion.identity);
					Vector2 vector = block.transform.position;
					Player[] array = PlayerManager.instance.players.ToArray();
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].teamID != player.teamID && (Vector2.Distance(vector, array[i].transform.position) < this.maxDistance && PlayerManager.instance.CanSeePlayer(player.transform.position, array[i]).canSee))
						{
							HealthHandler component = array[i].transform.GetComponent<HealthHandler>();
							if (this.canTrigger)
							{
								component.TakeDamage(20f * Vector2.down, array[i].transform.position, this.player.data.weaponHandler.gameObject, this.player, true, false);
								component.TakeForce(Vector2.MoveTowards(array[i].transform.position, new Vector2(array[i].transform.position.x, array[i].transform.position.y - 12000f), 12000f), 1, true, false, 0.8f);
								component.gameObject.AddComponent<CrushEffect>();
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

		// Token: 0x06000341 RID: 833 RVA: 0x000193E7 File Offset: 0x000175E7
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000193FB File Offset: 0x000175FB
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00019408 File Offset: 0x00017608
		// (set) Token: 0x06000344 RID: 836 RVA: 0x0001963F File Offset: 0x0001783F
		public static GameObject crushVisual
		{
			get
			{
				GameObject result;
				if (CrushMono.crushvisual != null)
				{
					result = CrushMono.crushvisual;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					CrushMono.crushvisual = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					CrushMono.crushvisual.name = "E_Crush";
					Object.DontDestroyOnLoad(CrushMono.crushvisual);
					ParticleSystem[] componentsInChildren = CrushMono.crushvisual.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(255f, 20f, 20f);
					}
					CrushMono.crushvisual.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(255f, 0f, 0f), 0.3f)
					};
					Object.Destroy(CrushMono.crushvisual.transform.GetChild(2).gameObject);
					CrushMono.crushvisual.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					CrushMono.crushvisual.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(CrushMono.crushvisual.GetComponent<FollowPlayer>());
					CrushMono.crushvisual.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(CrushMono.crushvisual.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(CrushMono.crushvisual.GetComponent<Explosion>());
					Object.Destroy(CrushMono.crushvisual.GetComponent<Explosion_Overpower>());
					Object.Destroy(CrushMono.crushvisual.GetComponent<RemoveAfterSeconds>());
					CrushMono.crushvisual.AddComponent<CrushMono.CrushSpawner>();
					result = CrushMono.crushvisual;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x04000423 RID: 1059
		private readonly float maxDistance = 8f;

		// Token: 0x04000424 RID: 1060
		public Block block;

		// Token: 0x04000425 RID: 1061
		public Player player;

		// Token: 0x04000426 RID: 1062
		public CharacterData data;

		// Token: 0x04000427 RID: 1063
		public Gun gun;

		// Token: 0x04000428 RID: 1064
		private Action<BlockTrigger.BlockTriggerType> gravy;

		// Token: 0x04000429 RID: 1065
		private Action<BlockTrigger.BlockTriggerType> basic;

		// Token: 0x0400042A RID: 1066
		private static GameObject crushvisual;

		// Token: 0x0400042B RID: 1067
		private readonly float updateDelay = 0.1f;

		// Token: 0x0400042C RID: 1068
		private readonly float effectCooldown;

		// Token: 0x0400042D RID: 1069
		private float startTime;

		// Token: 0x0400042E RID: 1070
		private float timeOfLastEffect;

		// Token: 0x0400042F RID: 1071
		private bool canTrigger;

		// Token: 0x04000430 RID: 1072
		private bool hasTriggered;

		// Token: 0x04000431 RID: 1073
		public int numcheck;

		// Token: 0x04000432 RID: 1074
		public static SoundEvent fieldsound;

		// Token: 0x04000433 RID: 1075
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.3f, 0);

		// Token: 0x0200014B RID: 331
		private class CrushSpawner : MonoBehaviour
		{
			// Token: 0x0600097A RID: 2426 RVA: 0x0002A120 File Offset: 0x00028320
			private void Start()
			{
				base.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				base.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 1f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "inited", false);
				typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 4.4f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "startWidth", 3.8f);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
			}
		}
	}
}
