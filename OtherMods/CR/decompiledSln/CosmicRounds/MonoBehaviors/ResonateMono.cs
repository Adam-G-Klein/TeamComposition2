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
	// Token: 0x02000085 RID: 133
	public class ResonateMono : MonoBehaviour
	{
		// Token: 0x0600035E RID: 862 RVA: 0x00019E00 File Offset: 0x00018000
		private void Start()
		{
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.data = base.GetComponent<CharacterData>();
			this.gun = base.GetComponent<Gun>();
			this.basic = this.block.BlockAction;
			if (this.block)
			{
				this.reso = new Action<BlockTrigger.BlockTriggerType>(this.GetDoBlockAction(this.player, this.block).Invoke);
				this.block.BlockAction = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(this.block.BlockAction, this.reso);
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00019EA4 File Offset: 0x000180A4
		public void Destroy()
		{
			Object.Destroy(this);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00019EAC File Offset: 0x000180AC
		public void OnDestroy()
		{
			this.block.BlockAction = this.basic;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00019EBF File Offset: 0x000180BF
		public Action<BlockTrigger.BlockTriggerType> GetDoBlockAction(Player player, Block block)
		{
			return delegate(BlockTrigger.BlockTriggerType trigger)
			{
				if (trigger != 1 && this.canTrigger)
				{
					Object.Instantiate<GameObject>(ResonateMono.resoVisual, this.player.transform.position, Quaternion.identity);
					this.canTrigger = false;
					if (!ResonateMono.fieldsound)
					{
						AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("glue");
						SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
						soundContainer.audioClip[0] = audioClip;
						soundContainer.setting.volumeIntensityEnable = true;
						ResonateMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
						ResonateMono.fieldsound.soundContainerArray[0] = soundContainer;
					}
					this.soundParameterIntensity.intensity = this.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
					SoundManager.Instance.Play(ResonateMono.fieldsound, this.transform, new SoundParameterBase[]
					{
						this.soundParameterIntensity
					});
					this.ResetEffectTimer();
					GameObject gameObject = new GameObject();
					gameObject.transform.position = player.gameObject.transform.position;
					gameObject.AddComponent<RemoveAfterSeconds>().seconds = 2f;
					gameObject.AddComponent<ResonateRing>().player = this.player;
					if (ResonateMono.lineEffect == null)
					{
						this.FindLineEffect();
					}
					GameObject gameObject2 = Object.Instantiate<GameObject>(ResonateMono.lineEffect, gameObject.transform);
					LineEffect componentInChildren = gameObject2.GetComponentInChildren<LineEffect>();
					componentInChildren.colorOverTime = new Gradient
					{
						alphaKeys = new GradientAlphaKey[]
						{
							new GradientAlphaKey(1f, 0f)
						},
						colorKeys = new GradientColorKey[]
						{
							new GradientColorKey(new Color(1f, 1f, 1f, 1f), 0f)
						},
						mode = 1
					};
					componentInChildren.widthMultiplier = 3f;
					componentInChildren.radius = 4.1f;
					componentInChildren.raycastCollision = false;
					componentInChildren.useColorOverTime = true;
					ParticleSystem particleSystem = gameObject2.AddComponent<ParticleSystem>();
					ParticleSystem.MainModule main = particleSystem.main;
					main.duration = 5f;
					main.startSpeed = 10f;
					main.startLifetime = 0.5f;
					main.startSize = 0.1f;
					ParticleSystem.EmissionModule emission = particleSystem.emission;
					emission.enabled = true;
					emission.rateOverTime = 150f;
					ParticleSystem.ShapeModule shape = particleSystem.shape;
					shape.enabled = true;
					shape.shapeType = 10;
					shape.radius = 0.8f;
					shape.radiusThickness = 1f;
					gameObject2.GetComponentInChildren<ParticleSystemRenderer>().material.color = new Color(1f, 1f, 1f, 1f);
					foreach (ParticleSystem particleSystem2 in gameObject2.GetComponentsInChildren<ParticleSystem>())
					{
						particleSystem.startColor = new Color(1f, 1f, 1f, 1f);
					}
				}
			};
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00019EE0 File Offset: 0x000180E0
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				int i = 0;
				this.ResetTimer();
				while (i <= this.player.data.currentCards.Count - 1)
				{
					if (this.player.data.currentCards[i].cardName == "Resonate")
					{
						this.numcheck++;
					}
					i++;
				}
				if (this.numcheck < 1)
				{
					Object.Destroy(this);
					return;
				}
				if (Time.time >= this.timeOfLastEffect + this.effectCooldown)
				{
					this.canTrigger = true;
					return;
				}
				this.canTrigger = false;
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00019F94 File Offset: 0x00018194
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00019FA8 File Offset: 0x000181A8
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00019FB8 File Offset: 0x000181B8
		private void FindLineEffect()
		{
			Debug.Log(string.Format("{0}", CardChoice.instance.cards.Length));
			ResonateMono.lineEffect = CardChoice.instance.cards.First((CardInfo c) => c.name.Equals("ChillingPresence")).gameObject.GetComponentInChildren<CharacterStatModifiers>().AddObjectToPlayer.GetComponentInChildren<LineEffect>().gameObject;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0001A034 File Offset: 0x00018234
		// (set) Token: 0x06000367 RID: 871 RVA: 0x0001A275 File Offset: 0x00018475
		public static GameObject resoVisual
		{
			get
			{
				GameObject result;
				if (ResonateMono.resovisual != null)
				{
					result = ResonateMono.resovisual;
				}
				else
				{
					IEnumerable<CardInfo> first = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>();
					List<CardInfo> second = (List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null);
					ResonateMono.resovisual = Object.Instantiate<GameObject>((from card in first.Concat(second).ToList<CardInfo>()
					where card.cardName.ToLower() == "overpower"
					select card).First<CardInfo>().GetComponent<CharacterStatModifiers>().AddObjectToPlayer.GetComponent<SpawnObjects>().objectToSpawn[0], new Vector3(0f, 100000f, 0f), Quaternion.identity);
					ResonateMono.resovisual.name = "E_Resonate";
					Object.DontDestroyOnLoad(ResonateMono.resovisual);
					ParticleSystem[] componentsInChildren = ResonateMono.resovisual.GetComponentsInChildren<ParticleSystem>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						componentsInChildren[i].startColor = new Color(1f, 1f, 1f, 1f);
					}
					ResonateMono.resovisual.transform.GetChild(1).GetComponent<LineEffect>().colorOverTime.colorKeys = new GradientColorKey[]
					{
						new GradientColorKey(new Color(0.8f, 0.8f, 0.8f, 1f), 0f)
					};
					Object.Destroy(ResonateMono.resovisual.transform.GetChild(2).gameObject);
					ResonateMono.resovisual.transform.GetChild(1).GetComponent<LineEffect>().offsetMultiplier = 0f;
					ResonateMono.resovisual.transform.GetChild(1).GetComponent<LineEffect>().playOnAwake = true;
					Object.Destroy(ResonateMono.resovisual.GetComponent<FollowPlayer>());
					ResonateMono.resovisual.GetComponent<DelayEvent>().time = 0f;
					Object.Destroy(ResonateMono.resovisual.GetComponent<SoundUnityEventPlayer>());
					Object.Destroy(ResonateMono.resovisual.GetComponent<Explosion>());
					Object.Destroy(ResonateMono.resovisual.GetComponent<Explosion_Overpower>());
					Object.Destroy(ResonateMono.resovisual.GetComponent<RemoveAfterSeconds>());
					ResonateMono.resovisual.AddComponent<ResonateMono.ResonateSpawner>();
					result = ResonateMono.resovisual;
				}
				return result;
			}
			set
			{
			}
		}

		// Token: 0x0400044F RID: 1103
		public Block block;

		// Token: 0x04000450 RID: 1104
		public Player player;

		// Token: 0x04000451 RID: 1105
		public CharacterData data;

		// Token: 0x04000452 RID: 1106
		public Gun gun;

		// Token: 0x04000453 RID: 1107
		private Action<BlockTrigger.BlockTriggerType> reso;

		// Token: 0x04000454 RID: 1108
		private Action<BlockTrigger.BlockTriggerType> basic;

		// Token: 0x04000455 RID: 1109
		private static GameObject resovisual;

		// Token: 0x04000456 RID: 1110
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000457 RID: 1111
		private float startTime;

		// Token: 0x04000458 RID: 1112
		public int numcheck;

		// Token: 0x04000459 RID: 1113
		private readonly float effectCooldown = 4f;

		// Token: 0x0400045A RID: 1114
		private float timeOfLastEffect;

		// Token: 0x0400045B RID: 1115
		private bool canTrigger;

		// Token: 0x0400045C RID: 1116
		public static SoundEvent fieldsound;

		// Token: 0x0400045D RID: 1117
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.7f, 0);

		// Token: 0x0400045E RID: 1118
		private static GameObject lineEffect;

		// Token: 0x02000151 RID: 337
		private class ResonateSpawner : MonoBehaviour
		{
			// Token: 0x06000988 RID: 2440 RVA: 0x0002A8EC File Offset: 0x00028AEC
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
