using System;
using ModdingUtils.RoundsEffects;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;
using WWMO.MonoBehaviours;

namespace CR.MonoBehaviors
{
	// Token: 0x02000057 RID: 87
	public class FrostMono : HitSurfaceEffect
	{
		// Token: 0x06000222 RID: 546 RVA: 0x00011994 File Offset: 0x0000FB94
		private void Awake()
		{
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.data = base.GetComponent<CharacterData>();
			this.gun = base.GetComponent<Gun>();
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000119C8 File Offset: 0x0000FBC8
		public override void Hit(Vector2 position, Vector2 normal, Vector2 velocity)
		{
			if (!FrostMono.fieldsound)
			{
				AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("ice");
				SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
				soundContainer.audioClip[0] = audioClip;
				soundContainer.setting.volumeIntensityEnable = true;
				FrostMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
				FrostMono.fieldsound.soundContainerArray[0] = soundContainer;
			}
			this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.5f * CR.globalVolMute.Value;
			SoundManager.Instance.PlayAtPosition(FrostMono.fieldsound, base.transform, position, new SoundParameterBase[]
			{
				this.soundParameterIntensity
			});
			GameObject gameObject = Object.Instantiate<GameObject>(((GameObject)Resources.Load("0 cards/Demonic pact")).GetComponent<Gun>().objectsToSpawn[0].effect);
			gameObject.transform.position = position;
			gameObject.hideFlags = 61;
			gameObject.name = "Explosion";
			gameObject.AddComponent<RemoveAfterSeconds>().seconds = 1f;
			ParticleSystem[] componentsInChildren = gameObject.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].startColor = new Color(0f, 1f, 1f, 1f);
			}
			foreach (ParticleSystemRenderer particleSystemRenderer in gameObject.GetComponentsInChildren<ParticleSystemRenderer>())
			{
				particleSystemRenderer.material.color = new Color(0f, 1f, 1f, 1f);
				particleSystemRenderer.sharedMaterial.color = new Color(0f, 1f, 1f, 1f);
			}
			Material[] componentsInChildren3 = gameObject.GetComponentsInChildren<Material>();
			for (int i = 0; i < componentsInChildren3.Length; i++)
			{
				componentsInChildren3[i].color = new Color(0f, 1f, 1f, 1f);
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00011BC0 File Offset: 0x0000FDC0
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Ice Shard")
					{
						this.numcheck++;
					}
				}
				if (this.numcheck > 0)
				{
					this.ResetTimer();
					return;
				}
				base.Destroy();
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00011C4E File Offset: 0x0000FE4E
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x0400029F RID: 671
		private readonly float updateDelay = 0.1f;

		// Token: 0x040002A0 RID: 672
		private float startTime;

		// Token: 0x040002A1 RID: 673
		private int numcheck;

		// Token: 0x040002A2 RID: 674
		public Block block;

		// Token: 0x040002A3 RID: 675
		public Player player;

		// Token: 0x040002A4 RID: 676
		public CharacterData data;

		// Token: 0x040002A5 RID: 677
		public Gun gun;

		// Token: 0x040002A6 RID: 678
		public BoxTouchingLava_Mono hotbox;

		// Token: 0x040002A7 RID: 679
		public static SoundEvent fieldsound;

		// Token: 0x040002A8 RID: 680
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.2f, 0);
	}
}
