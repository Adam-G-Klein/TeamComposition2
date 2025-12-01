using System;
using Photon.Pun;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000036 RID: 54
	public class AsteroidMono : MonoBehaviour
	{
		// Token: 0x0600013F RID: 319 RVA: 0x0000A2A2 File Offset: 0x000084A2
		private void Start()
		{
			this.move = base.GetComponentInParent<MoveTransform>();
			this.sync = base.GetComponentInParent<SyncProjectile>();
			this.sync.active = true;
			this.ResetTimer();
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000A2CE File Offset: 0x000084CE
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000A2D6 File Offset: 0x000084D6
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000A2E4 File Offset: 0x000084E4
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay && base.gameObject.transform.parent != null)
			{
				this.ResetTimer();
				if (this.move.velocity.x != 0f)
				{
					MoveTransform moveTransform = this.move;
					moveTransform.velocity.x = moveTransform.velocity.x * 1.025f;
				}
				if (this.move.velocity.y != 0f)
				{
					MoveTransform moveTransform2 = this.move;
					moveTransform2.velocity.y = moveTransform2.velocity.y * 1.025f;
				}
				this.move.velocity.z = 0f;
			}
		}

		// Token: 0x04000172 RID: 370
		private SyncProjectile sync;

		// Token: 0x04000173 RID: 371
		private MoveTransform move;

		// Token: 0x04000174 RID: 372
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000175 RID: 373
		private float startTime;
	}
}
