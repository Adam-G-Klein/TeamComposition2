using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Sonigon;
using Sonigon.Internal;
using SoundImplementation;
using UnboundLib;
using UnboundLib.Networking;
using UnboundLib.Utils;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000011 RID: 17
	public class SyphonMono : RayHitEffect
	{
		// Token: 0x0600004A RID: 74 RVA: 0x00003E2D File Offset: 0x0000202D
		private void Start()
		{
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003E2F File Offset: 0x0000202F
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003E38 File Offset: 0x00002038
		public override HasToReturn DoHitEffect(HitInfo hit)
		{
			if (!hit.transform)
			{
				return 1;
			}
			ProjectileHit componentInParent = base.GetComponentInParent<ProjectileHit>();
			hit.transform.GetComponent<SilenceHandler>();
			this.rand = new Random();
			Player component = hit.transform.GetComponent<Player>();
			if (component && component.data.view.IsMine)
			{
				this.chance = this.rand.Next(1, 101);
				NetworkingManager.RPC(typeof(SyphonMono), "SyncChance", new object[]
				{
					component.playerID,
					componentInParent.damage,
					this.chance
				});
			}
			return 1;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00003EF4 File Offset: 0x000020F4
		[UnboundRPC]
		public static void SyncChance(int playerID, float damage, int chance = 0)
		{
			Player player = (Player)typeof(PlayerManager).InvokeMember("GetPlayerWithID", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, PlayerManager.instance, new object[]
			{
				playerID
			});
			if (chance >= 50)
			{
				if (!SyphonMono.fieldsound)
				{
					AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("syphon");
					SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
					soundContainer.audioClip[0] = audioClip;
					soundContainer.setting.volumeIntensityEnable = true;
					SyphonMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
					SyphonMono.fieldsound.soundContainerArray[0] = soundContainer;
				}
				SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.6f, 0);
				soundParameterIntensity.intensity = Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
				SoundManager.Instance.Play(SyphonMono.fieldsound, player.transform, new SoundParameterBase[]
				{
					soundParameterIntensity
				});
				if (damage >= 21f)
				{
					player.data.silenceHandler.RPCA_AddSilence(1.5f);
				}
				else
				{
					player.data.silenceHandler.RPCA_AddSilence(0.8f);
				}
				Object.Instantiate<GameObject>(SyphonMono.syphonVisual, player.gameObject.transform.position, Quaternion.identity);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600004E RID: 78 RVA: 0x0000402C File Offset: 0x0000222C
		// (set) Token: 0x0600004F RID: 79 RVA: 0x0000426D File Offset: 0x0000246D
		public static GameObject syphonVisual
		{
			get
			{
				GameObject result;
				if (SyphonMono.syphvisual != null)
				{
					result = SyphonMono.syphvisual;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					SyphonMono.syphvisual = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					SyphonMono.syphvisual.name = "E_Syphon";
					Object.DontDestroyOnLoad(SyphonMono.syphvisual);
					ParticleSystem[] componentsInChildren = SyphonMono.syphvisual.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(1f, 0.3f, 0.8f, 1f);
					}
					SyphonMono.syphvisual.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(1f, 0.3f, 0.8f, 1f), 0f)
					};
					Object.Destroy(SyphonMono.syphvisual.transform.GetChild(2).gameObject);
					SyphonMono.syphvisual.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					SyphonMono.syphvisual.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(SyphonMono.syphvisual.GetComponent<FollowPlayer>());
					SyphonMono.syphvisual.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(SyphonMono.syphvisual.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(SyphonMono.syphvisual.GetComponent<Explosion>());
					Object.Destroy(SyphonMono.syphvisual.GetComponent<Explosion_Overpower>());
					Object.Destroy(SyphonMono.syphvisual.GetComponent<RemoveAfterSeconds>());
					SyphonMono.syphvisual.AddComponent<SyphonMono.SyphSpawner>();
					result = SyphonMono.syphvisual;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x04000048 RID: 72
		private Random rand;

		// Token: 0x04000049 RID: 73
		public int chance = 100;

		// Token: 0x0400004A RID: 74
		private int truechance = 100;

		// Token: 0x0400004B RID: 75
		private static GameObject syphvisual;

		// Token: 0x0400004C RID: 76
		public static SoundEvent fieldsound;

		// Token: 0x0400004D RID: 77
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.6f, 0);

		// Token: 0x02000108 RID: 264
		private class SyphSpawner : MonoBehaviour
		{
			// Token: 0x060008D3 RID: 2259 RVA: 0x000262A4 File Offset: 0x000244A4
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
