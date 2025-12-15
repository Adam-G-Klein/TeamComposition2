using System;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000073 RID: 115
	public class DingMono : MonoBehaviour
	{
		// Token: 0x060002DC RID: 732 RVA: 0x00016A14 File Offset: 0x00014C14
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

		// Token: 0x060002DD RID: 733 RVA: 0x00016A64 File Offset: 0x00014C64
		public void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Ding")
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
					if (!DingMono.fieldsound)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("critical");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						DingMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						DingMono.fieldsound.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx * CR.globalVolMute.Value;
					SoundManager.Instance.Play(DingMono.fieldsound, base.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
					Object.Instantiate<GameObject>(DischargeMono.dischargeVisual, this.player.transform.position, Quaternion.identity);
					Vector2 vector = this.block.transform.position;
					Player[] array = PlayerManager.instance.players.ToArray();
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j].teamID != this.player.teamID && (Vector2.Distance(vector, array[j].transform.position) < this.maxDistance && PlayerManager.instance.CanSeePlayer(this.player.transform.position, array[j]).canSee))
						{
							array[j].gameObject.GetComponent<CharacterData>();
							Player component = array[j].gameObject.GetComponent<Player>();
							array[j].gameObject.GetComponent<HealthHandler>();
							component.data.healthHandler.DoDamage(this.player.data.weaponHandler.gun.damage * 55f * 0.5f * Vector2.down, component.transform.position, new Color(1f, 1f, 0f, 1f), this.player.data.weaponHandler.gameObject, this.player, false, true, true);
						}
					}
					this.ResetEffectTimer();
					this.able = true;
				}
				if (Time.time >= this.timeOfLastEffect + this.effectCooldown && !this.player.data.weaponHandler.gun.isReloading && this.able)
				{
					if (!DingMono.fieldsound)
					{
						AudioClip audioClip2 = CR.ArtAsset.LoadAsset<AudioClip>("critical");
						SoundContainer soundContainer2 = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer2.audioClip[0] = audioClip2;
						soundContainer2.setting.volumeIntensityEnable = true;
						DingMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						DingMono.fieldsound.soundContainerArray[0] = soundContainer2;
					}
					this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx * CR.globalVolMute.Value;
					SoundManager.Instance.Play(DingMono.fieldsound, base.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
					Object.Instantiate<GameObject>(DischargeMono.dischargeVisual, this.player.transform.position, Quaternion.identity);
					Vector2 vector2 = this.block.transform.position;
					Player[] array2 = PlayerManager.instance.players.ToArray();
					for (int k = 0; k < array2.Length; k++)
					{
						if (array2[k].teamID != this.player.teamID && (Vector2.Distance(vector2, array2[k].transform.position) < this.maxDistance && PlayerManager.instance.CanSeePlayer(this.player.transform.position, array2[k]).canSee))
						{
							array2[k].gameObject.GetComponent<CharacterData>();
							Player component2 = array2[k].gameObject.GetComponent<Player>();
							array2[k].gameObject.GetComponent<HealthHandler>();
							component2.data.healthHandler.DoDamage(this.player.data.weaponHandler.gun.damage * 55f * 0.5f * Vector2.down, component2.transform.position, new Color(1f, 1f, 0f, 1f), this.player.data.weaponHandler.gameObject, this.player, false, true, true);
						}
					}
					this.ResetEffectTimer();
					this.able = false;
				}
			}
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00016FEF File Offset: 0x000151EF
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00017003 File Offset: 0x00015203
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00017010 File Offset: 0x00015210
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x04000397 RID: 919
		private CharacterData data;

		// Token: 0x04000398 RID: 920
		private Block block;

		// Token: 0x04000399 RID: 921
		public Player player;

		// Token: 0x0400039A RID: 922
		private Gun gun;

		// Token: 0x0400039B RID: 923
		public bool able;

		// Token: 0x0400039C RID: 924
		private readonly float updateDelay = 0.1f;

		// Token: 0x0400039D RID: 925
		private readonly float effectCooldown;

		// Token: 0x0400039E RID: 926
		private float timeOfLastEffect;

		// Token: 0x0400039F RID: 927
		private float startTime;

		// Token: 0x040003A0 RID: 928
		public int numcheck;

		// Token: 0x040003A1 RID: 929
		public static SoundEvent fieldsound;

		// Token: 0x040003A2 RID: 930
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.2f, 0);

		// Token: 0x040003A3 RID: 931
		private readonly float maxDistance = 10f;
	}
}
