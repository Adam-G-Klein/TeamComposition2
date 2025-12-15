using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000088 RID: 136
	public class ScarabRegenMono : ReversibleEffect
	{
		// Token: 0x06000373 RID: 883 RVA: 0x0001A726 File Offset: 0x00018926
		public override void OnOnEnable()
		{
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0001A728 File Offset: 0x00018928
		public override void OnStart()
		{
			this.player.data.healthHandler.regeneration += 15f;
			this.effect = this.player.GetComponent<ScarabMono>();
			this.ResetTimer();
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0001A764 File Offset: 0x00018964
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (this.player.data.health >= this.player.data.maxHealth)
				{
					base.Destroy();
				}
				if (base.GetComponent<Player>().data.dead || base.GetComponent<Player>().data.health <= 0f || !base.GetComponent<Player>().gameObject.activeInHierarchy)
				{
					this.ResetTimer();
					base.Destroy();
				}
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0001A7FA File Offset: 0x000189FA
		public override void OnOnDisable()
		{
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0001A7FC File Offset: 0x000189FC
		public override void OnOnDestroy()
		{
			this.effect.active = false;
			if (this.player.data.healthHandler.regeneration >= 15f)
			{
				this.player.data.healthHandler.regeneration -= 15f;
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0001A852 File Offset: 0x00018A52
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x04000472 RID: 1138
		private readonly Color color = Color.blue;

		// Token: 0x04000473 RID: 1139
		private ReversibleColorEffect colorEffect;

		// Token: 0x04000474 RID: 1140
		private ScarabMono effect;

		// Token: 0x04000475 RID: 1141
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000476 RID: 1142
		private float startTime;
	}
}
