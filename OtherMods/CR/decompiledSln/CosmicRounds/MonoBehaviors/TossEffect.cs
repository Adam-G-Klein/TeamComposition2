using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000084 RID: 132
	public class TossEffect : ReversibleEffect
	{
		// Token: 0x06000357 RID: 855 RVA: 0x00019C58 File Offset: 0x00017E58
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00019C74 File Offset: 0x00017E74
		public override void OnStart()
		{
			this.characterStatModifiersModifier.movementSpeed_mult = 0.5f;
			this.characterStatModifiersModifier.jump_mult = 2f;
			this.gravityModifier.gravityForce_add = -0.75f;
			this.ResetTimer();
			this.count = 0f;
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00019CF8 File Offset: 0x00017EF8
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				this.count += 0.1f;
				if (this.count >= 2f)
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

		// Token: 0x0600035A RID: 858 RVA: 0x00019D8B File Offset: 0x00017F8B
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00019DA6 File Offset: 0x00017FA6
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00019DC1 File Offset: 0x00017FC1
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x0400044A RID: 1098
		private readonly Color color = new Color(1f, 0.2f, 1f, 1f);

		// Token: 0x0400044B RID: 1099
		private ReversibleColorEffect colorEffect;

		// Token: 0x0400044C RID: 1100
		private readonly float updateDelay = 0.1f;

		// Token: 0x0400044D RID: 1101
		private float startTime;

		// Token: 0x0400044E RID: 1102
		private float count;
	}
}
