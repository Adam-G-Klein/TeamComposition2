using System;
using UnboundLib;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000059 RID: 89
	public class StarMono : MonoBehaviour
	{
		// Token: 0x0600022E RID: 558 RVA: 0x00011E00 File Offset: 0x00010000
		public void Start()
		{
			this.data = base.GetComponentInParent<CharacterData>();
			this.player = base.GetComponent<Player>();
			this.gun = this.data.weaponHandler.gun;
			this.goon = this.gun.ShootPojectileAction;
			this.gun.ShootPojectileAction = (Action<GameObject>)Delegate.Combine(this.gun.ShootPojectileAction, new Action<GameObject>(this.Attack));
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00011E78 File Offset: 0x00010078
		private void Attack(GameObject projectile)
		{
			if (CR.crSpecialVFX.Value)
			{
				ExtensionMethods.ExecuteAfterFrames(this, 1, delegate()
				{
					ProjectileHit component = projectile.GetComponent<ProjectileHit>();
					MoveTransform componentInParent = component.GetComponentInParent<MoveTransform>();
					GameObject gameObject = Object.Instantiate<GameObject>(CR.ArtAsset.LoadAsset<GameObject>("StarSprite"), projectile.transform);
					gameObject.transform.up = projectile.transform.forward;
					ConstantRot constantRot = gameObject.AddComponent<ConstantRot>();
					constantRot.speed = 240f;
					if (componentInParent.velocity.x >= 0f)
					{
						constantRot.clockwise = false;
					}
					else
					{
						constantRot.clockwise = true;
					}
					if (component.projectileColor == Color.black || component.projectileColor == Color.clear)
					{
						gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
						return;
					}
					gameObject.GetComponent<SpriteRenderer>().color = component.projectileColor;
				});
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00011EB1 File Offset: 0x000100B1
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00011EB9 File Offset: 0x000100B9
		public void OnDestroy()
		{
			this.gun.ShootPojectileAction = this.goon;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00011ECC File Offset: 0x000100CC
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Star" || this.player.data.currentCards[i].cardName == "Shooting Star")
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

		// Token: 0x06000233 RID: 563 RVA: 0x00011F84 File Offset: 0x00010184
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x040002B0 RID: 688
		private readonly float updateDelay = 0.1f;

		// Token: 0x040002B1 RID: 689
		private float startTime;

		// Token: 0x040002B2 RID: 690
		private int numcheck;

		// Token: 0x040002B3 RID: 691
		private CharacterData data;

		// Token: 0x040002B4 RID: 692
		private Player player;

		// Token: 0x040002B5 RID: 693
		private Gun gun;

		// Token: 0x040002B6 RID: 694
		private Action<GameObject> goon;
	}
}
