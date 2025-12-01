using System;
using Photon.Pun;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000054 RID: 84
	public class SatelliteMono : MonoBehaviour
	{
		// Token: 0x06000215 RID: 533 RVA: 0x000110B0 File Offset: 0x0000F2B0
		private void Start()
		{
			this.move = base.GetComponentInParent<MoveTransform>();
			this.sync = base.GetComponentInParent<SyncProjectile>();
			this.sync.active = true;
			this.ResetTimer();
			this.yvel = this.move.velocity.y;
			this.xvel = this.move.velocity.x;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00011113 File Offset: 0x0000F313
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0001111B File Offset: 0x0000F31B
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00011128 File Offset: 0x0000F328
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay && base.gameObject.transform.parent != null)
			{
				this.ResetTimer();
				this.count += 0.1f;
				if (!this.mode)
				{
					this.yvel = this.move.velocity.y;
					this.xvel = this.move.velocity.x;
				}
				else
				{
					this.move.simulateGravity--;
					this.move.velocity.y = 0f;
					this.move.velocity.x = 0f;
				}
				if (this.count < 0.5f)
				{
					this.mode = false;
				}
				if (this.count > 0.4f)
				{
					this.mode = true;
				}
				if (this.count > 1f)
				{
					this.move.simulateGravity++;
					this.move.velocity.y = this.yvel;
					this.move.velocity.x = this.xvel;
					this.count = 0f;
					this.mode = false;
				}
				this.move.velocity.z = 0f;
			}
		}

		// Token: 0x04000281 RID: 641
		private SyncProjectile sync;

		// Token: 0x04000282 RID: 642
		private MoveTransform move;

		// Token: 0x04000283 RID: 643
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000284 RID: 644
		private float count;

		// Token: 0x04000285 RID: 645
		private bool mode;

		// Token: 0x04000286 RID: 646
		public float yvel;

		// Token: 0x04000287 RID: 647
		public float xvel;

		// Token: 0x04000288 RID: 648
		private float startTime;
	}
}
