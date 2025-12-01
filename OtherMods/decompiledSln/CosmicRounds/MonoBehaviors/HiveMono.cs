using System;
using Sonigon;
using SoundImplementation;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000043 RID: 67
	public class HiveMono : MonoBehaviour
	{
		// Token: 0x06000196 RID: 406 RVA: 0x0000CCB0 File Offset: 0x0000AEB0
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
			this.bee = new Action<BlockTrigger.BlockTriggerType>(this.GetDoBlockAction(this.player, this.block, this.data).Invoke);
			this.block.FirstBlockActionThatDelaysOthers = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(this.block.FirstBlockActionThatDelaysOthers, this.bee);
			this.block.delayOtherActions = true;
			this.gun.ShootPojectileAction = (Action<GameObject>)Delegate.Combine(this.gun.ShootPojectileAction, new Action<GameObject>(this.Attack));
			this.Failsafe = false;
			this.Active = false;
			this.rain = false;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000CE30 File Offset: 0x0000B030
		private void Attack(GameObject projectile)
		{
			if (!this.rain)
			{
				base.GetComponents<HiveBlockMono>();
				if (this.Active)
				{
					SoundManager.Instance.PlayAtPosition(this.soundShoot, SoundManager.Instance.GetTransform(), base.transform);
					this.Failsafe = false;
					this.Active = false;
				}
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000CE82 File Offset: 0x0000B082
		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block, CharacterData data)
		{
			return delegate(BlockTrigger.BlockTriggerType trigger)
			{
				if (!this.rain && trigger != 1 && !this.Active && !this.player.transform.gameObject.GetComponent<HiveBlockMono>())
				{
					this.player.transform.gameObject.AddComponent<HiveBlockMono>();
					this.Active = true;
					this.Failsafe = true;
				}
			};
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000CE90 File Offset: 0x0000B090
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000CE98 File Offset: 0x0000B098
		public void OnDestroy()
		{
			this.block.BlockAction = this.basic;
			this.gun.ShootPojectileAction = this.goon;
			Object.Destroy(this.soundShoot);
			Object.Destroy(this.soundSpawn);
			this.Failsafe = false;
			this.Active = false;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000CEEC File Offset: 0x0000B0EC
		public void Update()
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
					for (int i = 0; i < array.Length; i++)
					{
						array[i].Play();
						array[i].startColor = Color.yellow;
					}
					this.alreadyActivated = true;
					return;
				}
			}
			else if (this.alreadyActivated)
			{
				ParticleSystem[] array2 = this.parts;
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j].Stop();
				}
				this.alreadyActivated = false;
			}
		}

		// Token: 0x040001E7 RID: 487
		private CharacterData data;

		// Token: 0x040001E8 RID: 488
		private Block block;

		// Token: 0x040001E9 RID: 489
		private Player player;

		// Token: 0x040001EA RID: 490
		private Gun gun;

		// Token: 0x040001EB RID: 491
		private Action<BlockTrigger.BlockTriggerType> bee;

		// Token: 0x040001EC RID: 492
		private Action<BlockTrigger.BlockTriggerType> basic;

		// Token: 0x040001ED RID: 493
		private Action<GameObject> goon;

		// Token: 0x040001EE RID: 494
		private Transform particleTransform;

		// Token: 0x040001EF RID: 495
		private SoundEvent soundSpawn;

		// Token: 0x040001F0 RID: 496
		private SoundEvent soundShoot;

		// Token: 0x040001F1 RID: 497
		private bool alreadyActivated;

		// Token: 0x040001F2 RID: 498
		private ParticleSystem[] parts;

		// Token: 0x040001F3 RID: 499
		public bool Active;

		// Token: 0x040001F4 RID: 500
		public bool Failsafe;

		// Token: 0x040001F5 RID: 501
		public bool rain;
	}
}
