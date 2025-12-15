using System;
using Photon.Pun;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000053 RID: 83
	public class ShootingStarMono : MonoBehaviour
	{
		// Token: 0x06000210 RID: 528 RVA: 0x00010FEE File Offset: 0x0000F1EE
		private void Start()
		{
			this.move = base.GetComponentInParent<MoveTransform>();
			this.sync = base.GetComponentInParent<SyncProjectile>();
			this.sync.active = true;
			this.ResetTimer();
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0001101A File Offset: 0x0000F21A
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00011022 File Offset: 0x0000F222
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00011030 File Offset: 0x0000F230
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay && base.gameObject.transform.parent != null)
			{
				this.ResetTimer();
				MoveTransform moveTransform = this.move;
				moveTransform.velocity.y = moveTransform.velocity.y * 1.025f;
				this.move.velocity.z = 0f;
			}
		}

		// Token: 0x0400027D RID: 637
		private SyncProjectile sync;

		// Token: 0x0400027E RID: 638
		private MoveTransform move;

		// Token: 0x0400027F RID: 639
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000280 RID: 640
		private float startTime;
	}
}
