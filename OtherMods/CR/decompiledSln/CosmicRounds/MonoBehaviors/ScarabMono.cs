using System;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000087 RID: 135
	public class ScarabMono : MonoBehaviour
	{
		// Token: 0x0600036C RID: 876 RVA: 0x0001A51F File Offset: 0x0001871F
		private void Start()
		{
			this.data = base.GetComponentInParent<CharacterData>();
			HealthHandler healthHandler = this.data.healthHandler;
			healthHandler.reviveAction = (Action)Delegate.Combine(healthHandler.reviveAction, new Action(this.ResetTimer));
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001A559 File Offset: 0x00018759
		private void OnDestroy()
		{
			HealthHandler healthHandler = this.data.healthHandler;
			healthHandler.reviveAction = (Action)Delegate.Combine(healthHandler.reviveAction, new Action(this.ResetTimer));
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0001A587 File Offset: 0x00018787
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0001A590 File Offset: 0x00018790
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

		// Token: 0x06000370 RID: 880 RVA: 0x0001A628 File Offset: 0x00018828
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Scarab")
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
						this.player.transform.gameObject.AddComponent<ScarabRegenMono>();
					}
				}
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001A6FF File Offset: 0x000188FF
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x04000467 RID: 1127
		internal Player player;

		// Token: 0x04000468 RID: 1128
		internal Gun gun;

		// Token: 0x04000469 RID: 1129
		internal GunAmmo gunAmmo;

		// Token: 0x0400046A RID: 1130
		internal Gravity gravity;

		// Token: 0x0400046B RID: 1131
		internal HealthHandler healthHandler;

		// Token: 0x0400046C RID: 1132
		internal CharacterData data;

		// Token: 0x0400046D RID: 1133
		internal Block block;

		// Token: 0x0400046E RID: 1134
		private readonly float updateDelay = 0.1f;

		// Token: 0x0400046F RID: 1135
		private float startTime;

		// Token: 0x04000470 RID: 1136
		public int numcheck;

		// Token: 0x04000471 RID: 1137
		public bool active;
	}
}
