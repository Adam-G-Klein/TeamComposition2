using System;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000079 RID: 121
	public class EnchantMono : MonoBehaviour
	{
		// Token: 0x06000307 RID: 775 RVA: 0x0001822C File Offset: 0x0001642C
		public void Start()
		{
			this.data = base.GetComponentInParent<CharacterData>();
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.gun = base.GetComponent<Gun>();
			this.able = true;
			this.ResetTimer();
			this.ResetEffectTimer();
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0001827C File Offset: 0x0001647C
		public void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Enchant")
					{
						this.numcheck++;
					}
				}
				if (this.numcheck < 1)
				{
					Object.Destroy(this);
					return;
				}
				if (Time.time >= this.timeOfLastEffect + this.effectCooldown && !this.player.data.block.IsOnCD() && !this.player.GetComponent<EnchantCDMono>())
				{
					this.player.transform.gameObject.AddComponent<EnchantCDMono>();
					this.ResetEffectTimer();
				}
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00018366 File Offset: 0x00016566
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0001837A File Offset: 0x0001657A
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00018387 File Offset: 0x00016587
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x040003DF RID: 991
		private CharacterData data;

		// Token: 0x040003E0 RID: 992
		private Block block;

		// Token: 0x040003E1 RID: 993
		public Player player;

		// Token: 0x040003E2 RID: 994
		private Gun gun;

		// Token: 0x040003E3 RID: 995
		public bool able;

		// Token: 0x040003E4 RID: 996
		private readonly float updateDelay = 0.1f;

		// Token: 0x040003E5 RID: 997
		private readonly float effectCooldown;

		// Token: 0x040003E6 RID: 998
		private float timeOfLastEffect;

		// Token: 0x040003E7 RID: 999
		private float startTime;

		// Token: 0x040003E8 RID: 1000
		public int numcheck;
	}
}
