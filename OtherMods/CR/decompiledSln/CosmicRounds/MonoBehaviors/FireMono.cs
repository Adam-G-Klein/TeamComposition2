using System;
using UnboundLib;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200005F RID: 95
	public class FireMono : MonoBehaviour
	{
		// Token: 0x06000254 RID: 596 RVA: 0x00012660 File Offset: 0x00010860
		public void Start()
		{
			this.data = base.GetComponentInParent<CharacterData>();
			this.player = base.GetComponent<Player>();
			this.gun = this.data.weaponHandler.gun;
			this.goon = this.gun.ShootPojectileAction;
			this.gun.ShootPojectileAction = (Action<GameObject>)Delegate.Combine(this.gun.ShootPojectileAction, new Action<GameObject>(this.Attack));
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000126D8 File Offset: 0x000108D8
		private void Attack(GameObject projectile)
		{
			ExtensionMethods.ExecuteAfterFrames(this, 1, delegate()
			{
				projectile.gameObject.AddComponent<FireballMono>();
			});
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00012705 File Offset: 0x00010905
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0001270D File Offset: 0x0001090D
		public void OnDestroy()
		{
			this.gun.ShootPojectileAction = this.goon;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00012720 File Offset: 0x00010920
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Ignite" || this.player.data.currentCards[i].cardName == "Flamethrower" || this.player.data.currentCards[i].cardName == "Sun" || this.player.data.currentCards[i].cardName == "Halo")
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

		// Token: 0x06000259 RID: 601 RVA: 0x0001282C File Offset: 0x00010A2C
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x040002D4 RID: 724
		private readonly float updateDelay = 0.1f;

		// Token: 0x040002D5 RID: 725
		private float startTime;

		// Token: 0x040002D6 RID: 726
		private int numcheck;

		// Token: 0x040002D7 RID: 727
		private CharacterData data;

		// Token: 0x040002D8 RID: 728
		private Player player;

		// Token: 0x040002D9 RID: 729
		private Gun gun;

		// Token: 0x040002DA RID: 730
		private Action<GameObject> goon;
	}
}
