using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000028 RID: 40
	public class RedMono : ReversibleEffect
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x000080CA File Offset: 0x000062CA
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000080E8 File Offset: 0x000062E8
		public override void OnStart()
		{
			this.gunStatModifier.damage_mult *= 2f;
			this.gunStatModifier.damageAfterDistanceMultiplier_mult *= 1.2f;
			this.gunStatModifier.projectileColor += new Color(1f, 0f, 0f, 1f);
			this.effect = this.player.GetComponent<UnicornMono>();
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000819C File Offset: 0x0000639C
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (this.effect.mode != 0)
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

		// Token: 0x060000DC RID: 220 RVA: 0x0000821D File Offset: 0x0000641D
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00008238 File Offset: 0x00006438
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00008253 File Offset: 0x00006453
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x04000108 RID: 264
		private readonly Color color = new Color(1f, 0f, 0f, 1f);

		// Token: 0x04000109 RID: 265
		private ReversibleColorEffect colorEffect;

		// Token: 0x0400010A RID: 266
		private readonly float updateDelay = 0.1f;

		// Token: 0x0400010B RID: 267
		private float startTime;

		// Token: 0x0400010C RID: 268
		private UnicornMono effect;
	}
}
