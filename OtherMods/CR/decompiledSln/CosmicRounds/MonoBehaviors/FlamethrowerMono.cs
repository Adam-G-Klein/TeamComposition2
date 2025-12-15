using System;
using Sonigon;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200000F RID: 15
	public class FlamethrowerMono : RayHitPoison
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00003975 File Offset: 0x00001B75
		private void Start()
		{
			if (base.GetComponentInParent<ProjectileHit>() != null)
			{
				base.GetComponentInParent<ProjectileHit>().bulletCanDealDeamage = false;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003994 File Offset: 0x00001B94
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
				component.TakeDamageOverTime(componentInParent.damage * base.transform.forward / (float)componentsInChildren.Length, base.transform.position, this.time, this.interval, new Color(1f, 0.3f, 0f, 1f), this.soundEventDamageOverTime, base.GetComponentInParent<ProjectileHit>().ownWeapon, base.GetComponentInParent<ProjectileHit>().ownPlayer, true);
			}
			return 1;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003A59 File Offset: 0x00001C59
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0400003E RID: 62
		[Header("Sounds")]
		public SoundEvent soundEventDamageOverTime;

		// Token: 0x0400003F RID: 63
		[Header("Settings")]
		public float time = 1f;

		// Token: 0x04000040 RID: 64
		public float interval = 0.25f;
	}
}
