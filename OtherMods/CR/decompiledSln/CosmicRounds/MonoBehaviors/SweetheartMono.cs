using System;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200004D RID: 77
	public class SweetheartMono : RayHitPoison
	{
		// Token: 0x060001EA RID: 490 RVA: 0x0000F868 File Offset: 0x0000DA68
		private void Start()
		{
			if (base.GetComponentInParent<ProjectileHit>() != null)
			{
				base.GetComponentInParent<ProjectileHit>().bulletCanDealDeamage = false;
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000F884 File Offset: 0x0000DA84
		public override HasToReturn DoHitEffect(HitInfo hit)
		{
			if (!hit.transform)
			{
				return 1;
			}
			RayHitPoison[] componentsInChildren = base.transform.root.GetComponentsInChildren<RayHitPoison>();
			ProjectileHit componentInParent = base.GetComponentInParent<ProjectileHit>();
			DamageOverTime component = hit.transform.GetComponent<DamageOverTime>();
			if (component)
			{
				if (!SweetheartMono.fieldsound)
				{
					AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("love");
					SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
					soundContainer.audioClip[0] = audioClip;
					soundContainer.setting.volumeIntensityEnable = true;
					SweetheartMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
					SweetheartMono.fieldsound.soundContainerArray[0] = soundContainer;
				}
				this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
				SoundManager.Instance.Play(SweetheartMono.fieldsound, base.transform, new SoundParameterBase[]
				{
					this.soundParameterIntensity
				});
				if (!hit.transform.gameObject.GetComponent<SweetheartMono>())
				{
					hit.transform.gameObject.AddComponent<SweetheartEffect>();
				}
				component.TakeDamageOverTime(componentInParent.damage * base.transform.forward / (float)componentsInChildren.Length, base.transform.position, this.time, this.interval, this.color, this.soundEventDamageOverTime, base.GetComponentInParent<ProjectileHit>().ownWeapon, base.GetComponentInParent<ProjectileHit>().ownPlayer, true);
			}
			return 1;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000FA0F File Offset: 0x0000DC0F
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0400024F RID: 591
		[Header("Sounds")]
		public SoundEvent soundEventDamageOverTime;

		// Token: 0x04000250 RID: 592
		[Header("Settings")]
		public float time = 3f;

		// Token: 0x04000251 RID: 593
		public float interval = 0.5f;

		// Token: 0x04000252 RID: 594
		public Color color = new Color(1f, 0.5f, 1f);

		// Token: 0x04000253 RID: 595
		public static SoundEvent fieldsound;

		// Token: 0x04000254 RID: 596
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.8f, 0);
	}
}
