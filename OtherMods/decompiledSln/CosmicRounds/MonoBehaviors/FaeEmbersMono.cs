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
	// Token: 0x02000034 RID: 52
	public class FaeEmbersMono : MonoBehaviour
	{
		// Token: 0x06000130 RID: 304 RVA: 0x00009B64 File Offset: 0x00007D64
		private void Start()
		{
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.data = base.GetComponent<CharacterData>();
			this.gun = base.GetComponent<Gun>();
			this.ResetEffectTimer();
			this.ResetTimer();
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00009BA2 File Offset: 0x00007DA2
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00009BAA File Offset: 0x00007DAA
		public void OnDestroy()
		{
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00009BAC File Offset: 0x00007DAC
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				if (this.player.data.weaponHandler.gun.isReloading)
				{
					if (Time.time >= this.timeOfLastEffect + this.effectCooldown)
					{
						if (!FaeEmbersMono.fieldsound)
						{
							AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("fire");
							SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
							soundContainer.audioClip[0] = audioClip;
							soundContainer.setting.volumeIntensityEnable = true;
							FaeEmbersMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
							FaeEmbersMono.fieldsound.soundContainerArray[0] = soundContainer;
						}
						this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
						SoundManager.Instance.Play(FaeEmbersMono.fieldsound, base.transform, new SoundParameterBase[]
						{
							this.soundParameterIntensity
						});
						Object.Instantiate<GameObject>(FaeEmbersMono.faeVisual, this.player.gameObject.transform.position, Quaternion.identity);
						if (this.effectCooldown > 0.3f)
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
									array[i].transform.GetComponent<HealthHandler>().DoDamage((5f + array[i].data.maxHealth * 0.05f) * Vector2.down, array[i].transform.position, new Color(1f, 0.5f, 1f, 1f), this.player.data.weaponHandler.gameObject, this.player, true, true, true);
								}
								else if (flag)
								{
									array[i].transform.GetComponent<HealthHandler>().DoDamage(5f * Vector2.down, array[i].transform.position, new Color(1f, 0.5f, 1f, 1f), this.player.data.weaponHandler.gameObject, this.player, true, true, true);
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

		// Token: 0x06000134 RID: 308 RVA: 0x00009EFE File Offset: 0x000080FE
		private void ResetTimer()
		{
			this.startTime = Time.time;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00009F0B File Offset: 0x0000810B
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00009F18 File Offset: 0x00008118
		// (set) Token: 0x06000137 RID: 311 RVA: 0x0000A0EF File Offset: 0x000082EF
		public static GameObject faeVisual
		{
			get
			{
				GameObject result;
				if (FaeEmbersMono.faeVisu != null)
				{
					result = FaeEmbersMono.faeVisu;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					FaeEmbersMono.faeVisu = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					FaeEmbersMono.faeVisu.name = "E_Fae";
					Object.DontDestroyOnLoad(FaeEmbersMono.faeVisu);
					ParticleSystem[] componentsInChildren = FaeEmbersMono.faeVisu.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(1f, 0.5f, 1f, 1f);
					}
					Object.Destroy(FaeEmbersMono.faeVisu.transform.GetChild(2).gameObject);
					Object.Destroy(FaeEmbersMono.faeVisu.transform.GetChild(1).GetComponent<LineEffect>().gameObject);
					Object.Destroy(FaeEmbersMono.faeVisu.GetComponent<FollowPlayer>());
					FaeEmbersMono.faeVisu.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(FaeEmbersMono.faeVisu.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(FaeEmbersMono.faeVisu.GetComponent<Explosion>());
					Object.Destroy(FaeEmbersMono.faeVisu.GetComponent<Explosion_Overpower>());
					Object.Destroy(FaeEmbersMono.faeVisu.GetComponent<RemoveAfterSeconds>());
					FaeEmbersMono.faeVisu.AddComponent<FaeEmbersMono.FaeSpawner>();
					result = FaeEmbersMono.faeVisu;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x0400015F RID: 351
		private readonly float maxDistance = 8f;

		// Token: 0x04000160 RID: 352
		public Block block;

		// Token: 0x04000161 RID: 353
		public Player player;

		// Token: 0x04000162 RID: 354
		public CharacterData data;

		// Token: 0x04000163 RID: 355
		public Gun gun;

		// Token: 0x04000164 RID: 356
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000165 RID: 357
		private float effectCooldown = 0.5f;

		// Token: 0x04000166 RID: 358
		private float startTime;

		// Token: 0x04000167 RID: 359
		private float timeOfLastEffect;

		// Token: 0x04000168 RID: 360
		private static GameObject faeVisu;

		// Token: 0x04000169 RID: 361
		public static SoundEvent fieldsound;

		// Token: 0x0400016A RID: 362
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.3f, 0);

		// Token: 0x02000115 RID: 277
		private class FaeSpawner : MonoBehaviour
		{
			// Token: 0x060008F2 RID: 2290 RVA: 0x00027000 File Offset: 0x00025200
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
