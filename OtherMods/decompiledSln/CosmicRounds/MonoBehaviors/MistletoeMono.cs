using System;
using System.Linq;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200006B RID: 107
	public class MistletoeMono : RayHitEffect
	{
		// Token: 0x060002A2 RID: 674 RVA: 0x000145A5 File Offset: 0x000127A5
		private void Start()
		{
			this.proj = base.GetComponentInParent<ProjectileHit>();
			this.me = this.proj.ownPlayer;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x000145C4 File Offset: 0x000127C4
		public override HasToReturn DoHitEffect(HitInfo hit)
		{
			if (!hit.transform)
			{
				return 1;
			}
			DamageOverTime component = hit.transform.GetComponent<DamageOverTime>();
			this.comp = hit.transform.GetComponent<Player>();
			if (this.comp != this.me && component && this.comp.isActiveAndEnabled && !this.comp.data.dead && this.comp.gameObject.GetComponent<FrozenMono>() == null)
			{
				this.activated = true;
				this.ResetEffectTimer();
				FrozenMono frozenMono = this.comp.gameObject.AddComponent<FrozenMono>();
				if (!MistletoeMono.fieldsound)
				{
					AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("ice");
					SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
					soundContainer.audioClip[0] = audioClip;
					soundContainer.setting.volumeIntensityEnable = true;
					MistletoeMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
					MistletoeMono.fieldsound.soundContainerArray[0] = soundContainer;
				}
				this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1f * CR.globalVolMute.Value;
				SoundManager.Instance.Play(MistletoeMono.fieldsound, base.transform, new SoundParameterBase[]
				{
					this.soundParameterIntensity
				});
				this.ice = Object.Instantiate<GameObject>(CR.ArtAsset.LoadAsset<GameObject>("snowflake"), hit.transform);
				this.ice.transform.right = hit.transform.right;
				this.ice.transform.localScale *= 1.9f;
				this.ice.AddComponent<RemoveAfterSeconds>().seconds = 4.2f;
				this.ice.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
				this.ice.GetComponent<Animator>().recorderStartTime = 0f;
				this.ice.GetComponent<Animator>().recorderStopTime = 4.2f;
				this.snow = Object.Instantiate<GameObject>(CR.ArtAsset.LoadAsset<GameObject>("snowparticles"), hit.transform);
				this.snow.transform.right = hit.transform.right;
				this.snow.transform.localScale *= 1.5f;
				this.snow.AddComponent<RemoveAfterSeconds>().seconds = 4.2f;
				this.snow.GetComponent<ParticleSystem>().startColor = new Color(0f, 1f, 1f, 1f);
				this.gameObject = new GameObject();
				this.gameObject.transform.position = hit.transform.position;
				this.gameObject.transform.parent = hit.transform;
				this.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 4.2f;
				this.iceRing = this.gameObject.AddComponent<IceRing>();
				this.iceRing.player = this.me;
				this.iceRing.hit = this.comp;
				this.iceRing.ice = this.ice;
				if (MistletoeMono.lineEffect == null)
				{
					this.FindLineEffect();
				}
				GameObject gameObject = Object.Instantiate<GameObject>(MistletoeMono.lineEffect, this.gameObject.transform);
				LineEffect componentInChildren = gameObject.GetComponentInChildren<LineEffect>();
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
				componentInChildren.widthMultiplier = 2f;
				componentInChildren.radius = 8.25f;
				componentInChildren.raycastCollision = false;
				componentInChildren.useColorOverTime = true;
				ParticleSystem particleSystem = gameObject.AddComponent<ParticleSystem>();
				ParticleSystem.MainModule main = particleSystem.main;
				main.duration = 4.2f;
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
				gameObject.GetComponentInChildren<ParticleSystemRenderer>().material.color = new Color(0.1f, 0.9f, 1f, 1f);
				foreach (ParticleSystem particleSystem2 in gameObject.GetComponentsInChildren<ParticleSystem>())
				{
					particleSystem.startColor = new Color(0.1f, 0.9f, 1f, 1f);
				}
				this.iceRing.gameObject2 = gameObject;
				this.iceRing.gameObject = this.gameObject;
				frozenMono.gameObject2 = gameObject;
				frozenMono.gameObject = this.gameObject;
				frozenMono.ice = this.ice;
				frozenMono.snow = this.snow;
				frozenMono.iceRing = this.iceRing;
			}
			return 1;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00014B48 File Offset: 0x00012D48
		public void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				float num = Time.time;
				float num2 = this.timeOfLastEffect + this.effectCooldown;
			}
			if (this.comp && (this.comp.data.dead || this.comp.data.health <= 0f || !this.comp.gameObject.activeInHierarchy))
			{
				this.Destroy();
				Object.Destroy(this.ice);
				Object.Destroy(this.iceRing);
				Object.Destroy(this.gameObject);
				Object.Destroy(this.gameObject2);
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00014C00 File Offset: 0x00012E00
		private void FindLineEffect()
		{
			Debug.Log(string.Format("{0}", CardChoice.instance.cards.Length));
			MistletoeMono.lineEffect = CardChoice.instance.cards.First((CardInfo c) => c.name.Equals("ChillingPresence")).gameObject.GetComponentInChildren<CharacterStatModifiers>().AddObjectToPlayer.GetComponentInChildren<LineEffect>().gameObject;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00014C79 File Offset: 0x00012E79
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00014C86 File Offset: 0x00012E86
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00014C93 File Offset: 0x00012E93
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x04000333 RID: 819
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000334 RID: 820
		private readonly float effectCooldown = 5f;

		// Token: 0x04000335 RID: 821
		private float startTime;

		// Token: 0x04000336 RID: 822
		private float timeOfLastEffect;

		// Token: 0x04000337 RID: 823
		public Color color = new Color(0.2f, 1f, 1f, 1f);

		// Token: 0x04000338 RID: 824
		public static SoundEvent fieldsound;

		// Token: 0x04000339 RID: 825
		public bool activated;

		// Token: 0x0400033A RID: 826
		public float time;

		// Token: 0x0400033B RID: 827
		public GameObject ice;

		// Token: 0x0400033C RID: 828
		public GameObject snow;

		// Token: 0x0400033D RID: 829
		public IceRing iceRing;

		// Token: 0x0400033E RID: 830
		public GameObject gameObject;

		// Token: 0x0400033F RID: 831
		public GameObject gameObject2;

		// Token: 0x04000340 RID: 832
		public ProjectileHit proj;

		// Token: 0x04000341 RID: 833
		private static GameObject lineEffect;

		// Token: 0x04000342 RID: 834
		private Player comp;

		// Token: 0x04000343 RID: 835
		private Player me;

		// Token: 0x04000344 RID: 836
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.5f, 0);
	}
}
