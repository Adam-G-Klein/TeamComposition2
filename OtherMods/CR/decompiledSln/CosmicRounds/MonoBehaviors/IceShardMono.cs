using System;
using UnboundLib;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000058 RID: 88
	public class IceShardMono : MonoBehaviour
	{
		// Token: 0x06000227 RID: 551 RVA: 0x00011C88 File Offset: 0x0000FE88
		public void Start()
		{
			this.data = base.GetComponentInParent<CharacterData>();
			this.player = base.GetComponent<Player>();
			this.gun = this.data.weaponHandler.gun;
			this.goon = this.gun.ShootPojectileAction;
			this.gun.ShootPojectileAction = (Action<GameObject>)Delegate.Combine(this.gun.ShootPojectileAction, new Action<GameObject>(this.Attack));
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00011D00 File Offset: 0x0000FF00
		private void Attack(GameObject projectile)
		{
			ExtensionMethods.ExecuteAfterFrames(this, 1, delegate()
			{
				ProjectileHit component = projectile.GetComponent<ProjectileHit>();
				component.GetComponentInParent<MoveTransform>();
				GameObject gameObject = Object.Instantiate<GameObject>(CR.ArtAsset.LoadAsset<GameObject>("crystalicon"), projectile.transform);
				gameObject.transform.up = projectile.transform.forward;
				gameObject.transform.localScale *= 0.7f;
				if (component.projectileColor == Color.black || component.projectileColor == Color.clear)
				{
					gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
					return;
				}
				gameObject.GetComponent<SpriteRenderer>().color = component.projectileColor;
			});
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00011D2D File Offset: 0x0000FF2D
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00011D35 File Offset: 0x0000FF35
		public void OnDestroy()
		{
			this.gun.ShootPojectileAction = this.goon;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00011D48 File Offset: 0x0000FF48
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Ice Shard")
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

		// Token: 0x0600022C RID: 556 RVA: 0x00011DD6 File Offset: 0x0000FFD6
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x040002A9 RID: 681
		private readonly float updateDelay = 0.1f;

		// Token: 0x040002AA RID: 682
		private float startTime;

		// Token: 0x040002AB RID: 683
		private int numcheck;

		// Token: 0x040002AC RID: 684
		private CharacterData data;

		// Token: 0x040002AD RID: 685
		private Player player;

		// Token: 0x040002AE RID: 686
		private Gun gun;

		// Token: 0x040002AF RID: 687
		private Action<GameObject> goon;
	}
}
