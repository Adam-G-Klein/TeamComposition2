using System;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000027 RID: 39
	public class UnicornMono : MonoBehaviour
	{
		// Token: 0x060000D4 RID: 212 RVA: 0x00007814 File Offset: 0x00005A14
		private void Start()
		{
			this.player = base.gameObject.GetComponent<Player>();
			this.gun = this.player.GetComponent<Holding>().holdable.GetComponent<Gun>();
			this.data = this.player.GetComponent<CharacterData>();
			this.healthHandler = this.player.GetComponent<HealthHandler>();
			this.gravity = this.player.GetComponent<Gravity>();
			this.block = this.player.GetComponent<Block>();
			this.gunAmmo = this.gun.GetComponentInChildren<GunAmmo>();
			this.mode = 0;
			this.realmode = 0;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000078B0 File Offset: 0x00005AB0
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000078B8 File Offset: 0x00005AB8
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000078C8 File Offset: 0x00005AC8
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				int i = 0;
				if (this.player.data.currentCards != null)
				{
					while (i <= this.player.data.currentCards.Count - 1)
					{
						if (this.player.data.currentCards[i].cardName == "Unicorn")
						{
							this.numcheck++;
						}
						i++;
					}
				}
				if (this.numcheck > 0)
				{
					if (this.realmode == 0)
					{
						this.player.gameObject.AddComponent<RedMono>();
						this.mode = 0;
						this.realmode++;
						if (!UnicornMono.fieldsound)
						{
							AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("aqua");
							SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
							soundContainer.audioClip[0] = audioClip;
							soundContainer.setting.volumeIntensityEnable = true;
							UnicornMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
							UnicornMono.fieldsound.soundContainerArray[0] = soundContainer;
						}
						this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
						SoundManager.Instance.Play(UnicornMono.fieldsound, base.transform, new SoundParameterBase[]
						{
							this.soundParameterIntensity
						});
						return;
					}
					if (this.realmode == 1)
					{
						this.player.gameObject.AddComponent<OrangeMono>();
						this.mode++;
						this.realmode++;
						if (!UnicornMono.fieldsound)
						{
							AudioClip audioClip2 = CR.ArtAsset.LoadAsset<AudioClip>("aqua");
							SoundContainer soundContainer2 = ScriptableObject.CreateInstance<SoundContainer>();
							soundContainer2.audioClip[0] = audioClip2;
							soundContainer2.setting.volumeIntensityEnable = true;
							UnicornMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
							UnicornMono.fieldsound.soundContainerArray[0] = soundContainer2;
						}
						this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
						SoundManager.Instance.Play(UnicornMono.fieldsound, base.transform, new SoundParameterBase[]
						{
							this.soundParameterIntensity
						});
						return;
					}
					if (this.realmode == 2)
					{
						this.player.gameObject.AddComponent<YellowMono>();
						this.mode++;
						this.realmode++;
						if (!UnicornMono.fieldsound)
						{
							AudioClip audioClip3 = CR.ArtAsset.LoadAsset<AudioClip>("aqua");
							SoundContainer soundContainer3 = ScriptableObject.CreateInstance<SoundContainer>();
							soundContainer3.audioClip[0] = audioClip3;
							soundContainer3.setting.volumeIntensityEnable = true;
							UnicornMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
							UnicornMono.fieldsound.soundContainerArray[0] = soundContainer3;
						}
						this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
						SoundManager.Instance.Play(UnicornMono.fieldsound, base.transform, new SoundParameterBase[]
						{
							this.soundParameterIntensity
						});
						return;
					}
					if (this.realmode == 3)
					{
						this.player.gameObject.AddComponent<GreenMono>();
						this.mode++;
						this.realmode++;
						if (!UnicornMono.fieldsound)
						{
							AudioClip audioClip4 = CR.ArtAsset.LoadAsset<AudioClip>("aqua");
							SoundContainer soundContainer4 = ScriptableObject.CreateInstance<SoundContainer>();
							soundContainer4.audioClip[0] = audioClip4;
							soundContainer4.setting.volumeIntensityEnable = true;
							UnicornMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
							UnicornMono.fieldsound.soundContainerArray[0] = soundContainer4;
						}
						this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
						SoundManager.Instance.Play(UnicornMono.fieldsound, base.transform, new SoundParameterBase[]
						{
							this.soundParameterIntensity
						});
						return;
					}
					if (this.realmode == 4)
					{
						this.player.gameObject.AddComponent<CyanMono>();
						this.mode++;
						this.realmode++;
						if (!UnicornMono.fieldsound)
						{
							AudioClip audioClip5 = CR.ArtAsset.LoadAsset<AudioClip>("aqua");
							SoundContainer soundContainer5 = ScriptableObject.CreateInstance<SoundContainer>();
							soundContainer5.audioClip[0] = audioClip5;
							soundContainer5.setting.volumeIntensityEnable = true;
							UnicornMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
							UnicornMono.fieldsound.soundContainerArray[0] = soundContainer5;
						}
						this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
						SoundManager.Instance.Play(UnicornMono.fieldsound, base.transform, new SoundParameterBase[]
						{
							this.soundParameterIntensity
						});
						return;
					}
					if (this.realmode == 5)
					{
						this.player.gameObject.AddComponent<BlueMono>();
						this.mode++;
						this.realmode++;
						if (!UnicornMono.fieldsound)
						{
							AudioClip audioClip6 = CR.ArtAsset.LoadAsset<AudioClip>("aqua");
							SoundContainer soundContainer6 = ScriptableObject.CreateInstance<SoundContainer>();
							soundContainer6.audioClip[0] = audioClip6;
							soundContainer6.setting.volumeIntensityEnable = true;
							UnicornMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
							UnicornMono.fieldsound.soundContainerArray[0] = soundContainer6;
						}
						this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
						SoundManager.Instance.Play(UnicornMono.fieldsound, base.transform, new SoundParameterBase[]
						{
							this.soundParameterIntensity
						});
						return;
					}
					if (this.realmode == 6)
					{
						this.player.gameObject.AddComponent<PurpleMono>();
						this.mode++;
						this.realmode++;
						if (!UnicornMono.fieldsound)
						{
							AudioClip audioClip7 = CR.ArtAsset.LoadAsset<AudioClip>("aqua");
							SoundContainer soundContainer7 = ScriptableObject.CreateInstance<SoundContainer>();
							soundContainer7.audioClip[0] = audioClip7;
							soundContainer7.setting.volumeIntensityEnable = true;
							UnicornMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
							UnicornMono.fieldsound.soundContainerArray[0] = soundContainer7;
						}
						this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
						SoundManager.Instance.Play(UnicornMono.fieldsound, base.transform, new SoundParameterBase[]
						{
							this.soundParameterIntensity
						});
						return;
					}
					if (this.realmode == 7)
					{
						this.player.gameObject.AddComponent<PinkMono>();
						this.mode++;
						this.realmode = 0;
						if (!UnicornMono.fieldsound)
						{
							AudioClip audioClip8 = CR.ArtAsset.LoadAsset<AudioClip>("aqua");
							SoundContainer soundContainer8 = ScriptableObject.CreateInstance<SoundContainer>();
							soundContainer8.audioClip[0] = audioClip8;
							soundContainer8.setting.volumeIntensityEnable = true;
							UnicornMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
							UnicornMono.fieldsound.soundContainerArray[0] = soundContainer8;
						}
						this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
						SoundManager.Instance.Play(UnicornMono.fieldsound, base.transform, new SoundParameterBase[]
						{
							this.soundParameterIntensity
						});
						return;
					}
				}
				else
				{
					this.Destroy();
				}
			}
		}

		// Token: 0x040000FA RID: 250
		private readonly float updateDelay = 8f;

		// Token: 0x040000FB RID: 251
		private float startTime;

		// Token: 0x040000FC RID: 252
		public int mode;

		// Token: 0x040000FD RID: 253
		public int realmode;

		// Token: 0x040000FE RID: 254
		internal Player player;

		// Token: 0x040000FF RID: 255
		internal Gun gun;

		// Token: 0x04000100 RID: 256
		internal GunAmmo gunAmmo;

		// Token: 0x04000101 RID: 257
		internal Gravity gravity;

		// Token: 0x04000102 RID: 258
		internal HealthHandler healthHandler;

		// Token: 0x04000103 RID: 259
		internal CharacterData data;

		// Token: 0x04000104 RID: 260
		internal Block block;

		// Token: 0x04000105 RID: 261
		public int numcheck;

		// Token: 0x04000106 RID: 262
		public static SoundEvent fieldsound;

		// Token: 0x04000107 RID: 263
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.4f, 0);
	}
}
