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
	// Token: 0x02000012 RID: 18
	public class TaserMono : MonoBehaviour
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00004298 File Offset: 0x00002498
		private void Start()
		{
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.data = base.GetComponent<CharacterData>();
			this.ResetEffectTimer();
			this.ResetTimer();
			this.canTrigger = true;
			this.hasTriggered = false;
			this.basic = this.block.BlockAction;
			this.numcheck = 0;
			if (this.block)
			{
				this.taser = new Action<BlockTrigger.BlockTriggerType>(this.GetDoBlockAction(this.player, this.block).Invoke);
				this.block.FirstBlockActionThatDelaysOthers = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(this.block.FirstBlockActionThatDelaysOthers, this.taser);
				this.block.delayOtherActions = true;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000435D File Offset: 0x0000255D
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004365 File Offset: 0x00002565
		public void OnDestroy()
		{
			this.block.BlockAction = this.basic;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00004378 File Offset: 0x00002578
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Taser")
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

		// Token: 0x06000055 RID: 85 RVA: 0x0000442C File Offset: 0x0000262C
		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block)
		{
			return delegate(BlockTrigger.BlockTriggerType trigger)
			{
				if (trigger != 1)
				{
					if (!TaserMono.fieldsound)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("taser");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						TaserMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						TaserMono.fieldsound.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity.intensity = this.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
					SoundManager.Instance.Play(TaserMono.fieldsound, this.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
					Object.Instantiate<GameObject>(TaserMono.taserVisual, this.player.transform.position, Quaternion.identity);
					Vector2 vector = block.transform.position;
					Player[] array = PlayerManager.instance.players.ToArray();
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].teamID != player.teamID && (Vector2.Distance(vector, array[i].transform.position) < this.maxDistance && PlayerManager.instance.CanSeePlayer(player.transform.position, array[i]).canSee))
						{
							array[i].transform.GetComponent<StunHandler>();
							array[i].transform.GetComponent<DamageOverTime>();
							if (this.canTrigger)
							{
								NetworkingManager.RPC(typeof(StunMono), "RPCA_StunPlayer", new object[]
								{
									0.75f,
									array[i].playerID
								});
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

		// Token: 0x06000056 RID: 86 RVA: 0x00004453 File Offset: 0x00002653
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004467 File Offset: 0x00002667
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00004474 File Offset: 0x00002674
		// (set) Token: 0x06000059 RID: 89 RVA: 0x0000468D File Offset: 0x0000288D
		public static GameObject taserVisual
		{
			get
			{
				GameObject result;
				if (TaserMono.taserVisu != null)
				{
					result = TaserMono.taserVisu;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					TaserMono.taserVisu = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					TaserMono.taserVisu.name = "E_Taser";
					Object.DontDestroyOnLoad(TaserMono.taserVisu);
					ParticleSystem[] componentsInChildren = TaserMono.taserVisu.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = Color.yellow;
					}
					TaserMono.taserVisu.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(Color.yellow, 0f)
					};
					Object.Destroy(TaserMono.taserVisu.transform.GetChild(2).gameObject);
					TaserMono.taserVisu.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					TaserMono.taserVisu.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(TaserMono.taserVisu.GetComponent<FollowPlayer>());
					TaserMono.taserVisu.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(TaserMono.taserVisu.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(TaserMono.taserVisu.GetComponent<Explosion>());
					Object.Destroy(TaserMono.taserVisu.GetComponent<Explosion_Overpower>());
					Object.Destroy(TaserMono.taserVisu.GetComponent<RemoveAfterSeconds>());
					TaserMono.taserVisu.AddComponent<TaserMono.TaserSpawner>();
					result = TaserMono.taserVisu;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x0400004E RID: 78
		private readonly float maxDistance = 8.2f;

		// Token: 0x0400004F RID: 79
		public Block block;

		// Token: 0x04000050 RID: 80
		public Player player;

		// Token: 0x04000051 RID: 81
		public CharacterData data;

		// Token: 0x04000052 RID: 82
		private Action<BlockTrigger.BlockTriggerType> taser;

		// Token: 0x04000053 RID: 83
		private Action<BlockTrigger.BlockTriggerType> basic;

		// Token: 0x04000054 RID: 84
		private static GameObject taserVisu;

		// Token: 0x04000055 RID: 85
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000056 RID: 86
		private readonly float effectCooldown = 1f;

		// Token: 0x04000057 RID: 87
		private float startTime;

		// Token: 0x04000058 RID: 88
		private float timeOfLastEffect;

		// Token: 0x04000059 RID: 89
		private bool canTrigger;

		// Token: 0x0400005A RID: 90
		private bool hasTriggered;

		// Token: 0x0400005B RID: 91
		public int numcheck;

		// Token: 0x0400005C RID: 92
		public static SoundEvent fieldsound;

		// Token: 0x0400005D RID: 93
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.5f, 0);

		// Token: 0x0200010A RID: 266
		private class TaserSpawner : MonoBehaviour
		{
			// Token: 0x060008D8 RID: 2264 RVA: 0x000263E0 File Offset: 0x000245E0
			private void Start()
			{
				base.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				base.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 1.5f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "inited", false);
				typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 4.4f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "startWidth", 1f);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
			}
		}
	}
}
