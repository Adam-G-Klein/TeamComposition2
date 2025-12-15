using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200003A RID: 58
	public class GlueEffect : ReversibleEffect
	{
		// Token: 0x06000155 RID: 341 RVA: 0x0000AE54 File Offset: 0x00009054
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000AE70 File Offset: 0x00009070
		public override void OnStart()
		{
			this.characterStatModifiersModifier.movementSpeed_mult = 0.1f;
			this.characterStatModifiersModifier.jump_mult = 0.1f;
			this.ResetTimer();
			this.count = 0f;
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x0000AEE4 File Offset: 0x000090E4
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				this.count += 0.1f;
				if (this.count >= 0.75f)
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

		// Token: 0x06000158 RID: 344 RVA: 0x0000AF77 File Offset: 0x00009177
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000AF92 File Offset: 0x00009192
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000AFAD File Offset: 0x000091AD
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x0400018B RID: 395
		private readonly Color color = new Color(1f, 1f, 1f, 1f);

		// Token: 0x0400018C RID: 396
		private ReversibleColorEffect colorEffect;

		// Token: 0x0400018D RID: 397
		private readonly float updateDelay = 0.1f;

		// Token: 0x0400018E RID: 398
		private float startTime;

		// Token: 0x0400018F RID: 399
		private float count;
	}
}
