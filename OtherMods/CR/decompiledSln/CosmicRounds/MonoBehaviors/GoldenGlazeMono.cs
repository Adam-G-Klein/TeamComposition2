using System;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000016 RID: 22
	public class GoldenGlazeMono : RayHitEffect
	{
		// Token: 0x0600006E RID: 110 RVA: 0x0000511C File Offset: 0x0000331C
		public override HasToReturn DoHitEffect(HitInfo hit)
		{
			if (!hit.transform)
			{
				return 1;
			}
			CharacterStatModifiers component = hit.transform.GetComponent<CharacterStatModifiers>();
			GoldHealthMono component2 = hit.transform.GetComponent<GoldHealthMono>();
			if (hit.transform.GetComponent<DamageOverTime>())
			{
				if (component)
				{
					component.RPCA_AddSlow(2.2f, true);
				}
				if (!component2)
				{
					hit.transform.gameObject.AddComponent<GoldHealthMono>();
					if (!GoldenGlazeMono.fieldsound)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("golden");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						GoldenGlazeMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						GoldenGlazeMono.fieldsound.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
					SoundManager.Instance.Play(GoldenGlazeMono.fieldsound, base.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
				}
			}
			return 1;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00005240 File Offset: 0x00003440
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x04000082 RID: 130
		public static SoundEvent fieldsound;

		// Token: 0x04000083 RID: 131
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.7f, 0);
	}
}
