using System;
using UnboundLib;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200005D RID: 93
	public class BeeSpriteMono : MonoBehaviour
	{
		// Token: 0x06000247 RID: 583 RVA: 0x00012329 File Offset: 0x00010529
		public void Awake()
		{
			this.player = base.GetComponent<Player>();
			if (CR.crSpecialVFX.Value)
			{
				ExtensionMethods.ExecuteAfterFrames(this, 1, delegate()
				{
					this.bullet = base.GetComponent<ProjectileHit>();
					this.move = this.bullet.GetComponent<MoveTransform>();
					GameObject gameObject = Object.Instantiate<GameObject>(CR.ArtAsset.LoadAsset<GameObject>("beeicon"), base.gameObject.transform);
					gameObject.transform.right = base.gameObject.transform.right;
					gameObject.transform.localScale *= 0.15f;
					if (this.bullet.projectileColor == Color.black || this.bullet.projectileColor == Color.clear)
					{
						gameObject.GetComponent<ParticleSystem>().startColor = new Color(0.6f, 1f, 0f, 1f);
						return;
					}
					gameObject.GetComponent<ParticleSystem>().startColor = this.bullet.projectileColor;
				});
			}
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00012356 File Offset: 0x00010556
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00012360 File Offset: 0x00010560
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Hive")
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

		// Token: 0x0600024A RID: 586 RVA: 0x000123EE File Offset: 0x000105EE
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x040002C7 RID: 711
		public ProjectileHit bullet;

		// Token: 0x040002C8 RID: 712
		public MoveTransform move;

		// Token: 0x040002C9 RID: 713
		private Player player;

		// Token: 0x040002CA RID: 714
		private readonly float updateDelay = 0.1f;

		// Token: 0x040002CB RID: 715
		private float startTime;

		// Token: 0x040002CC RID: 716
		private int numcheck;
	}
}
