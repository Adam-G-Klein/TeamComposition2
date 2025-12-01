using System;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000050 RID: 80
	public class RingObject : MonoBehaviour
	{
		// Token: 0x060001FE RID: 510 RVA: 0x000105D8 File Offset: 0x0000E7D8
		public void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				Vector2 vector = base.gameObject.transform.position;
				Player[] array = PlayerManager.instance.players.ToArray();
				for (int i = 0; i < array.Length; i++)
				{
					if (Vector2.Distance(vector, array[i].transform.position) <= 1f)
					{
						this.ringeffect = true;
					}
					else
					{
						this.ringeffect = false;
					}
				}
				this.ResetTimer();
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00010667 File Offset: 0x0000E867
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x04000262 RID: 610
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000263 RID: 611
		private float startTime;

		// Token: 0x04000264 RID: 612
		private float counter;

		// Token: 0x04000265 RID: 613
		public Player player;

		// Token: 0x04000266 RID: 614
		public bool ringeffect;
	}
}
