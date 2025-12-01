using System;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200001E RID: 30
	public class PogoMono : MonoBehaviour
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x000062D9 File Offset: 0x000044D9
		private void Awake()
		{
			this.player = base.GetComponent<Player>();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000062E7 File Offset: 0x000044E7
		private void Start()
		{
			this.ResetEffectTimer();
			this.ResetTimer();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000062F5 File Offset: 0x000044F5
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00006300 File Offset: 0x00004500
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (Time.time >= this.timeOfLastEffect + this.effectCooldown)
				{
					for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
					{
						if (this.player.data.currentCards[i].cardName == "Pogo")
						{
							this.numcheck++;
						}
					}
					if (this.numcheck > 0)
					{
						this.ResetEffectTimer();
						this.player.data.jump.Jump(true, 0.5f);
						if (!PogoMono.fieldsound)
						{
							AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("pogo");
							SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
							soundContainer.audioClip[0] = audioClip;
							soundContainer.setting.volumeIntensityEnable = true;
							PogoMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
							PogoMono.fieldsound.soundContainerArray[0] = soundContainer;
						}
						this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.5f * CR.globalVolMute.Value;
						SoundManager.Instance.Play(PogoMono.fieldsound, base.transform, new SoundParameterBase[]
						{
							this.soundParameterIntensity
						});
						return;
					}
					Object.Destroy(this);
				}
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00006476 File Offset: 0x00004676
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00006483 File Offset: 0x00004683
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x040000BA RID: 186
		private Player player;

		// Token: 0x040000BB RID: 187
		private readonly float updateDelay = 0.1f;

		// Token: 0x040000BC RID: 188
		private readonly float effectCooldown = 4f;

		// Token: 0x040000BD RID: 189
		private float startTime;

		// Token: 0x040000BE RID: 190
		private float timeOfLastEffect;

		// Token: 0x040000BF RID: 191
		public int numcheck;

		// Token: 0x040000C0 RID: 192
		public static SoundEvent fieldsound;

		// Token: 0x040000C1 RID: 193
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.2f, 0);
	}
}
