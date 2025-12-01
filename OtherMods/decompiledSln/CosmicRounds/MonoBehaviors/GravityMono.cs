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
	// Token: 0x02000031 RID: 49
	public class GravityMono : MonoBehaviour
	{
		// Token: 0x06000115 RID: 277 RVA: 0x000090D8 File Offset: 0x000072D8
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

		// Token: 0x06000116 RID: 278 RVA: 0x00009196 File Offset: 0x00007396
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000919E File Offset: 0x0000739E
		public void OnDestroy()
		{
			this.block.BlockAction = this.basic;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000091B4 File Offset: 0x000073B4
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Gravity")
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

		// Token: 0x06000119 RID: 281 RVA: 0x00009268 File Offset: 0x00007468
		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block)
		{
			return delegate(BlockTrigger.BlockTriggerType trigger)
			{
				if (trigger != 1)
				{
					if (!GravityMono.fieldsound)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("gravity");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						GravityMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						GravityMono.fieldsound.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity.intensity = this.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
					SoundManager.Instance.Play(GravityMono.fieldsound, this.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
					Object.Instantiate<GameObject>(GravityMono.gravityVisual, this.player.transform.position, Quaternion.identity);
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
								component.TakeForce(new Vector2(60f * component.transform.position.x, 80f * component.transform.position.y) * 20f, 1, true, false, 0.8f);
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

		// Token: 0x0600011A RID: 282 RVA: 0x0000928F File Offset: 0x0000748F
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000092A3 File Offset: 0x000074A3
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000092B0 File Offset: 0x000074B0
		// (set) Token: 0x0600011D RID: 285 RVA: 0x000094C9 File Offset: 0x000076C9
		public static GameObject gravityVisual
		{
			get
			{
				GameObject result;
				if (GravityMono.gravityvisual != null)
				{
					result = GravityMono.gravityvisual;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					GravityMono.gravityvisual = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					GravityMono.gravityvisual.name = "E_Gravity";
					Object.DontDestroyOnLoad(GravityMono.gravityvisual);
					ParticleSystem[] componentsInChildren = GravityMono.gravityvisual.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = Color.magenta;
					}
					GravityMono.gravityvisual.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(Color.magenta, 0f)
					};
					Object.Destroy(GravityMono.gravityvisual.transform.GetChild(2).gameObject);
					GravityMono.gravityvisual.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					GravityMono.gravityvisual.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(GravityMono.gravityvisual.GetComponent<FollowPlayer>());
					GravityMono.gravityvisual.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(GravityMono.gravityvisual.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(GravityMono.gravityvisual.GetComponent<Explosion>());
					Object.Destroy(GravityMono.gravityvisual.GetComponent<Explosion_Overpower>());
					Object.Destroy(GravityMono.gravityvisual.GetComponent<RemoveAfterSeconds>());
					GravityMono.gravityvisual.AddComponent<GravityMono.GravitySpawner>();
					result = GravityMono.gravityvisual;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x04000135 RID: 309
		private readonly float maxDistance = 8f;

		// Token: 0x04000136 RID: 310
		public Block block;

		// Token: 0x04000137 RID: 311
		public Player player;

		// Token: 0x04000138 RID: 312
		public CharacterData data;

		// Token: 0x04000139 RID: 313
		public Gun gun;

		// Token: 0x0400013A RID: 314
		private Action<BlockTrigger.BlockTriggerType> gravy;

		// Token: 0x0400013B RID: 315
		private Action<BlockTrigger.BlockTriggerType> basic;

		// Token: 0x0400013C RID: 316
		private static GameObject gravityvisual;

		// Token: 0x0400013D RID: 317
		private readonly float updateDelay = 0.1f;

		// Token: 0x0400013E RID: 318
		private readonly float effectCooldown;

		// Token: 0x0400013F RID: 319
		private float startTime;

		// Token: 0x04000140 RID: 320
		private float timeOfLastEffect;

		// Token: 0x04000141 RID: 321
		private bool canTrigger;

		// Token: 0x04000142 RID: 322
		private bool hasTriggered;

		// Token: 0x04000143 RID: 323
		public int numcheck;

		// Token: 0x04000144 RID: 324
		public static SoundEvent fieldsound;

		// Token: 0x04000145 RID: 325
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.5f, 0);

		// Token: 0x0200010F RID: 271
		private class GravitySpawner : MonoBehaviour
		{
			// Token: 0x060008E4 RID: 2276 RVA: 0x000268A8 File Offset: 0x00024AA8
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
