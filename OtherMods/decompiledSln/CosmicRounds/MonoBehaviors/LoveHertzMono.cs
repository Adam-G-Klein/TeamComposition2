using System;
using System.Collections.Generic;
using System.Linq;
using Sonigon;
using Sonigon.Internal;
using UnboundLib;
using UnityEngine;

namespace CR.MonoBehaviors
{
	// Token: 0x0200004C RID: 76
	public class LoveHertzMono : MonoBehaviour
	{
		// Token: 0x060001DE RID: 478 RVA: 0x0000F146 File Offset: 0x0000D346
		private void Awake()
		{
			this.player = base.GetComponent<Player>();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000F154 File Offset: 0x0000D354
		private void Start()
		{
			this.ResetEffectTimer();
			this.ResetTimer();
			this.factorObject = new GameObject();
			this.factorObject.transform.SetParent(this.player.transform);
			this.factorObject.transform.position = this.player.transform.position;
			this.factorObject.AddComponent<LoveHertzMono.DestroyOnUnparent>();
			if (LoveHertzMono.lineEffect == null)
			{
				this.GetLineEffect();
			}
			this.fearEffect = Object.Instantiate<GameObject>(LoveHertzMono.lineEffect, this.factorObject.transform);
			this.fearEffect.AddComponent<LoveHertzMono.DestroyOnUnparent>();
			this.componentInChildren = this.fearEffect.GetComponentInChildren<LineEffect>();
			this.componentInChildren.colorOverTime = new Gradient
			{
				alphaKeys = new GradientAlphaKey[]
				{
					new GradientAlphaKey(0.9f, 0f)
				},
				colorKeys = new GradientColorKey[]
				{
					new GradientColorKey(new Color(1f, 0.2f, 0.5f), 0f)
				},
				mode = 1
			};
			this.componentInChildren.widthMultiplier = 1f;
			this.componentInChildren.radius = 3.2f;
			this.componentInChildren.raycastCollision = true;
			this.componentInChildren.useColorOverTime = true;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000F2B0 File Offset: 0x0000D4B0
		private void GetLineEffect()
		{
			LoveHertzMono.lineEffect = CardChoice.instance.cards.First((CardInfo c) => c.name.Equals("ChillingPresence")).gameObject.GetComponentInChildren<CharacterStatModifiers>().AddObjectToPlayer.GetComponentInChildren<LineEffect>().gameObject;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000F309 File Offset: 0x0000D509
		public void Destroy()
		{
			this.componentInChildren.Stop();
			Object.Destroy(this.factorObject);
			Object.Destroy(this.fearEffect);
			Object.Destroy(this.componentInChildren);
			Object.Destroy(this);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000F340 File Offset: 0x0000D540
		private void OnDestroy()
		{
			LineEffect lineEffect = this.componentInChildren;
			if (lineEffect != null)
			{
				lineEffect.Stop();
			}
			if (this.componentInChildren != null)
			{
				Object.Destroy(this.componentInChildren);
			}
			Object.Destroy(this.factorObject);
			Object.Destroy(this.fearEffect);
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000F390 File Offset: 0x0000D590
		private void Update()
		{
			if (Time.time >= this.startTime + this.updateDelay)
			{
				for (int i = 0; i <= this.player.data.currentCards.Count - 1; i++)
				{
					if (this.player.data.currentCards[i].cardName == "Love Hertz")
					{
						this.numcheck++;
					}
				}
				if (this.numcheck > 0)
				{
					this.ResetTimer();
					if (Time.time < this.timeOfLastEffect + this.effectCooldown || !this.RangeCheck())
					{
						return;
					}
					using (List<Player>.Enumerator enumerator = this.GetEnemyPlayers().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Player player = enumerator.Current;
							CharacterData component = player.GetComponent<CharacterData>();
							player.GetComponent<StunHandler>();
							player.GetComponent<SilenceHandler>();
							Vector2 position = component.playerVel.position;
							if (Vector2.Distance(this.player.data.playerVel.position, position) <= 5.5f && PlayerManager.instance.CanSeePlayer(this.player.transform.position, player).canSee && component.isActiveAndEnabled && !component.dead && (bool)ExtensionMethods.GetFieldValue(this.player.data.playerVel, "simulated"))
							{
								component = player.GetComponent<CharacterData>();
								player.GetComponent<StunHandler>();
								player.GetComponent<SilenceHandler>();
								NetworkingManager.RPC(typeof(StunMono), "RPCA_StunPlayer", new object[]
								{
									0.65f,
									player.playerID
								});
								player.data.view.RPC("RPCA_AddSilence", 0, new object[]
								{
									0.65f
								});
								Object.Instantiate<GameObject>(SyphonMono.syphonVisual, player.transform.position, Quaternion.identity);
								component.healthHandler.TakeDamage(25f * Vector2.down, player.transform.position, this.player.data.weaponHandler.gameObject, this.player, true, false);
								if (!LoveHertzMono.fieldsound)
								{
									AudioClip audioClip = CR.ArtAsset.LoadAsset<AudioClip>("love");
									SoundContainer soundContainer = ScriptableObject.CreateInstance<SoundContainer>();
									soundContainer.audioClip[0] = audioClip;
									soundContainer.setting.volumeIntensityEnable = true;
									LoveHertzMono.fieldsound = ScriptableObject.CreateInstance<SoundEvent>();
									LoveHertzMono.fieldsound.soundContainerArray[0] = soundContainer;
								}
								this.soundParameterIntensity.intensity = base.transform.localScale.x * Optionshandler.vol_Master * Optionshandler.vol_Sfx / 1.2f * CR.globalVolMute.Value;
								SoundManager.Instance.Play(LoveHertzMono.fieldsound, base.transform, new SoundParameterBase[]
								{
									this.soundParameterIntensity
								});
								this.player.data.healthHandler.Heal(1f + player.data.maxHealth * 0.15f);
								this.ResetEffectTimer();
								Object.Instantiate<GameObject>(SyphonMono.syphonVisual, this.player.transform.position, Quaternion.identity);
							}
						}
						return;
					}
				}
				this.Destroy();
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000F71C File Offset: 0x0000D91C
		private void ResetTimer()
		{
			this.startTime = Time.time;
			this.numcheck = 0;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000F730 File Offset: 0x0000D930
		private void ResetEffectTimer()
		{
			this.timeOfLastEffect = Time.time;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000F740 File Offset: 0x0000D940
		private bool RangeCheck()
		{
			foreach (Player player in this.GetEnemyPlayers())
			{
				CharacterData component = player.GetComponent<CharacterData>();
				if ((double)Vector2.Distance(this.player.data.playerVel.position, component.playerVel.position) <= 7.2 && PlayerManager.instance.CanSeePlayer(this.player.transform.position, player).canSee)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000F7F4 File Offset: 0x0000D9F4
		public List<Player> GetEnemyPlayers()
		{
			return (from player in PlayerManager.instance.players
			where player.teamID != this.player.teamID
			select player).ToList<Player>();
		}

		// Token: 0x04000242 RID: 578
		private Player player;

		// Token: 0x04000243 RID: 579
		private static GameObject lineEffect;

		// Token: 0x04000244 RID: 580
		public LineEffect componentInChildren;

		// Token: 0x04000245 RID: 581
		public GameObject fearEffect;

		// Token: 0x04000246 RID: 582
		public GameObject factorObject;

		// Token: 0x04000247 RID: 583
		private readonly float updateDelay = 0.1f;

		// Token: 0x04000248 RID: 584
		private readonly float effectCooldown = 3f;

		// Token: 0x04000249 RID: 585
		private readonly float maxDistance = 5.4f;

		// Token: 0x0400024A RID: 586
		private float startTime;

		// Token: 0x0400024B RID: 587
		private float timeOfLastEffect;

		// Token: 0x0400024C RID: 588
		private int numcheck;

		// Token: 0x0400024D RID: 589
		public static SoundEvent fieldsound;

		// Token: 0x0400024E RID: 590
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(0.6f, 0);

		// Token: 0x0200012C RID: 300
		private class DestroyOnUnparent : MonoBehaviour
		{
			// Token: 0x06000931 RID: 2353 RVA: 0x0002890F File Offset: 0x00026B0F
			private void Update()
			{
				Object @object;
				if (this == null)
				{
					@object = null;
				}
				else
				{
					Transform transform = base.transform;
					@object = ((transform != null) ? transform.parent : null);
				}
				if (@object == null)
				{
					Object.Destroy(this);
				}
			}
		}
	}
}
