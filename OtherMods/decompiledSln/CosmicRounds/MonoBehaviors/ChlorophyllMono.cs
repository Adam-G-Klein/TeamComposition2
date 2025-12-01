using System;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000041 RID: 65
	public class ChlorophyllMono : MonoBehaviour
	{
		// Token: 0x06000189 RID: 393 RVA: 0x0000C888 File Offset: 0x0000AA88
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

		// Token: 0x0600018A RID: 394 RVA: 0x0000C8D8 File Offset: 0x0000AAD8
		public void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Chlorophyll")
					{
						this.numcheck++;
					}
				}
				if (this.numcheck < 1)
				{
					Object.Destroy(this);
					return;
				}
				if (Time.time >= this.timeOfLastEffect + this.effectCooldown && this.player.data.weaponHandler.gun.isReloading && this.stacks < this.maxstacks)
				{
					this.player.transform.gameObject.AddComponent<ChlorophyllReloadMono>();
					this.ResetEffectTimer();
					this.stacks++;
				}
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000C9D1 File Offset: 0x0000ABD1
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000C9E5 File Offset: 0x0000ABE5
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000C9F2 File Offset: 0x0000ABF2
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x040001D3 RID: 467
		private CharacterData data;

		// Token: 0x040001D4 RID: 468
		private Block block;

		// Token: 0x040001D5 RID: 469
		public Player player;

		// Token: 0x040001D6 RID: 470
		private Gun gun;

		// Token: 0x040001D7 RID: 471
		public bool able;

		// Token: 0x040001D8 RID: 472
		private readonly float updateDelay = 0.1f;

		// Token: 0x040001D9 RID: 473
		private readonly float effectCooldown = 1f;

		// Token: 0x040001DA RID: 474
		private readonly int maxstacks = 5;

		// Token: 0x040001DB RID: 475
		public int stacks;

		// Token: 0x040001DC RID: 476
		private float timeOfLastEffect;

		// Token: 0x040001DD RID: 477
		private float startTime;

		// Token: 0x040001DE RID: 478
		public int numcheck;
	}
}
