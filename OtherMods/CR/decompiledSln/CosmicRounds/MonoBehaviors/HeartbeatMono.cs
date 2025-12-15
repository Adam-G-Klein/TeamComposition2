using System;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000049 RID: 73
	public class HeartbeatMono : MonoBehaviour
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x0000E581 File Offset: 0x0000C781
		private void Awake()
		{
			this.player = base.GetComponent<Player>();
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000E58F File Offset: 0x0000C78F
		private void Start()
		{
			this.ResetEffectTimer();
			this.ResetTimer();
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000E59D File Offset: 0x0000C79D
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000E5A8 File Offset: 0x0000C7A8
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (Time.time >= this.timeOfLastEffect + this.effectCooldown)
				{
					for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
					{
						if (this.player.data.currentCards[i].cardName == "Heartbeat")
						{
							this.numcheck++;
						}
					}
					if (this.numcheck > 0)
					{
						this.ResetEffectTimer();
						this.player.data.jump.Jump(true, 0.5f);
						this.player.data.block.ResetCD(true);
						float num = this.player.data.maxHealth * 0.25f;
						this.player.data.healthHandler.Heal(num);
						if (!HeartbeatMono.fieldsound)
						{
							AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("pulsar");
							SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
							soundContainer.audioClip[0] = audioClip;
							soundContainer.setting.volumeIntensityEnable = true;
							HeartbeatMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
							HeartbeatMono.fieldsound.soundContainerArray[0] = soundContainer;
						}
						this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.4f * CR.globalVolMute.Value;
						SoundManager.Instance.Play(HeartbeatMono.fieldsound, base.transform, new SoundParameterBase[]
						{
							this.soundParameterIntensity
						});
						return;
					}
					Object.Destroy(this);
				}
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000E761 File Offset: 0x0000C961
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000E76E File Offset: 0x0000C96E
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x04000226 RID: 550
		private Player player;

		// Token: 0x04000227 RID: 551
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000228 RID: 552
		private readonly float effectCooldown = 5f;

		// Token: 0x04000229 RID: 553
		private float startTime;

		// Token: 0x0400022A RID: 554
		private float timeOfLastEffect;

		// Token: 0x0400022B RID: 555
		public int numcheck;

		// Token: 0x0400022C RID: 556
		public static SoundEvent fieldsound;

		// Token: 0x0400022D RID: 557
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.3f, 0);
	}
}
