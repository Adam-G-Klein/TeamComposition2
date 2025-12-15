using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200002D RID: 45
	public class BlueMono : ReversibleEffect
	{
		// Token: 0x060000FC RID: 252 RVA: 0x000089D7 File Offset: 0x00006BD7
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000089F4 File Offset: 0x00006BF4
		public override void OnStart()
		{
			this.characterStatModifiersModifier.movementSpeed_mult = 0.75f;
			this.characterDataModifier.maxHealth_mult = 1.5f;
			this.blockModifier.cdMultiplier_mult = 0.5f;
			this.blockModifier.additionalBlocks_add += 2;
			this.gunStatModifier.projectileColor += new Color(0f, 0f, 1f, 1f);
			this.effect = this.player.GetComponent<UnicornMono>();
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00008AC0 File Offset: 0x00006CC0
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (this.effect.mode != 5)
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

		// Token: 0x060000FF RID: 255 RVA: 0x00008B42 File Offset: 0x00006D42
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00008B5D File Offset: 0x00006D5D
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00008B78 File Offset: 0x00006D78
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x04000121 RID: 289
		private readonly Color color = new Color(0f, 0f, 1f, 1f);

		// Token: 0x04000122 RID: 290
		private ReversibleColorEffect colorEffect;

		// Token: 0x04000123 RID: 291
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000124 RID: 292
		private float startTime;

		// Token: 0x04000125 RID: 293
		private UnicornMono effect;
	}
}
