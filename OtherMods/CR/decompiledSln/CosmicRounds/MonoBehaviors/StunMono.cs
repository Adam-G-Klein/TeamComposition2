using System;
using ModsPlus;
using Sonigon;
using Sonigon.Internal;
using UnboundLib;
using UnboundLib.Networking;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000009 RID: 9
	public class StunMono : RayHitEffect
	{
		// Token: 0x0600001C RID: 28 RVA: 0x0000285F File Offset: 0x00000A5F
		private void Start()
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002861 File Offset: 0x00000A61
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000286C File Offset: 0x00000A6C
		public override HasToReturn DoHitEffect(HitInfo hit)
		{
			if (!hit.transform)
			{
				return 1;
			}
			ProjectileHit componentInParent = base.GetComponentInParent<ProjectileHit>();
			StunHandler component = hit.transform.GetComponent<StunHandler>();
			Player component2 = hit.transform.GetComponent<Player>();
			this.rand = new Random();
			if (component2 && component2.data.view.IsMine)
			{
				this.chance = this.rand.Next(1, 101);
				if (componentInParent.damage >= 25f)
				{
					if (component && this.chance <= 25)
					{
						NetworkingManager.RPC(typeof(StunMono), "RPCA_StunPlayer", new object[]
						{
							1f,
							component2.playerID
						});
						if (!StunMono.fieldsound)
						{
							AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("stun");
							SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
							soundContainer.audioClip[0] = audioClip;
							soundContainer.setting.volumeIntensityEnable = true;
							StunMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
							StunMono.fieldsound.soundContainerArray[0] = soundContainer;
						}
						this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
						SoundManager.Instance.Play(StunMono.fieldsound, base.transform, new SoundParameterBase[]
						{
							this.soundParameterIntensity
						});
					}
				}
				else if (component && (float)this.chance <= 25f && component2.data.view.IsMine)
				{
					NetworkingManager.RPC(typeof(StunMono), "RPCA_StunPlayer", new object[]
					{
						0.7f,
						component2.playerID
					});
					if (!StunMono.fieldsound)
					{
						AudioClip audioClip2 = CR.ArtAsset.LoadAsset<AudioClip>("stun");
						SoundContainer soundContainer2 = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer2.audioClip[0] = audioClip2;
						soundContainer2.setting.volumeIntensityEnable = true;
						StunMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						StunMono.fieldsound.soundContainerArray[0] = soundContainer2;
					}
					this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
					SoundManager.Instance.Play(StunMono.fieldsound, base.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
				}
			}
			return 1;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002B0D File Offset: 0x00000D0D
		[UnboundRPC]
		private static void RPCA_StunPlayer(float duration, int playerID)
		{
			ExtensionMethods.GetPlayerWithID(PlayerManager.instance, playerID).data.stunHandler.AddStun(duration);
		}

		// Token: 0x04000010 RID: 16
		private Random rand;

		// Token: 0x04000011 RID: 17
		private int chance;

		// Token: 0x04000012 RID: 18
		public static SoundEvent fieldsound;

		// Token: 0x04000013 RID: 19
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.8f, 0);
	}
}
