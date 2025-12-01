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
	// Token: 0x0200000E RID: 14
	public class CriticalHitMono : RayHitPoison
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000033E1 File Offset: 0x000015E1
		private void Start()
		{
			if (base.GetComponentInParent<ProjectileHit>() != null)
			{
				base.GetComponentInParent<ProjectileHit>().bulletCanDealDeamage = false;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003400 File Offset: 0x00001600
		public override HasToReturn DoHitEffect(HitInfo hit)
		{
			if (!hit.transform)
			{
				return 1;
			}
			RayHitPoison[] componentsInChildren = base.transform.root.GetComponentsInChildren<RayHitPoison>();
			ProjectileHit componentInParent = base.GetComponentInParent<ProjectileHit>();
			DamageOverTime component = hit.transform.GetComponent<DamageOverTime>();
			this.rand = new Random();
			Player component2 = hit.transform.GetComponent<Player>();
			if (component2)
			{
				if (component2.data.view.IsMine)
				{
					this.chance = this.rand.Next(1, 101);
				}
				Player[] array = PlayerManager.instance.players.ToArray();
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].data.view.IsMine)
					{
						this.truechance = this.chance;
					}
				}
				if (component && this.truechance >= 51)
				{
					if (!CriticalHitMono.fieldsound)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("critical");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						CriticalHitMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						CriticalHitMono.fieldsound.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
					SoundManager.Instance.PlayAtPosition(CriticalHitMono.fieldsound, base.transform, hit.point, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
					Object.Instantiate<GameObject>(CriticalHitMono.critVisual, base.gameObject.transform.position, Quaternion.identity);
					this.crit = hit.transform.gameObject.AddComponent<CriticalMono>();
					component.TakeDamageOverTime(componentInParent.damage * 2f * base.transform.forward / (float)componentsInChildren.Length, base.transform.position, this.time, this.interval, this.color, this.soundEventDamageOverTime, base.GetComponentInParent<ProjectileHit>().ownWeapon, base.GetComponentInParent<ProjectileHit>().ownPlayer, true);
				}
				else if (component)
				{
					component.TakeDamageOverTime(componentInParent.damage * base.transform.forward / (float)componentsInChildren.Length, base.transform.position, this.time, this.interval, new Color(1f, 1f, 1f, 1f), this.soundEventDamageOverTime, base.GetComponentInParent<ProjectileHit>().ownWeapon, base.GetComponentInParent<ProjectileHit>().ownPlayer, true);
				}
			}
			return 1;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000036D0 File Offset: 0x000018D0
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00003911 File Offset: 0x00001B11
		public static GameObject critVisual
		{
			get
			{
				GameObject result;
				if (CriticalHitMono.critvisual != null)
				{
					result = CriticalHitMono.critvisual;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					CriticalHitMono.critvisual = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					CriticalHitMono.critvisual.name = "E_Critical";
					Object.DontDestroyOnLoad(CriticalHitMono.critvisual);
					ParticleSystem[] componentsInChildren = CriticalHitMono.critvisual.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(0.8f, 1f, 0f, 1f);
					}
					CriticalHitMono.critvisual.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(0.8f, 1f, 0f, 1f), 0f)
					};
					Object.Destroy(CriticalHitMono.critvisual.transform.GetChild(2).gameObject);
					CriticalHitMono.critvisual.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					CriticalHitMono.critvisual.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(CriticalHitMono.critvisual.GetComponent<FollowPlayer>());
					CriticalHitMono.critvisual.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(CriticalHitMono.critvisual.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(CriticalHitMono.critvisual.GetComponent<Explosion>());
					Object.Destroy(CriticalHitMono.critvisual.GetComponent<Explosion_Overpower>());
					Object.Destroy(CriticalHitMono.critvisual.GetComponent<RemoveAfterSeconds>());
					CriticalHitMono.critvisual.AddComponent<CriticalHitMono.CritSpawner>();
					result = CriticalHitMono.critvisual;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003913 File Offset: 0x00001B13
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x04000033 RID: 51
		[Header("Sounds")]
		public SoundEvent soundEventDamageOverTime;

		// Token: 0x04000034 RID: 52
		[Header("Settings")]
		public float time = 0.1f;

		// Token: 0x04000035 RID: 53
		public float interval = 0.1f;

		// Token: 0x04000036 RID: 54
		public Color color = new Color(1f, 1f, 0f, 1f);

		// Token: 0x04000037 RID: 55
		private Random rand;

		// Token: 0x04000038 RID: 56
		private int chance;

		// Token: 0x04000039 RID: 57
		private int truechance;

		// Token: 0x0400003A RID: 58
		public CriticalMono crit;

		// Token: 0x0400003B RID: 59
		private static GameObject critvisual;

		// Token: 0x0400003C RID: 60
		public static SoundEvent fieldsound;

		// Token: 0x0400003D RID: 61
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.8f, 0);

		// Token: 0x02000105 RID: 261
		private class CritSpawner : MonoBehaviour
		{
			// Token: 0x060008CB RID: 2251 RVA: 0x00026120 File Offset: 0x00024320
			private void Start()
			{
				base.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				base.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 1f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "inited", false);
				typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 4f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "startWidth", 0.5f);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
			}
		}
	}
}
