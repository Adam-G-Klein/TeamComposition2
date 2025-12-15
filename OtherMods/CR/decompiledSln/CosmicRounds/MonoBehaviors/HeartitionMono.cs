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
	// Token: 0x02000047 RID: 71
	public class HeartitionMono : MonoBehaviour
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x0000DC24 File Offset: 0x0000BE24
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

		// Token: 0x060001B8 RID: 440 RVA: 0x0000DCC8 File Offset: 0x0000BEC8
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000DCD0 File Offset: 0x0000BED0
		public void OnDestroy()
		{
			this.block.BlockActionEarly = this.basic;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000DCE3 File Offset: 0x0000BEE3
		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block)
		{
			return delegate(BlockTrigger.BlockTriggerType trigger)
			{
				if (trigger != 1 && this.canact)
				{
					Object.Instantiate<GameObject>(HeartitionMono.heartitionBlock, this.player.transform.position, Quaternion.identity);
					this.ResetEffectTimer();
					GameObject gameObject = new GameObject();
					gameObject.transform.SetParent(this.player.transform);
					gameObject.transform.position = player.gameObject.transform.position;
					gameObject.AddComponent<RemoveAfterSeconds>().seconds = 2f;
					gameObject.AddComponent<HeartitionObject>().player = this.player;
					if (HeartitionMono.lineEffect == null)
					{
						this.FindLineEffect();
					}
					GameObject gameObject2 = Object.Instantiate<GameObject>(HeartitionMono.lineEffect, gameObject.transform);
					LineEffect componentInChildren = gameObject2.GetComponentInChildren<LineEffect>();
					componentInChildren.colorOverTime = new Gradient
					{
						alphaKeys = new GradientAlphaKey[]
						{
							new GradientAlphaKey(1f, 0f)
						},
						colorKeys = new GradientColorKey[]
						{
							new GradientColorKey(new Color(1f, 0.2f, 0.5f, 1f), 0f)
						},
						mode = 1
					};
					componentInChildren.widthMultiplier = 6f;
					componentInChildren.radius = 4f;
					componentInChildren.raycastCollision = false;
					componentInChildren.useColorOverTime = true;
					ParticleSystem particleSystem = gameObject2.AddComponent<ParticleSystem>();
					ParticleSystem.MainModule main = particleSystem.main;
					main.duration = 2f;
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
					gameObject2.GetComponentInChildren<ParticleSystemRenderer>().material.color = new Color(1f, 0.2f, 0.5f, 1f);
					foreach (ParticleSystem particleSystem2 in gameObject2.GetComponentsInChildren<ParticleSystem>())
					{
						particleSystem.startColor = new Color(1f, 0.2f, 0.5f, 1f);
					}
				}
			};
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000DD04 File Offset: 0x0000BF04
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				int i = 0;
				this.ResetTimer();
				while (i <= this.player.data.currentCards.Count - 1)
				{
					if (this.player.data.currentCards[i].cardName == "Heartition")
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

		// Token: 0x060001BC RID: 444 RVA: 0x0000DDB8 File Offset: 0x0000BFB8
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000DDCC File Offset: 0x0000BFCC
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
			this.canact = false;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000DDE0 File Offset: 0x0000BFE0
		private void FindLineEffect()
		{
			Debug.Log(string.Format("{0}", CardChoice.instance.cards.Length));
			HeartitionMono.lineEffect = CardChoice.instance.cards.First((CardInfo c) => c.name.Equals("ChillingPresence")).gameObject.GetComponentInChildren<CharacterStatModifiers>().AddObjectToPlayer.GetComponentInChildren<LineEffect>().gameObject;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060001BF RID: 447 RVA: 0x0000DE5C File Offset: 0x0000C05C
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x0000E09D File Offset: 0x0000C29D
		public static GameObject heartitionVisu
		{
			get
			{
				GameObject result;
				if (HeartitionMono.heartitionvisual != null)
				{
					result = HeartitionMono.heartitionvisual;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					HeartitionMono.heartitionvisual = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					HeartitionMono.heartitionvisual.name = "E_Heartition";
					Object.DontDestroyOnLoad(HeartitionMono.heartitionvisual);
					ParticleSystem[] componentsInChildren = HeartitionMono.heartitionvisual.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(1f, 0.2f, 0.5f, 1f);
					}
					HeartitionMono.heartitionvisual.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(1f, 0.2f, 0.5f, 1f), 0f)
					};
					Object.Destroy(HeartitionMono.heartitionvisual.transform.GetChild(2).gameObject);
					HeartitionMono.heartitionvisual.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					HeartitionMono.heartitionvisual.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(HeartitionMono.heartitionvisual.GetComponent<FollowPlayer>());
					HeartitionMono.heartitionvisual.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(HeartitionMono.heartitionvisual.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(HeartitionMono.heartitionvisual.GetComponent<Explosion>());
					Object.Destroy(HeartitionMono.heartitionvisual.GetComponent<Explosion_Overpower>());
					Object.Destroy(HeartitionMono.heartitionvisual.GetComponent<RemoveAfterSeconds>());
					HeartitionMono.heartitionvisual.AddComponent<HeartitionMono.HeartitionSpawner>();
					result = HeartitionMono.heartitionvisual;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000E0A0 File Offset: 0x0000C2A0
		// (set) Token: 0x060001C2 RID: 450 RVA: 0x0000E2E1 File Offset: 0x0000C4E1
		public static GameObject heartitionBlock
		{
			get
			{
				GameObject result;
				if (HeartitionMono.heartitionReflect != null)
				{
					result = HeartitionMono.heartitionReflect;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					HeartitionMono.heartitionReflect = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					HeartitionMono.heartitionReflect.name = "E_Heart";
					Object.DontDestroyOnLoad(HeartitionMono.heartitionReflect);
					ParticleSystem[] componentsInChildren = HeartitionMono.heartitionReflect.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(1f, 0.2f, 0.5f, 1f);
					}
					HeartitionMono.heartitionReflect.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(1f, 0.2f, 0.5f, 1f), 0f)
					};
					Object.Destroy(HeartitionMono.heartitionReflect.transform.GetChild(2).gameObject);
					HeartitionMono.heartitionReflect.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					HeartitionMono.heartitionReflect.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(HeartitionMono.heartitionReflect.GetComponent<FollowPlayer>());
					HeartitionMono.heartitionReflect.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(HeartitionMono.heartitionReflect.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(HeartitionMono.heartitionReflect.GetComponent<Explosion>());
					Object.Destroy(HeartitionMono.heartitionReflect.GetComponent<Explosion_Overpower>());
					Object.Destroy(HeartitionMono.heartitionReflect.GetComponent<RemoveAfterSeconds>());
					HeartitionMono.heartitionReflect.AddComponent<HeartitionMono.HeartitionSpawner>();
					result = HeartitionMono.heartitionReflect;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x04000211 RID: 529
		public Block block;

		// Token: 0x04000212 RID: 530
		public Player player;

		// Token: 0x04000213 RID: 531
		public CharacterData data;

		// Token: 0x04000214 RID: 532
		public Gun gun;

		// Token: 0x04000215 RID: 533
		private Action<BlockTrigger.BlockTriggerType> berry;

		// Token: 0x04000216 RID: 534
		private Action<BlockTrigger.BlockTriggerType> basic;

		// Token: 0x04000217 RID: 535
		private static GameObject heartitionvisual;

		// Token: 0x04000218 RID: 536
		private static GameObject heartitionReflect;

		// Token: 0x04000219 RID: 537
		private readonly float updateDelay;

		// Token: 0x0400021A RID: 538
		private readonly float effectCooldown = 2.5f;

		// Token: 0x0400021B RID: 539
		private float startTime;

		// Token: 0x0400021C RID: 540
		private float timeOfLastEffect;

		// Token: 0x0400021D RID: 541
		public bool canact;

		// Token: 0x0400021E RID: 542
		public int numcheck;

		// Token: 0x0400021F RID: 543
		private static GameObject lineEffect;

		// Token: 0x02000125 RID: 293
		private class HeartitionSpawner : MonoBehaviour
		{
			// Token: 0x0600091E RID: 2334 RVA: 0x0002829C File Offset: 0x0002649C
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
