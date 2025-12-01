using System;
using System.Collections.Generic;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200002E RID: 46
	public class PurpleMono : ReversibleEffect
	{
		// Token: 0x06000103 RID: 259 RVA: 0x00008BB7 File Offset: 0x00006DB7
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00008BD4 File Offset: 0x00006DD4
		public override void OnStart()
		{
			this.characterStatModifiersModifier.secondsToTakeDamageOver_add = 4f;
			this.characterDataModifier.maxHealth_mult = 1.5f;
			this.characterStatModifiersModifier.lifeSteal_add = 0.5f;
			this.gunStatModifier.damage_mult = 1.2f;
			this.gunStatModifier.projectileColor += new Color(1f, 0f, 1f, 1f);
			List<ObjectsToSpawn> objectsToSpawn_add = this.gunStatModifier.objectsToSpawn_add;
			objectsToSpawn_add.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("Buwwet UwU", new Type[]
				{
					typeof(PurpleBulletMono)
				})
			});
			this.gunStatModifier.objectsToSpawn_add = objectsToSpawn_add;
			this.effect = this.player.GetComponent<UnicornMono>();
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00008CE4 File Offset: 0x00006EE4
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (this.effect.mode != 6)
				{
					base.Destroy();
				}
				if (base.GetComponent<Player>().data.dead || base.GetComponent<Player>().data.health <= 0f || !base.GetComponent<Player>().gameObject.activeInHierarchy)
				{
					this.ResetTimer();
					base.Destroy();
				}
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00008D66 File Offset: 0x00006F66
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00008D81 File Offset: 0x00006F81
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00008D9C File Offset: 0x00006F9C
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x04000126 RID: 294
		private readonly Color color = new Color(1f, 0f, 1f, 1f);

		// Token: 0x04000127 RID: 295
		private ReversibleColorEffect colorEffect;

		// Token: 0x04000128 RID: 296
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000129 RID: 297
		private float startTime;

		// Token: 0x0400012A RID: 298
		private UnicornMono effect;

		// Token: 0x0400012B RID: 299
		public PurpleBulletMono buwwet;
	}
}
