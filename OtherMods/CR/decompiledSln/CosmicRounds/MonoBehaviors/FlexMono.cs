using System;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000014 RID: 20
	public class FlexMono : DealtDamageEffect
	{
		// Token: 0x06000063 RID: 99 RVA: 0x00004979 File Offset: 0x00002B79
		private void Start()
		{
			this.ResetEffectTimer();
			this.ResetTimer();
			this.canTrigger = true;
			this.numcheck = 0;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004995 File Offset: 0x00002B95
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000049A0 File Offset: 0x00002BA0
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
						if (this.player.data.currentCards[i].cardName == "Flex")
						{
							this.numcheck++;
						}
						i++;
					}
				}
				if (this.numcheck > 0)
				{
					if (Time.time >= this.timeOfLastEffect + this.effectCooldown)
					{
						if (!this.canTrigger)
						{
							this.canTrigger = true;
							this.player.data.block.RPCA_DoBlock(false, true, 0, default(Vector3), false);
							this.ResetEffectTimer();
						}
						this.canTrigger = true;
						return;
					}
					this.canTrigger = true;
					return;
				}
				else
				{
					Object.Destroy(this);
				}
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004AA0 File Offset: 0x00002CA0
		public override void DealtDamage(Vector2 damage, bool selfDamage, Player damagedPlayer = null)
		{
			this.player = base.gameObject.GetComponent<Player>();
			this.data = this.player.GetComponent<CharacterData>();
			this.block = this.player.GetComponent<Block>();
			if (this.canTrigger)
			{
				this.canTrigger = false;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004AEF File Offset: 0x00002CEF
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004B03 File Offset: 0x00002D03
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x04000067 RID: 103
		private Player player;

		// Token: 0x04000068 RID: 104
		private CharacterData data;

		// Token: 0x04000069 RID: 105
		private Block block;

		// Token: 0x0400006A RID: 106
		private readonly float updateDelay = 0.1f;

		// Token: 0x0400006B RID: 107
		private readonly float effectCooldown = 3f;

		// Token: 0x0400006C RID: 108
		private float startTime;

		// Token: 0x0400006D RID: 109
		private float timeOfLastEffect;

		// Token: 0x0400006E RID: 110
		private bool canTrigger;

		// Token: 0x0400006F RID: 111
		public int numcheck;
	}
}
