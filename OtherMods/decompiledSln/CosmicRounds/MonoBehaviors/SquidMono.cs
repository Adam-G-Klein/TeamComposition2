using System;
using Photon.Pun;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000080 RID: 128
	public class SquidMono : MonoBehaviour
	{
		// Token: 0x06000337 RID: 823 RVA: 0x00018CF8 File Offset: 0x00016EF8
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

		// Token: 0x06000338 RID: 824 RVA: 0x00018D81 File Offset: 0x00016F81
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00018D89 File Offset: 0x00016F89
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00018D98 File Offset: 0x00016F98
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
						if (!SquidMono.fieldsound)
						{
							AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("squid");
							SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
							soundContainer.audioClip[0] = audioClip;
							soundContainer.setting.volumeIntensityEnable = true;
							SquidMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
							SquidMono.fieldsound.soundContainerArray[0] = soundContainer;
						}
						this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
						SoundManager.Instance.Play(SquidMono.fieldsound, base.transform, new SoundParameterBase[]
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
						this.move.velocity.x = this.speedX * 9f;
						this.move.velocity.y = this.speedY * 9f;
						this.move.velocity.z = 0f;
						this.state = 3;
						return;
					}
					if (this.state == 3)
					{
						this.updateDelay = 0.3f;
						this.move.simulateGravity = 1;
						this.move.velocity.z = 0f;
						this.state = 1;
						this.detected = false;
					}
				}
			}
		}

		// Token: 0x04000413 RID: 1043
		private MoveTransform move;

		// Token: 0x04000414 RID: 1044
		private float updateDelay = 0.1f;

		// Token: 0x04000415 RID: 1045
		private float startTime;

		// Token: 0x04000416 RID: 1046
		private SyncProjectile sync;

		// Token: 0x04000417 RID: 1047
		public int state;

		// Token: 0x04000418 RID: 1048
		public bool detected;

		// Token: 0x04000419 RID: 1049
		private FlickerEvent[] flicks;

		// Token: 0x0400041A RID: 1050
		private PhotonView view;

		// Token: 0x0400041B RID: 1051
		public RotSpring rot1;

		// Token: 0x0400041C RID: 1052
		public RotSpring rot2;

		// Token: 0x0400041D RID: 1053
		public float x;

		// Token: 0x0400041E RID: 1054
		public float y;

		// Token: 0x0400041F RID: 1055
		public static SoundEvent fieldsound;

		// Token: 0x04000420 RID: 1056
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.5f, 0);

		// Token: 0x04000421 RID: 1057
		public float speedX;

		// Token: 0x04000422 RID: 1058
		public float speedY;
	}
}
