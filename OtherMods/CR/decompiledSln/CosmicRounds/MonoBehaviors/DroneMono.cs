using System;
using Photon.Pun;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000015 RID: 21
	public class DroneMono : MonoBehaviour
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00004B38 File Offset: 0x00002D38
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

		// Token: 0x0600006B RID: 107 RVA: 0x00004BAC File Offset: 0x00002DAC
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004BB4 File Offset: 0x00002DB4
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
					this.flag = (Vector2.Distance(base.transform.position, this.closestPlayer.transform.position) <= 30f);
				}
				Player ownPlayer = base.GetComponentInParent<ProjectileHit>().ownPlayer;
				if (!this.closestPlayer || !this.flag || this.closestPlayer.teamID == ownPlayer.teamID || !this.closestPlayer.data.gameObject.activeInHierarchy)
				{
					if (this.isOn)
					{
						this.move.simulateGravity--;
					}
					this.isOn = false;
					for (int i = 0; i < this.flicks.Length; i++)
					{
						this.flicks[i].isOn = false;
					}
					return;
				}
				if (this.closestPlayer.teamID != ownPlayer.teamID && this.closestPlayer && this.flag && !this.closestPlayer.data.dead && this.closestPlayer.data.gameObject.activeInHierarchy)
				{
					Vector3 vector = this.closestPlayer.transform.position + base.transform.right * this.move.selectedSpread * Vector3.Distance(base.transform.position, this.closestPlayer.transform.position) * this.spread;
					float num = Vector3.Angle(base.transform.root.forward, vector - base.transform.position);
					if (num <= 90f)
					{
						this.move.velocity -= this.move.velocity * num * TimeHandler.deltaTime * this.scalingDrag;
						this.move.velocity -= this.move.velocity * TimeHandler.deltaTime * this.drag;
						this.move.velocity += Vector3.ClampMagnitude(vector - base.transform.position, 10f) * TimeHandler.deltaTime * this.move.localForce.magnitude * 1.5f * this.amount;
						this.move.velocity.z = 0f;
						this.move.velocity += Vector3.up * TimeHandler.deltaTime * this.move.gravity * this.move.multiplier * 1.5f;
						if (!this.isOn)
						{
							this.move.simulateGravity++;
							if (this.soundHomingCanPlay)
							{
								this.soundHomingCanPlay = false;
								if (!DroneMono.fieldsound)
								{
									AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("drone");
									SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
									soundContainer.audioClip[0] = audioClip;
									soundContainer.setting.volumeIntensityEnable = true;
									DroneMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
									DroneMono.fieldsound.soundContainerArray[0] = soundContainer;
								}
								this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
								SoundManager.Instance.Play(DroneMono.fieldsound, base.transform, new SoundParameterBase[]
								{
									this.soundParameterIntensity
								});
							}
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

		// Token: 0x04000070 RID: 112
		[Header("Sound")]
		public SoundEvent soundHomingFound;

		// Token: 0x04000071 RID: 113
		private bool soundHomingCanPlay = true;

		// Token: 0x04000072 RID: 114
		[Header("Settings")]
		public float amount = 1f;

		// Token: 0x04000073 RID: 115
		public float scalingDrag = 0.85f;

		// Token: 0x04000074 RID: 116
		private SyncProjectile sync;

		// Token: 0x04000075 RID: 117
		public float drag = 0.85f;

		// Token: 0x04000076 RID: 118
		public float spread = 1f;

		// Token: 0x04000077 RID: 119
		private MoveTransform move;

		// Token: 0x04000078 RID: 120
		private bool isOn;

		// Token: 0x04000079 RID: 121
		public RotSpring rot1;

		// Token: 0x0400007A RID: 122
		public RotSpring rot2;

		// Token: 0x0400007B RID: 123
		private FlickerEvent[] flicks;

		// Token: 0x0400007C RID: 124
		private PhotonView view;

		// Token: 0x0400007D RID: 125
		private SoundEvent soundSpawn;

		// Token: 0x0400007E RID: 126
		public static SoundEvent fieldsound;

		// Token: 0x0400007F RID: 127
		private Player closestPlayer;

		// Token: 0x04000080 RID: 128
		private bool flag;

		// Token: 0x04000081 RID: 129
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.5f, 0);
	}
}
