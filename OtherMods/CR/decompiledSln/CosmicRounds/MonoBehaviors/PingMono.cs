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
	// Token: 0x02000052 RID: 82
	public class PingMono : MonoBehaviour
	{
		// Token: 0x06000206 RID: 518 RVA: 0x00010BD0 File Offset: 0x0000EDD0
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

		// Token: 0x06000207 RID: 519 RVA: 0x00010C8E File Offset: 0x0000EE8E
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00010C96 File Offset: 0x0000EE96
		public void OnDestroy()
		{
			this.block.BlockAction = this.basic;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00010CAC File Offset: 0x0000EEAC
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Ping")
					{
						this.numcheck++;
					}
				}
				if (this.numcheck > 0)
				{
					this.ResetTimer();
					if (Time.time >= this.timeOfLastEffect)
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

		// Token: 0x0600020A RID: 522 RVA: 0x00010D59 File Offset: 0x0000EF59
		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block)
		{
			return delegate(BlockTrigger.BlockTriggerType trigger)
			{
				if (trigger != 1)
				{
					if (!PingMono.fieldsound)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("golden");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						PingMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						PingMono.fieldsound.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity.intensity = this.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.3f * CR.globalVolMute.Value;
					SoundManager.Instance.Play(PingMono.fieldsound, this.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
					Object.Instantiate<GameObject>(PingMono.pingVisual, this.player.transform.position, Quaternion.identity);
					Vector2 vector = block.transform.position;
					Player[] array = PlayerManager.instance.players.ToArray();
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].teamID != player.teamID && (Vector2.Distance(vector, array[i].transform.position) < this.maxDistance && PlayerManager.instance.CanSeePlayer(player.transform.position, array[i]).canSee))
						{
							array[i].transform.GetComponent<HealthHandler>().TakeDamage(this.player.data.weaponHandler.gun.damage * 55f * 0.75f * Vector2.down, array[i].transform.position, this.player.data.weaponHandler.gameObject, this.player, true, true);
							this.hasTriggered = true;
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

		// Token: 0x0600020B RID: 523 RVA: 0x00010D80 File Offset: 0x0000EF80
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00010D94 File Offset: 0x0000EF94
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00010DA4 File Offset: 0x0000EFA4
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00010FBD File Offset: 0x0000F1BD
		public static GameObject pingVisual
		{
			get
			{
				GameObject result;
				if (PingMono.pingvisu != null)
				{
					result = PingMono.pingvisu;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					PingMono.pingvisu = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					PingMono.pingvisu.name = "E_Ping";
					Object.DontDestroyOnLoad(PingMono.pingvisu);
					ParticleSystem[] componentsInChildren = PingMono.pingvisu.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = Color.yellow;
					}
					PingMono.pingvisu.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(Color.yellow, 0f)
					};
					Object.Destroy(PingMono.pingvisu.transform.GetChild(2).gameObject);
					PingMono.pingvisu.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					PingMono.pingvisu.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(PingMono.pingvisu.GetComponent<FollowPlayer>());
					PingMono.pingvisu.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(PingMono.pingvisu.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(PingMono.pingvisu.GetComponent<Explosion>());
					Object.Destroy(PingMono.pingvisu.GetComponent<Explosion_Overpower>());
					Object.Destroy(PingMono.pingvisu.GetComponent<RemoveAfterSeconds>());
					PingMono.pingvisu.AddComponent<PingMono.PingSpawner>();
					result = PingMono.pingvisu;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x0400026C RID: 620
		private readonly float maxDistance = 8f;

		// Token: 0x0400026D RID: 621
		public Block block;

		// Token: 0x0400026E RID: 622
		public Player player;

		// Token: 0x0400026F RID: 623
		public CharacterData data;

		// Token: 0x04000270 RID: 624
		public Gun gun;

		// Token: 0x04000271 RID: 625
		private Action<BlockTrigger.BlockTriggerType> gravy;

		// Token: 0x04000272 RID: 626
		private Action<BlockTrigger.BlockTriggerType> basic;

		// Token: 0x04000273 RID: 627
		private static GameObject pingvisu;

		// Token: 0x04000274 RID: 628
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000275 RID: 629
		private readonly float effectCooldown;

		// Token: 0x04000276 RID: 630
		private float startTime;

		// Token: 0x04000277 RID: 631
		private float timeOfLastEffect;

		// Token: 0x04000278 RID: 632
		private bool canTrigger;

		// Token: 0x04000279 RID: 633
		private bool hasTriggered;

		// Token: 0x0400027A RID: 634
		public int numcheck;

		// Token: 0x0400027B RID: 635
		public static SoundEvent fieldsound;

		// Token: 0x0400027C RID: 636
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.2f, 0);

		// Token: 0x02000130 RID: 304
		private class PingSpawner : MonoBehaviour
		{
			// Token: 0x0600093C RID: 2364 RVA: 0x00028AB8 File Offset: 0x00026CB8
			private void Start()
			{
				base.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				base.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 2f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "inited", false);
				typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 4.3f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "startWidth", 1f);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
			}
		}
	}
}
