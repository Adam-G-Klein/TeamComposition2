using System;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200005A RID: 90
	public class ConstantRot : MonoBehaviour
	{
		// Token: 0x06000235 RID: 565 RVA: 0x00011FAB File Offset: 0x000101AB
		public void Update()
		{
			if (this.clockwise)
			{
				this.rotateClockwise();
				return;
			}
			this.rotateCtrClockwise();
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00011FC2 File Offset: 0x000101C2
		public void rotateClockwise()
		{
			base.transform.Rotate(0f, 0f, this.speed * Time.deltaTime);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00011FE5 File Offset: 0x000101E5
		public void rotateCtrClockwise()
		{
			base.transform.Rotate(0f, 0f, -(this.speed * Time.deltaTime));
		}

		// Token: 0x040002B7 RID: 695
		public float speed = 360f;

		// Token: 0x040002B8 RID: 696
		public bool clockwise = true;
	}
}
