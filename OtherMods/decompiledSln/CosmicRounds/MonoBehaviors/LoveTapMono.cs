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
	// Token: 0x0200006E RID: 110
	public class LoveTapMono : MonoBehaviour
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x0001522C File Offset: 0x0001342C
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

		// Token: 0x060002B7 RID: 695 RVA: 0x000152EA File Offset: 0x000134EA
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x000152F2 File Offset: 0x000134F2
		public void OnDestroy()
		{
			this.block.BlockAction = this.basic;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00015308 File Offset: 0x00013508
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Love Struck")
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

		// Token: 0x060002BA RID: 698 RVA: 0x000153B5 File Offset: 0x000135B5
		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block)
		{
			return delegate(BlockTrigger.BlockTriggerType trigger)
			{
				if (trigger != 1)
				{
					if (!LoveTapMono.fieldsound)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("love");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						LoveTapMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						LoveTapMono.fieldsound.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity.intensity = this.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.3f * CR.globalVolMute.Value;
					SoundManager.Instance.Play(LoveTapMono.fieldsound, this.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
					Object.Instantiate<GameObject>(LoveTapMono.lovetapBlock, this.player.transform.position, Quaternion.identity);
					Vector2 vector = block.transform.position;
					Player[] array = PlayerManager.instance.players.ToArray();
					for (int i = 0; i < array.Length; i++)
					{
						bool flag = array[i].playerID == player.playerID;
						bool flag2 = array[i].teamID == player.teamID;
						if (!flag && !flag2 && (Vector2.Distance(vector, array[i].transform.position) < this.maxDistance && PlayerManager.instance.CanSeePlayer(player.transform.position, array[i]).canSee))
						{
							array[i].transform.GetComponent<HealthHandler>().TakeDamage(this.player.data.stats.lifeSteal * 100f * Vector2.down, array[i].transform.position, this.player.data.weaponHandler.gameObject, this.player, true, true);
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

		// Token: 0x060002BB RID: 699 RVA: 0x000153DC File Offset: 0x000135DC
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x000153F0 File Offset: 0x000135F0
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00015400 File Offset: 0x00013600
		// (set) Token: 0x060002BE RID: 702 RVA: 0x00015641 File Offset: 0x00013841
		public static GameObject lovetapBlock
		{
			get
			{
				GameObject result;
				if (LoveTapMono.lovetapreflect != null)
				{
					result = LoveTapMono.lovetapreflect;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					LoveTapMono.lovetapreflect = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					LoveTapMono.lovetapreflect.name = "E_Heart";
					Object.DontDestroyOnLoad(LoveTapMono.lovetapreflect);
					ParticleSystem[] componentsInChildren = LoveTapMono.lovetapreflect.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(1f, 0.4f, 0.6f, 1f);
					}
					LoveTapMono.lovetapreflect.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(1f, 0.4f, 0.6f, 1f), 0f)
					};
					Object.Destroy(LoveTapMono.lovetapreflect.transform.GetChild(2).gameObject);
					LoveTapMono.lovetapreflect.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					LoveTapMono.lovetapreflect.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(LoveTapMono.lovetapreflect.GetComponent<FollowPlayer>());
					LoveTapMono.lovetapreflect.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(LoveTapMono.lovetapreflect.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(LoveTapMono.lovetapreflect.GetComponent<Explosion>());
					Object.Destroy(LoveTapMono.lovetapreflect.GetComponent<Explosion_Overpower>());
					Object.Destroy(LoveTapMono.lovetapreflect.GetComponent<RemoveAfterSeconds>());
					LoveTapMono.lovetapreflect.AddComponent<LoveTapMono.LoveTapSpawner>();
					result = LoveTapMono.lovetapreflect;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x0400035D RID: 861
		private readonly float maxDistance = 7f;

		// Token: 0x0400035E RID: 862
		public Block block;

		// Token: 0x0400035F RID: 863
		public Player player;

		// Token: 0x04000360 RID: 864
		public CharacterData data;

		// Token: 0x04000361 RID: 865
		public Gun gun;

		// Token: 0x04000362 RID: 866
		private Action<BlockTrigger.BlockTriggerType> gravy;

		// Token: 0x04000363 RID: 867
		private Action<BlockTrigger.BlockTriggerType> basic;

		// Token: 0x04000364 RID: 868
		private static GameObject lovetapvisual;

		// Token: 0x04000365 RID: 869
		private static GameObject lovetapreflect;

		// Token: 0x04000366 RID: 870
		private static GameObject pingvisu;

		// Token: 0x04000367 RID: 871
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000368 RID: 872
		private readonly float effectCooldown;

		// Token: 0x04000369 RID: 873
		private float startTime;

		// Token: 0x0400036A RID: 874
		private float timeOfLastEffect;

		// Token: 0x0400036B RID: 875
		private bool canTrigger;

		// Token: 0x0400036C RID: 876
		private bool hasTriggered;

		// Token: 0x0400036D RID: 877
		public int numcheck;

		// Token: 0x0400036E RID: 878
		public static SoundEvent fieldsound;

		// Token: 0x0400036F RID: 879
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.7f, 0);

		// Token: 0x0200013A RID: 314
		private class LoveTapSpawner : MonoBehaviour
		{
			// Token: 0x06000953 RID: 2387 RVA: 0x0002923C File Offset: 0x0002743C
			private void Start()
			{
				base.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				base.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 1f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "inited", false);
				typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 5f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "startWidth", 1f);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
			}
		}
	}
}
