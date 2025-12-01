using System;
using Sonigon;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200000A RID: 10
	public class MosquitoMono : RayHitPoison
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002B43 File Offset: 0x00000D43
		private void Start()
		{
			if (base.GetComponentInParent<ProjectileHit>() != null)
			{
				base.GetComponentInParent<ProjectileHit>().bulletCanDealDeamage = false;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002B60 File Offset: 0x00000D60
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

		// Token: 0x06000023 RID: 35 RVA: 0x00002C12 File Offset: 0x00000E12
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x04000014 RID: 20
		[Header("Sounds")]
		public SoundEvent soundEventDamageOverTime;

		// Token: 0x04000015 RID: 21
		[Header("Settings")]
		public float time = 2f;

		// Token: 0x04000016 RID: 22
		public float interval = 0.5f;

		// Token: 0x04000017 RID: 23
		public Color color = Color.red;
	}
}
