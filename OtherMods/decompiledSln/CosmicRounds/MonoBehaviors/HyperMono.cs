using System;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000067 RID: 103
	public class HyperMono : MonoBehaviour
	{
		// Token: 0x06000286 RID: 646 RVA: 0x00013EE7 File Offset: 0x000120E7
		private void Start()
		{
			this.data = base.GetComponentInParent<CharacterData>();
			HealthHandler healthHandler = this.data.healthHandler;
			healthHandler.reviveAction = (Action)Delegate.Combine(healthHandler.reviveAction, new Action(this.ResetTimer));
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00013F21 File Offset: 0x00012121
		private void OnDestroy()
		{
			HealthHandler healthHandler = this.data.healthHandler;
			healthHandler.reviveAction = (Action)Delegate.Combine(healthHandler.reviveAction, new Action(this.ResetTimer));
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00013F4F File Offset: 0x0001214F
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x00013F58 File Offset: 0x00012158
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

		// Token: 0x0600028A RID: 650 RVA: 0x00013FF0 File Offset: 0x000121F0
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Hyper Sonic")
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
						this.player.transform.gameObject.AddComponent<HyperRegenMono>();
					}
				}
			}
		}

		// Token: 0x0600028B RID: 651 RVA: 0x000140C7 File Offset: 0x000122C7
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x04000313 RID: 787
		internal Player player;

		// Token: 0x04000314 RID: 788
		internal Gun gun;

		// Token: 0x04000315 RID: 789
		internal GunAmmo gunAmmo;

		// Token: 0x04000316 RID: 790
		internal Gravity gravity;

		// Token: 0x04000317 RID: 791
		internal HealthHandler healthHandler;

		// Token: 0x04000318 RID: 792
		internal CharacterData data;

		// Token: 0x04000319 RID: 793
		internal Block block;

		// Token: 0x0400031A RID: 794
		private readonly float updateDelay = 0.1f;

		// Token: 0x0400031B RID: 795
		private float startTime;

		// Token: 0x0400031C RID: 796
		public int numcheck;

		// Token: 0x0400031D RID: 797
		public bool active;
	}
}
