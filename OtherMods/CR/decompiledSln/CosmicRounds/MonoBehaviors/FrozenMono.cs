using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200006D RID: 109
	public class FrozenMono : ReversibleEffect
	{
		// Token: 0x060002AE RID: 686 RVA: 0x00015023 File Offset: 0x00013223
		public override void OnOnEnable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00015040 File Offset: 0x00013240
		public override void OnStart()
		{
			this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
			this.colorEffect.SetColor(this.color);
			this.colorEffect.SetLivesToEffect(1);
			this.ResetTimer();
			this.ResetEffectTimer();
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0001508C File Offset: 0x0001328C
		public override void OnUpdate()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				if (base.GetComponent<Player>().data.dead || base.GetComponent<Player>().data.health <= 0f || !base.GetComponent<Player>().gameObject.activeInHierarchy)
				{
					this.ResetTimer();
					base.Destroy();
				}
				if (Time.time >= this.timeOfLastEffect + this.effectCooldown)
				{
					base.Destroy();
					this.colorEffect.Destroy();
				}
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00015120 File Offset: 0x00013320
		public override void OnOnDisable()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
				this.ResetEffectTimer();
				Object.Destroy(this.ice);
				Object.Destroy(this.snow);
				Object.Destroy(this.iceRing);
				Object.Destroy(this.gameObject);
				Object.Destroy(this.gameObject2);
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00015184 File Offset: 0x00013384
		public override void OnOnDestroy()
		{
			if (this.colorEffect != null)
			{
				this.colorEffect.Destroy();
				this.ResetEffectTimer();
				Object.Destroy(this.ice);
				Object.Destroy(this.snow);
				Object.Destroy(this.iceRing);
				Object.Destroy(this.gameObject);
				Object.Destroy(this.gameObject2);
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x000151E7 File Offset: 0x000133E7
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x000151F4 File Offset: 0x000133F4
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x04000351 RID: 849
		private readonly Color color = Color.cyan;

		// Token: 0x04000352 RID: 850
		private ReversibleColorEffect colorEffect;

		// Token: 0x04000353 RID: 851
		public MistletoeMono effect;

		// Token: 0x04000354 RID: 852
		public GameObject ice;

		// Token: 0x04000355 RID: 853
		public GameObject snow;

		// Token: 0x04000356 RID: 854
		public IceRing iceRing;

		// Token: 0x04000357 RID: 855
		public GameObject gameObject;

		// Token: 0x04000358 RID: 856
		public GameObject gameObject2;

		// Token: 0x04000359 RID: 857
		private readonly float effectCooldown = 4.2f;

		// Token: 0x0400035A RID: 858
		private readonly float updateDelay = 0.1f;

		// Token: 0x0400035B RID: 859
		private float timeOfLastEffect;

		// Token: 0x0400035C RID: 860
		private float startTime;
	}
}
