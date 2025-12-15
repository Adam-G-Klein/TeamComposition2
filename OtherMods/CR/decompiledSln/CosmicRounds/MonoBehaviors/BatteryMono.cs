using System;
using Sonigon;
using Sonigon.Internal;
using UnboundLib;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000071 RID: 113
	public class BatteryMono : MonoBehaviour
	{
		// Token: 0x060002CE RID: 718 RVA: 0x00015C20 File Offset: 0x00013E20
		public void Start()
		{
			this.data = base.GetComponentInParent<CharacterData>();
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.gun = base.GetComponent<Gun>();
			this.able = false;
			this.ResetTimer();
			this.ResetEffectTimer();
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00015C70 File Offset: 0x00013E70
		public void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Battery")
					{
						this.numcheck++;
					}
				}
				if (this.numcheck < 1)
				{
					Object.Destroy(this);
					return;
				}
				if (Time.time >= this.timeOfLastEffect + this.effectCooldown && this.player.data.weaponHandler.gun.isReloading && !this.able)
				{
					if (!BatteryMono.fieldsound)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("gravity");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						BatteryMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						BatteryMono.fieldsound.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
					SoundManager.Instance.Play(BatteryMono.fieldsound, base.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
					Object.Instantiate<GameObject>(TaserMono.taserVisual, this.player.transform.position, Quaternion.identity);
					Vector2 vector = this.block.transform.position;
					Player[] array = PlayerManager.instance.players.ToArray();
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j].teamID != this.player.teamID && (Vector2.Distance(vector, array[j].transform.position) < this.maxDistance && PlayerManager.instance.CanSeePlayer(this.player.transform.position, array[j]).canSee))
						{
							array[j].transform.GetComponent<StunHandler>();
							array[j].transform.GetComponent<DamageOverTime>();
							NetworkingManager.RPC(typeof(StunMono), "RPCA_StunPlayer", new object[]
							{
								0.75f,
								array[j].playerID
							});
						}
					}
					this.ResetEffectTimer();
					this.able = true;
				}
				if (Time.time >= this.timeOfLastEffect + this.effectCooldown && !this.player.data.weaponHandler.gun.isReloading && this.able)
				{
					if (!BatteryMono.fieldsound)
					{
						AudioClip audioClip2 = CR.ArtAsset.LoadAsset<AudioClip>("gravity");
						SoundContainer soundContainer2 = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer2.audioClip[0] = audioClip2;
						soundContainer2.setting.volumeIntensityEnable = true;
						BatteryMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						BatteryMono.fieldsound.soundContainerArray[0] = soundContainer2;
					}
					this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
					SoundManager.Instance.Play(BatteryMono.fieldsound, base.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
					Object.Instantiate<GameObject>(TaserMono.taserVisual, this.player.transform.position, Quaternion.identity);
					Vector2 vector2 = this.block.transform.position;
					Player[] array2 = PlayerManager.instance.players.ToArray();
					for (int k = 0; k < array2.Length; k++)
					{
						if (array2[k].teamID != this.player.teamID && (Vector2.Distance(vector2, array2[k].transform.position) < this.maxDistance && PlayerManager.instance.CanSeePlayer(this.player.transform.position, array2[k]).canSee))
						{
							array2[k].transform.GetComponent<StunHandler>();
							array2[k].transform.GetComponent<DamageOverTime>();
							NetworkingManager.RPC(typeof(StunMono), "RPCA_StunPlayer", new object[]
							{
								0.6f,
								array2[k].playerID
							});
						}
					}
					this.ResetEffectTimer();
					this.able = false;
				}
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0001613F File Offset: 0x0001433F
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00016153 File Offset: 0x00014353
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00016160 File Offset: 0x00014360
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0400037C RID: 892
		private CharacterData data;

		// Token: 0x0400037D RID: 893
		private Block block;

		// Token: 0x0400037E RID: 894
		public Player player;

		// Token: 0x0400037F RID: 895
		private Gun gun;

		// Token: 0x04000380 RID: 896
		public bool able;

		// Token: 0x04000381 RID: 897
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000382 RID: 898
		private readonly float effectCooldown = 0.5f;

		// Token: 0x04000383 RID: 899
		private float timeOfLastEffect;

		// Token: 0x04000384 RID: 900
		private float startTime;

		// Token: 0x04000385 RID: 901
		public int numcheck;

		// Token: 0x04000386 RID: 902
		public static SoundEvent fieldsound;

		// Token: 0x04000387 RID: 903
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.3f, 0);

		// Token: 0x04000388 RID: 904
		private readonly float maxDistance = 8f;
	}
}
