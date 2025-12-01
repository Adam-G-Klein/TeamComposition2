using System;
using ModdingUtils.MonoBehaviours;
using Sonigon;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000078 RID: 120
	public class ChargeCDMono : ReversibleEffect
	{
		// Token: 0x06000300 RID: 768 RVA: 0x00018086 File Offset: 0x00016286
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000180A4 File Offset: 0x000162A4
		public override void OnStart()
		{
			this.gunStatModifier.damage_mult = 1.4f;
			this.characterStatModifiersModifier.movementSpeed_mult = 1.5f;
			this.gunStatModifier.projectileColor = Color.yellow;
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0001811C File Offset: 0x0001631C
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (!this.player.data.block.IsOnCD())
				{
					base.Destroy();
				}
				if (base.GetComponent<Player>().data.dead || base.GetComponent<Player>().data.health <= 0f || base.GetComponent<Player>().data.health <= 0f)
				{
					this.ResetTimer();
					this.colorEffect.Destroy();
					base.Destroy();
				}
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x000181B7 File Offset: 0x000163B7
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000181D2 File Offset: 0x000163D2
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000181ED File Offset: 0x000163ED
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x040003D8 RID: 984
		private readonly Color color = new Color(1f, 1f, 0f, 1f);

		// Token: 0x040003D9 RID: 985
		private ReversibleColorEffect colorEffect;

		// Token: 0x040003DA RID: 986
		private readonly float updateDelay = 0.001f;

		// Token: 0x040003DB RID: 987
		private float startTime;

		// Token: 0x040003DC RID: 988
		private float regen;

		// Token: 0x040003DD RID: 989
		private ChargeMono effect;

		// Token: 0x040003DE RID: 990
		private SoundEvent soundSpawn;
	}
}
