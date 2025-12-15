using System;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200005E RID: 94
	public class BeeShotMono : MonoBehaviour
	{
		// Token: 0x0600024D RID: 589 RVA: 0x00012504 File Offset: 0x00010704
		public void Start()
		{
			this.data = base.GetComponentInParent<CharacterData>();
			this.player = base.GetComponent<Player>();
			this.gun = this.data.weaponHandler.gun;
			this.goon = this.gun.ShootPojectileAction;
			this.gun.ShootPojectileAction = (Action<GameObject>)Delegate.Combine(this.gun.ShootPojectileAction, new Action<GameObject>(this.Attack));
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0001257C File Offset: 0x0001077C
		private void Attack(GameObject projectile)
		{
			projectile.gameObject.AddComponent<BeeSpriteMono>();
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0001258A File Offset: 0x0001078A
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00012592 File Offset: 0x00010792
		public void OnDestroy()
		{
			this.gun.ShootPojectileAction = this.goon;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000125A8 File Offset: 0x000107A8
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Hive")
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

		// Token: 0x06000252 RID: 594 RVA: 0x00012636 File Offset: 0x00010836
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x040002CD RID: 717
		private readonly float updateDelay = 0.1f;

		// Token: 0x040002CE RID: 718
		private float startTime;

		// Token: 0x040002CF RID: 719
		private int numcheck;

		// Token: 0x040002D0 RID: 720
		private CharacterData data;

		// Token: 0x040002D1 RID: 721
		private Player player;

		// Token: 0x040002D2 RID: 722
		private Gun gun;

		// Token: 0x040002D3 RID: 723
		private Action<GameObject> goon;
	}
}
