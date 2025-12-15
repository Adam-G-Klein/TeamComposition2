using System;
using Sonigon;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200002F RID: 47
	public class PurpleBulletMono : RayHitPoison
	{
		// Token: 0x0600010A RID: 266 RVA: 0x00008DDB File Offset: 0x00006FDB
		private void Start()
		{
			if (base.GetComponentInParent<ProjectileHit>() != null)
			{
				base.GetComponentInParent<ProjectileHit>().bulletCanDealDeamage = false;
			}
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00008DF8 File Offset: 0x00006FF8
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
				component.TakeDamageOverTime(componentInParent.damage * base.transform.forward * 1.1f / (float)componentsInChildren.Length, base.transform.position, this.time, this.interval, this.color, this.soundEventDamageOverTime, base.GetComponentInParent<ProjectileHit>().ownWeapon, base.GetComponentInParent<ProjectileHit>().ownPlayer, true);
			}
			return 1;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00008EB4 File Offset: 0x000070B4
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0400012C RID: 300
		[Header("Sounds")]
		public SoundEvent soundEventDamageOverTime;

		// Token: 0x0400012D RID: 301
		[Header("Settings")]
		public float time = 1f;

		// Token: 0x0400012E RID: 302
		public float interval = 0.2f;

		// Token: 0x0400012F RID: 303
		public Color color = Color.magenta;
	}
}
