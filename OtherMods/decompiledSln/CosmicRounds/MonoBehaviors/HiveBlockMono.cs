using System;
using System.Collections.Generic;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000044 RID: 68
	public class HiveBlockMono : ReversibleEffect
	{
		// Token: 0x0600019E RID: 414 RVA: 0x0000D042 File Offset: 0x0000B242
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000D060 File Offset: 0x0000B260
		public override void OnStart()
		{
			this.gunStatModifier.numberOfProjectiles_add += 2;
			this.gunStatModifier.damage_mult *= 0.75f;
			this.gunStatModifier.spread_add += 0.2f;
			this.gunStatModifier.gravity_mult *= 0.3f;
			this.gunStatModifier.projectielSimulatonSpeed_mult *= 0.6f;
			this.gunStatModifier.projectileColor = new Color(0.8f, 1f, 0.3f, 1f);
			List<ObjectsToSpawn> objectsToSpawn_add = this.gunStatModifier.objectsToSpawn_add;
			objectsToSpawn_add.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("NOT THE BEES AHH", new Type[]
				{
					typeof(BeeMono)
				})
			});
			ObjectsToSpawn item = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			objectsToSpawn_add.Add(item);
			this.gunStatModifier.objectsToSpawn_add = objectsToSpawn_add;
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.effect = this.player.GetComponent<HiveMono>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
			this.beeShot = this.player.gameObject.AddComponent<BeeShotMono>();
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000D1CC File Offset: 0x0000B3CC
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (!this.effect.Failsafe || !this.effect.Active)
				{
					this.beeShot.Destroy();
					base.Destroy();
					this.colorEffect.Destroy();
				}
				if (base.GetComponent<Player>().data.dead || base.GetComponent<Player>().data.health <= 0f || !base.GetComponent<Player>().gameObject.activeInHierarchy)
				{
					this.ResetTimer();
					base.Destroy();
				}
			}
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000D273 File Offset: 0x0000B473
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.beeShot.Destroy();
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000D299 File Offset: 0x0000B499
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.beeShot.Destroy();
				this.colorEffect.Destroy();
				this.effect.Active = false;
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000D2CB File Offset: 0x0000B4CB
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x040001F6 RID: 502
		private readonly Color color = Color.yellow;

		// Token: 0x040001F7 RID: 503
		private ReversibleColorEffect colorEffect;

		// Token: 0x040001F8 RID: 504
		public HiveMono effect;

		// Token: 0x040001F9 RID: 505
		public BeeShotMono beeShot;

		// Token: 0x040001FA RID: 506
		private readonly float updateDelay = 0.1f;

		// Token: 0x040001FB RID: 507
		private float startTime;
	}
}
