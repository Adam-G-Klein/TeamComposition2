using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Photon.Pun;
using SoundImplementation;
using UnboundLib;
using UnboundLib.Utils;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000089 RID: 137
	public class MoonMono : MonoBehaviour
	{
		// Token: 0x0600037A RID: 890 RVA: 0x0001A880 File Offset: 0x00018A80
		private void Start()
		{
			this.move = base.GetComponentInParent<MoveTransform>();
			this.sync = base.GetComponentInParent<SyncProjectile>();
			this.sync.active = true;
			this.ResetTimer();
			Player ownPlayer = base.GetComponentInParent<ProjectileHit>().ownPlayer;
			GameObject gameObject = new GameObject();
			gameObject.transform.position = base.GetComponentInParent<ProjectileHit>().transform.position;
			gameObject.transform.SetParent(base.GetComponentInParent<ProjectileHit>().transform);
			RingObject ringObject = gameObject.AddComponent<RingObject>();
			if (MoonMono.lineEffect == null)
			{
				MoonMono.FindLineEffect();
			}
			if (CR.crSpecialVFX.Value)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(MoonMono.lineEffect, gameObject.transform);
				if (ringObject.ringeffect)
				{
					LineEffect componentInChildren = gameObject2.GetComponentInChildren<LineEffect>();
					componentInChildren.colorOverTime = new Gradient
					{
						alphaKeys = new GradientAlphaKey[]
						{
							new GradientAlphaKey(1f, 0f)
						},
						colorKeys = new GradientColorKey[]
						{
							new GradientColorKey(new Color(0.2f, 0.2f, 1f, 1f), 0f)
						},
						mode = 1
					};
					componentInChildren.widthMultiplier = 6f;
					componentInChildren.radius = 1.5f;
					componentInChildren.raycastCollision = false;
					componentInChildren.useColorOverTime = true;
					ParticleSystem particleSystem = gameObject2.AddComponent<ParticleSystem>();
					ParticleSystem.MainModule main = particleSystem.main;
					main.duration = 5f;
					main.startSpeed = 10f;
					main.startLifetime = 1f;
					main.startSize = 0.1f;
					ParticleSystem.EmissionModule emission = particleSystem.emission;
					emission.enabled = true;
					emission.rateOverTime = 150f;
					ParticleSystem.ShapeModule shape = particleSystem.shape;
					shape.enabled = true;
					shape.shapeType = 10;
					shape.radius = 0.75f;
					shape.radiusThickness = 0.5f;
					gameObject2.GetComponentInChildren<ParticleSystemRenderer>().material.color = new Color(0.2f, 0.2f, 1f, 1f);
					foreach (ParticleSystem particleSystem2 in gameObject2.GetComponentsInChildren<ParticleSystem>())
					{
						particleSystem.startColor = new Color(0.2f, 0.2f, 1f, 1f);
					}
					return;
				}
				LineEffect componentInChildren2 = gameObject2.GetComponentInChildren<LineEffect>();
				componentInChildren2.colorOverTime = new Gradient
				{
					alphaKeys = new GradientAlphaKey[]
					{
						new GradientAlphaKey(1f, 0f)
					},
					colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(0.2f, 0.2f, 1f, 1f), 0f)
					},
					mode = 1
				};
				componentInChildren2.widthMultiplier = 5f;
				componentInChildren2.radius = 2f;
				componentInChildren2.raycastCollision = false;
				componentInChildren2.useColorOverTime = true;
				ParticleSystem particleSystem3 = gameObject2.AddComponent<ParticleSystem>();
				ParticleSystem.MainModule main2 = particleSystem3.main;
				main2.duration = 5f;
				main2.startSpeed = 10f;
				main2.startLifetime = 1f;
				main2.startSize = 0.1f;
				ParticleSystem.EmissionModule emission2 = particleSystem3.emission;
				emission2.enabled = true;
				emission2.rateOverTime = 150f;
				ParticleSystem.ShapeModule shape2 = particleSystem3.shape;
				shape2.enabled = true;
				shape2.shapeType = 10;
				shape2.radius = 0.75f;
				shape2.radiusThickness = 0.5f;
				gameObject2.GetComponentInChildren<ParticleSystemRenderer>().material.color = new Color(0.2f, 0.2f, 1f, 1f);
				foreach (ParticleSystem particleSystem4 in gameObject2.GetComponentsInChildren<ParticleSystem>())
				{
					particleSystem3.startColor = new Color(0.2f, 0.2f, 1f, 1f);
				}
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0001AC99 File Offset: 0x00018E99
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0001ACA1 File Offset: 0x00018EA1
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0001ACB0 File Offset: 0x00018EB0
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay && base.gameObject.transform.parent != null)
			{
				this.ResetTimer();
				ProjectileHit componentInParent = base.GetComponentInParent<ProjectileHit>();
				Player ownPlayer = componentInParent.ownPlayer;
				this.move.velocity.z = 0f;
				Vector2 vector = componentInParent.gameObject.transform.position;
				Player[] array = PlayerManager.instance.players.ToArray();
				for (int i = 0; i < array.Length; i++)
				{
					if (Vector2.Distance(vector, array[i].transform.position) < 4.2f && array[i] != ownPlayer)
					{
						CharacterData component = array[i].gameObject.GetComponent<CharacterData>();
						Player component2 = array[i].gameObject.GetComponent<Player>();
						array[i].gameObject.GetComponent<HealthHandler>();
						if (!component2.data.block.IsBlocking())
						{
							array[i].gameObject.transform.position = Vector2.MoveTowards(array[i].transform.position, vector, Vector3.Distance(base.gameObject.transform.position, ownPlayer.transform.position) * (Time.deltaTime / 2f) * 20f);
						}
						component.stats.RPCA_AddSlow(0.5f, true);
					}
				}
			}
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001AE2C File Offset: 0x0001902C
		public static void FindLineEffect()
		{
			Debug.Log(string.Format("{0}", CardChoice.instance.cards.Length));
			MoonMono.lineEffect = CardChoice.instance.cards.First((CardInfo c) => c.name.Equals("ChillingPresence")).gameObject.GetComponentInChildren<CharacterStatModifiers>().AddObjectToPlayer.GetComponentInChildren<LineEffect>().gameObject;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0001AEA8 File Offset: 0x000190A8
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0001B0E9 File Offset: 0x000192E9
		public static GameObject ringVisual
		{
			get
			{
				GameObject result;
				if (MoonMono.ringvisu != null)
				{
					result = MoonMono.ringvisu;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					MoonMono.ringvisu = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					MoonMono.ringvisu.name = "E_Moon";
					Object.DontDestroyOnLoad(MoonMono.ringvisu);
					ParticleSystem[] componentsInChildren = MoonMono.ringvisu.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(0.2f, 0.2f, 1f, 1f);
					}
					MoonMono.ringvisu.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(0.2f, 0.2f, 1f, 1f), 0f)
					};
					Object.Destroy(MoonMono.ringvisu.transform.GetChild(2).gameObject);
					MoonMono.ringvisu.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					MoonMono.ringvisu.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(MoonMono.ringvisu.GetComponent<FollowPlayer>());
					MoonMono.ringvisu.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(MoonMono.ringvisu.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(MoonMono.ringvisu.GetComponent<Explosion>());
					Object.Destroy(MoonMono.ringvisu.GetComponent<Explosion_Overpower>());
					Object.Destroy(MoonMono.ringvisu.GetComponent<RemoveAfterSeconds>());
					MoonMono.ringvisu.AddComponent<MoonMono.MoonSpawner>();
					result = MoonMono.ringvisu;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x04000477 RID: 1143
		public static GameObject lineEffect;

		// Token: 0x04000478 RID: 1144
		public static GameObject ringvisu;

		// Token: 0x04000479 RID: 1145
		private SyncProjectile sync;

		// Token: 0x0400047A RID: 1146
		private MoveTransform move;

		// Token: 0x0400047B RID: 1147
		private readonly float updateDelay = 0.1f;

		// Token: 0x0400047C RID: 1148
		private float startTime;

		// Token: 0x02000154 RID: 340
		private class MoonSpawner : MonoBehaviour
		{
			// Token: 0x06000990 RID: 2448 RVA: 0x0002AD88 File Offset: 0x00028F88
			private void Start()
			{
				base.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				base.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 1f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "inited", false);
				typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 7.5f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "startWidth", 0.25f);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
			}
		}
	}
}
