using System;
using Photon.Pun;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000021 RID: 33
	public class DriveMono : MonoBehaviour
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x0000683C File Offset: 0x00004A3C
		private void Start()
		{
			this.move = base.GetComponentInParent<MoveTransform>();
			this.flicks = base.GetComponentsInChildren<FlickerEvent>();
			this.view = base.GetComponentInParent<PhotonView>();
			this.sync = base.GetComponentInParent<SyncProjectile>();
			this.sync.active = true;
			this.state = 0;
			this.x = this.move.velocity.x;
			this.y = this.move.velocity.y;
			this.ResetTimer();
			this.detected = false;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000068C5 File Offset: 0x00004AC5
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000068CD File Offset: 0x00004ACD
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000068DC File Offset: 0x00004ADC
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay && base.gameObject.transform.parent != null)
			{
				this.ResetTimer();
				if (this.state == 0)
				{
					this.state = 1;
					this.move.velocity.z = 0f;
					MoveTransform moveTransform = this.move;
					moveTransform.velocity.x = moveTransform.velocity.x * 1f;
					MoveTransform moveTransform2 = this.move;
					moveTransform2.velocity.y = moveTransform2.velocity.y * 1f;
					return;
				}
				if (this.state == 1)
				{
					Player closestPlayer = PlayerManager.instance.GetClosestPlayer(base.transform.position, true);
					Player ownPlayer = base.GetComponentInParent<ProjectileHit>().ownPlayer;
					if (closestPlayer && closestPlayer != ownPlayer && !closestPlayer.data.dead && closestPlayer.data.gameObject.activeInHierarchy)
					{
						this.detected = true;
					}
					bool flag = Vector2.Distance(base.transform.position, closestPlayer.transform.position) <= 20f;
					if (this.detected && flag)
					{
						this.updateDelay = 0.2f;
						if (!DriveMono.fieldsound)
						{
							AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("drive");
							SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
							soundContainer.audioClip[0] = audioClip;
							soundContainer.setting.volumeIntensityEnable = true;
							DriveMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
							DriveMono.fieldsound.soundContainerArray[0] = soundContainer;
						}
						this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
						SoundManager.Instance.Play(DriveMono.fieldsound, base.transform, new SoundParameterBase[]
						{
							this.soundParameterIntensity
						});
						this.state = 2;
						Vector3 vector = closestPlayer.transform.position + base.transform.right * this.move.selectedSpread * Vector3.Distance(base.transform.position, closestPlayer.transform.position);
						float num = Vector3.Angle(base.transform.root.forward, vector - base.transform.position);
						this.move.velocity -= this.move.velocity * num;
						this.move.velocity -= this.move.velocity;
						this.move.velocity += Vector3.ClampMagnitude(vector - base.transform.position, 900f) * TimeHandler.deltaTime * this.move.localForce.magnitude;
						this.move.velocity.z = 0f;
						this.speedX = this.move.velocity.x;
						this.speedY = this.move.velocity.y;
						this.move.simulateGravity--;
						MoveTransform moveTransform3 = this.move;
						moveTransform3.velocity.x = moveTransform3.velocity.x * 0.01f;
						MoveTransform moveTransform4 = this.move;
						moveTransform4.velocity.y = moveTransform4.velocity.y * 0.01f;
						for (int i = 0; i < this.flicks.Length; i++)
						{
							this.flicks[i].isOn = true;
						}
						return;
					}
				}
				else
				{
					if (this.state == 2)
					{
						new Vector3(this.speedX, this.speedY);
						this.move.velocity.x = this.speedX * 21f;
						this.move.velocity.y = this.speedY * 21f;
						this.move.velocity.z = 0f;
						this.state = 3;
						return;
					}
					if (this.state == 3)
					{
						this.updateDelay = 0.5f;
						this.move.simulateGravity = 1;
						this.move.velocity.z = 0f;
						this.state = 3;
					}
				}
			}
		}

		// Token: 0x040000CC RID: 204
		private MoveTransform move;

		// Token: 0x040000CD RID: 205
		private float updateDelay = 0.1f;

		// Token: 0x040000CE RID: 206
		private float startTime;

		// Token: 0x040000CF RID: 207
		private SyncProjectile sync;

		// Token: 0x040000D0 RID: 208
		public int state;

		// Token: 0x040000D1 RID: 209
		public bool detected;

		// Token: 0x040000D2 RID: 210
		private FlickerEvent[] flicks;

		// Token: 0x040000D3 RID: 211
		private PhotonView view;

		// Token: 0x040000D4 RID: 212
		public RotSpring rot1;

		// Token: 0x040000D5 RID: 213
		public RotSpring rot2;

		// Token: 0x040000D6 RID: 214
		public float x;

		// Token: 0x040000D7 RID: 215
		public float y;

		// Token: 0x040000D8 RID: 216
		public static SoundEvent fieldsound;

		// Token: 0x040000D9 RID: 217
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.5f, 0);

		// Token: 0x040000DA RID: 218
		public float speedX;

		// Token: 0x040000DB RID: 219
		public float speedY;
	}
}
