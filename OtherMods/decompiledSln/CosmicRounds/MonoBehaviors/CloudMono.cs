using System;
using Photon.Pun;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200001F RID: 31
	public class CloudMono : MonoBehaviour
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x000064C6 File Offset: 0x000046C6
		private void Start()
		{
			this.move = base.GetComponentInParent<MoveTransform>();
			this.sync = base.GetComponentInParent<SyncProjectile>();
			this.sync.active = true;
			this.ResetTimer();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000064F2 File Offset: 0x000046F2
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000064FA File Offset: 0x000046FA
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00006508 File Offset: 0x00004708
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay && base.gameObject.transform.parent != null)
			{
				this.ResetTimer();
				MoveTransform moveTransform = this.move;
				moveTransform.velocity.x = moveTransform.velocity.x * 0.9f;
				MoveTransform moveTransform2 = this.move;
				moveTransform2.velocity.y = moveTransform2.velocity.y * 0.85f;
				this.move.velocity.z = 0f;
			}
		}

		// Token: 0x040000C2 RID: 194
		private MoveTransform move;

		// Token: 0x040000C3 RID: 195
		private readonly float updateDelay = 0.1f;

		// Token: 0x040000C4 RID: 196
		private float startTime;

		// Token: 0x040000C5 RID: 197
		private SyncProjectile sync;
	}
}
