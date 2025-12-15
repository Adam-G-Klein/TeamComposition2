using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200002A RID: 42
	public class YellowMono : ReversibleEffect
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x00008473 File Offset: 0x00006673
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00008490 File Offset: 0x00006690
		public override void OnStart()
		{
			this.gunStatModifier.projectileSize_mult *= 1.5f;
			this.gunStatModifier.projectileSpeed_mult *= 2f;
			this.gunStatModifier.projectileColor += new Color(1f, 1f, 0f, 1f);
			this.effect = this.player.GetComponent<UnicornMono>();
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00008544 File Offset: 0x00006744
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (this.effect.mode != 2)
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

		// Token: 0x060000EA RID: 234 RVA: 0x000085C6 File Offset: 0x000067C6
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000085E1 File Offset: 0x000067E1
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000085FC File Offset: 0x000067FC
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x04000112 RID: 274
		private readonly Color color = new Color(1f, 1f, 0f, 1f);

		// Token: 0x04000113 RID: 275
		private ReversibleColorEffect colorEffect;

		// Token: 0x04000114 RID: 276
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000115 RID: 277
		private float startTime;

		// Token: 0x04000116 RID: 278
		private UnicornMono effect;
	}
}
