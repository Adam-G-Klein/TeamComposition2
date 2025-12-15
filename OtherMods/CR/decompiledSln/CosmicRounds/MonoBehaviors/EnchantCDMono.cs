using System;
using System.Collections.Generic;
using ModdingUtils.MonoBehaviours;
using Sonigon;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200007A RID: 122
	public class EnchantCDMono : ReversibleEffect
	{
		// Token: 0x0600030D RID: 781 RVA: 0x000183A2 File Offset: 0x000165A2
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000183C0 File Offset: 0x000165C0
		public override void OnStart()
		{
			List<ObjectsToSpawn> objectsToSpawn_add = this.gunStatModifier.objectsToSpawn_add;
			ObjectsToSpawn item = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			objectsToSpawn_add.Add(item);
			this.gunStatModifier.projectileColor = this.color;
			this.gunStatModifier.damage_mult *= 1.3f;
			this.gunStatModifier.reflects_add += 5;
			this.gunStatModifier.projectielSimulatonSpeed_mult *= 1.3f;
			this.ResetTimer();
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00018454 File Offset: 0x00016654
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (this.player.data.block.IsOnCD())
				{
					base.Destroy();
				}
				if (base.GetComponent<Player>().data.dead || base.GetComponent<Player>().data.health <= 0f || base.GetComponent<Player>().data.health <= 0f)
				{
					this.ResetTimer();
					this.colorEffect.Destroy();
					base.Destroy();
				}
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000184EF File Offset: 0x000166EF
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0001850A File Offset: 0x0001670A
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x00018525 File Offset: 0x00016725
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x040003E9 RID: 1001
		private readonly Color color = new Color(1f, 0.7f, 1f, 1f);

		// Token: 0x040003EA RID: 1002
		private ReversibleColorEffect colorEffect;

		// Token: 0x040003EB RID: 1003
		private readonly float updateDelay = 0.001f;

		// Token: 0x040003EC RID: 1004
		private float startTime;

		// Token: 0x040003ED RID: 1005
		private float regen;

		// Token: 0x040003EE RID: 1006
		private ChargeMono effect;

		// Token: 0x040003EF RID: 1007
		private SoundEvent soundSpawn;
	}
}
