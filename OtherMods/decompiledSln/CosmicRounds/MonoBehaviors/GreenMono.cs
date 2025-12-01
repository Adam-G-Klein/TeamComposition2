using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200002B RID: 43
	public class GreenMono : ReversibleEffect
	{
		// Token: 0x060000EE RID: 238 RVA: 0x0000863B File Offset: 0x0000683B
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00008658 File Offset: 0x00006858
		public override void OnStart()
		{
			this.characterDataModifier.maxHealth_mult *= 3f;
			this.gunStatModifier.projectileColor += new Color(0f, 1f, 0f, 1f);
			this.effect = this.player.GetComponent<UnicornMono>();
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000086F8 File Offset: 0x000068F8
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (this.effect.mode != 3)
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

		// Token: 0x060000F1 RID: 241 RVA: 0x0000877A File Offset: 0x0000697A
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00008795 File Offset: 0x00006995
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000087B0 File Offset: 0x000069B0
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x04000117 RID: 279
		private readonly Color color = new Color(0f, 1f, 0f, 1f);

		// Token: 0x04000118 RID: 280
		private ReversibleColorEffect colorEffect;

		// Token: 0x04000119 RID: 281
		private readonly float updateDelay = 0.1f;

		// Token: 0x0400011A RID: 282
		private float startTime;

		// Token: 0x0400011B RID: 283
		private UnicornMono effect;
	}
}
