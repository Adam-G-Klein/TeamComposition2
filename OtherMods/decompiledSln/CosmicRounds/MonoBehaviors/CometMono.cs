using System;
using Photon.Pun;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000026 RID: 38
	public class CometMono : MonoBehaviour
	{
		// Token: 0x060000CF RID: 207 RVA: 0x0000773B File Offset: 0x0000593B
		private void Start()
		{
			this.move = base.GetComponentInParent<MoveTransform>();
			this.sync = base.GetComponentInParent<SyncProjectile>();
			this.sync.active = true;
			this.ResetTimer();
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00007767 File Offset: 0x00005967
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000776F File Offset: 0x0000596F
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000777C File Offset: 0x0000597C
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay && base.gameObject.transform.parent != null)
			{
				this.ResetTimer();
				if (this.move.velocity.x != 0f)
				{
					MoveTransform moveTransform = this.move;
					moveTransform.velocity.x = moveTransform.velocity.x * 1.1f;
				}
				this.move.velocity.z = 0f;
			}
		}

		// Token: 0x040000F6 RID: 246
		private SyncProjectile sync;

		// Token: 0x040000F7 RID: 247
		private MoveTransform move;

		// Token: 0x040000F8 RID: 248
		private readonly float updateDelay = 0.1f;

		// Token: 0x040000F9 RID: 249
		private float startTime;
	}
}
