using System;
using UnboundLib;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200005B RID: 91
	public class PumpkinMono : MonoBehaviour
	{
		// Token: 0x06000239 RID: 569 RVA: 0x00012024 File Offset: 0x00010224
		public void Start()
		{
			this.data = base.GetComponentInParent<CharacterData>();
			this.player = base.GetComponent<Player>();
			this.gun = this.data.weaponHandler.gun;
			this.goon = this.gun.ShootPojectileAction;
			this.gun.ShootPojectileAction = (Action<GameObject>)Delegate.Combine(this.gun.ShootPojectileAction, new Action<GameObject>(this.Attack));
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0001209C File Offset: 0x0001029C
		private void Attack(GameObject projectile)
		{
			if (CR.crSpecialVFX.Value)
			{
				ExtensionMethods.ExecuteAfterFrames(this, 1, delegate()
				{
					ProjectileHit component = projectile.GetComponent<ProjectileHit>();
					component.GetComponentInParent<MoveTransform>();
					GameObject gameObject = Object.Instantiate<GameObject>(CR.ArtAsset.LoadAsset<GameObject>("jackolantern"), projectile.transform);
					gameObject.transform.up = -projectile.transform.forward;
					gameObject.transform.localScale *= 0.8f;
					if (component.projectileColor == Color.black || component.projectileColor == Color.clear)
					{
						gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
						return;
					}
					gameObject.GetComponent<SpriteRenderer>().color = component.projectileColor;
				});
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000120D5 File Offset: 0x000102D5
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000120DD File Offset: 0x000102DD
		public void OnDestroy()
		{
			this.gun.ShootPojectileAction = this.goon;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000120F0 File Offset: 0x000102F0
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Jack-O-Lantern")
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

		// Token: 0x0600023E RID: 574 RVA: 0x0001217E File Offset: 0x0001037E
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x040002B9 RID: 697
		private readonly float updateDelay = 0.1f;

		// Token: 0x040002BA RID: 698
		private float startTime;

		// Token: 0x040002BB RID: 699
		private int numcheck;

		// Token: 0x040002BC RID: 700
		private CharacterData data;

		// Token: 0x040002BD RID: 701
		private Player player;

		// Token: 0x040002BE RID: 702
		private Gun gun;

		// Token: 0x040002BF RID: 703
		private Action<GameObject> goon;
	}
}
