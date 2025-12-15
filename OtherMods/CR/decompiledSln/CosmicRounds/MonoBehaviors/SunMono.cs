using System;
using Photon.Pun;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000024 RID: 36
	public class SunMono : BounceTrigger
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x00007294 File Offset: 0x00005494
		private void Start()
		{
			this.move = base.GetComponentInParent<MoveTransform>();
			this.sync = base.GetComponentInParent<SyncProjectile>();
			this.sync.active = true;
			this.ResetTimer();
			this.loop = 0f;
			this.bounceEffects = base.GetComponents<BounceEffect>();
			RayHitReflect componentInParent = base.GetComponentInParent<RayHitReflect>();
			componentInParent.reflectAction = (Action<HitInfo>)Delegate.Combine(componentInParent.reflectAction, new Action<HitInfo>(this.Reflect));
			this.caree = componentInParent.reflects;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00007317 File Offset: 0x00005517
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00007324 File Offset: 0x00005524
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000732C File Offset: 0x0000552C
		public void Reflect(HitInfo hit)
		{
			for (int i = 0; i < this.bounceEffects.Length; i++)
			{
				this.bounceEffects[i].DoBounce(hit);
			}
			this.caree--;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00007368 File Offset: 0x00005568
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay && base.gameObject.transform.parent != null)
			{
				if (this.caree == 0)
				{
					this.move.simulateGravity--;
					this.move.drag = 1f;
					Vector3 vector = base.transform.right * this.move.selectedSpread;
					float num = Vector3.Angle(base.transform.root.forward, vector - base.transform.position);
					this.move.velocity -= this.move.velocity * num * TimeHandler.deltaTime * 0.75f;
					this.move.velocity -= this.move.velocity * TimeHandler.deltaTime * 0.75f;
					this.move.velocity += Vector3.ClampMagnitude(vector - base.transform.position, 1f) * TimeHandler.deltaTime * this.move.localForce.magnitude * this.loop;
					if (this.loop < 5f)
					{
						this.loop += 0.5f;
					}
					this.move.velocity += Vector3.up * TimeHandler.deltaTime * this.move.gravity * this.move.multiplier * 1.75f;
				}
				this.move.velocity.z = 0f;
				this.ResetTimer();
			}
		}

		// Token: 0x040000E9 RID: 233
		public int caree = 1;

		// Token: 0x040000EA RID: 234
		public Player player;

		// Token: 0x040000EB RID: 235
		private MoveTransform move;

		// Token: 0x040000EC RID: 236
		private SyncProjectile sync;

		// Token: 0x040000ED RID: 237
		private readonly float updateDelay = 0.1f;

		// Token: 0x040000EE RID: 238
		private float startTime;

		// Token: 0x040000EF RID: 239
		private float loop;

		// Token: 0x040000F0 RID: 240
		private BounceEffect[] bounceEffects;
	}
}
