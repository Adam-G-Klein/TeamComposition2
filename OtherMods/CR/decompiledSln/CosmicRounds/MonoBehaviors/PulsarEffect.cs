using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000038 RID: 56
	public class PulsarEffect : ReversibleEffect
	{
		// Token: 0x06000149 RID: 329 RVA: 0x0000A87C File Offset: 0x00008A7C
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000A898 File Offset: 0x00008A98
		public override void OnStart()
		{
			this.characterStatModifiersModifier.movementSpeed_mult = 0.6f;
			this.ResetTimer();
			this.count = 0f;
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000A8FC File Offset: 0x00008AFC
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				this.count += 0.1f;
				if (this.count >= 1.5f)
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

		// Token: 0x0600014C RID: 332 RVA: 0x0000A98F File Offset: 0x00008B8F
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000A9AA File Offset: 0x00008BAA
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000A9C5 File Offset: 0x00008BC5
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x0400017E RID: 382
		private readonly Color color = new Color(1f, 0.6f, 1f, 1f);

		// Token: 0x0400017F RID: 383
		private ReversibleColorEffect colorEffect;

		// Token: 0x04000180 RID: 384
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000181 RID: 385
		private float startTime;

		// Token: 0x04000182 RID: 386
		private float count;
	}
}
