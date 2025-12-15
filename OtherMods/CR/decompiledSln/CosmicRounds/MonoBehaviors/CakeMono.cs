using System;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200003F RID: 63
	public class CakeMono : MonoBehaviour
	{
		// Token: 0x0600017C RID: 380 RVA: 0x0000C484 File Offset: 0x0000A684
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

		// Token: 0x0600017D RID: 381 RVA: 0x0000C4D4 File Offset: 0x0000A6D4
		public void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Cake")
					{
						this.numcheck++;
					}
				}
				if (this.numcheck < 1)
				{
					Object.Destroy(this);
					return;
				}
				if (Time.time >= this.timeOfLastEffect + this.effectCooldown && this.player.data.weaponHandler.gun.isReloading && this.able)
				{
					this.player.transform.gameObject.AddComponent<CakeReloadMono>();
					this.ResetEffectTimer();
					this.able = false;
				}
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000C5C0 File Offset: 0x0000A7C0
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000C5D4 File Offset: 0x0000A7D4
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000C5E1 File Offset: 0x0000A7E1
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x040001C2 RID: 450
		private CharacterData data;

		// Token: 0x040001C3 RID: 451
		private Block block;

		// Token: 0x040001C4 RID: 452
		public Player player;

		// Token: 0x040001C5 RID: 453
		private Gun gun;

		// Token: 0x040001C6 RID: 454
		public bool able;

		// Token: 0x040001C7 RID: 455
		private readonly float updateDelay = 0.1f;

		// Token: 0x040001C8 RID: 456
		private readonly float effectCooldown = 1f;

		// Token: 0x040001C9 RID: 457
		private float timeOfLastEffect;

		// Token: 0x040001CA RID: 458
		private float startTime;

		// Token: 0x040001CB RID: 459
		public int numcheck;
	}
}
