using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000017 RID: 23
	public class GoldHealthMono : ReversibleEffect
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00005261 File Offset: 0x00003461
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000527C File Offset: 0x0000347C
		public override void OnStart()
		{
			this.characterDataModifier.maxHealth_mult = 0.7f;
			this.characterStatModifiersModifier.movementSpeed_mult = 0.5f;
			this.characterStatModifiersModifier.jump_mult = 0.5f;
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetEffectTimer();
			this.ResetTimer();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000052F8 File Offset: 0x000034F8
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (Time.time >= this.timeOfLastEffect + this.effectCooldown)
				{
					base.Destroy();
					this.colorEffect.Destroy();
				}
				if (base.GetComponent<Player>().data.dead || base.GetComponent<Player>().data.health <= 0f || !base.GetComponent<Player>().gameObject.activeInHierarchy)
				{
					this.ResetTimer();
					base.Destroy();
				}
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000538B File Offset: 0x0000358B
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
				this.ResetEffectTimer();
				this.ResetTimer();
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000053B2 File Offset: 0x000035B2
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
				this.ResetEffectTimer();
				this.ResetTimer();
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000053D9 File Offset: 0x000035D9
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000053E6 File Offset: 0x000035E6
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x04000084 RID: 132
		private readonly Color color = Color.yellow;

		// Token: 0x04000085 RID: 133
		private ReversibleColorEffect colorEffect;

		// Token: 0x04000086 RID: 134
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000087 RID: 135
		private readonly float effectCooldown = 3f;

		// Token: 0x04000088 RID: 136
		private float startTime;

		// Token: 0x04000089 RID: 137
		private float timeOfLastEffect;
	}
}
