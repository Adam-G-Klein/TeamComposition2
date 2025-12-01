using System;
using System.Linq;
using ModdingUtils.RoundsEffects;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;
using WWMO.MonoBehaviours;

namespace CR.MonoBehaviors
{
	// Token: 0x02000062 RID: 98
	public class HolyMono : HitSurfaceEffect
	{
		// Token: 0x06000268 RID: 616 RVA: 0x00013070 File Offset: 0x00011270
		private void Awake()
		{
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.data = base.GetComponent<CharacterData>();
			this.gun = base.GetComponent<Gun>();
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000130A4 File Offset: 0x000112A4
		public override void Hit(Vector2 position, Vector2 normal, Vector2 velocity)
		{
			if (!HolyMono.fieldsound)
			{
				AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("holy");
				SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
				soundContainer.audioClip[0] = audioClip;
				soundContainer.setting.volumeIntensityEnable = true;
				HolyMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
				HolyMono.fieldsound.soundContainerArray[0] = soundContainer;
			}
			this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
			SoundManager.Instance.Play(HolyMono.fieldsound, base.transform, new SoundParameterBase[]
			{
				this.soundParameterIntensity
			});
			if (CR.crSpecialVFX.Value)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(((GameObject)Resources.Load("0 cards/Demonic pact")).GetComponent<Gun>().objectsToSpawn[0].effect);
				gameObject.transform.position = position;
				gameObject.hideFlags = 61;
				gameObject.name = "Explosion";
				gameObject.AddComponent<RemoveAfterSeconds>().seconds = 2f;
				ParticleSystem[] componentsInChildren = gameObject.GetComponentsInChildren<ParticleSystem>();
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].startColor = new Color(1f, 1f, 1f, 1f);
				}
				foreach (ParticleSystemRenderer particleSystemRenderer in gameObject.GetComponentsInChildren<ParticleSystemRenderer>())
				{
					particleSystemRenderer.material.color = new Color(1f, 1f, 1f, 1f);
					particleSystemRenderer.sharedMaterial.color = new Color(1f, 1f, 1f, 1f);
				}
				Material[] componentsInChildren3 = gameObject.GetComponentsInChildren<Material>();
				for (int i = 0; i < componentsInChildren3.Length; i++)
				{
					componentsInChildren3[i].color = new Color(1f, 1f, 1f, 1f);
				}
			}
			foreach (Collider2D collider2D in from uwu in Physics2D.OverlapCircleAll(position, 3f)
			where !uwu.gameObject.GetComponentInParent<ProjectileHit>() && !uwu.gameObject.GetComponent<Player>() && uwu.GetComponent<Rigidbody2D>()
			select uwu)
			{
				if (!collider2D.attachedRigidbody.isKinematic && collider2D.attachedRigidbody.gameObject.layer != LayerMask.NameToLayer("BackgroundObject"))
				{
					if (collider2D.attachedRigidbody.GetComponent<DamagableEvent>())
					{
						collider2D.attachedRigidbody.GetComponent<DamagableEvent>().CallTakeDamage(Vector2.up * 2f, Vector2.zero, null, null, true);
					}
					else
					{
						if (!collider2D.attachedRigidbody.gameObject.GetComponent<BoxTouchingLava_Mono>())
						{
							this.hotbox = collider2D.attachedRigidbody.gameObject.AddComponent<BoxTouchingLava_Mono>();
						}
						else
						{
							this.hotbox = collider2D.attachedRigidbody.gameObject.GetComponent<BoxTouchingLava_Mono>();
						}
						this.hotbox.heatPercent += 0.5f;
						this.hotbox.heatPercent = Mathf.Min(this.hotbox.heatPercent, 3f);
					}
				}
			}
		}

		// Token: 0x040002E7 RID: 743
		public Block block;

		// Token: 0x040002E8 RID: 744
		public Player player;

		// Token: 0x040002E9 RID: 745
		public CharacterData data;

		// Token: 0x040002EA RID: 746
		public Gun gun;

		// Token: 0x040002EB RID: 747
		public BoxTouchingLava_Mono hotbox;

		// Token: 0x040002EC RID: 748
		public static SoundEvent fieldsound;

		// Token: 0x040002ED RID: 749
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.4f, 0);
	}
}
