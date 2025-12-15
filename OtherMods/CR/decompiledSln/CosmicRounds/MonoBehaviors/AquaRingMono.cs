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
	// Token: 0x0200003B RID: 59
	public class AquaRingMono : MonoBehaviour
	{
		// Token: 0x0600015C RID: 348 RVA: 0x0000AFEC File Offset: 0x000091EC
		private void Start()
		{
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.data = base.GetComponent<CharacterData>();
			this.gun = base.GetComponent<Gun>();
			this.basic = this.block.BlockAction;
			if (this.block)
			{
				this.aqua = new Action<BlockTrigger.BlockTriggerType>(this.GetDoBlockAction(this.player, this.block).Invoke);
				this.block.BlockAction = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(this.block.BlockAction, this.aqua);
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000B090 File Offset: 0x00009290
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000B098 File Offset: 0x00009298
		public void OnDestroy()
		{
			this.block.BlockAction = this.basic;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000B0AB File Offset: 0x000092AB
		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block)
		{
			return delegate(BlockTrigger.BlockTriggerType trigger)
			{
				if (trigger != 1 && this.canTrigger)
				{
					Object.Instantiate<GameObject>(AquaRingMono.aquaVisual, this.player.transform.position, Quaternion.identity);
					this.canTrigger = false;
					if (!AquaRingMono.fieldsound)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("aqua");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						AquaRingMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						AquaRingMono.fieldsound.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity.intensity = this.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
					SoundManager.Instance.Play(AquaRingMono.fieldsound, this.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
					this.ResetEffectTimer();
					GameObject gameObject = new GameObject();
					gameObject.transform.SetParent(this.player.transform);
					gameObject.transform.position = player.gameObject.transform.position;
					gameObject.AddComponent<RemoveAfterSeconds>().seconds = 2f;
					gameObject.AddComponent<AquaRing>();
					if (AquaRingMono.lineEffect == null)
					{
						this.FindLineEffect();
					}
					GameObject gameObject2 = Object.Instantiate<GameObject>(AquaRingMono.lineEffect, gameObject.transform);
					LineEffect componentInChildren = gameObject2.GetComponentInChildren<LineEffect>();
					componentInChildren.colorOverTime = new Gradient
					{
						alphaKeys = new GradientAlphaKey[]
						{
							new GradientAlphaKey(1f, 0f)
						},
						colorKeys = new GradientColorKey[]
						{
							new GradientColorKey(new Color(0.1f, 0.9f, 1f, 1f), 0f)
						},
						mode = 1
					};
					componentInChildren.widthMultiplier = 3f;
					componentInChildren.radius = 4.1f;
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
					shape.radius = 0.8f;
					shape.radiusThickness = 1f;
					gameObject2.GetComponentInChildren<ParticleSystemRenderer>().material.color = new Color(0.1f, 0.9f, 1f, 1f);
					foreach (ParticleSystem particleSystem2 in gameObject2.GetComponentsInChildren<ParticleSystem>())
					{
						particleSystem.startColor = new Color(0.1f, 0.9f, 1f, 1f);
					}
				}
			};
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000B0CC File Offset: 0x000092CC
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				int i = 0;
				this.ResetTimer();
				while (i <= this.player.data.currentCards.Count - 1)
				{
					if (this.player.data.currentCards[i].cardName == "Aqua Ring")
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

		// Token: 0x06000161 RID: 353 RVA: 0x0000B180 File Offset: 0x00009380
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000B194 File Offset: 0x00009394
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000B1A4 File Offset: 0x000093A4
		private void FindLineEffect()
		{
			Debug.Log(string.Format("{0}", CardChoice.instance.cards.Length));
			AquaRingMono.lineEffect = CardChoice.instance.cards.First((CardInfo c) => c.name.Equals("ChillingPresence")).gameObject.GetComponentInChildren<CharacterStatModifiers>().AddObjectToPlayer.GetComponentInChildren<LineEffect>().gameObject;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000164 RID: 356 RVA: 0x0000B220 File Offset: 0x00009420
		// (set) Token: 0x06000165 RID: 357 RVA: 0x0000B461 File Offset: 0x00009661
		public static GameObject aquaVisual
		{
			get
			{
				GameObject result;
				if (AquaRingMono.aquavisual != null)
				{
					result = AquaRingMono.aquavisual;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					AquaRingMono.aquavisual = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					AquaRingMono.aquavisual.name = "E_Aqua";
					Object.DontDestroyOnLoad(AquaRingMono.aquavisual);
					ParticleSystem[] componentsInChildren = AquaRingMono.aquavisual.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(0.1f, 0.9f, 1f, 1f);
					}
					AquaRingMono.aquavisual.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(0.1f, 0.9f, 1f, 1f), 0f)
					};
					Object.Destroy(AquaRingMono.aquavisual.transform.GetChild(2).gameObject);
					AquaRingMono.aquavisual.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					AquaRingMono.aquavisual.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(AquaRingMono.aquavisual.GetComponent<FollowPlayer>());
					AquaRingMono.aquavisual.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(AquaRingMono.aquavisual.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(AquaRingMono.aquavisual.GetComponent<Explosion>());
					Object.Destroy(AquaRingMono.aquavisual.GetComponent<Explosion_Overpower>());
					Object.Destroy(AquaRingMono.aquavisual.GetComponent<RemoveAfterSeconds>());
					AquaRingMono.aquavisual.AddComponent<AquaRingMono.AquaSpawner>();
					result = AquaRingMono.aquavisual;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x04000190 RID: 400
		public Block block;

		// Token: 0x04000191 RID: 401
		public Player player;

		// Token: 0x04000192 RID: 402
		public CharacterData data;

		// Token: 0x04000193 RID: 403
		public Gun gun;

		// Token: 0x04000194 RID: 404
		private Action<BlockTrigger.BlockTriggerType> aqua;

		// Token: 0x04000195 RID: 405
		private Action<BlockTrigger.BlockTriggerType> basic;

		// Token: 0x04000196 RID: 406
		private static GameObject aquavisual;

		// Token: 0x04000197 RID: 407
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000198 RID: 408
		private float startTime;

		// Token: 0x04000199 RID: 409
		public int numcheck;

		// Token: 0x0400019A RID: 410
		private readonly float effectCooldown = 4f;

		// Token: 0x0400019B RID: 411
		private float timeOfLastEffect;

		// Token: 0x0400019C RID: 412
		private bool canTrigger;

		// Token: 0x0400019D RID: 413
		public static SoundEvent fieldsound;

		// Token: 0x0400019E RID: 414
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.7f, 0);

		// Token: 0x0400019F RID: 415
		private static GameObject lineEffect;

		// Token: 0x0200011B RID: 283
		private class AquaSpawner : MonoBehaviour
		{
			// Token: 0x06000901 RID: 2305 RVA: 0x000273B4 File Offset: 0x000255B4
			private void Start()
			{
				base.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				base.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 1f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "inited", false);
				typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 4f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "startWidth", 0.5f);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
			}
		}
	}
}
