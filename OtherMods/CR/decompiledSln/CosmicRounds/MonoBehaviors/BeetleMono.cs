using System;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200000B RID: 11
	public class BeetleMono : MonoBehaviour
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002C43 File Offset: 0x00000E43
		private void Start()
		{
			this.data = base.GetComponentInParent<CharacterData>();
			HealthHandler healthHandler = this.data.healthHandler;
			healthHandler.reviveAction = (Action)Delegate.Combine(healthHandler.reviveAction, new Action(this.ResetTimer));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002C7D File Offset: 0x00000E7D
		private void OnDestroy()
		{
			HealthHandler healthHandler = this.data.healthHandler;
			healthHandler.reviveAction = (Action)Delegate.Combine(healthHandler.reviveAction, new Action(this.ResetTimer));
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002CAB File Offset: 0x00000EAB
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002CB4 File Offset: 0x00000EB4
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

		// Token: 0x06000029 RID: 41 RVA: 0x00002D4C File Offset: 0x00000F4C
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Beetle")
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
						this.player.transform.gameObject.AddComponent<BeetleRegenMono>();
					}
				}
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002E23 File Offset: 0x00001023
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x04000018 RID: 24
		internal Player player;

		// Token: 0x04000019 RID: 25
		internal Gun gun;

		// Token: 0x0400001A RID: 26
		internal GunAmmo gunAmmo;

		// Token: 0x0400001B RID: 27
		internal Gravity gravity;

		// Token: 0x0400001C RID: 28
		internal HealthHandler healthHandler;

		// Token: 0x0400001D RID: 29
		internal CharacterData data;

		// Token: 0x0400001E RID: 30
		internal Block block;

		// Token: 0x0400001F RID: 31
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000020 RID: 32
		private float startTime;

		// Token: 0x04000021 RID: 33
		public int numcheck;

		// Token: 0x04000022 RID: 34
		public bool active;
	}
}
