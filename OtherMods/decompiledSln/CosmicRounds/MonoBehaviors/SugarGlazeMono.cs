using System;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000018 RID: 24
	public class SugarGlazeMono : RayHitEffect
	{
		// Token: 0x06000079 RID: 121 RVA: 0x0000541C File Offset: 0x0000361C
		public override HasToReturn DoHitEffect(HitInfo hit)
		{
			if (!hit.transform)
			{
				return 1;
			}
			PlayerJump component = hit.transform.GetComponent<PlayerJump>();
			SugarMoveMono component2 = hit.transform.GetComponent<SugarMoveMono>();
			if (hit.transform.GetComponent<DamageOverTime>())
			{
				if (component)
				{
					component.Jump(false, 2f);
				}
				if (!component2)
				{
					hit.transform.gameObject.AddComponent<SugarMoveMono>();
					if (!SugarGlazeMono.fieldsound)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("pogo");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						SugarGlazeMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						SugarGlazeMono.fieldsound.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
					SoundManager.Instance.Play(SugarGlazeMono.fieldsound, base.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
				}
			}
			return 1;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00005540 File Offset: 0x00003740
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0400008A RID: 138
		public static SoundEvent fieldsound;

		// Token: 0x0400008B RID: 139
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.6f, 0);
	}
}
