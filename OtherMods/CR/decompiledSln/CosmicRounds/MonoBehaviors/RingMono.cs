using System;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000051 RID: 81
	public class RingMono : MonoBehaviour
	{
		// Token: 0x06000201 RID: 513 RVA: 0x00010688 File Offset: 0x0000E888
		private void Start()
		{
			Player ownPlayer = base.GetComponentInParent<ProjectileHit>().ownPlayer;
			this.move = base.GetComponentInParent<MoveTransform>();
			this.ResetTimer();
			GameObject gameObject = new GameObject();
			gameObject.transform.position = base.GetComponentInParent<ProjectileHit>().transform.position;
			gameObject.transform.SetParent(base.GetComponentInParent<ProjectileHit>().transform);
			gameObject.AddComponent<RingObject>();
			if (DarkMono.lineEffect == null)
			{
				DarkMono.FindLineEffect();
			}
			if (CR.crSpecialVFX.Value)
			{
				GameObject gameObject2 = Object.Instantiate<GameObject>(DarkMono.lineEffect, gameObject.transform);
				LineEffect componentInChildren = gameObject2.GetComponentInChildren<LineEffect>();
				componentInChildren.colorOverTime = new Gradient
				{
					alphaKeys = new GradientAlphaKey[]
					{
						new GradientAlphaKey(1f, 0f)
					},
					colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(1f, 1f, 0f, 1f), 0f)
					},
					mode = 1
				};
				componentInChildren.widthMultiplier = 4f;
				componentInChildren.radius = 1f;
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
				gameObject2.GetComponentInChildren<ParticleSystemRenderer>().material.color = new Color(1f, 1f, 0f, 1f);
				foreach (ParticleSystem particleSystem2 in gameObject2.GetComponentsInChildren<ParticleSystem>())
				{
					particleSystem.startColor = new Color(1f, 1f, 0f, 1f);
				}
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000108C8 File Offset: 0x0000EAC8
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000108D0 File Offset: 0x0000EAD0
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000108E0 File Offset: 0x0000EAE0
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
						if (Vector2.Distance(vector, array[i].transform.position) < 2.3f && PlayerManager.instance.CanSeePlayer(vector, array[i]).canSee && array[i].teamID != componentInParent.ownPlayer.teamID && array[i].data.isActiveAndEnabled && !array[i].data.dead)
						{
							array[i].gameObject.GetComponent<CharacterData>();
							Player component = array[i].gameObject.GetComponent<Player>();
							array[i].gameObject.GetComponent<HealthHandler>();
							base.gameObject.AddComponent<ProjectileCollision>();
							MoveTransform componentInParent2 = base.GetComponentInParent<MoveTransform>();
							component.data.healthHandler.DoDamage(componentInParent.damage * Vector2.down * 0.75f, component.transform.position, new Color(1f, 1f, 0f, 1f), componentInParent.ownPlayer.data.weaponHandler.gameObject, componentInParent.ownPlayer, false, true, false);
							if (!RingMono.fieldsound)
							{
								AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("critical");
								SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
								soundContainer.audioClip[0] = audioClip;
								soundContainer.setting.volumeIntensityEnable = true;
								RingMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
								RingMono.fieldsound.soundContainerArray[0] = soundContainer;
							}
							this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.35f * CR.globalVolMute.Value;
							SoundManager.Instance.Play(RingMono.fieldsound, base.transform, new SoundParameterBase[]
							{
								this.soundParameterIntensity
							});
							componentInParent2.velocity = Vector2.Reflect(this.move.velocity, component.gameObject.transform.localPosition.normalized);
							this.updateDelay = 0.2f;
						}
					}
				}
			}
		}

		// Token: 0x04000267 RID: 615
		private MoveTransform move;

		// Token: 0x04000268 RID: 616
		private float updateDelay;

		// Token: 0x04000269 RID: 617
		private float startTime;

		// Token: 0x0400026A RID: 618
		public static SoundEvent fieldsound;

		// Token: 0x0400026B RID: 619
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.5f, 0);
	}
}
