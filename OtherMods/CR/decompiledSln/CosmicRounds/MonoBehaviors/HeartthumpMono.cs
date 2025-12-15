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
	// Token: 0x0200004B RID: 75
	public class HeartthumpMono : HitSurfaceEffect
	{
		// Token: 0x060001D9 RID: 473 RVA: 0x0000EDAF File Offset: 0x0000CFAF
		private void Awake()
		{
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.data = base.GetComponent<CharacterData>();
			this.gun = base.GetComponent<Gun>();
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000EDE4 File Offset: 0x0000CFE4
		public override void Hit(Vector2 position, Vector2 normal, Vector2 velocity)
		{
			if (!HeartthumpMono.fieldsound)
			{
				AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("pulsar");
				SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
				soundContainer.audioClip[0] = audioClip;
				soundContainer.setting.volumeIntensityEnable = true;
				HeartthumpMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
				HeartthumpMono.fieldsound.soundContainerArray[0] = soundContainer;
			}
			this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
			SoundManager.Instance.PlayAtPosition(HeartthumpMono.fieldsound, base.transform, position, new SoundParameterBase[]
			{
				this.soundParameterIntensity
			});
			Object.Instantiate<GameObject>(HeartthumpMono.heartVisual, position, Quaternion.identity);
			this.player.data.healthHandler.Heal(this.player.data.weaponHandler.gun.damage * 55f / 10f);
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000EEF4 File Offset: 0x0000D0F4
		// (set) Token: 0x060001DC RID: 476 RVA: 0x0000F12B File Offset: 0x0000D32B
		public static GameObject heartVisual
		{
			get
			{
				GameObject result;
				if (HeartthumpMono.heartVisu != null)
				{
					result = HeartthumpMono.heartVisu;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					HeartthumpMono.heartVisu = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					HeartthumpMono.heartVisu.name = "E_Heartthump";
					Object.DontDestroyOnLoad(HeartthumpMono.heartVisu);
					ParticleSystem[] componentsInChildren = HeartthumpMono.heartVisu.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(1f, 0.3f, 0.7f);
					}
					HeartthumpMono.heartVisu.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(1f, 0.3f, 0.7f), 0f)
					};
					Object.Destroy(HeartthumpMono.heartVisu.transform.GetChild(2).gameObject);
					HeartthumpMono.heartVisu.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					HeartthumpMono.heartVisu.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(HeartthumpMono.heartVisu.GetComponent<FollowPlayer>());
					HeartthumpMono.heartVisu.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(HeartthumpMono.heartVisu.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(HeartthumpMono.heartVisu.GetComponent<Explosion>());
					Object.Destroy(HeartthumpMono.heartVisu.GetComponent<Explosion_Overpower>());
					Object.Destroy(HeartthumpMono.heartVisu.GetComponent<RemoveAfterSeconds>());
					HeartthumpMono.heartVisu.AddComponent<HeartthumpMono.ThumpSpawner>();
					result = HeartthumpMono.heartVisu;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x0400023A RID: 570
		private static GameObject heartVisu;

		// Token: 0x0400023B RID: 571
		public Block block;

		// Token: 0x0400023C RID: 572
		public Player player;

		// Token: 0x0400023D RID: 573
		public CharacterData data;

		// Token: 0x0400023E RID: 574
		public Gun gun;

		// Token: 0x0400023F RID: 575
		public Color col;

		// Token: 0x04000240 RID: 576
		public static SoundEvent fieldsound;

		// Token: 0x04000241 RID: 577
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.2f, 0);

		// Token: 0x0200012A RID: 298
		private class ThumpSpawner : MonoBehaviour
		{
			// Token: 0x0600092C RID: 2348 RVA: 0x000287D4 File Offset: 0x000269D4
			private void Start()
			{
				base.gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
				base.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 0.2f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "inited", false);
				typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 0.3f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "startWidth", 0.1f);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
			}
		}
	}
}
