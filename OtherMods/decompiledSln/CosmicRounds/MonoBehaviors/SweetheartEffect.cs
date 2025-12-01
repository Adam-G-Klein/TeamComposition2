using System;
using ModdingUtils.MonoBehaviours;
using Sonigon;
using Sonigon.Internal;
using UnboundLib;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200004E RID: 78
	public class SweetheartEffect : ReversibleEffect
	{
		// Token: 0x060001EE RID: 494 RVA: 0x0000FA6C File Offset: 0x0000DC6C
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0000FA88 File Offset: 0x0000DC88
		public override void OnStart()
		{
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetEffectTimer();
			this.ResetTimer();
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000FAD4 File Offset: 0x0000DCD4
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (this.player.data.isGrounded && this.player.isActiveAndEnabled && !this.player.data.dead && (bool)ExtensionMethods.GetFieldValue(this.player.data.playerVel, "simulated"))
				{
					this.player.data.jump.Jump(false, 2.5f);
					if (!SweetheartEffect.fieldsound)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("drop");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						SweetheartEffect.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						SweetheartEffect.fieldsound.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.5f * CR.globalVolMute.Value;
					SoundManager.Instance.Play(SweetheartEffect.fieldsound, base.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
				}
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

		// Token: 0x060001F1 RID: 497 RVA: 0x0000FC8D File Offset: 0x0000DE8D
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
				this.ResetEffectTimer();
				this.ResetTimer();
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000FCB4 File Offset: 0x0000DEB4
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
				this.ResetEffectTimer();
				this.ResetTimer();
			}
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000FCDB File Offset: 0x0000DEDB
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000FCE8 File Offset: 0x0000DEE8
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x04000255 RID: 597
		private readonly Color color = Color.magenta;

		// Token: 0x04000256 RID: 598
		private ReversibleColorEffect colorEffect;

		// Token: 0x04000257 RID: 599
		private readonly float updateDelay = 0.5f;

		// Token: 0x04000258 RID: 600
		private readonly float effectCooldown = 3f;

		// Token: 0x04000259 RID: 601
		private float startTime;

		// Token: 0x0400025A RID: 602
		private float timeOfLastEffect;

		// Token: 0x0400025B RID: 603
		public static SoundEvent fieldsound;

		// Token: 0x0400025C RID: 604
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.2f, 0);
	}
}
