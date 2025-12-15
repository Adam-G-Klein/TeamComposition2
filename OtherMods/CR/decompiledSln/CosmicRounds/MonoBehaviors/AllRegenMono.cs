using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000069 RID: 105
	public class AllRegenMono : ReversibleEffect
	{
		// Token: 0x06000294 RID: 660 RVA: 0x000142F6 File Offset: 0x000124F6
		public override void OnOnEnable()
		{
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000142F8 File Offset: 0x000124F8
		public override void OnStart()
		{
			this.player.data.healthHandler.regeneration += 10f;
			this.effect = this.player.GetComponent<AllMono>();
			this.ResetTimer();
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00014334 File Offset: 0x00012534
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

		// Token: 0x06000297 RID: 663 RVA: 0x000143CA File Offset: 0x000125CA
		public override void OnOnDisable()
		{
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000143CC File Offset: 0x000125CC
		public override void OnOnDestroy()
		{
			this.effect.active = false;
			if (this.player.data.healthHandler.regeneration >= 10f)
			{
				this.player.data.healthHandler.regeneration -= 10f;
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00014422 File Offset: 0x00012622
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x04000329 RID: 809
		private readonly Color color = Color.blue;

		// Token: 0x0400032A RID: 810
		private ReversibleColorEffect colorEffect;

		// Token: 0x0400032B RID: 811
		private AllMono effect;

		// Token: 0x0400032C RID: 812
		private readonly float updateDelay = 0.1f;

		// Token: 0x0400032D RID: 813
		private float startTime;
	}
}
