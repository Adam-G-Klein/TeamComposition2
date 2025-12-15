using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using ModdingUtils.RoundsEffects;
using Sonigon;
using Sonigon.Internal;
using SoundImplementation;
using UnboundLib;
using UnboundLib.Utils;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x02000037 RID: 55
	public class PulsarMono : HitSurfaceEffect
	{
		// Token: 0x06000144 RID: 324 RVA: 0x0000A3AE File Offset: 0x000085AE
		private void Awake()
		{
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.data = base.GetComponent<CharacterData>();
			this.gun = base.GetComponent<Gun>();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000A3E0 File Offset: 0x000085E0
		public override void Hit(Vector2 position, Vector2 normal, Vector2 velocity)
		{
			if (!PulsarMono.fieldsound)
			{
				AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("pulsar");
				SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
				soundContainer.audioClip[0] = audioClip;
				soundContainer.setting.volumeIntensityEnable = true;
				PulsarMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
				PulsarMono.fieldsound.soundContainerArray[0] = soundContainer;
			}
			this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
			SoundManager.Instance.Play(PulsarMono.fieldsound, base.transform, new SoundParameterBase[]
			{
				this.soundParameterIntensity
			});
			Object.Instantiate<GameObject>(PulsarMono.pulsarVisual, position, Quaternion.identity);
			Player[] array = PlayerManager.instance.players.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				if (Vector2.Distance(position, array[i].transform.position) < 7f)
				{
					array[i].gameObject.GetComponent<CharacterData>();
					Player component = array[i].gameObject.GetComponent<Player>();
					array[i].data.healthHandler.TakeForce(Vector2.MoveTowards(array[i].transform.position, position, 100f), 1, false, true, 0.3f);
					array[i].gameObject.transform.position = Vector2.MoveTowards(array[i].transform.position, position, 1f);
					component.gameObject.AddComponent<PulsarEffect>();
					component.data.healthHandler.DoDamage(this.player.data.weaponHandler.gun.damage * 55f * 0.09f * Vector2.down, component.transform.position, new Color(1f, 0.6f, 1f, 1f), this.player.data.weaponHandler.gameObject, this.player, false, true, true);
				}
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000A620 File Offset: 0x00008820
		// (set) Token: 0x06000147 RID: 327 RVA: 0x0000A861 File Offset: 0x00008A61
		public static GameObject pulsarVisual
		{
			get
			{
				GameObject result;
				if (PulsarMono.pulseVisu != null)
				{
					result = PulsarMono.pulseVisu;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					PulsarMono.pulseVisu = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					PulsarMono.pulseVisu.name = "E_Pulsar";
					Object.DontDestroyOnLoad(PulsarMono.pulseVisu);
					ParticleSystem[] componentsInChildren = PulsarMono.pulseVisu.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(1f, 0.6f, 1f, 1f);
					}
					PulsarMono.pulseVisu.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(1f, 0.6f, 1f, 1f), 0f)
					};
					Object.Destroy(PulsarMono.pulseVisu.transform.GetChild(2).gameObject);
					PulsarMono.pulseVisu.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					PulsarMono.pulseVisu.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(PulsarMono.pulseVisu.GetComponent<FollowPlayer>());
					PulsarMono.pulseVisu.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(PulsarMono.pulseVisu.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(PulsarMono.pulseVisu.GetComponent<Explosion>());
					Object.Destroy(PulsarMono.pulseVisu.GetComponent<Explosion_Overpower>());
					Object.Destroy(PulsarMono.pulseVisu.GetComponent<RemoveAfterSeconds>());
					PulsarMono.pulseVisu.AddComponent<PulsarMono.PulsarSpawner>();
					result = PulsarMono.pulseVisu;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x04000176 RID: 374
		private static GameObject pulseVisu;

		// Token: 0x04000177 RID: 375
		public Block block;

		// Token: 0x04000178 RID: 376
		public Player player;

		// Token: 0x04000179 RID: 377
		public CharacterData data;

		// Token: 0x0400017A RID: 378
		public Gun gun;

		// Token: 0x0400017B RID: 379
		public Color col;

		// Token: 0x0400017C RID: 380
		public static SoundEvent fieldsound;

		// Token: 0x0400017D RID: 381
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.6f, 0);

		// Token: 0x02000117 RID: 279
		private class PulsarSpawner : MonoBehaviour
		{
			// Token: 0x060008F7 RID: 2295 RVA: 0x0002713C File Offset: 0x0002533C
			private void Start()
			{
				base.gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
				base.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 0.2f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "inited", false);
				typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 2f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "startWidth", 2f);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
			}
		}
	}
}
