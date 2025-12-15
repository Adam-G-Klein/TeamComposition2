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
	// Token: 0x02000076 RID: 118
	public class QuantumMono : MonoBehaviour
	{
		// Token: 0x060002F0 RID: 752 RVA: 0x00017AB4 File Offset: 0x00015CB4
		private void Start()
		{
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.data = base.GetComponent<CharacterData>();
			this.ResetEffectTimer();
			this.ResetTimer();
			this.canTrigger = true;
			this.hasTriggered = false;
			this.basic = this.block.BlockAction;
			this.numcheck = 0;
			if (this.block)
			{
				this.taser = new Action<BlockTrigger.BlockTriggerType>(this.GetDoBlockAction(this.player, this.block).Invoke);
				this.block.FirstBlockActionThatDelaysOthers = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(this.block.FirstBlockActionThatDelaysOthers, this.taser);
				this.block.delayOtherActions = true;
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x00017B79 File Offset: 0x00015D79
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00017B81 File Offset: 0x00015D81
		public void OnDestroy()
		{
			this.block.BlockAction = this.basic;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00017B94 File Offset: 0x00015D94
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Quantum")
					{
						this.numcheck++;
					}
				}
				if (this.numcheck > 0)
				{
					this.ResetTimer();
					if (Time.time >= this.timeOfLastEffect + this.effectCooldown)
					{
						this.canTrigger = true;
						return;
					}
					this.canTrigger = false;
					return;
				}
				else
				{
					Object.Destroy(this);
				}
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00017C48 File Offset: 0x00015E48
		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block)
		{
			return delegate(BlockTrigger.BlockTriggerType trigger)
			{
				if (trigger != 1 && this.canTrigger)
				{
					if (!QuantumMono.fieldsound)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("taser");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						QuantumMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						QuantumMono.fieldsound.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity.intensity = this.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
					SoundManager.Instance.Play(QuantumMono.fieldsound, this.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
					Object.Instantiate<GameObject>(QuantumMono.quantumVisual, this.player.transform.position, Quaternion.identity);
					Vector2 vector = block.transform.position;
					Player[] array = PlayerManager.instance.players.ToArray();
					Vector2 vector2 = vector;
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i].teamID != player.teamID && (Vector2.Distance(vector, array[i].transform.position) < this.maxDistance && PlayerManager.instance.CanSeePlayer(player.transform.position, array[i]).canSee) && (vector2 == vector || Vector2.Distance(vector, vector2) > Vector2.Distance(vector, array[i].transform.position)))
						{
							vector2 = array[i].transform.position;
						}
					}
					if (vector2 != vector)
					{
						this.player.data.block.DoBlockAtPosition(false, true, 4, vector2, true);
						this.ResetEffectTimer();
					}
				}
			};
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00017C6F File Offset: 0x00015E6F
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00017C83 File Offset: 0x00015E83
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00017C90 File Offset: 0x00015E90
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x00017ED1 File Offset: 0x000160D1
		public static GameObject quantumVisual
		{
			get
			{
				GameObject result;
				if (QuantumMono.quantumVisu != null)
				{
					result = QuantumMono.quantumVisu;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					QuantumMono.quantumVisu = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					QuantumMono.quantumVisu.name = "E_Quantum";
					Object.DontDestroyOnLoad(QuantumMono.quantumVisu);
					ParticleSystem[] componentsInChildren = QuantumMono.quantumVisu.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(0.2f, 0.2f, 1f, 1f);
					}
					QuantumMono.quantumVisu.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(0.2f, 0.2f, 1f, 1f), 0f)
					};
					Object.Destroy(QuantumMono.quantumVisu.transform.GetChild(2).gameObject);
					QuantumMono.quantumVisu.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					QuantumMono.quantumVisu.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(QuantumMono.quantumVisu.GetComponent<FollowPlayer>());
					QuantumMono.quantumVisu.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(QuantumMono.quantumVisu.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(QuantumMono.quantumVisu.GetComponent<Explosion>());
					Object.Destroy(QuantumMono.quantumVisu.GetComponent<Explosion_Overpower>());
					Object.Destroy(QuantumMono.quantumVisu.GetComponent<RemoveAfterSeconds>());
					QuantumMono.quantumVisu.AddComponent<QuantumMono.QuantumSpawner>();
					result = QuantumMono.quantumVisu;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x040003BE RID: 958
		private readonly float maxDistance = 9f;

		// Token: 0x040003BF RID: 959
		public Block block;

		// Token: 0x040003C0 RID: 960
		public Player player;

		// Token: 0x040003C1 RID: 961
		public CharacterData data;

		// Token: 0x040003C2 RID: 962
		private Action<BlockTrigger.BlockTriggerType> taser;

		// Token: 0x040003C3 RID: 963
		private Action<BlockTrigger.BlockTriggerType> basic;

		// Token: 0x040003C4 RID: 964
		private static GameObject quantumVisu;

		// Token: 0x040003C5 RID: 965
		private readonly float updateDelay = 0.1f;

		// Token: 0x040003C6 RID: 966
		private readonly float effectCooldown = 1f;

		// Token: 0x040003C7 RID: 967
		private float startTime;

		// Token: 0x040003C8 RID: 968
		private float timeOfLastEffect;

		// Token: 0x040003C9 RID: 969
		private bool canTrigger;

		// Token: 0x040003CA RID: 970
		private bool hasTriggered;

		// Token: 0x040003CB RID: 971
		public int numcheck;

		// Token: 0x040003CC RID: 972
		public static SoundEvent fieldsound;

		// Token: 0x040003CD RID: 973
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.5f, 0);

		// Token: 0x02000143 RID: 323
		private class QuantumSpawner : MonoBehaviour
		{
			// Token: 0x06000969 RID: 2409 RVA: 0x000299B4 File Offset: 0x00027BB4
			private void Start()
			{
				base.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
				base.gameObject.AddComponent<RemoveAfterSeconds>().seconds = 1.5f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "inited", false);
				typeof(LineEffect).InvokeMember("Init", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), new object[0]);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().radius = 5.5f;
				ExtensionMethods.SetFieldValue(base.gameObject.transform.GetChild(1).GetComponent<LineEffect>(), "startWidth", 1f);
				base.gameObject.transform.GetChild(1).GetComponent<LineEffect>().Play();
			}
		}
	}
}
