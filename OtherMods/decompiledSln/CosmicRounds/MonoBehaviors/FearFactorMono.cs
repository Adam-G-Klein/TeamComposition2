using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200000D RID: 13
	public class FearFactorMono : MonoBehaviour
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002FA1 File Offset: 0x000011A1
		private void Awake()
		{
			this.player = base.GetComponent<Player>();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002FB0 File Offset: 0x000011B0
		private void Start()
		{
			this.ResetEffectTimer();
			this.ResetTimer();
			this.factorObject = new GameObject();
			this.factorObject.transform.SetParent(this.player.transform);
			this.factorObject.transform.position = this.player.transform.position;
			this.factorObject.AddComponent<FearFactorMono.DestroyOnUnparent>();
			if (FearFactorMono.lineEffect == null)
			{
				this.GetLineEffect();
			}
			this.fearEffect = Object.Instantiate<GameObject>(FearFactorMono.lineEffect, this.factorObject.transform);
			this.fearEffect.AddComponent<FearFactorMono.DestroyOnUnparent>();
			this.componentInChildren = this.fearEffect.GetComponentInChildren<LineEffect>();
			this.componentInChildren.colorOverTime = new Gradient
			{
				alphaKeys = new GradientAlphaKey[]
				{
					new GradientAlphaKey(0.7f, 0f)
				},
				colorKeys = new GradientColorKey[]
				{
					new GradientColorKey(Color.red, 0f)
				},
				mode = 1
			};
			this.componentInChildren.widthMultiplier = 1f;
			this.componentInChildren.radius = 3f;
			this.componentInChildren.raycastCollision = true;
			this.componentInChildren.useColorOverTime = true;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000030FC File Offset: 0x000012FC
		private void GetLineEffect()
		{
			FearFactorMono.lineEffect = CardChoice.instance.cards.First((CardInfo c) => c.name.Equals("ChillingPresence")).gameObject.GetComponentInChildren<CharacterStatModifiers>().AddObjectToPlayer.GetComponentInChildren<LineEffect>().gameObject;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003155 File Offset: 0x00001355
		public void Destroy()
		{
			this.componentInChildren.Stop();
			Object.Destroy(this.factorObject);
			Object.Destroy(this.fearEffect);
			Object.Destroy(this.componentInChildren);
			Object.Destroy(this);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000318C File Offset: 0x0000138C
		private void OnDestroy()
		{
			LineEffect lineEffect = this.componentInChildren;
			if (lineEffect != null)
			{
				lineEffect.Stop();
			}
			if (this.componentInChildren != null)
			{
				Object.Destroy(this.componentInChildren);
			}
			Object.Destroy(this.factorObject);
			Object.Destroy(this.fearEffect);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000031DC File Offset: 0x000013DC
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Fear Factor")
					{
						this.numcheck++;
					}
				}
				if (this.numcheck > 0)
				{
					this.ResetTimer();
					if (Time.time < this.timeOfLastEffect + this.effectCooldown)
					{
						return;
					}
					int mask = LayerMask.GetMask(new string[]
					{
						"Projectile"
					});
					using (IEnumerator<Collider2D> enumerator = (from uwu in Physics2D.OverlapCircleAll(base.transform.position, 7.2f, mask)
					where uwu.gameObject.GetComponentInParent<ProjectileHit>() != null && uwu.gameObject.GetComponentInParent<ProjectileHit>().ownPlayer != this.player && PlayerManager.instance.CanSeePlayer(uwu.gameObject.transform.position, this.player).canSee
					select uwu).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Collider2D collider2D = enumerator.Current;
							this.ResetEffectTimer();
							this.player.data.weaponHandler.gun.Attack(0f, true, 0.75f, 1f, false);
						}
						return;
					}
				}
				this.Destroy();
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000332C File Offset: 0x0000152C
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003340 File Offset: 0x00001540
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x04000028 RID: 40
		private Player player;

		// Token: 0x04000029 RID: 41
		private static GameObject lineEffect;

		// Token: 0x0400002A RID: 42
		public LineEffect componentInChildren;

		// Token: 0x0400002B RID: 43
		public GameObject fearEffect;

		// Token: 0x0400002C RID: 44
		public GameObject factorObject;

		// Token: 0x0400002D RID: 45
		private readonly float updateDelay = 0.1f;

		// Token: 0x0400002E RID: 46
		private readonly float effectCooldown = 1f;

		// Token: 0x0400002F RID: 47
		private readonly float maxDistance = 7.1f;

		// Token: 0x04000030 RID: 48
		private float startTime;

		// Token: 0x04000031 RID: 49
		private float timeOfLastEffect;

		// Token: 0x04000032 RID: 50
		private int numcheck;

		// Token: 0x02000103 RID: 259
		private class DestroyOnUnparent : MonoBehaviour
		{
			// Token: 0x060008C6 RID: 2246 RVA: 0x000260C7 File Offset: 0x000242C7
			private void Update()
			{
				Object @object;
				if (this == null)
				{
					@object = null;
				}
				else
				{
					Transform transform = base.transform;
					@object = ((transform != null) ? transform.parent : null);
				}
				if (@object == null)
				{
					Object.Destroy(this);
				}
			}
		}
	}
}
