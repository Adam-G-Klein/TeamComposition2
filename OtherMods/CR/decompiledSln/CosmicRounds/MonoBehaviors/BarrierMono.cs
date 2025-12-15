using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using SoundImplementation;
using UnboundLib;
using UnboundLib.Utils;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000045 RID: 69
	public class BarrierMono : MonoBehaviour
	{
		// Token: 0x060001A5 RID: 421 RVA: 0x0000D2F8 File Offset: 0x0000B4F8
		private void Start()
		{
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.data = base.GetComponent<CharacterData>();
			this.gun = base.GetComponent<Gun>();
			this.basic = this.block.BlockActionEarly;
			if (this.block)
			{
				this.berry = new Action<BlockTrigger.BlockTriggerType>(this.GetDoBlockAction(this.player, this.block).Invoke);
				this.block.BlockActionEarly = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(this.block.BlockActionEarly, this.berry);
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000D39C File Offset: 0x0000B59C
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000D3A4 File Offset: 0x0000B5A4
		public void OnDestroy()
		{
			this.block.BlockActionEarly = this.basic;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000D3B7 File Offset: 0x0000B5B7
		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block)
		{
			return delegate(BlockTrigger.BlockTriggerType trigger)
			{
				if (trigger != 1 && this.canact)
				{
					Object.Instantiate<GameObject>(BarrierMono.barrierBlock, this.player.transform.position, Quaternion.identity);
					this.ResetEffectTimer();
					GameObject gameObject = new GameObject();
					gameObject.transform.SetParent(this.player.transform);
					gameObject.transform.position = player.gameObject.transform.position;
					gameObject.AddComponent<RemoveAfterSeconds>().seconds = 1.2f;
					gameObject.AddComponent<BarrierObject>().player = this.player;
					if (BarrierMono.lineEffect == null)
					{
						this.FindLineEffect();
					}
					GameObject gameObject2 = Object.Instantiate<GameObject>(BarrierMono.lineEffect, gameObject.transform);
					LineEffect componentInChildren = gameObject2.GetComponentInChildren<LineEffect>();
					componentInChildren.colorOverTime = new Gradient
					{
						alphaKeys = new GradientAlphaKey[]
						{
							new GradientAlphaKey(1f, 0f)
						},
						colorKeys = new GradientColorKey[]
						{
							new GradientColorKey(new Color(0.3f, 1f, 0.7f, 1f), 0f)
						},
						mode = 1
					};
					componentInChildren.widthMultiplier = 4f;
					componentInChildren.radius = 4f;
					componentInChildren.raycastCollision = false;
					componentInChildren.useColorOverTime = true;
					ParticleSystem particleSystem = gameObject2.AddComponent<ParticleSystem>();
					ParticleSystem.MainModule main = particleSystem.main;
					main.duration = 1f;
					main.startSpeed = 10f;
					main.startLifetime = 0.5f;
					main.startSize = 0.1f;
					ParticleSystem.EmissionModule emission = particleSystem.emission;
					emission.enabled = true;
					emission.rateOverTime = 150f;
					ParticleSystem.ShapeModule shape = particleSystem.shape;
					shape.enabled = true;
					shape.shapeType = 10;
					shape.radius = 4f;
					shape.radiusThickness = 1f;
					gameObject2.GetComponentInChildren<ParticleSystemRenderer>().material.color = new Color(0.3f, 1f, 0.7f, 1f);
					foreach (ParticleSystem particleSystem2 in gameObject2.GetComponentsInChildren<ParticleSystem>())
					{
						particleSystem.startColor = new Color(0.3f, 1f, 0.7f, 1f);
					}
				}
			};
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000D3D8 File Offset: 0x0000B5D8
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				int i = 0;
				this.ResetTimer();
				while (i <= this.player.data.currentCards.Count - 1)
				{
					if (this.player.data.currentCards[i].cardName == "Barrier")
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
				if (Time.time >= this.timeOfLastEffect + this.effectCooldown && !this.canact)
				{
					this.canact = true;
				}
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000D48C File Offset: 0x0000B68C
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000D4A0 File Offset: 0x0000B6A0
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
			this.canact = false;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000D4B4 File Offset: 0x0000B6B4
		private void FindLineEffect()
		{
			Debug.Log(string.Format("{0}", CardChoice.instance.cards.Length));
			BarrierMono.lineEffect = CardChoice.instance.cards.First((CardInfo c) => c.name.Equals("ChillingPresence")).gameObject.GetComponentInChildren<CharacterStatModifiers>().AddObjectToPlayer.GetComponentInChildren<LineEffect>().gameObject;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0000D530 File Offset: 0x0000B730
		// (set) Token: 0x060001AE RID: 430 RVA: 0x0000D771 File Offset: 0x0000B971
		public static GameObject barrierVisual
		{
			get
			{
				GameObject result;
				if (BarrierMono.barriervisual != null)
				{
					result = BarrierMono.barriervisual;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					BarrierMono.barriervisual = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					BarrierMono.barriervisual.name = "E_Barrier";
					Object.DontDestroyOnLoad(BarrierMono.barriervisual);
					ParticleSystem[] componentsInChildren = BarrierMono.barriervisual.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(0.3f, 1f, 0.7f, 1f);
					}
					BarrierMono.barriervisual.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(0.3f, 1f, 0.7f, 1f), 0f)
					};
					Object.Destroy(BarrierMono.barriervisual.transform.GetChild(2).gameObject);
					BarrierMono.barriervisual.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					BarrierMono.barriervisual.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(BarrierMono.barriervisual.GetComponent<FollowPlayer>());
					BarrierMono.barriervisual.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(BarrierMono.barriervisual.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(BarrierMono.barriervisual.GetComponent<Explosion>());
					Object.Destroy(BarrierMono.barriervisual.GetComponent<Explosion_Overpower>());
					Object.Destroy(BarrierMono.barriervisual.GetComponent<RemoveAfterSeconds>());
					BarrierMono.barriervisual.AddComponent<BarrierMono.BarrierSpawner>();
					result = BarrierMono.barriervisual;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000D774 File Offset: 0x0000B974
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x0000D9B5 File Offset: 0x0000BBB5
		public static GameObject barrierBlock
		{
			get
			{
				GameObject result;
				if (BarrierMono.barrierReflect != null)
				{
					result = BarrierMono.barrierReflect;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					BarrierMono.barrierReflect = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					BarrierMono.barrierReflect.name = "E_Barrier";
					Object.DontDestroyOnLoad(BarrierMono.barrierReflect);
					ParticleSystem[] componentsInChildren = BarrierMono.barrierReflect.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(0f, 0.7f, 1f, 1f);
					}
					BarrierMono.barrierReflect.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(0f, 0.7f, 1f, 1f), 0f)
					};
					Object.Destroy(BarrierMono.barrierReflect.transform.GetChild(2).gameObject);
					BarrierMono.barrierReflect.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					BarrierMono.barrierReflect.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(BarrierMono.barrierReflect.GetComponent<FollowPlayer>());
					BarrierMono.barrierReflect.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(BarrierMono.barrierReflect.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(BarrierMono.barrierReflect.GetComponent<Explosion>());
					Object.Destroy(BarrierMono.barrierReflect.GetComponent<Explosion_Overpower>());
					Object.Destroy(BarrierMono.barrierReflect.GetComponent<RemoveAfterSeconds>());
					BarrierMono.barrierReflect.AddComponent<BarrierMono.BarrierSpawner>();
					result = BarrierMono.barrierReflect;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x040001FC RID: 508
		public Block block;

		// Token: 0x040001FD RID: 509
		public Player player;

		// Token: 0x040001FE RID: 510
		public CharacterData data;

		// Token: 0x040001FF RID: 511
		public Gun gun;

		// Token: 0x04000200 RID: 512
		private Action<BlockTrigger.BlockTriggerType> berry;

		// Token: 0x04000201 RID: 513
		private Action<BlockTrigger.BlockTriggerType> basic;

		// Token: 0x04000202 RID: 514
		private static GameObject barriervisual;

		// Token: 0x04000203 RID: 515
		private static GameObject barrierReflect;

		// Token: 0x04000204 RID: 516
		private readonly float updateDelay;

		// Token: 0x04000205 RID: 517
		private readonly float effectCooldown = 2f;

		// Token: 0x04000206 RID: 518
		private float startTime;

		// Token: 0x04000207 RID: 519
		private float timeOfLastEffect;

		// Token: 0x04000208 RID: 520
		public bool canact;

		// Token: 0x04000209 RID: 521
		public int numcheck;

		// Token: 0x0400020A RID: 522
		private static GameObject lineEffect;

		// Token: 0x02000122 RID: 290
		private class BarrierSpawner : MonoBehaviour
		{
			// Token: 0x06000915 RID: 2325 RVA: 0x00027EA0 File Offset: 0x000260A0
			private void Start()
			{
				base.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				base.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 1f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "inited", false);
				typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 2.5f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "startWidth", 1f);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
			}
		}
	}
}
