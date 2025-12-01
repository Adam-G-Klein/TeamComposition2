using System;
using UnboundLib;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000061 RID: 97
	public class HolyFireMono : MonoBehaviour
	{
		// Token: 0x06000262 RID: 610 RVA: 0x00012BF4 File Offset: 0x00010DF4
		public void Awake()
		{
			this.player = base.GetComponent<Player>();
			this.bullet = base.GetComponent<ProjectileHit>();
			this.move = this.bullet.GetComponent<MoveTransform>();
			if (CR.crSpecialVFX.Value)
			{
				GameObject gameObject = Object.Instantiate<GameObject>(CR.ArtAsset.LoadAsset<GameObject>("fireball"), base.gameObject.transform);
				gameObject.transform.right = base.gameObject.transform.forward;
				gameObject.transform.localScale *= 0.5f;
				if (this.bullet.projectileColor == Color.black || this.bullet.projectileColor == Color.clear)
				{
					Color color = gameObject.GetComponent<ParticleSystem>().main.startColor.color;
					new Color(1f, 1f, 1f, 1f);
				}
				else
				{
					Color color2 = gameObject.GetComponent<ParticleSystem>().main.startColor.color;
					Color projectileColor = this.bullet.projectileColor;
				}
				GameObject gameObject2 = Object.Instantiate<GameObject>(CR.ArtAsset.LoadAsset<GameObject>("fireballparticles"), base.gameObject.transform);
				gameObject2.transform.forward = -base.gameObject.transform.forward;
				gameObject2.transform.localScale *= 0.65f;
				if (this.bullet.projectileColor == Color.black || this.bullet.projectileColor == Color.clear)
				{
					Color color3 = gameObject2.GetComponent<ParticleSystem>().main.startColor.color;
					new Color(1f, 1f, 1f, 1f);
				}
				else
				{
					Color color4 = gameObject2.GetComponent<ParticleSystem>().main.startColor.color;
					Color projectileColor2 = this.bullet.projectileColor;
				}
				GameObject gameObject3 = Object.Instantiate<GameObject>(CR.ArtAsset.LoadAsset<GameObject>("Halo"), base.gameObject.transform);
				gameObject3.transform.forward = base.gameObject.transform.up;
				gameObject3.transform.localScale *= 0.4f;
				if (this.bullet.projectileColor == Color.black || this.bullet.projectileColor == Color.clear)
				{
					Color color5 = gameObject3.GetComponent<ParticleSystem>().main.startColor.color;
					new Color(1f, 1f, 1f, 1f);
				}
				else
				{
					Color color6 = gameObject3.GetComponent<ParticleSystem>().main.startColor.color;
					Color projectileColor3 = this.bullet.projectileColor;
				}
				GameObject gameObject4 = Object.Instantiate<GameObject>(CR.ArtAsset.LoadAsset<GameObject>("plusparticles"), base.gameObject.transform);
				gameObject4.transform.forward = -base.gameObject.transform.forward;
				Color color7 = gameObject4.GetComponent<ParticleSystem>().main.startColor.color;
				new Color(1f, 1f, 0.3f, 1f);
				gameObject4.transform.localScale *= 1.5f;
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00012F80 File Offset: 0x00011180
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00012F88 File Offset: 0x00011188
		public void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Halo")
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

		// Token: 0x06000265 RID: 613 RVA: 0x00013041 File Offset: 0x00011241
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x040002E1 RID: 737
		public ProjectileHit bullet;

		// Token: 0x040002E2 RID: 738
		public MoveTransform move;

		// Token: 0x040002E3 RID: 739
		public Player player;

		// Token: 0x040002E4 RID: 740
		private readonly float updateDelay = 0.1f;

		// Token: 0x040002E5 RID: 741
		private float startTime;

		// Token: 0x040002E6 RID: 742
		private int numcheck;
	}
}
