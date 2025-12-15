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
	// Token: 0x02000032 RID: 50
	public class IgniteMono : MonoBehaviour
	{
		// Token: 0x0600011F RID: 287 RVA: 0x000094FC File Offset: 0x000076FC
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
				this.igi = new Action<BlockTrigger.BlockTriggerType>(this.GetDoBlockAction(this.player, this.block).Invoke);
				this.block.BlockAction = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(this.block.BlockAction, this.igi);
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000095BA File Offset: 0x000077BA
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000095C2 File Offset: 0x000077C2
		public void OnDestroy()
		{
			this.block.BlockAction = this.basic;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000095D8 File Offset: 0x000077D8
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Ignite")
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

		// Token: 0x06000123 RID: 291 RVA: 0x0000968C File Offset: 0x0000788C
		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block)
		{
			return delegate(BlockTrigger.BlockTriggerType trigger)
			{
				if (trigger != 1)
				{
					if (!IgniteMono.fieldsound)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("fire");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						IgniteMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						IgniteMono.fieldsound.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity.intensity = this.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
					SoundManager.Instance.Play(IgniteMono.fieldsound, this.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
					Object.Instantiate<GameObject>(IgniteMono.igniteVisual, this.player.transform.position, Quaternion.identity);
					Vector2 vector = block.transform.position;
					Player[] array = PlayerManager.instance.players.ToArray();
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].teamID != player.teamID && (Vector2.Distance(vector, array[i].transform.position) < this.maxDistance && PlayerManager.instance.CanSeePlayer(player.transform.position, array[i]).canSee))
						{
							array[i].gameObject.GetComponent<CharacterData>();
							Player component = array[i].gameObject.GetComponent<Player>();
							array[i].gameObject.GetComponent<HealthHandler>();
							if (this.canTrigger)
							{
								component.gameObject.AddComponent<IgniteEffect>();
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

		// Token: 0x06000124 RID: 292 RVA: 0x000096B3 File Offset: 0x000078B3
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000096C7 File Offset: 0x000078C7
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000096D4 File Offset: 0x000078D4
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00009915 File Offset: 0x00007B15
		public static GameObject igniteVisual
		{
			get
			{
				GameObject result;
				if (IgniteMono.ignitevisual != null)
				{
					result = IgniteMono.ignitevisual;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					IgniteMono.ignitevisual = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					IgniteMono.ignitevisual.name = "E_Ignite";
					Object.DontDestroyOnLoad(IgniteMono.ignitevisual);
					ParticleSystem[] componentsInChildren = IgniteMono.ignitevisual.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(1f, 0.4f, 0f, 1f);
					}
					IgniteMono.ignitevisual.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(1f, 0.4f, 0f, 1f), 0f)
					};
					Object.Destroy(IgniteMono.ignitevisual.transform.GetChild(2).gameObject);
					IgniteMono.ignitevisual.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					IgniteMono.ignitevisual.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(IgniteMono.ignitevisual.GetComponent<FollowPlayer>());
					IgniteMono.ignitevisual.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(IgniteMono.ignitevisual.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(IgniteMono.ignitevisual.GetComponent<Explosion>());
					Object.Destroy(IgniteMono.ignitevisual.GetComponent<Explosion_Overpower>());
					Object.Destroy(IgniteMono.ignitevisual.GetComponent<RemoveAfterSeconds>());
					IgniteMono.ignitevisual.AddComponent<IgniteMono.IgniteSpawner>();
					result = IgniteMono.ignitevisual;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x04000146 RID: 326
		private readonly float maxDistance = 8f;

		// Token: 0x04000147 RID: 327
		public Block block;

		// Token: 0x04000148 RID: 328
		public Player player;

		// Token: 0x04000149 RID: 329
		public CharacterData data;

		// Token: 0x0400014A RID: 330
		public Gun gun;

		// Token: 0x0400014B RID: 331
		private Action<BlockTrigger.BlockTriggerType> igi;

		// Token: 0x0400014C RID: 332
		private Action<BlockTrigger.BlockTriggerType> basic;

		// Token: 0x0400014D RID: 333
		private static GameObject ignitevisual;

		// Token: 0x0400014E RID: 334
		private readonly float updateDelay = 0.1f;

		// Token: 0x0400014F RID: 335
		private readonly float effectCooldown;

		// Token: 0x04000150 RID: 336
		private float startTime;

		// Token: 0x04000151 RID: 337
		private float timeOfLastEffect;

		// Token: 0x04000152 RID: 338
		private bool canTrigger;

		// Token: 0x04000153 RID: 339
		private bool hasTriggered;

		// Token: 0x04000154 RID: 340
		[Header("Sounds")]
		public SoundEvent soundEventDamageOverTime;

		// Token: 0x04000155 RID: 341
		public int numcheck;

		// Token: 0x04000156 RID: 342
		public static SoundEvent fieldsound;

		// Token: 0x04000157 RID: 343
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.5f, 0);

		// Token: 0x02000112 RID: 274
		private class IgniteSpawner : MonoBehaviour
		{
			// Token: 0x060008EB RID: 2283 RVA: 0x00026C90 File Offset: 0x00024E90
			private void Start()
			{
				base.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				base.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 1.5f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "inited", false);
				typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 4.2f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "startWidth", 1f);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
			}
		}
	}
}
