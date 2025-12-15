using System;
using Sonigon;
using Sonigon.Internal;
using UnboundLib;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200003E RID: 62
	public class Quasar : MonoBehaviour
	{
		// Token: 0x06000179 RID: 377 RVA: 0x0000C07C File Offset: 0x0000A27C
		public void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				Vector2 vector = base.gameObject.transform.position;
				Player[] array = PlayerManager.instance.players.ToArray();
				for (int i = 0; i < array.Length; i++)
				{
					if (Vector2.Distance(vector, array[i].transform.position) <= 5.5f && array[i].teamID != this.player.teamID)
					{
						HealthHandler component = array[i].gameObject.GetComponent<HealthHandler>();
						CharacterData component2 = array[i].gameObject.GetComponent<CharacterData>();
						array[i].gameObject.GetComponent<StunHandler>();
						array[i].transform.position += Vector3.ClampMagnitude(base.transform.position - array[i].transform.position, 0.5f) * 1.2f;
						float num = component2.maxHealth * 0.025f;
						NetworkingManager.RPC(typeof(StunMono), "RPCA_StunPlayer", new object[]
						{
							0.1f,
							array[i].playerID
						});
						component.TakeDamage(num * Vector2.down, array[i].transform.position, this.player.data.weaponHandler.gameObject, this.player, true, true);
						component.TakeForce(Vector2.MoveTowards(array[i].transform.position, vector, 25f), 1, false, true, 0.3f);
						Object.Instantiate<GameObject>(QuasarMono.blackholeVisual, base.gameObject.transform.position, Quaternion.identity);
					}
				}
				this.counter += 0.1f;
				if ((double)this.counter >= 1.2)
				{
					Object.Instantiate<GameObject>(QuasarMono.quasarVisual, base.gameObject.transform.position, Quaternion.identity);
					if (!Quasar.fieldsound2)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("quasar");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						Quasar.fieldsound2 = ScriptableObject.CreateInstance<SoundEvent>();
						Quasar.fieldsound2.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity2.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
					SoundManager.Instance.Play(Quasar.fieldsound2, base.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity2
					});
					SoundManager.Instance.Stop(Quasar.fieldsound2, base.transform, true);
				}
				Object.Instantiate<GameObject>(QuasarMono.blackholeVisual2, base.gameObject.transform.position, Quaternion.identity);
				if (!Quasar.fieldsound)
				{
					AudioClip audioClip2 = CR.ArtAsset.LoadAsset<AudioClip>("blackhole2");
					SoundContainer soundContainer2 = ScriptableObject.CreateInstance<SoundContainer>();
					soundContainer2.audioClip[0] = audioClip2;
					soundContainer2.setting.volumeIntensityEnable = true;
					Quasar.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
					Quasar.fieldsound.soundContainerArray[0] = soundContainer2;
				}
				this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
				SoundManager.Instance.Play(Quasar.fieldsound, base.transform, new SoundParameterBase[]
				{
					this.soundParameterIntensity
				});
				this.ResetTimer();
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000C440 File Offset: 0x0000A640
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x040001BA RID: 442
		private readonly float updateDelay = 0.35f;

		// Token: 0x040001BB RID: 443
		private float startTime;

		// Token: 0x040001BC RID: 444
		private float counter;

		// Token: 0x040001BD RID: 445
		public Player player;

		// Token: 0x040001BE RID: 446
		public static SoundEvent fieldsound;

		// Token: 0x040001BF RID: 447
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.6f, 0);

		// Token: 0x040001C0 RID: 448
		public static SoundEvent fieldsound2;

		// Token: 0x040001C1 RID: 449
		private SoundParameterIntensity soundParameterIntensity2 = new SoundParameterIntensity(0.3f, 0);
	}
}
