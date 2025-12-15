using System;
using Sonigon;
using SoundImplementation;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200001A RID: 26
	public class MitosisMono : MonoBehaviour
	{
		// Token: 0x06000084 RID: 132 RVA: 0x0000571C File Offset: 0x0000391C
		public void Start()
		{
			this.data = base.GetComponentInParent<CharacterData>();
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			GameObject addObjectToPlayer = ((GameObject)Resources.Load("0 cards/Empower")).GetComponent<CharacterStatModifiers>().AddObjectToPlayer;
			this.soundSpawn = addObjectToPlayer.GetComponent<Empower>().soundEmpowerSpawn;
			this.soundShoot = addObjectToPlayer.GetComponent<Empower>().addObjectToBullet.GetComponent<SoundUnityEventPlayer>().soundStart;
			GameObject gameObject = addObjectToPlayer.transform.GetChild(0).gameObject;
			this.particleTransform = Object.Instantiate<GameObject>(gameObject, base.transform).transform;
			this.parts = base.GetComponentsInChildren<ParticleSystem>();
			this.gun = this.data.weaponHandler.gun;
			this.basic = this.block.FirstBlockActionThatDelaysOthers;
			this.goon = this.gun.ShootPojectileAction;
			this.mitosis = new Action<BlockTrigger.BlockTriggerType>(this.GetDoBlockAction(this.player, this.block, this.data).Invoke);
			this.block.FirstBlockActionThatDelaysOthers = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(this.block.FirstBlockActionThatDelaysOthers, this.mitosis);
			this.block.delayOtherActions = true;
			this.gun.ShootPojectileAction = (Action<GameObject>)Delegate.Combine(this.gun.ShootPojectileAction, new Action<GameObject>(this.Attack));
			this.Failsafe = false;
			this.Active = false;
			this.rain = false;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000589C File Offset: 0x00003A9C
		private void Attack(GameObject projectile)
		{
			if (!this.rain)
			{
				base.GetComponents<MitosisBlockMono>();
				if (this.Active)
				{
					SoundManager.Instance.PlayAtPosition(this.soundShoot, SoundManager.Instance.GetTransform(), base.transform);
					this.Failsafe = false;
					this.Active = false;
				}
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000058EE File Offset: 0x00003AEE
		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block, CharacterData data)
		{
			return delegate(BlockTrigger.BlockTriggerType trigger)
			{
				if (!this.rain && trigger != 1 && !this.Active && !this.player.transform.gameObject.GetComponent<MitosisBlockMono>())
				{
					this.player.transform.gameObject.AddComponent<MitosisBlockMono>();
					this.Active = true;
					this.Failsafe = true;
				}
			};
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000058FC File Offset: 0x00003AFC
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00005904 File Offset: 0x00003B04
		public void OnDestroy()
		{
			this.block.BlockAction = this.basic;
			this.gun.ShootPojectileAction = this.goon;
			Object.Destroy(this.soundShoot);
			Object.Destroy(this.soundSpawn);
			this.Failsafe = false;
			this.Active = false;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00005958 File Offset: 0x00003B58
		public void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				int i = 0;
				if (this.player.data.currentCards != null)
				{
					while (i <= this.player.data.currentCards.Count - 1)
					{
						if (this.player.data.currentCards[i].cardName == "Mitosis")
						{
							this.numcheck++;
						}
						i++;
					}
				}
				if (this.numcheck > 0)
				{
					if (this.Active)
					{
						Transform transform = this.data.weaponHandler.gun.transform;
						this.particleTransform.position = transform.position;
						this.particleTransform.rotation = transform.rotation;
						ParticleSystem[] array = this.parts;
						if (!this.alreadyActivated)
						{
							SoundManager.Instance.PlayAtPosition(this.soundSpawn, SoundManager.Instance.GetTransform(), base.transform);
							array = this.parts;
							for (int j = 0; j < array.Length; j++)
							{
								array[j].Play();
								array[j].startColor = Color.green;
							}
							this.alreadyActivated = true;
							return;
						}
					}
					else if (this.alreadyActivated)
					{
						ParticleSystem[] array2 = this.parts;
						for (int k = 0; k < array2.Length; k++)
						{
							array2[k].Stop();
						}
						this.alreadyActivated = false;
						return;
					}
				}
				else
				{
					this.Destroy();
				}
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005AD9 File Offset: 0x00003CD9
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x04000092 RID: 146
		private CharacterData data;

		// Token: 0x04000093 RID: 147
		private Block block;

		// Token: 0x04000094 RID: 148
		private Player player;

		// Token: 0x04000095 RID: 149
		private Gun gun;

		// Token: 0x04000096 RID: 150
		private Action<BlockTrigger.BlockTriggerType> mitosis;

		// Token: 0x04000097 RID: 151
		private Action<BlockTrigger.BlockTriggerType> basic;

		// Token: 0x04000098 RID: 152
		private Action<GameObject> goon;

		// Token: 0x04000099 RID: 153
		private Transform particleTransform;

		// Token: 0x0400009A RID: 154
		private SoundEvent soundSpawn;

		// Token: 0x0400009B RID: 155
		private SoundEvent soundShoot;

		// Token: 0x0400009C RID: 156
		private bool alreadyActivated;

		// Token: 0x0400009D RID: 157
		private ParticleSystem[] parts;

		// Token: 0x0400009E RID: 158
		public bool Active;

		// Token: 0x0400009F RID: 159
		public bool Failsafe;

		// Token: 0x040000A0 RID: 160
		public bool rain;

		// Token: 0x040000A1 RID: 161
		private readonly float updateDelay = 0.1f;

		// Token: 0x040000A2 RID: 162
		private float startTime;

		// Token: 0x040000A3 RID: 163
		public int numcheck;
	}
}
