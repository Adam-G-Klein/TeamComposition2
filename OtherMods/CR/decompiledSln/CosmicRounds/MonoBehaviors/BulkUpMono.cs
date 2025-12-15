using System;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000075 RID: 117
	public class BulkUpMono : MonoBehaviour
	{
		// Token: 0x060002EA RID: 746 RVA: 0x0001787C File Offset: 0x00015A7C
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

		// Token: 0x060002EB RID: 747 RVA: 0x000178CC File Offset: 0x00015ACC
		public void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Bulk Up")
					{
						this.numcheck++;
					}
				}
				if (this.numcheck < 1)
				{
					Object.Destroy(this);
					return;
				}
				if (Time.time >= this.timeOfLastEffect + this.effectCooldown && this.player.data.block.IsOnCD())
				{
					if (!BulkUpMono.fieldsound)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("bulk");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						BulkUpMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						BulkUpMono.fieldsound.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx * CR.globalVolMute.Value;
					SoundManager.Instance.Play(BulkUpMono.fieldsound, base.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
					this.player.data.block.RPCA_DoBlock(false, true, 0, default(Vector3), true);
					this.ResetEffectTimer();
				}
			}
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00017A5A File Offset: 0x00015C5A
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00017A6E File Offset: 0x00015C6E
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00017A7B File Offset: 0x00015C7B
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x040003B2 RID: 946
		private CharacterData data;

		// Token: 0x040003B3 RID: 947
		private Block block;

		// Token: 0x040003B4 RID: 948
		public Player player;

		// Token: 0x040003B5 RID: 949
		private Gun gun;

		// Token: 0x040003B6 RID: 950
		public bool able;

		// Token: 0x040003B7 RID: 951
		private readonly float updateDelay = 0.1f;

		// Token: 0x040003B8 RID: 952
		private readonly float effectCooldown = 1f;

		// Token: 0x040003B9 RID: 953
		private float timeOfLastEffect;

		// Token: 0x040003BA RID: 954
		private float startTime;

		// Token: 0x040003BB RID: 955
		public int numcheck;

		// Token: 0x040003BC RID: 956
		public static SoundEvent fieldsound;

		// Token: 0x040003BD RID: 957
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.9f, 0);
	}
}
