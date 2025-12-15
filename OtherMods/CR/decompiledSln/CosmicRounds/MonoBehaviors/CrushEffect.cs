using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000082 RID: 130
	public class CrushEffect : ReversibleEffect
	{
		// Token: 0x06000346 RID: 838 RVA: 0x00019670 File Offset: 0x00017870
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0001968C File Offset: 0x0001788C
		public override void OnStart()
		{
			this.characterStatModifiersModifier.movementSpeed_mult = 0.5f;
			this.characterStatModifiersModifier.jump_mult = 0.3f;
			this.gravityModifier.gravityForce_mult = 3f;
			this.ResetTimer();
			this.count = 0f;
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00019710 File Offset: 0x00017910
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				this.count += 0.1f;
				if (this.count >= 2f)
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

		// Token: 0x06000349 RID: 841 RVA: 0x000197A3 File Offset: 0x000179A3
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000197BE File Offset: 0x000179BE
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x000197D9 File Offset: 0x000179D9
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x04000434 RID: 1076
		private readonly Color color = new Color(1f, 0.3f, 0f, 1f);

		// Token: 0x04000435 RID: 1077
		private ReversibleColorEffect colorEffect;

		// Token: 0x04000436 RID: 1078
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000437 RID: 1079
		private float startTime;

		// Token: 0x04000438 RID: 1080
		private float count;
	}
}
