using System;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000013 RID: 19
	public class HolsterMono : MonoBehaviour
	{
		// Token: 0x0600005B RID: 91 RVA: 0x000046CC File Offset: 0x000028CC
		private void Awake()
		{
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.data = base.GetComponent<CharacterData>();
			this.gun = base.GetComponent<Gun>();
			this.basic = this.block.BlockAction;
			this.numcheck = 0;
			if (this.block)
			{
				this.handgun = new Action<BlockTrigger.BlockTriggerType>(this.GetDoBlockAction(this.player, this.block, this.data).Invoke);
				this.block.BlockAction = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(this.block.BlockAction, this.handgun);
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004780 File Offset: 0x00002980
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Holster")
					{
						this.numcheck++;
					}
				}
				if (this.numcheck > 0)
				{
					this.ResetTimer();
					return;
				}
				Object.Destroy(this);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000480E File Offset: 0x00002A0E
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004822 File Offset: 0x00002A22
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000482A File Offset: 0x00002A2A
		public void OnDestroy()
		{
			this.block.BlockAction = this.basic;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000483D File Offset: 0x00002A3D
		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block, CharacterData data)
		{
			return delegate(BlockTrigger.BlockTriggerType trigger)
			{
				if (trigger != 1)
				{
					this.player.data.weaponHandler.gun.Attack(1E-05f, true, 2f, 1f, false);
					foreach (MitosisMono mitosisMono in this.player.GetComponents<MitosisMono>())
					{
						if (mitosisMono != null)
						{
							mitosisMono.Failsafe = false;
							mitosisMono.Active = false;
						}
					}
					foreach (HiveMono hiveMono in this.player.GetComponents<HiveMono>())
					{
						if (hiveMono != null)
						{
							hiveMono.Failsafe = false;
							hiveMono.Active = false;
						}
					}
					foreach (MitosisBlockMono mitosisBlockMono in this.player.GetComponents<MitosisBlockMono>())
					{
						if (mitosisBlockMono != null)
						{
							mitosisBlockMono.Destroy();
						}
					}
					foreach (HiveBlockMono hiveBlockMono in this.player.GetComponents<HiveBlockMono>())
					{
						if (hiveBlockMono != null)
						{
							hiveBlockMono.Destroy();
						}
					}
				}
			};
		}

		// Token: 0x0400005E RID: 94
		public Block block;

		// Token: 0x0400005F RID: 95
		public Player player;

		// Token: 0x04000060 RID: 96
		public CharacterData data;

		// Token: 0x04000061 RID: 97
		public Gun gun;

		// Token: 0x04000062 RID: 98
		private Action<BlockTrigger.BlockTriggerType> handgun;

		// Token: 0x04000063 RID: 99
		private Action<BlockTrigger.BlockTriggerType> basic;

		// Token: 0x04000064 RID: 100
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000065 RID: 101
		private float startTime;

		// Token: 0x04000066 RID: 102
		public int numcheck;
	}
}
