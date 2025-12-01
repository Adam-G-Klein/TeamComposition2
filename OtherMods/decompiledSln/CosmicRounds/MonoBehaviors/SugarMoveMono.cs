using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000019 RID: 25
	public class SugarMoveMono : ReversibleEffect
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00005561 File Offset: 0x00003761
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000557C File Offset: 0x0000377C
		public override void OnStart()
		{
			this.characterStatModifiersModifier.movementSpeed_mult = 2.5f;
			this.gravityModifier.gravityForce_mult = 2.5f;
			this.characterStatModifiersModifier.jump_mult = 2.5f;
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetEffectTimer();
			this.ResetTimer();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000055F8 File Offset: 0x000037F8
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

		// Token: 0x0600007F RID: 127 RVA: 0x0000568B File Offset: 0x0000388B
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
				this.ResetEffectTimer();
				this.ResetTimer();
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000056B2 File Offset: 0x000038B2
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
				this.ResetEffectTimer();
				this.ResetTimer();
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000056D9 File Offset: 0x000038D9
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000056E6 File Offset: 0x000038E6
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x0400008C RID: 140
		private readonly Color color = Color.magenta;

		// Token: 0x0400008D RID: 141
		private ReversibleColorEffect colorEffect;

		// Token: 0x0400008E RID: 142
		private readonly float updateDelay = 0.1f;

		// Token: 0x0400008F RID: 143
		private readonly float effectCooldown = 2f;

		// Token: 0x04000090 RID: 144
		private float startTime;

		// Token: 0x04000091 RID: 145
		private float timeOfLastEffect;
	}
}
