using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200006A RID: 106
	public class HyperRegenMono : ReversibleEffect
	{
		// Token: 0x0600029B RID: 667 RVA: 0x0001444D File Offset: 0x0001264D
		public override void OnOnEnable()
		{
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0001444F File Offset: 0x0001264F
		public override void OnStart()
		{
			this.player.data.healthHandler.regeneration += 10f;
			this.effect = this.player.GetComponent<HyperMono>();
			this.ResetTimer();
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0001448C File Offset: 0x0001268C
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

		// Token: 0x0600029E RID: 670 RVA: 0x00014522 File Offset: 0x00012722
		public override void OnOnDisable()
		{
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00014524 File Offset: 0x00012724
		public override void OnOnDestroy()
		{
			this.effect.active = false;
			if (this.player.data.healthHandler.regeneration >= 10f)
			{
				this.player.data.healthHandler.regeneration -= 10f;
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0001457A File Offset: 0x0001277A
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x0400032E RID: 814
		private readonly Color color = Color.blue;

		// Token: 0x0400032F RID: 815
		private ReversibleColorEffect colorEffect;

		// Token: 0x04000330 RID: 816
		private HyperMono effect;

		// Token: 0x04000331 RID: 817
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000332 RID: 818
		private float startTime;
	}
}
