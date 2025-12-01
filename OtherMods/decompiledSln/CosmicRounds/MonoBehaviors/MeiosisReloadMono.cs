using System;
using ModdingUtils.MonoBehaviours;
using Sonigon;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200001D RID: 29
	public class MeiosisReloadMono : ReversibleEffect
	{
		// Token: 0x0600009A RID: 154 RVA: 0x0000609E File Offset: 0x0000429E
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000060BC File Offset: 0x000042BC
		public override void OnStart()
		{
			this.characterStatModifiersModifier.movementSpeed_mult = 1.5f;
			this.characterStatModifiersModifier.jump_mult = 1.5f;
			this.blockModifier.cdMultiplier_mult = 0.5f;
			this.blockModifier.additionalBlocks_add++;
			this.effect = this.player.GetComponent<MeiosisMono>();
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
			this.effect.able = false;
			GameObject addObjectToPlayer = ((GameObject)Resources.Load("0 cards/Empower")).GetComponent<CharacterStatModifiers>().AddObjectToPlayer;
			this.soundSpawn = addObjectToPlayer.GetComponent<Empower>().soundEmpowerSpawn;
			SoundManager.Instance.PlayAtPosition(this.soundSpawn, SoundManager.Instance.GetTransform(), this.player.transform);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000061B4 File Offset: 0x000043B4
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
				if (base.GetComponent<Player>().data.dead || base.GetComponent<Player>().data.health <= 0f || !base.GetComponent<Player>().gameObject.activeInHierarchy)
				{
					this.ResetTimer();
					base.Destroy();
				}
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00006258 File Offset: 0x00004458
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00006273 File Offset: 0x00004473
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
				this.effect.able = true;
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000629A File Offset: 0x0000449A
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x040000B4 RID: 180
		private readonly Color color = new Color(0f, 1f, 1f, 1f);

		// Token: 0x040000B5 RID: 181
		private ReversibleColorEffect colorEffect;

		// Token: 0x040000B6 RID: 182
		private readonly float updateDelay = 0.001f;

		// Token: 0x040000B7 RID: 183
		private float startTime;

		// Token: 0x040000B8 RID: 184
		private MeiosisMono effect;

		// Token: 0x040000B9 RID: 185
		private SoundEvent soundSpawn;
	}
}
