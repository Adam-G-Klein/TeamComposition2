using System;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000086 RID: 134
	public class ResonateRing : MonoBehaviour
	{
		// Token: 0x06000369 RID: 873 RVA: 0x0001A2A8 File Offset: 0x000184A8
		public void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				this.counter += 1f;
				if (this.counter % 3f == 0f)
				{
					if (!ResonateRing.fieldsound)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("glue");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						ResonateRing.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						ResonateRing.fieldsound.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
					SoundManager.Instance.Play(ResonateRing.fieldsound, base.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
					this.player.data.block.DoBlockAtPosition(false, true, 4, base.gameObject.transform.position, true);
					Object.Instantiate<GameObject>(ResonateMono.resoVisual, base.gameObject.transform.position, Quaternion.identity);
				}
				if (this.counter >= 10f)
				{
					Object.Instantiate<GameObject>(ResonateMono.resoVisual, base.gameObject.transform.position, Quaternion.identity);
					if (!ResonateRing.fieldsound2)
					{
						AudioClip audioClip2 = CR.ArtAsset.LoadAsset<AudioClip>("pulsar");
						SoundContainer soundContainer2 = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer2.audioClip[0] = audioClip2;
						soundContainer2.setting.volumeIntensityEnable = true;
						ResonateRing.fieldsound2 = ScriptableObject.CreateInstance<SoundEvent>();
						ResonateRing.fieldsound2.soundContainerArray[0] = soundContainer2;
					}
					this.soundParameterIntensity2.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
					SoundManager.Instance.Play(ResonateRing.fieldsound2, base.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity2
					});
					SoundManager.Instance.Stop(ResonateRing.fieldsound2, base.transform, true);
				}
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0001A4DD File Offset: 0x000186DD
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x0400045F RID: 1119
		private readonly float updateDelay = 0.2f;

		// Token: 0x04000460 RID: 1120
		private float startTime;

		// Token: 0x04000461 RID: 1121
		private float counter;

		// Token: 0x04000462 RID: 1122
		public Player player;

		// Token: 0x04000463 RID: 1123
		public static SoundEvent fieldsound;

		// Token: 0x04000464 RID: 1124
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.6f, 0);

		// Token: 0x04000465 RID: 1125
		public static SoundEvent fieldsound2;

		// Token: 0x04000466 RID: 1126
		private SoundParameterIntensity soundParameterIntensity2 = new SoundParameterIntensity(0.4f, 0);
	}
}
