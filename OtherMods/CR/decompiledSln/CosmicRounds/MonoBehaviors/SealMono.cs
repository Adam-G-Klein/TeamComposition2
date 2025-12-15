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
	// Token: 0x0200006F RID: 111
	public class SealMono : RayHitEffect
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x00015672 File Offset: 0x00013872
		private void Start()
		{
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00015674 File Offset: 0x00013874
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0001567C File Offset: 0x0001387C
		public override HasToReturn DoHitEffect(HitInfo hit)
		{
			if (!hit.transform)
			{
				return 1;
			}
			base.GetComponentInParent<ProjectileHit>();
			SealSealMono component = hit.transform.GetComponent<SealSealMono>();
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
				if ((float)this.truechance <= 50f && component == null)
				{
					hit.transform.gameObject.AddComponent<SealSealMono>();
					PlayerManager.instance.players.ToArray();
					if (!SealMono.fieldsound)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("syphon");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						SealMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						SealMono.fieldsound.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
					SoundManager.Instance.Play(SealMono.fieldsound, base.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
					Object.Instantiate<GameObject>(SealMono.sealVisual, base.gameObject.transform.position, Quaternion.identity);
				}
			}
			return 1;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00015840 File Offset: 0x00013A40
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x00015A81 File Offset: 0x00013C81
		public static GameObject sealVisual
		{
			get
			{
				GameObject result;
				if (SealMono.seavisual != null)
				{
					result = SealMono.seavisual;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					SealMono.seavisual = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					SealMono.seavisual.name = "E_Seal";
					Object.DontDestroyOnLoad(SealMono.seavisual);
					ParticleSystem[] componentsInChildren = SealMono.seavisual.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(1f, 0.1f, 0f, 1f);
					}
					SealMono.seavisual.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(1f, 0.1f, 0f, 1f), 0f)
					};
					Object.Destroy(SealMono.seavisual.transform.GetChild(2).gameObject);
					SealMono.seavisual.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					SealMono.seavisual.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(SealMono.seavisual.GetComponent<FollowPlayer>());
					SealMono.seavisual.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(SealMono.seavisual.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(SealMono.seavisual.GetComponent<Explosion>());
					Object.Destroy(SealMono.seavisual.GetComponent<Explosion_Overpower>());
					Object.Destroy(SealMono.seavisual.GetComponent<RemoveAfterSeconds>());
					SealMono.seavisual.AddComponent<SealMono.SealSpawner>();
					result = SealMono.seavisual;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x04000370 RID: 880
		private Random rand;

		// Token: 0x04000371 RID: 881
		private int chance = 100;

		// Token: 0x04000372 RID: 882
		private int truechance = 100;

		// Token: 0x04000373 RID: 883
		private static GameObject seavisual;

		// Token: 0x04000374 RID: 884
		public static SoundEvent fieldsound;

		// Token: 0x04000375 RID: 885
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.6f, 0);

		// Token: 0x0200013D RID: 317
		private class SealSpawner : MonoBehaviour
		{
			// Token: 0x0600095A RID: 2394 RVA: 0x00029600 File Offset: 0x00027800
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
