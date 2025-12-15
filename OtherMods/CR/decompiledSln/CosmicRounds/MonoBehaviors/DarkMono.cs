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
	// Token: 0x0200004F RID: 79
	public class DarkMono : MonoBehaviour
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x0000FD30 File Offset: 0x0000DF30
		private void Start()
		{
			Player ownPlayer = base.GetComponentInParent<ProjectileHit>().ownPlayer;
			this.move = base.GetComponentInParent<MoveTransform>();
			this.ResetTimer();
			GameObject gameObject = new GameObject();
			gameObject.transform.position = base.GetComponentInParent<ProjectileHit>().transform.position;
			gameObject.transform.SetParent(base.GetComponentInParent<ProjectileHit>().transform);
			RingObject ringObject = gameObject.AddComponent<RingObject>();
			if (DarkMono.lineEffect == null)
			{
				DarkMono.FindLineEffect();
			}
			if (CR.crSpecialVFX.Value)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(DarkMono.lineEffect, gameObject.transform);
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
							new GradientColorKey(new Color(1f, 0.4f, 0.4f, 1f), 0f)
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
					shape.radius = 0.5f;
					shape.radiusThickness = 1f;
					gameObject2.GetComponentInChildren<ParticleSystemRenderer>().material.color = new Color(1f, 0.4f, 0.4f, 1f);
					foreach (ParticleSystem particleSystem2 in gameObject2.GetComponentsInChildren<ParticleSystem>())
					{
						particleSystem.startColor = new Color(1f, 0.4f, 0.4f, 1f);
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
						new GradientColorKey(new Color(1f, 0.4f, 0f, 1f), 0f)
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
				shape2.radius = 0.5f;
				shape2.radiusThickness = 1f;
				gameObject2.GetComponentInChildren<ParticleSystemRenderer>().material.color = new Color(1f, 0.4f, 0f, 1f);
				foreach (ParticleSystem particleSystem4 in gameObject2.GetComponentsInChildren<ParticleSystem>())
				{
					particleSystem3.startColor = new Color(1f, 0.4f, 0f, 1f);
				}
			}
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00010131 File Offset: 0x0000E331
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00010139 File Offset: 0x0000E339
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00010148 File Offset: 0x0000E348
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				Object @object;
				if (this == null)
				{
					@object = null;
				}
				else
				{
					GameObject gameObject = base.gameObject;
					if (gameObject == null)
					{
						@object = null;
					}
					else
					{
						Transform transform = gameObject.transform;
						@object = ((transform != null) ? transform.parent : null);
					}
				}
				if (@object != null)
				{
					this.ResetTimer();
					ProjectileHit componentInParent = base.GetComponentInParent<ProjectileHit>();
					this.move.velocity.z = 0f;
					Vector2 vector = componentInParent.gameObject.transform.position;
					Player[] array = PlayerManager.instance.players.ToArray();
					for (int i = 0; i < array.Length; i++)
					{
						if (Vector2.Distance(vector, array[i].transform.position) < 2.8f && PlayerManager.instance.CanSeePlayer(vector, array[i]).canSee && array[i] != componentInParent.ownPlayer)
						{
							CharacterData component = array[i].gameObject.GetComponent<CharacterData>();
							Player component2 = array[i].gameObject.GetComponent<Player>();
							array[i].gameObject.GetComponent<HealthHandler>();
							component2.data.healthHandler.DoDamage((componentInParent.damage * 0.06f + component2.data.maxHealth / 20f) * Vector2.down, component2.transform.position, new Color(1f, 1f, 1f, 1f), componentInParent.ownPlayer.data.weaponHandler.gameObject, componentInParent.ownPlayer, false, true, false);
							component.stats.RPCA_AddSlow(0.5f, true);
						}
					}
				}
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00010304 File Offset: 0x0000E504
		public static void FindLineEffect()
		{
			Debug.Log(string.Format("{0}", CardChoice.instance.cards.Length));
			DarkMono.lineEffect = CardChoice.instance.cards.First((CardInfo c) => c.name.Equals("ChillingPresence")).gameObject.GetComponentInChildren<CharacterStatModifiers>().AddObjectToPlayer.GetComponentInChildren<LineEffect>().gameObject;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00010380 File Offset: 0x0000E580
		// (set) Token: 0x060001FC RID: 508 RVA: 0x000105C1 File Offset: 0x0000E7C1
		public static GameObject ringVisual
		{
			get
			{
				GameObject result;
				if (DarkMono.ringvisu != null)
				{
					result = DarkMono.ringvisu;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					DarkMono.ringvisu = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					DarkMono.ringvisu.name = "E_Ring";
					Object.DontDestroyOnLoad(DarkMono.ringvisu);
					ParticleSystem[] componentsInChildren = DarkMono.ringvisu.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(0.3f, 1f, 0.7f, 1f);
					}
					DarkMono.ringvisu.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(0.3f, 1f, 0.7f, 1f), 0f)
					};
					Object.Destroy(DarkMono.ringvisu.transform.GetChild(2).gameObject);
					DarkMono.ringvisu.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					DarkMono.ringvisu.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(DarkMono.ringvisu.GetComponent<FollowPlayer>());
					DarkMono.ringvisu.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(DarkMono.ringvisu.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(DarkMono.ringvisu.GetComponent<Explosion>());
					Object.Destroy(DarkMono.ringvisu.GetComponent<Explosion_Overpower>());
					Object.Destroy(DarkMono.ringvisu.GetComponent<RemoveAfterSeconds>());
					DarkMono.ringvisu.AddComponent<DarkMono.DarkSpawner>();
					result = DarkMono.ringvisu;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x0400025D RID: 605
		public static GameObject lineEffect;

		// Token: 0x0400025E RID: 606
		private MoveTransform move;

		// Token: 0x0400025F RID: 607
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000260 RID: 608
		private float startTime;

		// Token: 0x04000261 RID: 609
		public static GameObject ringvisu;

		// Token: 0x0200012E RID: 302
		private class DarkSpawner : MonoBehaviour
		{
			// Token: 0x06000936 RID: 2358 RVA: 0x00028968 File Offset: 0x00026B68
			private void Start()
			{
				base.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				base.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 1f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "inited", false);
				typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 7.5f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "startWidth", 0.5f);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
			}
		}
	}
}
