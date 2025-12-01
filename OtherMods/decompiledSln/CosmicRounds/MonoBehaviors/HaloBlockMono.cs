using System;
using System.Collections.Generic;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000064 RID: 100
	public class HaloBlockMono : ReversibleEffect
	{
		// Token: 0x06000274 RID: 628 RVA: 0x000139C8 File Offset: 0x00011BC8
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000139E4 File Offset: 0x00011BE4
		public override void OnStart()
		{
			this.gunStatModifier.numberOfProjectiles_add += 2;
			this.gunStatModifier.damage_mult *= 0.75f;
			this.gunStatModifier.spread_add += 0.15f;
			this.gunStatModifier.projectielSimulatonSpeed_mult *= 0.75f;
			List<ObjectsToSpawn> objectsToSpawn_add = this.gunStatModifier.objectsToSpawn_add;
			objectsToSpawn_add.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("OPOPOP", new Type[]
				{
					typeof(DriveMono)
				})
			});
			objectsToSpawn_add.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("FWOOSH", new Type[]
				{
					typeof(HaloFireMono)
				})
			});
			ObjectsToSpawn item = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			objectsToSpawn_add.Add(item);
			this.gunStatModifier.reflects_add++;
			this.gunStatModifier.objectsToSpawn_add = objectsToSpawn_add;
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.effect = this.player.GetComponent<HaloMono>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
			this.holyshot = this.player.gameObject.AddComponent<HolyShotMono>();
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00013B58 File Offset: 0x00011D58
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (!this.effect.Failsafe || !this.effect.Active)
				{
					base.Destroy();
					this.colorEffect.Destroy();
					this.holyshot.Destroy();
				}
				if (base.GetComponent<Player>().data.dead || base.GetComponent<Player>().data.health <= 0f || !base.GetComponent<Player>().gameObject.activeInHierarchy)
				{
					this.ResetTimer();
					base.Destroy();
				}
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00013BFF File Offset: 0x00011DFF
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
				this.holyshot.Destroy();
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00013C25 File Offset: 0x00011E25
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
				this.effect.Active = false;
				this.holyshot.Destroy();
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00013C57 File Offset: 0x00011E57
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x04000302 RID: 770
		private readonly Color color = Color.yellow;

		// Token: 0x04000303 RID: 771
		private ReversibleColorEffect colorEffect;

		// Token: 0x04000304 RID: 772
		public HaloMono effect;

		// Token: 0x04000305 RID: 773
		public HolyShotMono holyshot;

		// Token: 0x04000306 RID: 774
		public HolyMono holy;

		// Token: 0x04000307 RID: 775
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000308 RID: 776
		private float startTime;
	}
}
