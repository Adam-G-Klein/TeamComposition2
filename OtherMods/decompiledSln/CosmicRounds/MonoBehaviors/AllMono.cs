using System;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000068 RID: 104
	public class AllMono : MonoBehaviour
	{
		// Token: 0x0600028D RID: 653 RVA: 0x000140EE File Offset: 0x000122EE
		private void Start()
		{
			this.data = base.GetComponentInParent<CharacterData>();
			HealthHandler healthHandler = this.data.healthHandler;
			healthHandler.reviveAction = (Action)Delegate.Combine(healthHandler.reviveAction, new Action(this.ResetTimer));
		}

		// Token: 0x0600028E RID: 654 RVA: 0x00014128 File Offset: 0x00012328
		private void OnDestroy()
		{
			HealthHandler healthHandler = this.data.healthHandler;
			healthHandler.reviveAction = (Action)Delegate.Combine(healthHandler.reviveAction, new Action(this.ResetTimer));
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00014156 File Offset: 0x00012356
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00014160 File Offset: 0x00012360
		public void Awake()
		{
			this.player = base.gameObject.GetComponent<Player>();
			this.gun = this.player.GetComponent<Holding>().holdable.GetComponent<Gun>();
			this.data = this.player.GetComponent<CharacterData>();
			this.healthHandler = this.player.GetComponent<HealthHandler>();
			this.gravity = this.player.GetComponent<Gravity>();
			this.block = this.player.GetComponent<Block>();
			this.gunAmmo = this.gun.GetComponentInChildren<GunAmmo>();
			this.numcheck = 0;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x000141F8 File Offset: 0x000123F8
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "ALL")
					{
						this.numcheck++;
					}
				}
				if (this.numcheck > 0)
				{
					this.ResetTimer();
					if (this.player.data.health < this.player.data.maxHealth && !this.active)
					{
						this.active = true;
						this.player.transform.gameObject.AddComponent<AllRegenMono>();
					}
				}
			}
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000142CF File Offset: 0x000124CF
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x0400031E RID: 798
		internal Player player;

		// Token: 0x0400031F RID: 799
		internal Gun gun;

		// Token: 0x04000320 RID: 800
		internal GunAmmo gunAmmo;

		// Token: 0x04000321 RID: 801
		internal Gravity gravity;

		// Token: 0x04000322 RID: 802
		internal HealthHandler healthHandler;

		// Token: 0x04000323 RID: 803
		internal CharacterData data;

		// Token: 0x04000324 RID: 804
		internal Block block;

		// Token: 0x04000325 RID: 805
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000326 RID: 806
		private float startTime;

		// Token: 0x04000327 RID: 807
		public int numcheck;

		// Token: 0x04000328 RID: 808
		public bool active;
	}
}
