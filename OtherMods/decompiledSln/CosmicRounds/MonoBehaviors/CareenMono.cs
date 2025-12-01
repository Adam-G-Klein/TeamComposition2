using System;
using Photon.Pun;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000035 RID: 53
	public class CareenMono : BounceTrigger
	{
		// Token: 0x06000139 RID: 313 RVA: 0x0000A12C File Offset: 0x0000832C
		private void Start()
		{
			this.move = base.GetComponentInParent<MoveTransform>();
			this.sync = base.GetComponentInParent<SyncProjectile>();
			this.sync.active = true;
			this.ResetTimer();
			this.bounceEffects = base.GetComponents<BounceEffect>();
			RayHitReflect componentInParent = base.GetComponentInParent<RayHitReflect>();
			componentInParent.reflectAction = (Action<HitInfo>)Delegate.Combine(componentInParent.reflectAction, new Action<HitInfo>(this.Reflect));
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000A196 File Offset: 0x00008396
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000A1A3 File Offset: 0x000083A3
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000A1AC File Offset: 0x000083AC
		public void Reflect(HitInfo hit)
		{
			for (int i = 0; i < this.bounceEffects.Length; i++)
			{
				this.bounceEffects[i].DoBounce(hit);
			}
			this.caree++;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000A1E8 File Offset: 0x000083E8
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay && base.gameObject.transform.parent != null)
			{
				if (this.move.velocity.y < 0f && this.move.velocity.y > -15f && this.caree == 1)
				{
					this.move.velocity.y = 0f;
				}
				this.move.velocity.z = 0f;
				this.ResetTimer();
			}
		}

		// Token: 0x0400016B RID: 363
		private SyncProjectile sync;

		// Token: 0x0400016C RID: 364
		public int caree = 1;

		// Token: 0x0400016D RID: 365
		public Player player;

		// Token: 0x0400016E RID: 366
		private MoveTransform move;

		// Token: 0x0400016F RID: 367
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000170 RID: 368
		private float startTime;

		// Token: 0x04000171 RID: 369
		private BounceEffect[] bounceEffects;
	}
}
