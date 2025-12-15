using System;
using UnboundLib;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200007E RID: 126
	public class DoveSpriteMono : MonoBehaviour
	{
		// Token: 0x06000329 RID: 809 RVA: 0x000189F0 File Offset: 0x00016BF0
		public void Start()
		{
			this.data = base.GetComponentInParent<CharacterData>();
			this.player = base.GetComponent<Player>();
			this.gun = this.data.weaponHandler.gun;
			this.goon = this.gun.ShootPojectileAction;
			this.gun.ShootPojectileAction = (Action<GameObject>)Delegate.Combine(this.gun.ShootPojectileAction, new Action<GameObject>(this.Attack));
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00018A68 File Offset: 0x00016C68
		private void Attack(GameObject projectile)
		{
			if (CR.crSpecialVFX.Value)
			{
				ExtensionMethods.ExecuteAfterFrames(this, 1, delegate()
				{
					ProjectileHit component = projectile.GetComponent<ProjectileHit>();
					component.GetComponentInParent<MoveTransform>();
					GameObject gameObject = Object.Instantiate<GameObject>(CR.ArtAsset.LoadAsset<GameObject>("DoveSprite"), projectile.transform);
					gameObject.transform.up = projectile.transform.right;
					gameObject.transform.localScale *= 0.2f;
					if (component.projectileColor == Color.black || component.projectileColor == Color.clear)
					{
						gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
						return;
					}
					gameObject.GetComponent<SpriteRenderer>().color = component.projectileColor;
				});
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x00018AA1 File Offset: 0x00016CA1
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00018AA9 File Offset: 0x00016CA9
		public void OnDestroy()
		{
			this.gun.ShootPojectileAction = this.goon;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00018ABC File Offset: 0x00016CBC
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Dove")
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

		// Token: 0x0600032E RID: 814 RVA: 0x00018B4A File Offset: 0x00016D4A
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x04000405 RID: 1029
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000406 RID: 1030
		private float startTime;

		// Token: 0x04000407 RID: 1031
		private int numcheck;

		// Token: 0x04000408 RID: 1032
		private CharacterData data;

		// Token: 0x04000409 RID: 1033
		private Player player;

		// Token: 0x0400040A RID: 1034
		private Gun gun;

		// Token: 0x0400040B RID: 1035
		private Action<GameObject> goon;
	}
}
