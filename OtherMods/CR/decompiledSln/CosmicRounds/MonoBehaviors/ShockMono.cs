using System;
using Sonigon;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000008 RID: 8
	public class ShockMono : RayHitPoison
	{
		// Token: 0x06000018 RID: 24 RVA: 0x0000275D File Offset: 0x0000095D
		private void Start()
		{
			if (base.GetComponentInParent<ProjectileHit>() != null)
			{
				base.GetComponentInParent<ProjectileHit>().bulletCanDealDeamage = false;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000277C File Offset: 0x0000097C
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
				component.TakeDamageOverTime(componentInParent.damage * base.transform.forward / (float)componentsInChildren.Length, base.transform.position, this.time, this.interval, this.color, this.soundEventDamageOverTime, base.GetComponentInParent<ProjectileHit>().ownWeapon, base.GetComponentInParent<ProjectileHit>().ownPlayer, true);
			}
			return 1;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000282E File Offset: 0x00000A2E
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0400000C RID: 12
		[Header("Sounds")]
		public SoundEvent soundEventDamageOverTime;

		// Token: 0x0400000D RID: 13
		[Header("Settings")]
		public float time = 1f;

		// Token: 0x0400000E RID: 14
		public float interval = 0.2f;

		// Token: 0x0400000F RID: 15
		public Color color = Color.yellow;
	}
}
