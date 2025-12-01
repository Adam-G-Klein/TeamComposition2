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
	// Token: 0x02000039 RID: 57
	public class GlueMono : HitSurfaceEffect
	{
		// Token: 0x06000150 RID: 336 RVA: 0x0000AA04 File Offset: 0x00008C04
		private void Awake()
		{
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.data = base.GetComponent<CharacterData>();
			this.gun = base.GetComponent<Gun>();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000AA38 File Offset: 0x00008C38
		public override void Hit(Vector2 position, Vector2 normal, Vector2 velocity)
		{
			if (!GlueMono.fieldsound)
			{
				AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("glue");
				SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
				soundContainer.audioClip[0] = audioClip;
				soundContainer.setting.volumeIntensityEnable = true;
				GlueMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
				GlueMono.fieldsound.soundContainerArray[0] = soundContainer;
			}
			this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
			SoundManager.Instance.Play(GlueMono.fieldsound, base.transform, new SoundParameterBase[]
			{
				this.soundParameterIntensity
			});
			Object.Instantiate<GameObject>(GlueMono.glueVisual, position, Quaternion.identity);
			Player[] array = PlayerManager.instance.players.ToArray();
			for (int i = 0; i < array.Length; i++)
			{
				if (Vector2.Distance(position, array[i].transform.position) < 7f)
				{
					array[i].gameObject.GetComponent<CharacterData>();
					Player component = array[i].gameObject.GetComponent<Player>();
					array[i].gameObject.GetComponent<HealthHandler>();
					component.gameObject.AddComponent<GlueEffect>();
					component.data.healthHandler.DoDamage(2f * Vector2.down, component.transform.position, new Color(1f, 1f, 1f, 1f), this.player.data.weaponHandler.gameObject, this.player, false, true, true);
				}
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000ABF8 File Offset: 0x00008DF8
		// (set) Token: 0x06000153 RID: 339 RVA: 0x0000AE39 File Offset: 0x00009039
		public static GameObject glueVisual
		{
			get
			{
				GameObject result;
				if (GlueMono.glueVisu != null)
				{
					result = GlueMono.glueVisu;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					GlueMono.glueVisu = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					GlueMono.glueVisu.name = "E_Pulsar";
					Object.DontDestroyOnLoad(GlueMono.glueVisu);
					ParticleSystem[] componentsInChildren = GlueMono.glueVisu.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(1f, 1f, 1f, 1f);
					}
					GlueMono.glueVisu.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(1f, 1f, 1f, 1f), 0f)
					};
					Object.Destroy(GlueMono.glueVisu.transform.GetChild(2).gameObject);
					GlueMono.glueVisu.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					GlueMono.glueVisu.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(GlueMono.glueVisu.GetComponent<FollowPlayer>());
					GlueMono.glueVisu.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(GlueMono.glueVisu.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(GlueMono.glueVisu.GetComponent<Explosion>());
					Object.Destroy(GlueMono.glueVisu.GetComponent<Explosion_Overpower>());
					Object.Destroy(GlueMono.glueVisu.GetComponent<RemoveAfterSeconds>());
					GlueMono.glueVisu.AddComponent<GlueMono.GlueSpawner>();
					result = GlueMono.glueVisu;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x04000183 RID: 387
		private static GameObject glueVisu;

		// Token: 0x04000184 RID: 388
		public Block block;

		// Token: 0x04000185 RID: 389
		public Player player;

		// Token: 0x04000186 RID: 390
		public CharacterData data;

		// Token: 0x04000187 RID: 391
		public Gun gun;

		// Token: 0x04000188 RID: 392
		public Color col;

		// Token: 0x04000189 RID: 393
		public static SoundEvent fieldsound;

		// Token: 0x0400018A RID: 394
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.4f, 0);

		// Token: 0x02000119 RID: 281
		private class GlueSpawner : MonoBehaviour
		{
			// Token: 0x060008FC RID: 2300 RVA: 0x00027278 File Offset: 0x00025478
			private void Start()
			{
				base.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
				base.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 0.5f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "inited", false);
				typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 1.3f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "startWidth", 1.3f);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
			}
		}
	}
}
