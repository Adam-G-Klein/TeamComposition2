using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000022 RID: 34
	public class CriticalMono : ReversibleEffect
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00006D6C File Offset: 0x00004F6C
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
			if (base.gameObject.transform.parent == null)
			{
				this.ResetTimer();
				base.Destroy();
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00006DAC File Offset: 0x00004FAC
		public override void OnStart()
		{
			this.effect = this.player.GetComponent<CriticalHitMono>();
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
			GameObject addObjectToPlayer = ((GameObject)Resources.Load("0 cards/Empower")).GetComponent<CharacterStatModifiers>().AddObjectToPlayer;
			if (base.gameObject.transform.parent == null)
			{
				this.ResetTimer();
				base.Destroy();
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00006E44 File Offset: 0x00005044
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				base.Destroy();
			}
			if (base.GetComponent<Player>().data.dead || base.GetComponent<Player>().data.health <= 0f || !base.GetComponent<Player>().gameObject.activeInHierarchy)
			{
				this.ResetTimer();
				base.Destroy();
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00006EB8 File Offset: 0x000050B8
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
			if (base.gameObject.transform.parent == null)
			{
				this.ResetTimer();
				base.Destroy();
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00006EF7 File Offset: 0x000050F7
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
				Object.Destroy(this);
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00006F18 File Offset: 0x00005118
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x040000DC RID: 220
		private readonly Color color = new Color(1f, 0.8f, 0f, 1f);

		// Token: 0x040000DD RID: 221
		private ReversibleColorEffect colorEffect;

		// Token: 0x040000DE RID: 222
		private readonly float updateDelay = 0.2f;

		// Token: 0x040000DF RID: 223
		private float startTime;

		// Token: 0x040000E0 RID: 224
		private CriticalHitMono effect;
	}
}
