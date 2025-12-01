using System;
using ModdingUtils.MonoBehaviours;
using Sonigon;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000042 RID: 66
	public class ChlorophyllReloadMono : ReversibleEffect
	{
		// Token: 0x0600018F RID: 399 RVA: 0x0000CA1F File Offset: 0x0000AC1F
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000CA3C File Offset: 0x0000AC3C
		public override void OnStart()
		{
			this.characterStatModifiersModifier.movementSpeed_mult = 1.15f;
			this.characterStatModifiersModifier.jump_mult = 1.15f;
			this.characterDataModifier.maxHealth_mult = 1.3f;
			this.regen = 8f;
			this.amount += this.regen;
			this.player.data.healthHandler.regeneration += this.regen;
			this.effect = this.player.GetComponent<ChlorophyllMono>();
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
			this.effect.able = false;
			GameObject addObjectToPlayer = ((GameObject)Resources.Load("0 cards/Empower")).GetComponent<CharacterStatModifiers>().AddObjectToPlayer;
			this.soundSpawn = addObjectToPlayer.GetComponent<Empower>().soundEmpowerSpawn;
			SoundManager.Instance.PlayAtPosition(this.soundSpawn, SoundManager.Instance.GetTransform(), this.player.transform);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000CB60 File Offset: 0x0000AD60
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (!this.effect.player.data.weaponHandler.gun.isReloading)
				{
					base.Destroy();
				}
				if (base.GetComponent<Player>().data.dead || base.GetComponent<Player>().data.health <= 0f || !base.GetComponent<Player>().gameObject.activeInHierarchy)
				{
					this.ResetTimer();
					this.colorEffect.Destroy();
					base.Destroy();
				}
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000CC00 File Offset: 0x0000AE00
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000CC1C File Offset: 0x0000AE1C
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
			this.player.data.healthHandler.regeneration -= this.amount;
			this.effect.able = true;
			this.effect.stacks = 0;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000CC7C File Offset: 0x0000AE7C
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x040001DF RID: 479
		private readonly Color color = new Color(0.1f, 1f, 0.1f, 1f);

		// Token: 0x040001E0 RID: 480
		private ReversibleColorEffect colorEffect;

		// Token: 0x040001E1 RID: 481
		private readonly float updateDelay;

		// Token: 0x040001E2 RID: 482
		private float startTime;

		// Token: 0x040001E3 RID: 483
		private float regen;

		// Token: 0x040001E4 RID: 484
		private float amount;

		// Token: 0x040001E5 RID: 485
		private ChlorophyllMono effect;

		// Token: 0x040001E6 RID: 486
		private SoundEvent soundSpawn;
	}
}
