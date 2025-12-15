using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000033 RID: 51
	public class IgniteEffect : ReversibleEffect
	{
		// Token: 0x06000129 RID: 297 RVA: 0x00009946 File Offset: 0x00007B46
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00009964 File Offset: 0x00007B64
		public override void OnStart()
		{
			this.characterStatModifiersModifier.movementSpeed_mult = 0.35f;
			this.heat = this.player.transform.GetComponent<HealthHandler>();
			this.dat = this.player.transform.GetComponent<CharacterData>();
			this.ResetTimer();
			this.count = 0f;
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000099F4 File Offset: 0x00007BF4
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.heat.DoDamage(this.dat.maxHealth / 4f * 0.125f * Vector2.down, this.player.transform.position, this.color, this.player.data.weaponHandler.gameObject, this.player, false, true, true);
				this.ResetTimer();
				this.count += 0.1f;
				if (this.count >= 1f)
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

		// Token: 0x0600012C RID: 300 RVA: 0x00009AEF File Offset: 0x00007CEF
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00009B0A File Offset: 0x00007D0A
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00009B25 File Offset: 0x00007D25
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x04000158 RID: 344
		private readonly Color color = new Color(1f, 0.3f, 0f, 1f);

		// Token: 0x04000159 RID: 345
		private ReversibleColorEffect colorEffect;

		// Token: 0x0400015A RID: 346
		private readonly float updateDelay = 0.25f;

		// Token: 0x0400015B RID: 347
		private float startTime;

		// Token: 0x0400015C RID: 348
		private HealthHandler heat;

		// Token: 0x0400015D RID: 349
		private CharacterData dat;

		// Token: 0x0400015E RID: 350
		private float count;
	}
}
