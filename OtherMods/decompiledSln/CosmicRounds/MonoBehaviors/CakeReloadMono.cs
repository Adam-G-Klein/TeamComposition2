using System;
using ModdingUtils.MonoBehaviours;
using Sonigon;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000040 RID: 64
	public class CakeReloadMono : ReversibleEffect
	{
		// Token: 0x06000182 RID: 386 RVA: 0x0000C607 File Offset: 0x0000A807
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000C624 File Offset: 0x0000A824
		public override void OnStart()
		{
			this.characterStatModifiersModifier.movementSpeed_mult = 1.5f;
			this.characterStatModifiersModifier.jump_mult = 1.5f;
			this.regen = 10f;
			this.player.data.healthHandler.regeneration += this.regen;
			this.effect = this.player.GetComponent<CakeMono>();
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
			this.effect.able = false;
			GameObject addObjectToPlayer = ((GameObject)Resources.Load("0 cards/Empower")).GetComponent<CharacterStatModifiers>().AddObjectToPlayer;
			this.soundSpawn = addObjectToPlayer.GetComponent<Empower>().soundEmpowerSpawn;
			SoundManager.Instance.PlayAtPosition(this.soundSpawn, SoundManager.Instance.GetTransform(), this.player.transform);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000C724 File Offset: 0x0000A924
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (!this.effect.player.data.weaponHandler.gun.isReloading)
				{
					base.Destroy();
					this.effect.able = false;
				}
				if (base.GetComponent<Player>().data.dead || base.GetComponent<Player>().data.health <= 0f || base.GetComponent<Player>().data.health <= 0f)
				{
					this.ResetTimer();
					this.colorEffect.Destroy();
					base.Destroy();
				}
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000C7D8 File Offset: 0x0000A9D8
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000C7F4 File Offset: 0x0000A9F4
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
				this.player.data.healthHandler.regeneration -= this.regen;
				this.effect.able = true;
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000C848 File Offset: 0x0000AA48
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x040001CC RID: 460
		private readonly Color color = new Color(1f, 0.3f, 1f, 1f);

		// Token: 0x040001CD RID: 461
		private ReversibleColorEffect colorEffect;

		// Token: 0x040001CE RID: 462
		private readonly float updateDelay = 0.001f;

		// Token: 0x040001CF RID: 463
		private float startTime;

		// Token: 0x040001D0 RID: 464
		private float regen;

		// Token: 0x040001D1 RID: 465
		private CakeMono effect;

		// Token: 0x040001D2 RID: 466
		private SoundEvent soundSpawn;
	}
}
