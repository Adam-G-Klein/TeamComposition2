using System;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200008A RID: 138
	public class ReplicateMono : MonoBehaviour
	{
		// Token: 0x06000382 RID: 898 RVA: 0x0001B100 File Offset: 0x00019300
		private void Start()
		{
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.data = base.GetComponent<CharacterData>();
			this.gun = base.GetComponent<Gun>();
			this.ResetEffectTimer();
			this.ResetTimer();
			this.canTrigger = true;
			this.hasTriggered = false;
			this.basic = this.block.BlockAction;
			if (this.block)
			{
				this.gravy = new Action<BlockTrigger.BlockTriggerType>(this.GetDoBlockAction(this.player, this.block).Invoke);
				this.block.BlockAction = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(this.block.BlockAction, this.gravy);
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001B1BE File Offset: 0x000193BE
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001B1C6 File Offset: 0x000193C6
		public void OnDestroy()
		{
			this.block.BlockAction = this.basic;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001B1DC File Offset: 0x000193DC
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Replicate")
					{
						this.numcheck++;
					}
				}
				if (this.numcheck > 0)
				{
					this.repeatblock = this.numcheck;
					this.ResetTimer();
					if (Time.time >= this.timeOfLastEffect + this.effectCooldown)
					{
						this.canTrigger = true;
					}
					if (this.canTrigger)
					{
						if (this.blocknum == 0 && this.player.data.block.IsOnCD())
						{
							this.player.data.block.counter += this.player.data.block.Cooldown();
							this.blocknum++;
							this.ResetEffectTimer();
							this.canTrigger = false;
							if (!ReplicateMono.fieldsound)
							{
								AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("bulk");
								SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
								soundContainer.audioClip[0] = audioClip;
								soundContainer.setting.volumeIntensityEnable = true;
								ReplicateMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
								ReplicateMono.fieldsound.soundContainerArray[0] = soundContainer;
							}
							this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
							SoundManager.Instance.Play(ReplicateMono.fieldsound, base.transform, new SoundParameterBase[]
							{
								this.soundParameterIntensity
							});
							return;
						}
						if (this.blocknum != 0 && this.player.data.block.IsOnCD())
						{
							this.blocknum = 0;
							return;
						}
					}
				}
				else
				{
					Object.Destroy(this);
				}
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0001B3E4 File Offset: 0x000195E4
		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block)
		{
			return delegate(BlockTrigger.BlockTriggerType trigger)
			{
				if (trigger != 1 && this.hasTriggered)
				{
					this.hasTriggered = false;
					this.canTrigger = false;
				}
			};
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0001B3F2 File Offset: 0x000195F2
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0001B406 File Offset: 0x00019606
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x0400047D RID: 1149
		public Block block;

		// Token: 0x0400047E RID: 1150
		public Player player;

		// Token: 0x0400047F RID: 1151
		public CharacterData data;

		// Token: 0x04000480 RID: 1152
		public Gun gun;

		// Token: 0x04000481 RID: 1153
		private Action<BlockTrigger.BlockTriggerType> gravy;

		// Token: 0x04000482 RID: 1154
		private Action<BlockTrigger.BlockTriggerType> basic;

		// Token: 0x04000483 RID: 1155
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000484 RID: 1156
		private readonly float effectCooldown = 4f;

		// Token: 0x04000485 RID: 1157
		private float startTime;

		// Token: 0x04000486 RID: 1158
		private float timeOfLastEffect;

		// Token: 0x04000487 RID: 1159
		private bool canTrigger;

		// Token: 0x04000488 RID: 1160
		private bool hasTriggered;

		// Token: 0x04000489 RID: 1161
		public int numcheck;

		// Token: 0x0400048A RID: 1162
		public int blocknum;

		// Token: 0x0400048B RID: 1163
		public int repeatblock;

		// Token: 0x0400048C RID: 1164
		public static SoundEvent fieldsound;

		// Token: 0x0400048D RID: 1165
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.3f, 0);
	}
}
