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
	// Token: 0x02000023 RID: 35
	public class DropMono : HitSurfaceEffect
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00006F57 File Offset: 0x00005157
		private void Awake()
		{
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.data = base.GetComponent<CharacterData>();
			this.gun = base.GetComponent<Gun>();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00006F8C File Offset: 0x0000518C
		public override void Hit(Vector2 position, Vector2 normal, Vector2 velocity)
		{
			Object.Instantiate<GameObject>(DropMono.dropVisual, position, Quaternion.identity);
			if (!DropMono.fieldsound)
			{
				AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("drop");
				SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
				soundContainer.audioClip[0] = audioClip;
				soundContainer.setting.volumeIntensityEnable = true;
				DropMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
				DropMono.fieldsound.soundContainerArray[0] = soundContainer;
			}
			this.soundParameterIntensity.intensity = base.transform.localScale.x * (Optionshandler.vol_Master * Optionshandler.vol_Sfx * CR.globalVolMute.Value * 0.9f);
			SoundManager.Instance.PlayAtPosition(DropMono.fieldsound, base.transform, position, new SoundParameterBase[]
			{
				this.soundParameterIntensity
			});
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00007060 File Offset: 0x00005260
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00007279 File Offset: 0x00005479
		public static GameObject dropVisual
		{
			get
			{
				GameObject result;
				if (DropMono.dropVisu != null)
				{
					result = DropMono.dropVisu;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					DropMono.dropVisu = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					DropMono.dropVisu.name = "E_Drop";
					Object.DontDestroyOnLoad(DropMono.dropVisu);
					ParticleSystem[] componentsInChildren = DropMono.dropVisu.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = Color.yellow;
					}
					DropMono.dropVisu.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(Color.yellow, 0f)
					};
					Object.Destroy(DropMono.dropVisu.transform.GetChild(2).gameObject);
					DropMono.dropVisu.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					DropMono.dropVisu.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(DropMono.dropVisu.GetComponent<FollowPlayer>());
					DropMono.dropVisu.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(DropMono.dropVisu.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(DropMono.dropVisu.GetComponent<Explosion>());
					Object.Destroy(DropMono.dropVisu.GetComponent<Explosion_Overpower>());
					Object.Destroy(DropMono.dropVisu.GetComponent<RemoveAfterSeconds>());
					DropMono.dropVisu.AddComponent<DropMono.DropSpawner>();
					result = DropMono.dropVisu;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x040000E1 RID: 225
		private static GameObject dropVisu;

		// Token: 0x040000E2 RID: 226
		public Block block;

		// Token: 0x040000E3 RID: 227
		public Player player;

		// Token: 0x040000E4 RID: 228
		public CharacterData data;

		// Token: 0x040000E5 RID: 229
		public Gun gun;

		// Token: 0x040000E6 RID: 230
		public Color col;

		// Token: 0x040000E7 RID: 231
		public static SoundEvent fieldsound;

		// Token: 0x040000E8 RID: 232
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.1f, 0);

		// Token: 0x0200010D RID: 269
		private class DropSpawner : MonoBehaviour
		{
			// Token: 0x060008DF RID: 2271 RVA: 0x0002676C File Offset: 0x0002496C
			private void Start()
			{
				base.gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
				base.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 0.5f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "inited", false);
				typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 0.3f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "startWidth", 0.2f);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
			}
		}
	}
}
