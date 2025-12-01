using System;
using Sonigon;
using Sonigon.Internal;
using SoundImplementation;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000063 RID: 99
	public class HaloMono : MonoBehaviour
	{
		// Token: 0x0600026B RID: 619 RVA: 0x0001342C File Offset: 0x0001162C
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

		// Token: 0x0600026C RID: 620 RVA: 0x000135AC File Offset: 0x000117AC
		private void Attack(GameObject projectile)
		{
			if (!this.rain)
			{
				base.GetComponents<HaloBlockMono>();
				if (this.Active)
				{
					SoundManager.Instance.PlayAtPosition(this.soundShoot, SoundManager.Instance.GetTransform(), base.transform);
					this.Failsafe = false;
					this.Active = false;
					if (!HaloMono.fieldsound)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("holy");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						HaloMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						HaloMono.fieldsound.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity.intensity = base.transform.localScale.x * (Optionshandler.vol_Master * Optionshandler.vol_Sfx * CR.globalVolMute.Value * 0.9f);
					SoundManager.Instance.Play(HaloMono.fieldsound, base.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
				}
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x000136AE File Offset: 0x000118AE
		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block, CharacterData data)
		{
			return delegate(BlockTrigger.BlockTriggerType trigger)
			{
				if (!this.rain && trigger != 1 && !this.Active && !this.player.transform.gameObject.GetComponent<HaloBlockMono>())
				{
					this.player.transform.gameObject.AddComponent<HaloBlockMono>();
					this.Active = true;
					this.Failsafe = true;
					if (!HaloMono.fieldsound)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("holy");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						HaloMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						HaloMono.fieldsound.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.5f * CR.globalVolMute.Value;
					SoundManager.Instance.Play(HaloMono.fieldsound, base.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
				}
			};
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000136BC File Offset: 0x000118BC
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000136C4 File Offset: 0x000118C4
		public void OnDestroy()
		{
			this.block.BlockAction = this.basic;
			this.gun.ShootPojectileAction = this.goon;
			Object.Destroy(this.soundShoot);
			Object.Destroy(this.soundSpawn);
			this.Failsafe = false;
			this.Active = false;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00013718 File Offset: 0x00011918
		public void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Halo")
					{
						this.numcheck++;
					}
				}
				if (this.numcheck > 0)
				{
					this.ResetTimer();
				}
				else
				{
					this.Destroy();
				}
			}
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
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00013872 File Offset: 0x00011A72
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x040002EE RID: 750
		private readonly float updateDelay = 0.1f;

		// Token: 0x040002EF RID: 751
		private float startTime;

		// Token: 0x040002F0 RID: 752
		private int numcheck;

		// Token: 0x040002F1 RID: 753
		private CharacterData data;

		// Token: 0x040002F2 RID: 754
		private Block block;

		// Token: 0x040002F3 RID: 755
		private Player player;

		// Token: 0x040002F4 RID: 756
		private Gun gun;

		// Token: 0x040002F5 RID: 757
		private Action<BlockTrigger.BlockTriggerType> bee;

		// Token: 0x040002F6 RID: 758
		private Action<BlockTrigger.BlockTriggerType> basic;

		// Token: 0x040002F7 RID: 759
		private Action<GameObject> goon;

		// Token: 0x040002F8 RID: 760
		private Transform particleTransform;

		// Token: 0x040002F9 RID: 761
		private SoundEvent soundSpawn;

		// Token: 0x040002FA RID: 762
		private SoundEvent soundShoot;

		// Token: 0x040002FB RID: 763
		private bool alreadyActivated;

		// Token: 0x040002FC RID: 764
		private ParticleSystem[] parts;

		// Token: 0x040002FD RID: 765
		public bool Active;

		// Token: 0x040002FE RID: 766
		public bool Failsafe;

		// Token: 0x040002FF RID: 767
		public bool rain;

		// Token: 0x04000300 RID: 768
		public static SoundEvent fieldsound;

		// Token: 0x04000301 RID: 769
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.3f, 0);
	}
}
