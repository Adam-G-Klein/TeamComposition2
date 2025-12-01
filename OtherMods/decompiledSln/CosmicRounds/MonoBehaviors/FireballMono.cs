using System;
using UnboundLib;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000060 RID: 96
	public class FireballMono : MonoBehaviour
	{
		// Token: 0x0600025B RID: 603 RVA: 0x00012853 File Offset: 0x00010A53
		public void Awake()
		{
			this.player = base.GetComponent<Player>();
			if (CR.crSpecialVFX.Value)
			{
				ExtensionMethods.ExecuteAfterFrames(this, 1, delegate()
				{
					this.bullet = base.GetComponent<ProjectileHit>();
					this.move = this.bullet.GetComponent<MoveTransform>();
					GameObject gameObject = Object.Instantiate<GameObject>(CR.ArtAsset.LoadAsset<GameObject>("fireball"), base.gameObject.transform);
					gameObject.transform.right = base.gameObject.transform.forward;
					gameObject.transform.localScale *= 0.6f;
					if (this.bullet.projectileColor == Color.black || this.bullet.projectileColor == Color.clear)
					{
						gameObject.GetComponent<ParticleSystem>().startColor = new Color(1f, 1f, 0f, 1f);
					}
					else
					{
						gameObject.GetComponent<ParticleSystem>().startColor = this.bullet.projectileColor;
					}
					GameObject gameObject2 = Object.Instantiate<GameObject>(CR.ArtAsset.LoadAsset<GameObject>("fireballparticles"), base.gameObject.transform);
					gameObject2.transform.forward = -base.gameObject.transform.forward;
					if (this.bullet.projectileColor == Color.black || this.bullet.projectileColor == Color.clear)
					{
						gameObject2.GetComponent<ParticleSystem>().startColor = new Color(1f, 1f, 0f, 1f);
					}
					else
					{
						gameObject2.GetComponent<ParticleSystem>().startColor = this.bullet.projectileColor;
					}
					gameObject2.transform.localScale *= 0.7f;
					GameObject gameObject3 = Object.Instantiate<GameObject>(CR.ArtAsset.LoadAsset<GameObject>("fireballparticles"), base.gameObject.transform);
					gameObject3.transform.forward = -base.gameObject.transform.forward;
					gameObject3.transform.localScale *= 0.5f;
					gameObject3.GetComponent<ParticleSystem>().startColor = new Color(0.4f, 0.4f, 0.4f, 0.8f);
				});
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00012880 File Offset: 0x00010A80
		public void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Ignite" || this.player.data.currentCards[i].cardName == "Flamethrower" || this.player.data.currentCards[i].cardName == "Sun")
					{
						this.numcheck++;
					}
				}
				if (this.numcheck > 0)
				{
					this.ResetTimer();
				}
				else
				{
					this.Destroy();
				}
			}
			if (this.move.velocity.magnitude == 0f)
			{
				ExtensionMethods.ExecuteAfterFrames(this, 5, delegate()
				{
					this.Destroy();
				});
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00012990 File Offset: 0x00010B90
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00012998 File Offset: 0x00010B98
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x040002DB RID: 731
		public ProjectileHit bullet;

		// Token: 0x040002DC RID: 732
		public MoveTransform move;

		// Token: 0x040002DD RID: 733
		public Player player;

		// Token: 0x040002DE RID: 734
		private readonly float updateDelay = 0.1f;

		// Token: 0x040002DF RID: 735
		private float startTime;

		// Token: 0x040002E0 RID: 736
		private int numcheck;
	}
}
