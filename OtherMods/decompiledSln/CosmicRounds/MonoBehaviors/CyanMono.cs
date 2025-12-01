using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200002C RID: 44
	public class CyanMono : ReversibleEffect
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x000087EF File Offset: 0x000069EF
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000880C File Offset: 0x00006A0C
		public override void OnStart()
		{
			this.characterStatModifiersModifier.movementSpeed_mult = 1.3f;
			this.characterStatModifiersModifier.jump_mult = 1.3f;
			this.gunStatModifier.slow_add = 2f;
			this.gunStatModifier.bursts_add = 2;
			this.gunStatModifier.timeBetweenBullets_add = 0.5f;
			this.gunStatModifier.projectileColor += new Color(0f, 1f, 1f, 1f);
			this.effect = this.player.GetComponent<UnicornMono>();
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000088E0 File Offset: 0x00006AE0
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (this.effect.mode != 4)
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

		// Token: 0x060000F8 RID: 248 RVA: 0x00008962 File Offset: 0x00006B62
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000897D File Offset: 0x00006B7D
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00008998 File Offset: 0x00006B98
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x0400011C RID: 284
		private readonly Color color = new Color(0f, 1f, 1f, 1f);

		// Token: 0x0400011D RID: 285
		private ReversibleColorEffect colorEffect;

		// Token: 0x0400011E RID: 286
		private readonly float updateDelay = 0.1f;

		// Token: 0x0400011F RID: 287
		private float startTime;

		// Token: 0x04000120 RID: 288
		private UnicornMono effect;
	}
}
