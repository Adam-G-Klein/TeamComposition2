using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Sonigon;
using Sonigon.Internal;
using SoundImplementation;
using UnboundLib;
using UnboundLib.Utils;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200004A RID: 74
	public class HeartburnMono : MonoBehaviour
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x0000E7B1 File Offset: 0x0000C9B1
		private void Start()
		{
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.data = base.GetComponent<CharacterData>();
			this.gun = base.GetComponent<Gun>();
			this.ResetEffectTimer();
			this.ResetTimer();
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000E7EF File Offset: 0x0000C9EF
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000E7F7 File Offset: 0x0000C9F7
		public void OnDestroy()
		{
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000E7FC File Offset: 0x0000C9FC
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				if (this.player.data.weaponHandler.gun.isReloading)
				{
					if (Time.time >= this.timeOfLastEffect + this.effectCooldown)
					{
						if (!HeartburnMono.fieldsound)
						{
							AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("fire");
							SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
							soundContainer.audioClip[0] = audioClip;
							soundContainer.setting.volumeIntensityEnable = true;
							HeartburnMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
							HeartburnMono.fieldsound.soundContainerArray[0] = soundContainer;
						}
						this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.3f * CR.globalVolMute.Value;
						SoundManager.Instance.Play(HeartburnMono.fieldsound, base.transform, new SoundParameterBase[]
						{
							this.soundParameterIntensity
						});
						Object.Instantiate<GameObject>(HeartburnMono.burnVisual, this.player.gameObject.transform.position, Quaternion.identity);
						float num = this.player.data.maxHealth * 0.02f;
						this.player.data.healthHandler.Heal(num);
						if (this.effectCooldown > 0.15f)
						{
							this.effectCooldown -= 0.05f;
						}
						Vector2 vector = this.player.transform.position;
						Player[] array = PlayerManager.instance.players.ToArray();
						for (int i = 0; i < array.Length; i++)
						{
							if (array[i].teamID != this.player.teamID)
							{
								bool flag = Vector2.Distance(vector, array[i].transform.position) < this.maxDistance && PlayerManager.instance.CanSeePlayer(this.player.transform.position, array[i]).canSee;
								if (Vector2.Distance(vector, array[i].transform.position) < this.maxDistance * 0.5f && PlayerManager.instance.CanSeePlayer(this.player.transform.position, array[i]).canSee)
								{
									array[i].transform.GetComponent<HealthHandler>().DoDamage((7f + array[i].data.maxHealth * 0.05f) * Vector2.down, array[i].transform.position, new Color(1f, 0.2f, 0.2f, 1f), this.player.data.weaponHandler.gameObject, this.player, true, true, true);
								}
								else if (flag)
								{
									array[i].transform.GetComponent<HealthHandler>().DoDamage(7f * Vector2.down, array[i].transform.position, new Color(1f, 0.2f, 0.2f, 1f), this.player.data.weaponHandler.gameObject, this.player, true, true, true);
								}
							}
						}
						this.ResetEffectTimer();
					}
				}
				else
				{
					this.effectCooldown = 0.5f;
				}
				this.ResetTimer();
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000EB7F File Offset: 0x0000CD7F
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000EB8C File Offset: 0x0000CD8C
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000EB9C File Offset: 0x0000CD9C
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x0000ED73 File Offset: 0x0000CF73
		public static GameObject burnVisual
		{
			get
			{
				GameObject result;
				if (HeartburnMono.burnVisu != null)
				{
					result = HeartburnMono.burnVisu;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					HeartburnMono.burnVisu = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					HeartburnMono.burnVisu.name = "E_Heartburn";
					Object.DontDestroyOnLoad(HeartburnMono.burnVisu);
					ParticleSystem[] componentsInChildren = HeartburnMono.burnVisu.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(1f, 0.2f, 0.2f, 1f);
					}
					Object.Destroy(HeartburnMono.burnVisu.transform.GetChild(2).gameObject);
					Object.Destroy(HeartburnMono.burnVisu.transform.GetChild(1).GetComponent<LineEffect>().gameObject);
					Object.Destroy(HeartburnMono.burnVisu.GetComponent<FollowPlayer>());
					HeartburnMono.burnVisu.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(HeartburnMono.burnVisu.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(HeartburnMono.burnVisu.GetComponent<Explosion>());
					Object.Destroy(HeartburnMono.burnVisu.GetComponent<Explosion_Overpower>());
					Object.Destroy(HeartburnMono.burnVisu.GetComponent<RemoveAfterSeconds>());
					HeartburnMono.burnVisu.AddComponent<HeartburnMono.BurnSpawner>();
					result = HeartburnMono.burnVisu;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x0400022E RID: 558
		private readonly float maxDistance = 8f;

		// Token: 0x0400022F RID: 559
		public Block block;

		// Token: 0x04000230 RID: 560
		public Player player;

		// Token: 0x04000231 RID: 561
		public CharacterData data;

		// Token: 0x04000232 RID: 562
		public Gun gun;

		// Token: 0x04000233 RID: 563
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000234 RID: 564
		private float effectCooldown = 0.4f;

		// Token: 0x04000235 RID: 565
		private float startTime;

		// Token: 0x04000236 RID: 566
		private float timeOfLastEffect;

		// Token: 0x04000237 RID: 567
		private static GameObject burnVisu;

		// Token: 0x04000238 RID: 568
		public static SoundEvent fieldsound;

		// Token: 0x04000239 RID: 569
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.3f, 0);

		// Token: 0x02000128 RID: 296
		private class BurnSpawner : MonoBehaviour
		{
			// Token: 0x06000927 RID: 2343 RVA: 0x00028698 File Offset: 0x00026898
			private void Start()
			{
				base.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				base.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 3f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "inited", false);
				typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 4f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "startWidth", 1f);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
			}
		}
	}
}
