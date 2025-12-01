using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000070 RID: 112
	public class SealSealMono : ReversibleEffect
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x00015AAC File Offset: 0x00013CAC
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00015AC8 File Offset: 0x00013CC8
		public override void OnStart()
		{
			this.characterStatModifiersModifier.respawns_mult = 0;
			this.characterStatModifiersModifier.lifeSteal_mult = 0f;
			this.characterStatModifiersModifier.secondsToTakeDamageOver_mult = 0f;
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetLivesToEffect(2);
			this.colorEffect.SetColor(this.color);
			this.ResetEffectTimer();
			this.ResetTimer();
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00015B40 File Offset: 0x00013D40
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
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00015B8C File Offset: 0x00013D8C
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
				this.ResetEffectTimer();
				this.ResetTimer();
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00015BB3 File Offset: 0x00013DB3
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
				this.ResetEffectTimer();
				this.ResetTimer();
			}
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00015BDA File Offset: 0x00013DDA
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00015BE7 File Offset: 0x00013DE7
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x04000376 RID: 886
		private readonly Color color = Color.red;

		// Token: 0x04000377 RID: 887
		private ReversibleColorEffect colorEffect;

		// Token: 0x04000378 RID: 888
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000379 RID: 889
		private readonly float effectCooldown = 4f;

		// Token: 0x0400037A RID: 890
		private float startTime;

		// Token: 0x0400037B RID: 891
		private float timeOfLastEffect;
	}
}
