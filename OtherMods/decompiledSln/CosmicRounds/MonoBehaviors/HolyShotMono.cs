using System;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000065 RID: 101
	public class HolyShotMono : MonoBehaviour
	{
		// Token: 0x0600027B RID: 635 RVA: 0x00013C84 File Offset: 0x00011E84
		public void Start()
		{
			this.data = base.GetComponentInParent<CharacterData>();
			this.player = base.GetComponent<Player>();
			this.gun = this.data.weaponHandler.gun;
			this.goon = this.gun.ShootPojectileAction;
			this.gun.ShootPojectileAction = (Action<GameObject>)Delegate.Combine(this.gun.ShootPojectileAction, new Action<GameObject>(this.Attack));
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00013CFC File Offset: 0x00011EFC
		private void Attack(GameObject projectile)
		{
			projectile.gameObject.AddComponent<HolyFireMono>();
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00013D0A File Offset: 0x00011F0A
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00013D12 File Offset: 0x00011F12
		public void OnDestroy()
		{
			this.gun.ShootPojectileAction = this.goon;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00013D28 File Offset: 0x00011F28
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Halo")
					{
						this.numcheck++;
					}
				}
				if (this.numcheck > 0)
				{
					this.ResetTimer();
					return;
				}
				this.Destroy();
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00013DB6 File Offset: 0x00011FB6
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x04000309 RID: 777
		private readonly float updateDelay = 0.1f;

		// Token: 0x0400030A RID: 778
		private float startTime;

		// Token: 0x0400030B RID: 779
		private int numcheck;

		// Token: 0x0400030C RID: 780
		private CharacterData data;

		// Token: 0x0400030D RID: 781
		private Player player;

		// Token: 0x0400030E RID: 782
		private Gun gun;

		// Token: 0x0400030F RID: 783
		private Action<GameObject> goon;
	}
}
