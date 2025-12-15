using System;
using Photon.Pun;
using Sonigon;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000025 RID: 37
	public class MeteorMono : RayHitPoison
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00007586 File Offset: 0x00005786
		private void Start()
		{
			this.move = base.GetComponentInParent<MoveTransform>();
			base.GetComponentInParent<ProjectileHit>().bulletCanDealDeamage = false;
			this.sync = base.GetComponentInParent<SyncProjectile>();
			this.sync.active = true;
			this.ResetTimer();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000075C0 File Offset: 0x000057C0
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
				component.TakeDamageOverTime(componentInParent.damage * 1.25f * base.transform.forward / (float)componentsInChildren.Length, base.transform.position, 1f, 0.25f, new Color(0.1f, 1f, 0.3f, 1f), this.soundEventDamageOverTime, base.GetComponentInParent<ProjectileHit>().ownWeapon, base.GetComponentInParent<ProjectileHit>().ownPlayer, true);
			}
			return 1;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000768C File Offset: 0x0000588C
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00007694 File Offset: 0x00005894
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000076A4 File Offset: 0x000058A4
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay && base.gameObject.transform.parent != null)
			{
				this.ResetTimer();
				if (this.move.velocity.y < 0f)
				{
					MoveTransform moveTransform = this.move;
					moveTransform.velocity.y = moveTransform.velocity.y * 1.5f;
				}
				this.move.velocity.z = 0f;
			}
		}

		// Token: 0x040000F1 RID: 241
		private SyncProjectile sync;

		// Token: 0x040000F2 RID: 242
		private MoveTransform move;

		// Token: 0x040000F3 RID: 243
		private readonly float updateDelay = 0.1f;

		// Token: 0x040000F4 RID: 244
		private float startTime;

		// Token: 0x040000F5 RID: 245
		[Header("Sounds")]
		public SoundEvent soundEventDamageOverTime;
	}
}
