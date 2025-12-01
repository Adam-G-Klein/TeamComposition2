using System;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200003C RID: 60
	public class AquaRing : MonoBehaviour
	{
		// Token: 0x06000167 RID: 359 RVA: 0x0000B494 File Offset: 0x00009694
		public void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				if (!AquaRing.fieldsound)
				{
					AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("water");
					SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
					soundContainer.audioClip[0] = audioClip;
					soundContainer.setting.volumeIntensityEnable = true;
					AquaRing.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
					AquaRing.fieldsound.soundContainerArray[0] = soundContainer;
				}
				this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
				SoundManager.Instance.Play(AquaRing.fieldsound, base.transform, new SoundParameterBase[]
				{
					this.soundParameterIntensity
				});
				Vector2 vector = base.gameObject.transform.position;
				Player[] array = PlayerManager.instance.players.ToArray();
				for (int i = 0; i < array.Length; i++)
				{
					if (Vector2.Distance(vector, array[i].transform.position) <= 6f)
					{
						CharacterData component = array[i].gameObject.GetComponent<CharacterData>();
						float num = 1f + component.maxHealth * 0.018f;
						array[i].gameObject.GetComponent<HealthHandler>().Heal(num);
					}
				}
				this.ResetTimer();
				this.counter += 0.1f;
				if (this.counter >= 1f)
				{
					Object.Instantiate<GameObject>(AquaRingMono.aquaVisual, base.gameObject.transform.position, Quaternion.identity);
					if (!AquaRing.fieldsound2)
					{
						AudioClip audioClip2 = CR.ArtAsset.LoadAsset<AudioClip>("pulsar");
						SoundContainer soundContainer2 = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer2.audioClip[0] = audioClip2;
						soundContainer2.setting.volumeIntensityEnable = true;
						AquaRing.fieldsound2 = ScriptableObject.CreateInstance<SoundEvent>();
						AquaRing.fieldsound2.soundContainerArray[0] = soundContainer2;
					}
					this.soundParameterIntensity2.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
					SoundManager.Instance.Play(AquaRing.fieldsound2, base.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity2
					});
					SoundManager.Instance.Stop(AquaRing.fieldsound2, base.transform, true);
				}
			}
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000B707 File Offset: 0x00009907
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x040001A0 RID: 416
		private readonly float updateDelay = 0.2f;

		// Token: 0x040001A1 RID: 417
		private float startTime;

		// Token: 0x040001A2 RID: 418
		private float counter;

		// Token: 0x040001A3 RID: 419
		public static SoundEvent fieldsound;

		// Token: 0x040001A4 RID: 420
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.6f, 0);

		// Token: 0x040001A5 RID: 421
		public static SoundEvent fieldsound2;

		// Token: 0x040001A6 RID: 422
		private SoundParameterIntensity soundParameterIntensity2 = new SoundParameterIntensity(0.6f, 0);
	}
}
