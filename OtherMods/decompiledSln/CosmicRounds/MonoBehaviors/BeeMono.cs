using System;
using Photon.Pun;
using Sonigon;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000055 RID: 85
	public class BeeMono : MonoBehaviour
	{
		// Token: 0x0600021A RID: 538 RVA: 0x000112A0 File Offset: 0x0000F4A0
		private void Start()
		{
			this.move = base.GetComponentInParent<MoveTransform>();
			this.flicks = base.GetComponentsInChildren<FlickerEvent>();
			this.view = base.GetComponentInParent<PhotonView>();
			this.sync = base.GetComponentInParent<SyncProjectile>();
			this.sync.active = true;
			GameObject addObjectToPlayer = ((GameObject)Resources.Load("0 cards/Homing")).GetComponent<CharacterStatModifiers>().AddObjectToPlayer;
			this.soundSpawn = addObjectToPlayer.GetComponent<Homing>().soundHomingFound;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00011314 File Offset: 0x0000F514
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0001131C File Offset: 0x0000F51C
		private void Update()
		{
			Object @object;
			if (this == null)
			{
				@object = null;
			}
			else
			{
				GameObject gameObject = base.gameObject;
				if (gameObject == null)
				{
					@object = null;
				}
				else
				{
					Transform transform = gameObject.transform;
					@object = ((transform != null) ? transform.parent : null);
				}
			}
			if (@object != null)
			{
				if (PlayerManager.instance.GetClosestPlayer(base.transform.position, true) != null)
				{
					this.closestPlayer = PlayerManager.instance.GetClosestPlayer(base.transform.position, true);
				}
				Player ownPlayer = base.GetComponentInParent<ProjectileHit>().ownPlayer;
				if (!this.closestPlayer)
				{
					if (this.isOn)
					{
						this.move.simulateGravity--;
						this.soundHomingCanPlay = true;
					}
					this.isOn = false;
					for (int i = 0; i < this.flicks.Length; i++)
					{
						this.flicks[i].isOn = false;
					}
					this.rot1.target = 90f;
					this.rot2.target = -90f;
					return;
				}
				if (this.closestPlayer.teamID != ownPlayer.teamID && this.closestPlayer)
				{
					Vector3 vector = this.closestPlayer.transform.position + base.transform.right * this.move.selectedSpread * Vector3.Distance(base.transform.position, this.closestPlayer.transform.position) * this.spread;
					float num = Vector3.Angle(base.transform.root.forward, vector - base.transform.position);
					if (num <= 90f)
					{
						this.move.velocity -= this.move.velocity * num * TimeHandler.deltaTime * this.scalingDrag;
						this.move.velocity -= this.move.velocity * TimeHandler.deltaTime * this.drag;
						this.move.velocity += Vector3.ClampMagnitude(vector - base.transform.position, 10f) * TimeHandler.deltaTime * this.move.localForce.magnitude * this.amount;
						this.move.velocity.z = 0f;
						this.move.velocity += Vector3.up * TimeHandler.deltaTime * this.move.gravity * this.move.multiplier;
						if (!this.isOn)
						{
							this.move.simulateGravity++;
						}
						this.isOn = true;
						for (int j = 0; j < this.flicks.Length; j++)
						{
							this.flicks[j].isOn = true;
						}
						this.rot1.target = 90f;
						this.rot2.target = -90f;
						return;
					}
				}
				if (this.isOn)
				{
					this.move.simulateGravity--;
					this.soundHomingCanPlay = true;
				}
				this.isOn = false;
				for (int k = 0; k < this.flicks.Length; k++)
				{
					this.flicks[k].isOn = false;
				}
				this.rot1.target = 90f;
				this.rot2.target = -90f;
			}
		}

		// Token: 0x04000289 RID: 649
		[Header("Sound")]
		public SoundEvent soundHomingFound;

		// Token: 0x0400028A RID: 650
		private SyncProjectile sync;

		// Token: 0x0400028B RID: 651
		private bool soundHomingCanPlay = true;

		// Token: 0x0400028C RID: 652
		[Header("Settings")]
		public float amount = 1f;

		// Token: 0x0400028D RID: 653
		public float scalingDrag = 0.75f;

		// Token: 0x0400028E RID: 654
		public float drag = 0.75f;

		// Token: 0x0400028F RID: 655
		public float spread = 1f;

		// Token: 0x04000290 RID: 656
		private MoveTransform move;

		// Token: 0x04000291 RID: 657
		private bool isOn;

		// Token: 0x04000292 RID: 658
		public RotSpring rot1;

		// Token: 0x04000293 RID: 659
		public RotSpring rot2;

		// Token: 0x04000294 RID: 660
		private FlickerEvent[] flicks;

		// Token: 0x04000295 RID: 661
		private PhotonView view;

		// Token: 0x04000296 RID: 662
		private SoundEvent soundSpawn;

		// Token: 0x04000297 RID: 663
		public static SoundEvent fieldsound;

		// Token: 0x04000298 RID: 664
		private Player closestPlayer;

		// Token: 0x04000299 RID: 665
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.5f, 0);
	}
}
