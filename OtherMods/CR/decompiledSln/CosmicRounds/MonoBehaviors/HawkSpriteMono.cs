using System;
using UnboundLib;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200007C RID: 124
	public class HawkSpriteMono : MonoBehaviour
	{
		// Token: 0x0600031B RID: 795 RVA: 0x000186E8 File Offset: 0x000168E8
		public void Start()
		{
			this.data = base.GetComponentInParent<CharacterData>();
			this.player = base.GetComponent<Player>();
			this.gun = this.data.weaponHandler.gun;
			this.goon = this.gun.ShootPojectileAction;
			this.gun.ShootPojectileAction = (Action<GameObject>)Delegate.Combine(this.gun.ShootPojectileAction, new Action<GameObject>(this.Attack));
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00018760 File Offset: 0x00016960
		private void Attack(GameObject projectile)
		{
			if (CR.crSpecialVFX.Value)
			{
				ExtensionMethods.ExecuteAfterFrames(this, 1, delegate()
				{
					ProjectileHit component = projectile.GetComponent<ProjectileHit>();
					component.GetComponentInParent<MoveTransform>();
					GameObject gameObject = Object.Instantiate<GameObject>(CR.ArtAsset.LoadAsset<GameObject>("HawkSprite"), projectile.transform);
					gameObject.transform.up = projectile.transform.right;
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

		// Token: 0x0600031D RID: 797 RVA: 0x00018799 File Offset: 0x00016999
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000187A1 File Offset: 0x000169A1
		public void OnDestroy()
		{
			this.gun.ShootPojectileAction = this.goon;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000187B4 File Offset: 0x000169B4
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Hawk")
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

		// Token: 0x06000320 RID: 800 RVA: 0x00018842 File Offset: 0x00016A42
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x040003F7 RID: 1015
		private readonly float updateDelay = 0.1f;

		// Token: 0x040003F8 RID: 1016
		private float startTime;

		// Token: 0x040003F9 RID: 1017
		private int numcheck;

		// Token: 0x040003FA RID: 1018
		private CharacterData data;

		// Token: 0x040003FB RID: 1019
		private Player player;

		// Token: 0x040003FC RID: 1020
		private Gun gun;

		// Token: 0x040003FD RID: 1021
		private Action<GameObject> goon;
	}
}
