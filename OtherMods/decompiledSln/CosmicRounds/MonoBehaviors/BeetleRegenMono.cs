using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200000C RID: 12
	public class BeetleRegenMono : ReversibleEffect
	{
		// Token: 0x0600002C RID: 44 RVA: 0x00002E4A File Offset: 0x0000104A
		public override void OnOnEnable()
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002E4C File Offset: 0x0000104C
		public override void OnStart()
		{
			this.player.data.healthHandler.regeneration += 5f;
			this.effect = this.player.GetComponent<BeetleMono>();
			this.ResetTimer();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002E88 File Offset: 0x00001088
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

		// Token: 0x0600002F RID: 47 RVA: 0x00002F1E File Offset: 0x0000111E
		public override void OnOnDisable()
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002F20 File Offset: 0x00001120
		public override void OnOnDestroy()
		{
			this.effect.active = false;
			if (this.player.data.healthHandler.regeneration >= 5f)
			{
				this.player.data.healthHandler.regeneration -= 5f;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002F76 File Offset: 0x00001176
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x04000023 RID: 35
		private readonly Color color = Color.blue;

		// Token: 0x04000024 RID: 36
		private ReversibleColorEffect colorEffect;

		// Token: 0x04000025 RID: 37
		private BeetleMono effect;

		// Token: 0x04000026 RID: 38
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000027 RID: 39
		private float startTime;
	}
}
