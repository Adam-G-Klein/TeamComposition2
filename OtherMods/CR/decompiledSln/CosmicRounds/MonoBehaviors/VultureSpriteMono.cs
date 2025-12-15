using System;
using UnboundLib;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200007F RID: 127
	public class VultureSpriteMono : MonoBehaviour
	{
		// Token: 0x06000330 RID: 816 RVA: 0x00018B74 File Offset: 0x00016D74
		public void Start()
		{
			this.data = base.GetComponentInParent<CharacterData>();
			this.player = base.GetComponent<Player>();
			this.gun = this.data.weaponHandler.gun;
			this.goon = this.gun.ShootPojectileAction;
			this.gun.ShootPojectileAction = (Action<GameObject>)Delegate.Combine(this.gun.ShootPojectileAction, new Action<GameObject>(this.Attack));
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00018BEC File Offset: 0x00016DEC
		private void Attack(GameObject projectile)
		{
			if (CR.crSpecialVFX.Value)
			{
				ExtensionMethods.ExecuteAfterFrames(this, 1, delegate()
				{
					ProjectileHit component = projectile.GetComponent<ProjectileHit>();
					component.GetComponentInParent<MoveTransform>();
					GameObject gameObject = Object.Instantiate<GameObject>(CR.ArtAsset.LoadAsset<GameObject>("VultureSprite"), projectile.transform);
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

		// Token: 0x06000332 RID: 818 RVA: 0x00018C25 File Offset: 0x00016E25
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00018C2D File Offset: 0x00016E2D
		public void OnDestroy()
		{
			this.gun.ShootPojectileAction = this.goon;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00018C40 File Offset: 0x00016E40
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Vulture")
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

		// Token: 0x06000335 RID: 821 RVA: 0x00018CCE File Offset: 0x00016ECE
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x0400040C RID: 1036
		private readonly float updateDelay = 0.1f;

		// Token: 0x0400040D RID: 1037
		private float startTime;

		// Token: 0x0400040E RID: 1038
		private int numcheck;

		// Token: 0x0400040F RID: 1039
		private CharacterData data;

		// Token: 0x04000410 RID: 1040
		private Player player;

		// Token: 0x04000411 RID: 1041
		private Gun gun;

		// Token: 0x04000412 RID: 1042
		private Action<GameObject> goon;
	}
}
