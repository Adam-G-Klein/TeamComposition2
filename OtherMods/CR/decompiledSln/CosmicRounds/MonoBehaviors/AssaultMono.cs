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
	// Token: 0x02000072 RID: 114
	public class AssaultMono : MonoBehaviour
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x000161A4 File Offset: 0x000143A4
		public void Start()
		{
			this.data = base.GetComponentInParent<CharacterData>();
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.gun = base.GetComponent<Gun>();
			this.able = false;
			this.ResetTimer();
			this.ResetEffectTimer();
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000161F4 File Offset: 0x000143F4
		public void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Assault")
					{
						this.numcheck++;
					}
				}
				if (this.numcheck < 1)
				{
					Object.Destroy(this);
					return;
				}
				if (Time.time >= this.timeOfLastEffect + this.effectCooldown && this.player.data.weaponHandler.gun.isReloading && !this.able)
				{
					if (!AssaultMono.fieldsound)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("stun");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						AssaultMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						AssaultMono.fieldsound.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx * CR.globalVolMute.Value;
					SoundManager.Instance.Play(AssaultMono.fieldsound, base.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
					Object.Instantiate<GameObject>(AssaultMono.assaultVisual, this.player.transform.position, Quaternion.identity);
					Vector2 vector = this.block.transform.position;
					Player[] array = PlayerManager.instance.players.ToArray();
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j].teamID != this.player.teamID && (Vector2.Distance(vector, array[j].transform.position) < this.maxDistance && PlayerManager.instance.CanSeePlayer(this.player.transform.position, array[j]).canSee))
						{
							CharacterData component = array[j].gameObject.GetComponent<CharacterData>();
							Player component2 = array[j].gameObject.GetComponent<Player>();
							array[j].gameObject.GetComponent<HealthHandler>();
							component2.data.healthHandler.DoDamage((1f + component2.data.maxHealth / 3f) * Vector2.down, component2.transform.position, new Color(1f, 0.2f, 0f, 1f), this.player.data.weaponHandler.gameObject, this.player, false, true, true);
							component.stats.RPCA_AddSlow(0.5f, false);
						}
					}
					this.ResetEffectTimer();
					this.able = true;
				}
				if (Time.time >= this.timeOfLastEffect + this.effectCooldown && !this.player.data.weaponHandler.gun.isReloading && this.able)
				{
					if (!AssaultMono.fieldsound)
					{
						AudioClip audioClip2 = CR.ArtAsset.LoadAsset<AudioClip>("stun");
						SoundContainer soundContainer2 = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer2.audioClip[0] = audioClip2;
						soundContainer2.setting.volumeIntensityEnable = true;
						AssaultMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						AssaultMono.fieldsound.soundContainerArray[0] = soundContainer2;
					}
					this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx * CR.globalVolMute.Value;
					SoundManager.Instance.Play(AssaultMono.fieldsound, base.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
					Object.Instantiate<GameObject>(AssaultMono.assaultVisual, this.player.transform.position, Quaternion.identity);
					Vector2 vector2 = this.block.transform.position;
					Player[] array2 = PlayerManager.instance.players.ToArray();
					for (int k = 0; k < array2.Length; k++)
					{
						if (array2[k].teamID != this.player.teamID && (Vector2.Distance(vector2, array2[k].transform.position) < this.maxDistance && PlayerManager.instance.CanSeePlayer(this.player.transform.position, array2[k]).canSee))
						{
							CharacterData component3 = array2[k].gameObject.GetComponent<CharacterData>();
							Player component4 = array2[k].gameObject.GetComponent<Player>();
							array2[k].gameObject.GetComponent<HealthHandler>();
							component4.data.healthHandler.DoDamage((1f + component4.data.maxHealth / 3f) * Vector2.down, component4.transform.position, new Color(1f, 0.2f, 0f, 1f), this.player.data.weaponHandler.gameObject, this.player, false, true, true);
							component3.stats.RPCA_AddSlow(0.5f, false);
						}
					}
					this.ResetEffectTimer();
					this.able = false;
				}
			}
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00016781 File Offset: 0x00014981
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00016795 File Offset: 0x00014995
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x000167A2 File Offset: 0x000149A2
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x000167AC File Offset: 0x000149AC
		// (set) Token: 0x060002DA RID: 730 RVA: 0x000169E3 File Offset: 0x00014BE3
		public static GameObject assaultVisual
		{
			get
			{
				GameObject result;
				if (AssaultMono.assaultVisu != null)
				{
					result = AssaultMono.assaultVisu;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					AssaultMono.assaultVisu = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					AssaultMono.assaultVisu.name = "E_Assault";
					Object.DontDestroyOnLoad(AssaultMono.assaultVisu);
					ParticleSystem[] componentsInChildren = AssaultMono.assaultVisu.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(1f, 0.5f, 0f);
					}
					AssaultMono.assaultVisu.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(1f, 0.5f, 0f), 0f)
					};
					Object.Destroy(AssaultMono.assaultVisu.transform.GetChild(2).gameObject);
					AssaultMono.assaultVisu.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					AssaultMono.assaultVisu.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(AssaultMono.assaultVisu.GetComponent<FollowPlayer>());
					AssaultMono.assaultVisu.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(AssaultMono.assaultVisu.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(AssaultMono.assaultVisu.GetComponent<Explosion>());
					Object.Destroy(AssaultMono.assaultVisu.GetComponent<Explosion_Overpower>());
					Object.Destroy(AssaultMono.assaultVisu.GetComponent<RemoveAfterSeconds>());
					AssaultMono.assaultVisu.AddComponent<AssaultMono.AssaualtSpawner>();
					result = AssaultMono.assaultVisu;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x04000389 RID: 905
		private CharacterData data;

		// Token: 0x0400038A RID: 906
		private Block block;

		// Token: 0x0400038B RID: 907
		public Player player;

		// Token: 0x0400038C RID: 908
		private Gun gun;

		// Token: 0x0400038D RID: 909
		public bool able;

		// Token: 0x0400038E RID: 910
		private readonly float updateDelay = 0.1f;

		// Token: 0x0400038F RID: 911
		private readonly float effectCooldown;

		// Token: 0x04000390 RID: 912
		private float timeOfLastEffect;

		// Token: 0x04000391 RID: 913
		private float startTime;

		// Token: 0x04000392 RID: 914
		public int numcheck;

		// Token: 0x04000393 RID: 915
		public static SoundEvent fieldsound;

		// Token: 0x04000394 RID: 916
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.2f, 0);

		// Token: 0x04000395 RID: 917
		private readonly float maxDistance = 10f;

		// Token: 0x04000396 RID: 918
		private static GameObject assaultVisu;

		// Token: 0x0200013F RID: 319
		private class AssaualtSpawner : MonoBehaviour
		{
			// Token: 0x0600095F RID: 2399 RVA: 0x0002973C File Offset: 0x0002793C
			private void Start()
			{
				base.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				base.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 1.5f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "inited", false);
				typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 5.5f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "startWidth", 0.5f);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
			}
		}
	}
}
