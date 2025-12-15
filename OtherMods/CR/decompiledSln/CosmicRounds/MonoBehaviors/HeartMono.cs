using System;
using UnboundLib;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200005C RID: 92
	public class HeartMono : MonoBehaviour
	{
		// Token: 0x06000240 RID: 576 RVA: 0x000121A8 File Offset: 0x000103A8
		public void Start()
		{
			this.data = base.GetComponentInParent<CharacterData>();
			this.player = base.GetComponent<Player>();
			this.gun = this.data.weaponHandler.gun;
			this.goon = this.gun.ShootPojectileAction;
			this.gun.ShootPojectileAction = (Action<GameObject>)Delegate.Combine(this.gun.ShootPojectileAction, new Action<GameObject>(this.Attack));
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00012220 File Offset: 0x00010420
		private void Attack(GameObject projectile)
		{
			if (CR.crSpecialVFX.Value)
			{
				ExtensionMethods.ExecuteAfterFrames(this, 1, delegate()
				{
					ProjectileHit component = projectile.GetComponent<ProjectileHit>();
					component.GetComponentInParent<MoveTransform>();
					GameObject gameObject = Object.Instantiate<GameObject>(CR.ArtAsset.LoadAsset<GameObject>("Heart"), projectile.transform);
					gameObject.transform.up = -projectile.transform.forward;
					gameObject.transform.localScale *= 0.2f;
					if (component.projectileColor == Color.black || component.projectileColor == Color.clear)
					{
						gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0.5f, 0.5f, 1f);
						return;
					}
					gameObject.GetComponent<SpriteRenderer>().color = component.projectileColor;
				});
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00012259 File Offset: 0x00010459
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00012261 File Offset: 0x00010461
		public void OnDestroy()
		{
			this.gun.ShootPojectileAction = this.goon;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00012274 File Offset: 0x00010474
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Heart Thump")
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

		// Token: 0x06000245 RID: 581 RVA: 0x00012302 File Offset: 0x00010502
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x040002C0 RID: 704
		private readonly float updateDelay = 0.1f;

		// Token: 0x040002C1 RID: 705
		private float startTime;

		// Token: 0x040002C2 RID: 706
		private int numcheck;

		// Token: 0x040002C3 RID: 707
		private CharacterData data;

		// Token: 0x040002C4 RID: 708
		private Player player;

		// Token: 0x040002C5 RID: 709
		private Gun gun;

		// Token: 0x040002C6 RID: 710
		private Action<GameObject> goon;
	}
}
