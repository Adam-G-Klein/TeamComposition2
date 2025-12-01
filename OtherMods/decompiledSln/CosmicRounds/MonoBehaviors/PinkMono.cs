using System;
using System.Collections.Generic;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000030 RID: 48
	public class PinkMono : ReversibleEffect
	{
		// Token: 0x0600010E RID: 270 RVA: 0x00008EE5 File Offset: 0x000070E5
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00008F00 File Offset: 0x00007100
		public override void OnStart()
		{
			List<ObjectsToSpawn> objectsToSpawn_add = this.gunStatModifier.objectsToSpawn_add;
			ObjectsToSpawn item = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			objectsToSpawn_add.Add(item);
			this.gunStatModifier.reflects_add += 7;
			this.gunStatModifier.projectileSpeed_mult *= 0.8f;
			this.gunStatModifier.objectsToSpawn_add = objectsToSpawn_add;
			this.gunStatModifier.projectileColor = new Color(1f, 0.5f, 1f, 1f);
			this.effect = this.player.GetComponent<UnicornMono>();
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00008FE0 File Offset: 0x000071E0
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (this.effect.mode != 7)
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

		// Token: 0x06000111 RID: 273 RVA: 0x00009062 File Offset: 0x00007262
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000907D File Offset: 0x0000727D
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00009098 File Offset: 0x00007298
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x04000130 RID: 304
		private readonly Color color = new Color(1f, 0.7f, 1f, 1f);

		// Token: 0x04000131 RID: 305
		private ReversibleColorEffect colorEffect;

		// Token: 0x04000132 RID: 306
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000133 RID: 307
		private float startTime;

		// Token: 0x04000134 RID: 308
		private UnicornMono effect;
	}
}
