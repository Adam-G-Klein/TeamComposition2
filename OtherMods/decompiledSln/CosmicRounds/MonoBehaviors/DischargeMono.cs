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
	// Token: 0x02000074 RID: 116
	public class DischargeMono : MonoBehaviour
	{
		// Token: 0x060002E2 RID: 738 RVA: 0x00017048 File Offset: 0x00015248
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

		// Token: 0x060002E3 RID: 739 RVA: 0x00017098 File Offset: 0x00015298
		public void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				this.ResetTimer();
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Discharge")
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
					if (!DischargeMono.fieldsound)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("taser");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						DischargeMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						DischargeMono.fieldsound.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx * CR.globalVolMute.Value;
					SoundManager.Instance.Play(DischargeMono.fieldsound, base.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
					Object.Instantiate<GameObject>(DischargeMono.dischargeVisual, this.player.transform.position, Quaternion.identity);
					Vector2 vector = this.block.transform.position;
					Player[] array = PlayerManager.instance.players.ToArray();
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j].teamID != this.player.teamID && (Vector2.Distance(vector, array[j].transform.position) < this.maxDistance && PlayerManager.instance.CanSeePlayer(this.player.transform.position, array[j]).canSee))
						{
							array[j].gameObject.GetComponent<CharacterData>();
							Player component = array[j].gameObject.GetComponent<Player>();
							array[j].gameObject.GetComponent<HealthHandler>();
							component.data.healthHandler.TakeDamageOverTime(component.data.maxHealth / 4f * Vector2.down, component.transform.position, 3f, 0.25f, new Color(1f, 1f, 0f, 1f), this.player.data.weaponHandler.gameObject, this.player, true);
						}
					}
					this.able = true;
				}
				if (Time.time >= this.timeOfLastEffect + this.effectCooldown && !this.player.data.weaponHandler.gun.isReloading && this.able)
				{
					if (!DischargeMono.fieldsound)
					{
						AudioClip audioClip2 = CR.ArtAsset.LoadAsset<AudioClip>("taser");
						SoundContainer soundContainer2 = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer2.audioClip[0] = audioClip2;
						soundContainer2.setting.volumeIntensityEnable = true;
						DischargeMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						DischargeMono.fieldsound.soundContainerArray[0] = soundContainer2;
					}
					this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx * CR.globalVolMute.Value;
					SoundManager.Instance.Play(DischargeMono.fieldsound, base.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
					Object.Instantiate<GameObject>(DischargeMono.dischargeVisual, this.player.transform.position, Quaternion.identity);
					Vector2 vector2 = this.block.transform.position;
					Player[] array2 = PlayerManager.instance.players.ToArray();
					for (int k = 0; k < array2.Length; k++)
					{
						if (array2[k].teamID != this.player.teamID && (Vector2.Distance(vector2, array2[k].transform.position) < this.maxDistance && PlayerManager.instance.CanSeePlayer(this.player.transform.position, array2[k]).canSee))
						{
							array2[k].gameObject.GetComponent<CharacterData>();
							Player component2 = array2[k].gameObject.GetComponent<Player>();
							array2[k].gameObject.GetComponent<HealthHandler>();
							component2.data.healthHandler.TakeDamageOverTime(component2.data.maxHealth / 4f * Vector2.down, component2.transform.position, 3f, 0.25f, new Color(1f, 1f, 0f, 1f), this.player.data.weaponHandler.gameObject, this.player, true);
						}
					}
					this.ResetEffectTimer();
					this.able = false;
				}
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00017605 File Offset: 0x00015805
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00017619 File Offset: 0x00015819
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00017626 File Offset: 0x00015826
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00017630 File Offset: 0x00015830
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x00017849 File Offset: 0x00015A49
		public static GameObject dischargeVisual
		{
			get
			{
				GameObject result;
				if (DischargeMono.dischargeVisu != null)
				{
					result = DischargeMono.dischargeVisu;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					DischargeMono.dischargeVisu = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					DischargeMono.dischargeVisu.name = "E_Discharge";
					Object.DontDestroyOnLoad(DischargeMono.dischargeVisu);
					ParticleSystem[] componentsInChildren = DischargeMono.dischargeVisu.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = Color.yellow;
					}
					DischargeMono.dischargeVisu.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(Color.yellow, 0f)
					};
					Object.Destroy(DischargeMono.dischargeVisu.transform.GetChild(2).gameObject);
					DischargeMono.dischargeVisu.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					DischargeMono.dischargeVisu.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(DischargeMono.dischargeVisu.GetComponent<FollowPlayer>());
					DischargeMono.dischargeVisu.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(DischargeMono.dischargeVisu.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(DischargeMono.dischargeVisu.GetComponent<Explosion>());
					Object.Destroy(DischargeMono.dischargeVisu.GetComponent<Explosion_Overpower>());
					Object.Destroy(DischargeMono.dischargeVisu.GetComponent<RemoveAfterSeconds>());
					DischargeMono.dischargeVisu.AddComponent<DischargeMono.DischargeSpawner>();
					result = DischargeMono.dischargeVisu;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x040003A4 RID: 932
		private CharacterData data;

		// Token: 0x040003A5 RID: 933
		private Block block;

		// Token: 0x040003A6 RID: 934
		public Player player;

		// Token: 0x040003A7 RID: 935
		private Gun gun;

		// Token: 0x040003A8 RID: 936
		public bool able;

		// Token: 0x040003A9 RID: 937
		private readonly float updateDelay = 0.1f;

		// Token: 0x040003AA RID: 938
		private readonly float effectCooldown;

		// Token: 0x040003AB RID: 939
		private float timeOfLastEffect;

		// Token: 0x040003AC RID: 940
		private float startTime;

		// Token: 0x040003AD RID: 941
		public int numcheck;

		// Token: 0x040003AE RID: 942
		public static SoundEvent fieldsound;

		// Token: 0x040003AF RID: 943
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.2f, 0);

		// Token: 0x040003B0 RID: 944
		private readonly float maxDistance = 10f;

		// Token: 0x040003B1 RID: 945
		private static GameObject dischargeVisu;

		// Token: 0x02000141 RID: 321
		private class DischargeSpawner : MonoBehaviour
		{
			// Token: 0x06000964 RID: 2404 RVA: 0x00029878 File Offset: 0x00027A78
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
