using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000029 RID: 41
	public class OrangeMono : ReversibleEffect
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00008292 File Offset: 0x00006492
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000082B0 File Offset: 0x000064B0
		public override void OnStart()
		{
			this.gunAmmoStatModifier.reloadTimeMultiplier_mult *= 0.1f;
			this.gunStatModifier.attackSpeed_mult *= 0.001f;
			this.gunStatModifier.damage_mult *= 0.75f;
			this.gunStatModifier.projectileColor += new Color(1f, 0.5f, 0f, 1f);
			this.effect = this.player.GetComponent<UnicornMono>();
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000837C File Offset: 0x0000657C
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (this.effect.mode != 1)
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

		// Token: 0x060000E3 RID: 227 RVA: 0x000083FE File Offset: 0x000065FE
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00008419 File Offset: 0x00006619
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00008434 File Offset: 0x00006634
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x0400010D RID: 269
		private readonly Color color = new Color(1f, 0.5f, 0f, 1f);

		// Token: 0x0400010E RID: 270
		private ReversibleColorEffect colorEffect;

		// Token: 0x0400010F RID: 271
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000110 RID: 272
		private float startTime;

		// Token: 0x04000111 RID: 273
		private UnicornMono effect;
	}
}
