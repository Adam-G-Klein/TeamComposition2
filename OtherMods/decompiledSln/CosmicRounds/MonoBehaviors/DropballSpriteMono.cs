using System;
using UnboundLib;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200007D RID: 125
	public class DropballSpriteMono : MonoBehaviour
	{
		// Token: 0x06000322 RID: 802 RVA: 0x0001886C File Offset: 0x00016A6C
		public void Start()
		{
			this.data = base.GetComponentInParent<CharacterData>();
			this.player = base.GetComponent<Player>();
			this.gun = this.data.weaponHandler.gun;
			this.goon = this.gun.ShootPojectileAction;
			this.gun.ShootPojectileAction = (Action<GameObject>)Delegate.Combine(this.gun.ShootPojectileAction, new Action<GameObject>(this.Attack));
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000188E4 File Offset: 0x00016AE4
		private void Attack(GameObject projectile)
		{
			if (CR.crSpecialVFX.Value)
			{
				ExtensionMethods.ExecuteAfterFrames(this, 1, delegate()
				{
					ProjectileHit component = projectile.GetComponent<ProjectileHit>();
					component.GetComponentInParent<MoveTransform>();
					GameObject gameObject = Object.Instantiate<GameObject>(CR.ArtAsset.LoadAsset<GameObject>("DropballSprite"), projectile.transform);
					gameObject.transform.up = -projectile.transform.forward;
					gameObject.transform.localScale *= 0.2f;
					if (component.projectileColor == Color.black || component.projectileColor == Color.clear)
					{
						gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
						return;
					}
					gameObject.GetComponent<SpriteRenderer>().color = component.projectileColor;
				});
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0001891D File Offset: 0x00016B1D
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00018925 File Offset: 0x00016B25
		public void OnDestroy()
		{
			this.gun.ShootPojectileAction = this.goon;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00018938 File Offset: 0x00016B38
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Dropshot")
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

		// Token: 0x06000327 RID: 807 RVA: 0x000189C6 File Offset: 0x00016BC6
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x040003FE RID: 1022
		private readonly float updateDelay = 0.1f;

		// Token: 0x040003FF RID: 1023
		private float startTime;

		// Token: 0x04000400 RID: 1024
		private int numcheck;

		// Token: 0x04000401 RID: 1025
		private CharacterData data;

		// Token: 0x04000402 RID: 1026
		private Player player;

		// Token: 0x04000403 RID: 1027
		private Gun gun;

		// Token: 0x04000404 RID: 1028
		private Action<GameObject> goon;
	}
}
