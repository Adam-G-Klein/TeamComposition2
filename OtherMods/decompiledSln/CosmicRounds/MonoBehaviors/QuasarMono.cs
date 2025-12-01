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
	// Token: 0x0200003D RID: 61
	public class QuasarMono : MonoBehaviour
	{
		// Token: 0x0600016A RID: 362 RVA: 0x0000B74C File Offset: 0x0000994C
		private void Start()
		{
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.data = base.GetComponent<CharacterData>();
			this.gun = base.GetComponent<Gun>();
			this.basic = this.block.BlockAction;
			if (this.block)
			{
				this.quasa = new Action<BlockTrigger.BlockTriggerType>(this.GetDoBlockAction(this.player, this.block).Invoke);
				this.block.BlockAction = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(this.block.BlockAction, this.quasa);
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000B7F0 File Offset: 0x000099F0
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000B7F8 File Offset: 0x000099F8
		public void OnDestroy()
		{
			this.block.BlockAction = this.basic;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000B80B File Offset: 0x00009A0B
		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block)
		{
			return delegate(BlockTrigger.BlockTriggerType trigger)
			{
				if (trigger != 1 && this.canTrigger)
				{
					this.canTrigger = false;
					Object.Instantiate<GameObject>(QuasarMono.quasarVisual, this.player.transform.position, Quaternion.identity);
					this.ResetEffectTimer();
					if (!QuasarMono.fieldsound)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("taser");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						QuasarMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						QuasarMono.fieldsound.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity.intensity = this.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
					SoundManager.Instance.Play(QuasarMono.fieldsound, this.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
					GameObject gameObject = new GameObject();
					gameObject.transform.position = player.gameObject.transform.position;
					gameObject.AddComponent<RemoveAfterSeconds>().seconds = 5f;
					gameObject.AddComponent<Quasar>().player = this.player;
					if (QuasarMono.lineEffect == null)
					{
						this.FindLineEffect();
					}
					GameObject gameObject2 = Object.Instantiate<GameObject>(QuasarMono.lineEffect, gameObject.transform);
					LineEffect componentInChildren = gameObject2.GetComponentInChildren<LineEffect>();
					componentInChildren.colorOverTime = new Gradient
					{
						alphaKeys = new GradientAlphaKey[]
						{
							new GradientAlphaKey(1f, 0f)
						},
						colorKeys = new GradientColorKey[]
						{
							new GradientColorKey(new Color(1f, 0.4f, 0f, 1f), 0f)
						},
						mode = 1
					};
					componentInChildren.widthMultiplier = 2f;
					componentInChildren.radius = 5.5f;
					componentInChildren.raycastCollision = false;
					componentInChildren.useColorOverTime = true;
					ParticleSystem particleSystem = gameObject2.AddComponent<ParticleSystem>();
					ParticleSystem.MainModule main = particleSystem.main;
					main.duration = 5f;
					main.startSpeed = 10f;
					main.startLifetime = 0.5f;
					main.startSize = 0.1f;
					ParticleSystem.EmissionModule emission = particleSystem.emission;
					emission.enabled = true;
					emission.rateOverTime = 150f;
					ParticleSystem.ShapeModule shape = particleSystem.shape;
					shape.enabled = true;
					shape.shapeType = 10;
					shape.radius = 0.5f;
					shape.radiusThickness = 1f;
					gameObject2.GetComponentInChildren<ParticleSystemRenderer>().material.color = new Color(1f, 0.4f, 0f, 1f);
					foreach (ParticleSystem particleSystem2 in gameObject2.GetComponentsInChildren<ParticleSystem>())
					{
						particleSystem.startColor = new Color(1f, 0.4f, 0f, 1f);
					}
					if (CR.crSpecialVFX.Value)
					{
						this.black = Object.Instantiate<GameObject>(CR.ArtAsset.LoadAsset<GameObject>("BlackholeObject"), gameObject.transform);
						this.black.transform.right = gameObject.transform.right;
						this.black.AddComponent<RemoveAfterSeconds>().seconds = 5f;
					}
				}
			};
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000B82C File Offset: 0x00009A2C
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				int i = 0;
				this.ResetTimer();
				while (i <= this.player.data.currentCards.Count - 1)
				{
					if (this.player.data.currentCards[i].cardName == "Quasar")
					{
						this.numcheck++;
					}
					i++;
				}
				if (this.numcheck < 1)
				{
					Object.Destroy(this);
					return;
				}
				if (Time.time >= this.timeOfLastEffect + this.effectCooldown)
				{
					this.canTrigger = true;
					return;
				}
				this.canTrigger = false;
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000B8E0 File Offset: 0x00009AE0
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000B8F4 File Offset: 0x00009AF4
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000B904 File Offset: 0x00009B04
		public void FindLineEffect()
		{
			Debug.Log(string.Format("{0}", CardChoice.instance.cards.Length));
			QuasarMono.lineEffect = CardChoice.instance.cards.First((CardInfo c) => c.name.Equals("ChillingPresence")).gameObject.GetComponentInChildren<CharacterStatModifiers>().AddObjectToPlayer.GetComponentInChildren<LineEffect>().gameObject;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000172 RID: 370 RVA: 0x0000B980 File Offset: 0x00009B80
		// (set) Token: 0x06000173 RID: 371 RVA: 0x0000BBC1 File Offset: 0x00009DC1
		public static GameObject quasarVisual
		{
			get
			{
				GameObject result;
				if (QuasarMono.quasarvisual != null)
				{
					result = QuasarMono.quasarvisual;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					QuasarMono.quasarvisual = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					QuasarMono.quasarvisual.name = "E_Quasar";
					Object.DontDestroyOnLoad(QuasarMono.quasarvisual);
					ParticleSystem[] componentsInChildren = QuasarMono.quasarvisual.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(1f, 0.4f, 0f, 1f);
					}
					QuasarMono.quasarvisual.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(1f, 0.4f, 0f, 1f), 0f)
					};
					Object.Destroy(QuasarMono.quasarvisual.transform.GetChild(2).gameObject);
					QuasarMono.quasarvisual.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					QuasarMono.quasarvisual.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(QuasarMono.quasarvisual.GetComponent<FollowPlayer>());
					QuasarMono.quasarvisual.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(QuasarMono.quasarvisual.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(QuasarMono.quasarvisual.GetComponent<Explosion>());
					Object.Destroy(QuasarMono.quasarvisual.GetComponent<Explosion_Overpower>());
					Object.Destroy(QuasarMono.quasarvisual.GetComponent<RemoveAfterSeconds>());
					QuasarMono.quasarvisual.AddComponent<QuasarMono.QuasarSpawner>();
					result = QuasarMono.quasarvisual;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000174 RID: 372 RVA: 0x0000BBC4 File Offset: 0x00009DC4
		// (set) Token: 0x06000175 RID: 373 RVA: 0x0000BE05 File Offset: 0x0000A005
		public static GameObject blackholeVisual
		{
			get
			{
				GameObject result;
				if (QuasarMono.blackholevisu != null)
				{
					result = QuasarMono.blackholevisu;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					QuasarMono.blackholevisu = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					QuasarMono.blackholevisu.name = "E_Blackhole";
					Object.DontDestroyOnLoad(QuasarMono.blackholevisu);
					ParticleSystem[] componentsInChildren = QuasarMono.blackholevisu.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(0f, 0f, 0f, 1f);
					}
					QuasarMono.blackholevisu.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(0f, 0f, 0f, 1f), 0f)
					};
					Object.Destroy(QuasarMono.blackholevisu.transform.GetChild(2).gameObject);
					QuasarMono.blackholevisu.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					QuasarMono.blackholevisu.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(QuasarMono.blackholevisu.GetComponent<FollowPlayer>());
					QuasarMono.blackholevisu.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(QuasarMono.blackholevisu.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(QuasarMono.blackholevisu.GetComponent<Explosion>());
					Object.Destroy(QuasarMono.blackholevisu.GetComponent<Explosion_Overpower>());
					Object.Destroy(QuasarMono.blackholevisu.GetComponent<RemoveAfterSeconds>());
					QuasarMono.blackholevisu.AddComponent<QuasarMono.BlackHoleSpawner>();
					result = QuasarMono.blackholevisu;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000176 RID: 374 RVA: 0x0000BE08 File Offset: 0x0000A008
		// (set) Token: 0x06000177 RID: 375 RVA: 0x0000C049 File Offset: 0x0000A249
		public static GameObject blackholeVisual2
		{
			get
			{
				GameObject result;
				if (QuasarMono.blackholevisu2 != null)
				{
					result = QuasarMono.blackholevisu2;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					QuasarMono.blackholevisu2 = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					QuasarMono.blackholevisu2.name = "E_Blackhole2";
					Object.DontDestroyOnLoad(QuasarMono.blackholevisu2);
					ParticleSystem[] componentsInChildren = QuasarMono.blackholevisu2.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(0f, 0f, 0f, 1f);
					}
					QuasarMono.blackholevisu2.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(0f, 0f, 0f, 1f), 0f)
					};
					Object.Destroy(QuasarMono.blackholevisu2.transform.GetChild(1).gameObject);
					QuasarMono.blackholevisu2.transform.GetChild(2).GetComponent<LineEffect>().offsetMultiplier = 0f;
					QuasarMono.blackholevisu2.transform.GetChild(2).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(QuasarMono.blackholevisu2.GetComponent<FollowPlayer>());
					QuasarMono.blackholevisu2.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(QuasarMono.blackholevisu2.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(QuasarMono.blackholevisu2.GetComponent<Explosion>());
					Object.Destroy(QuasarMono.blackholevisu2.GetComponent<Explosion_Overpower>());
					Object.Destroy(QuasarMono.blackholevisu2.GetComponent<RemoveAfterSeconds>());
					QuasarMono.blackholevisu2.AddComponent<QuasarMono.BlackHoleSpawner>();
					result = QuasarMono.blackholevisu2;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x040001A7 RID: 423
		public Block block;

		// Token: 0x040001A8 RID: 424
		public Player player;

		// Token: 0x040001A9 RID: 425
		public CharacterData data;

		// Token: 0x040001AA RID: 426
		public Gun gun;

		// Token: 0x040001AB RID: 427
		private Action<BlockTrigger.BlockTriggerType> quasa;

		// Token: 0x040001AC RID: 428
		private Action<BlockTrigger.BlockTriggerType> basic;

		// Token: 0x040001AD RID: 429
		public static GameObject quasarvisual;

		// Token: 0x040001AE RID: 430
		public static GameObject blackholevisu;

		// Token: 0x040001AF RID: 431
		public static GameObject blackholevisu2;

		// Token: 0x040001B0 RID: 432
		public GameObject black;

		// Token: 0x040001B1 RID: 433
		private readonly float updateDelay = 0.1f;

		// Token: 0x040001B2 RID: 434
		private float startTime;

		// Token: 0x040001B3 RID: 435
		public int numcheck;

		// Token: 0x040001B4 RID: 436
		private readonly float effectCooldown = 5f;

		// Token: 0x040001B5 RID: 437
		private float timeOfLastEffect;

		// Token: 0x040001B6 RID: 438
		private bool canTrigger;

		// Token: 0x040001B7 RID: 439
		public static SoundEvent fieldsound;

		// Token: 0x040001B8 RID: 440
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.5f, 0);

		// Token: 0x040001B9 RID: 441
		public static GameObject lineEffect;

		// Token: 0x0200011E RID: 286
		private class QuasarSpawner : MonoBehaviour
		{
			// Token: 0x06000909 RID: 2313 RVA: 0x0002785C File Offset: 0x00025A5C
			private void Start()
			{
				base.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				base.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 1f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "inited", false);
				typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 4.5f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "startWidth", 0.5f);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
			}
		}

		// Token: 0x0200011F RID: 287
		private class BlackHoleSpawner : MonoBehaviour
		{
			// Token: 0x0600090B RID: 2315 RVA: 0x0002796C File Offset: 0x00025B6C
			private void Start()
			{
				base.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				base.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 1f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "inited", false);
				typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 4.5f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "startWidth", 0.5f);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
			}
		}
	}
}
