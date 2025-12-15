using System;
using UnboundLib;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200007B RID: 123
	public class CrowSpriteMono : MonoBehaviour
	{
		// Token: 0x06000314 RID: 788 RVA: 0x00018564 File Offset: 0x00016764
		public void Start()
		{
			this.data = base.GetComponentInParent<CharacterData>();
			this.player = base.GetComponent<Player>();
			this.gun = this.data.weaponHandler.gun;
			this.goon = this.gun.ShootPojectileAction;
			this.gun.ShootPojectileAction = (Action<GameObject>)Delegate.Combine(this.gun.ShootPojectileAction, new Action<GameObject>(this.Attack));
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000185DC File Offset: 0x000167DC
		private void Attack(GameObject projectile)
		{
			if (CR.crSpecialVFX.Value)
			{
				ExtensionMethods.ExecuteAfterFrames(this, 1, delegate()
				{
					ProjectileHit component = projectile.GetComponent<ProjectileHit>();
					component.GetComponentInParent<MoveTransform>();
					GameObject gameObject = Object.Instantiate<GameObject>(CR.ArtAsset.LoadAsset<GameObject>("CrowSprite"), projectile.transform);
					gameObject.transform.up = projectile.transform.right;
					gameObject.transform.localScale *= 0.2f;
					if (component.projectileColor == Color.black || component.projectileColor == Color.clear)
					{
						gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
						return;
					}
					gameObject.GetComponent<SpriteRenderer>().color = component.projectileColor;
				});
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00018615 File Offset: 0x00016815
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0001861D File Offset: 0x0001681D
		public void OnDestroy()
		{
			this.gun.ShootPojectileAction = this.goon;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00018630 File Offset: 0x00016830
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Crow")
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

		// Token: 0x06000319 RID: 793 RVA: 0x000186BE File Offset: 0x000168BE
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x040003F0 RID: 1008
		private readonly float updateDelay = 0.1f;

		// Token: 0x040003F1 RID: 1009
		private float startTime;

		// Token: 0x040003F2 RID: 1010
		private int numcheck;

		// Token: 0x040003F3 RID: 1011
		private CharacterData data;

		// Token: 0x040003F4 RID: 1012
		private Player player;

		// Token: 0x040003F5 RID: 1013
		private Gun gun;

		// Token: 0x040003F6 RID: 1014
		private Action<GameObject> goon;
	}
}
