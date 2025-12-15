using System;
using Sonigon;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000066 RID: 102
	public class HaloFireMono : RayHitPoison
	{
		// Token: 0x06000282 RID: 642 RVA: 0x00013DDD File Offset: 0x00011FDD
		private void Start()
		{
			if (base.GetComponentInParent<ProjectileHit>() != null)
			{
				base.GetComponentInParent<ProjectileHit>().bulletCanDealDeamage = false;
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00013DFC File Offset: 0x00011FFC
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
				component.TakeDamageOverTime(componentInParent.damage * base.transform.forward / (float)componentsInChildren.Length, base.transform.position, this.time, this.interval, new Color(1f, 1f, 0.5f, 1f), this.soundEventDamageOverTime, base.GetComponentInParent<ProjectileHit>().ownWeapon, base.GetComponentInParent<ProjectileHit>().ownPlayer, true);
			}
			return 1;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00013EC1 File Offset: 0x000120C1
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x04000310 RID: 784
		[Header("Sounds")]
		public SoundEvent soundEventDamageOverTime;

		// Token: 0x04000311 RID: 785
		[Header("Settings")]
		public float time = 1f;

		// Token: 0x04000312 RID: 786
		public float interval = 0.25f;
	}
}
