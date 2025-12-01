using System;
using System.Linq;
using ModdingUtils.RoundsEffects;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;
using WWMO.MonoBehaviours;

namespace CR.MonoBehaviors
{
	// Token: 0x02000010 RID: 16
	public class BurnMono : HitSurfaceEffect
	{
		// Token: 0x06000047 RID: 71 RVA: 0x00003A7F File Offset: 0x00001C7F
		private void Awake()
		{
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.data = base.GetComponent<CharacterData>();
			this.gun = base.GetComponent<Gun>();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00003AB4 File Offset: 0x00001CB4
		public override void Hit(Vector2 position, Vector2 normal, Vector2 velocity)
		{
			if (!BurnMono.fieldsound)
			{
				AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("fire");
				SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
				soundContainer.audioClip[0] = audioClip;
				soundContainer.setting.volumeIntensityEnable = true;
				BurnMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
				BurnMono.fieldsound.soundContainerArray[0] = soundContainer;
			}
			this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
			SoundManager.Instance.PlayAtPosition(BurnMono.fieldsound, base.transform, position, new SoundParameterBase[]
			{
				this.soundParameterIntensity
			});
			GameObject gameObject = Object.Instantiate<GameObject>(((GameObject)Resources.Load("0 cards/Demonic pact")).GetComponent<Gun>().objectsToSpawn[0].effect);
			gameObject.transform.position = position;
			gameObject.hideFlags = 61;
			gameObject.name = "Explosion";
			gameObject.AddComponent<RemoveAfterSeconds>().seconds = 2f;
			ParticleSystem[] componentsInChildren = gameObject.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].startColor = new Color(1f, 0.8f, 0.1f, 1f);
			}
			foreach (ParticleSystemRenderer particleSystemRenderer in gameObject.GetComponentsInChildren<ParticleSystemRenderer>())
			{
				particleSystemRenderer.material.color = new Color(1f, 0.8f, 0.1f, 1f);
				particleSystemRenderer.sharedMaterial.color = new Color(1f, 0.8f, 0.1f, 1f);
			}
			Material[] componentsInChildren3 = gameObject.GetComponentsInChildren<Material>();
			for (int i = 0; i < componentsInChildren3.Length; i++)
			{
				componentsInChildren3[i].color = new Color(1f, 0.8f, 0.1f, 1f);
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
						this.hotbox.heatPercent += 0.25f;
						this.hotbox.heatPercent = Mathf.Min(this.hotbox.heatPercent, 1f);
					}
				}
			}
		}

		// Token: 0x04000041 RID: 65
		public Block block;

		// Token: 0x04000042 RID: 66
		public Player player;

		// Token: 0x04000043 RID: 67
		public CharacterData data;

		// Token: 0x04000044 RID: 68
		public Gun gun;

		// Token: 0x04000045 RID: 69
		public BoxTouchingLava_Mono hotbox;

		// Token: 0x04000046 RID: 70
		public static SoundEvent fieldsound;

		// Token: 0x04000047 RID: 71
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.7f, 0);
	}
}
