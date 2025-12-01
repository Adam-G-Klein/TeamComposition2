using System;
using System.Collections;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Configuration;
using CR.Cards;
using CR.MonoBehaviors;
using Jotunn.Utils;
using ModdingUtils.Utils;
using Sonigon;
using TMPro;
using UnboundLib;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using UnboundLib.Utils.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CR
{
	// Token: 0x02000002 RID: 2
	[BepInDependency("com.willis.rounds.unbound", 1)]
	[BepInDependency("root.classes.manager.reborn", 1)]
	[BepInDependency("root.rarity.lib", 1)]
	[BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", 1)]
	[BepInPlugin("com.XAngelMoonX.rounds.CosmicRounds", "Cosmic Rounds", "2.7.0")]
	[BepInProcess("Rounds.exe")]
	public class CR : BaseUnityPlugin
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private static void Main()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002054 File Offset: 0x00000254
		private void Awake()
		{
			CR.globalVolMute = base.Config.Bind<float>("CR", "Volume for CR SFX", 100f, "Volume for CR SFX");
			CR.crSpecialVFX = base.Config.Bind<bool>("CR", "Toggle for CR VFX", true, "Toggle for CR VFX");
			GameModeManager.AddHook("GameEnd", new Func<IGameModeHandler, IEnumerator>(this.ResetEffects));
			GameModeManager.AddHook("PointEnd", new Func<IGameModeHandler, IEnumerator>(this.ResetColors));
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020D1 File Offset: 0x000002D1
		private IEnumerator ResetEffects(IGameModeHandler gm)
		{
			this.DestroyAll<ShockMono>();
			this.DestroyAll<StunMono>();
			this.DestroyAll<MosquitoMono>();
			this.DestroyAll<BeetleMono>();
			this.DestroyAll<FearFactorMono>();
			this.DestroyAll<CriticalHitMono>();
			this.DestroyAll<FlamethrowerMono>();
			this.DestroyAll<SyphonMono>();
			this.DestroyAll<TaserMono>();
			this.DestroyAll<HolsterMono>();
			this.DestroyAll<FlexMono>();
			this.DestroyAll<DroneMono>();
			this.DestroyAll<GoldenGlazeMono>();
			this.DestroyAll<GoldHealthMono>();
			this.DestroyAll<SugarGlazeMono>();
			this.DestroyAll<SugarMoveMono>();
			this.DestroyAll<MitosisMono>();
			this.DestroyAll<MitosisBlockMono>();
			this.DestroyAll<MeiosisMono>();
			this.DestroyAll<MeiosisReloadMono>();
			this.DestroyAll<PogoMono>();
			this.DestroyAll<CloudMono>();
			this.DestroyAll<PulseMono>();
			this.DestroyAll<DriveMono>();
			this.DestroyAll<CriticalMono>();
			this.DestroyAll<DropMono>();
			this.DestroyAll<SunMono>();
			this.DestroyAll<MeteorMono>();
			this.DestroyAll<CometMono>();
			this.DestroyAll<UnicornMono>();
			this.DestroyAll<GravityMono>();
			this.DestroyAll<IgniteMono>();
			this.DestroyAll<IgniteEffect>();
			this.DestroyAll<FaeEmbersMono>();
			this.DestroyAll<CareenMono>();
			this.DestroyAll<AsteroidMono>();
			this.DestroyAll<PulsarMono>();
			this.DestroyAll<GlueMono>();
			this.DestroyAll<AquaRingMono>();
			this.DestroyAll<QuasarMono>();
			this.DestroyAll<CakeMono>();
			this.DestroyAll<ChlorophyllMono>();
			this.DestroyAll<HiveMono>();
			this.DestroyAll<BarrierMono>();
			this.DestroyAll<HeartitionMono>();
			this.DestroyAll<HeartbeatMono>();
			this.DestroyAll<HeartburnMono>();
			this.DestroyAll<HeartthumpMono>();
			this.DestroyAll<LoveHertzMono>();
			this.DestroyAll<SweetheartMono>();
			this.DestroyAll<DarkMono>();
			this.DestroyAll<RingMono>();
			this.DestroyAll<PingMono>();
			this.DestroyAll<ShootingStarMono>();
			this.DestroyAll<SatelliteMono>();
			this.DestroyAll<BeeMono>();
			this.DestroyAll<IceTrailMono>();
			this.DestroyAll<StarMono>();
			this.DestroyAll<PumpkinMono>();
			this.DestroyAll<HeartMono>();
			this.DestroyAll<BeeSpriteMono>();
			this.DestroyAll<BeeShotMono>();
			this.DestroyAll<FireMono>();
			this.DestroyAll<FireballMono>();
			this.DestroyAll<HolyFireMono>();
			this.DestroyAll<HolyMono>();
			this.DestroyAll<HaloMono>();
			this.DestroyAll<HaloBlockMono>();
			this.DestroyAll<HolyShotMono>();
			this.DestroyAll<HaloFireMono>();
			this.DestroyAll<HyperMono>();
			this.DestroyAll<AllMono>();
			this.DestroyAll<AllRegenMono>();
			this.DestroyAll<HyperRegenMono>();
			this.DestroyAll<MistletoeMono>();
			this.DestroyAll<FrozenMono>();
			this.DestroyAll<LoveTapMono>();
			this.DestroyAll<SealMono>();
			this.DestroyAll<SealSealMono>();
			this.DestroyAll<BatteryMono>();
			this.DestroyAll<AssaultMono>();
			this.DestroyAll<DingMono>();
			this.DestroyAll<DischargeMono>();
			this.DestroyAll<BulkUpMono>();
			this.DestroyAll<QuantumMono>();
			this.DestroyAll<ChargeMono>();
			this.DestroyAll<ChargeCDMono>();
			this.DestroyAll<EnchantMono>();
			this.DestroyAll<EnchantCDMono>();
			this.DestroyAll<CrowSpriteMono>();
			this.DestroyAll<HawkSpriteMono>();
			this.DestroyAll<DropballSpriteMono>();
			this.DestroyAll<DoveSpriteMono>();
			this.DestroyAll<VultureSpriteMono>();
			this.DestroyAll<SquidMono>();
			this.DestroyAll<CrushMono>();
			this.DestroyAll<TossMono>();
			this.DestroyAll<ResonateMono>();
			this.DestroyAll<ScarabMono>();
			this.DestroyAll<MoonMono>();
			this.DestroyAll<ReplicateMono>();
			this.DestroyAll<ZodiacMono>();
			yield break;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E0 File Offset: 0x000002E0
		private IEnumerator ResetColors(IGameModeHandler gm)
		{
			this.DestroyAll<BeetleRegenMono>();
			this.DestroyAll<GoldHealthMono>();
			this.DestroyAll<SugarMoveMono>();
			this.DestroyAll<MitosisBlockMono>();
			this.DestroyAll<MeiosisReloadMono>();
			this.DestroyAll<CriticalMono>();
			this.DestroyAll<RedMono>();
			this.DestroyAll<OrangeMono>();
			this.DestroyAll<YellowMono>();
			this.DestroyAll<GreenMono>();
			this.DestroyAll<CyanMono>();
			this.DestroyAll<BlueMono>();
			this.DestroyAll<PurpleMono>();
			this.DestroyAll<PinkMono>();
			this.DestroyAll<IgniteEffect>();
			this.DestroyAll<PulsarEffect>();
			this.DestroyAll<GlueEffect>();
			this.DestroyAll<CakeReloadMono>();
			this.DestroyAll<ChlorophyllReloadMono>();
			this.DestroyAll<HiveBlockMono>();
			this.DestroyAll<SweetheartEffect>();
			this.DestroyAll<HaloBlockMono>();
			this.DestroyAll<AllRegenMono>();
			this.DestroyAll<HyperRegenMono>();
			this.DestroyAll<FrozenMono>();
			this.DestroyAll<SealSealMono>();
			this.DestroyAll<ChargeCDMono>();
			this.DestroyAll<EnchantCDMono>();
			this.DestroyAll<CrushEffect>();
			this.DestroyAll<TossEffect>();
			this.DestroyAll<ResonateRing>();
			this.DestroyAll<ScarabRegenMono>();
			yield break;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020F0 File Offset: 0x000002F0
		private void DestroyAll<T>() where T : Object
		{
			T[] array = Object.FindObjectsOfType<T>();
			for (int i = array.Length - 1; i >= 0; i--)
			{
				Object.Destroy(array[i]);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002124 File Offset: 0x00000324
		private void Start()
		{
			CR.ArtAsset = AssetUtils.LoadAssetBundleFromResources("cr_assets", typeof(CR).Assembly);
			if (CR.ArtAsset == null)
			{
				Debug.Log("Failed to load CR art asset bundle");
			}
			Unbound.RegisterMenu("CR Settings", delegate()
			{
			}, new Action<GameObject>(this.NewGUI), null, true);
			GameModeManager.AddHook("GameEnd", new Func<IGameModeHandler, IEnumerator>(this.ResetEffects));
			GameModeManager.AddHook("GameStart", new Func<IGameModeHandler, IEnumerator>(this.ResetEffects));
			GameModeManager.AddHook("GameEnd", new Func<IGameModeHandler, IEnumerator>(this.ResetEffects));
			GameModeManager.AddHook("GameStart", new Func<IGameModeHandler, IEnumerator>(this.ResetEffects));
			CustomCard.BuildCard<BeetleCard>();
			CustomCard.BuildCard<CrowCard>();
			CustomCard.BuildCard<HawkCard>();
			CustomCard.BuildCard<SpeedUpCard>();
			CustomCard.BuildCard<MosquitoCard>();
			CustomCard.BuildCard<SuperSonicCard>();
			CustomCard.BuildCard<StasisCard>();
			CustomCard.BuildCard<OnesKingCard>();
			CustomCard.BuildCard<BulletTimeCard>();
			CustomCard.BuildCard<StunCard>();
			CustomCard.BuildCard<FearFactorCard>();
			CustomCard.BuildCard<StarCard>();
			CustomCard.BuildCard<CriticalHitCard>();
			CustomCard.BuildCard<FlamethrowerCard>();
			CustomCard.BuildCard<SyphonCard>();
			CustomCard.BuildCard<DropshotCard>();
			CustomCard.BuildCard<ReconCard>();
			CustomCard.BuildCard<TaserCard>();
			CustomCard.BuildCard<HolsterCard>();
			CustomCard.BuildCard<FlexCard>();
			CustomCard.BuildCard<DroneCard>();
			CustomCard.BuildCard<SparkCard>();
			CustomCard.BuildCard<GoldenGlazeCard>();
			CustomCard.BuildCard<FocusCard>();
			CustomCard.BuildCard<SugarGlazeCard>();
			CustomCard.BuildCard<MitosisCard>();
			CustomCard.BuildCard<MeiosisCard>();
			CustomCard.BuildCard<PogoCard>();
			CustomCard.BuildCard<AllCard>();
			CustomCard.BuildCard<CloudCard>();
			CustomCard.BuildCard<PulseCard>();
			CustomCard.BuildCard<DriveCard>();
			CustomCard.BuildCard<SunCard>();
			CustomCard.BuildCard<CometCard>();
			CustomCard.BuildCard<MeteorCard>();
			CustomCard.BuildCard<UnicornCard>();
			CustomCard.BuildCard<GravityCard>();
			CustomCard.BuildCard<IgniteCard>();
			CustomCard.BuildCard<FaeEmbersCard>();
			CustomCard.BuildCard<CareenCard>();
			CustomCard.BuildCard<AsteroidCard>();
			CustomCard.BuildCard<PulsarCard>();
			CustomCard.BuildCard<GlueCard>();
			CustomCard.BuildCard<AquaRingCard>();
			CustomCard.BuildCard<QuasarCard>();
			CustomCard.BuildCard<CakeCard>();
			CustomCard.BuildCard<EggCard>();
			CustomCard.BuildCard<ChlorophyllCard>();
			CustomCard.BuildCard<HiveCard>();
			CustomCard.BuildCard<BarrierCard>();
			CustomCard.BuildCard<DoveCard>();
			CustomCard.BuildCard<HeartitionCard>();
			CustomCard.BuildCard<HeartbeatCard>();
			CustomCard.BuildCard<HeartburnCard>();
			CustomCard.BuildCard<HeartthumpCard>();
			CustomCard.BuildCard<LoveHertzCard>();
			CustomCard.BuildCard<SweetheartCard>();
			CustomCard.BuildCard<DarkMatterCard>();
			CustomCard.BuildCard<RingCard>();
			CustomCard.BuildCard<PingCard>();
			CustomCard.BuildCard<ShootingStarCard>();
			CustomCard.BuildCard<SatelliteCard>();
			CustomCard.BuildCard<VultureCard>();
			CustomCard.BuildCard<HyperSonicCard>();
			CustomCard.BuildCard<IceShardCard>();
			CustomCard.BuildCard<JackOLanternCard>();
			CustomCard.BuildCard<HaloCard>();
			CustomCard.BuildCard<MistletoeCard>();
			CustomCard.BuildCard<LoveStruckCard>();
			CustomCard.BuildCard<TabulaRasaCard>();
			CustomCard.BuildCard<SealCard>();
			CustomCard.BuildCard<BatteryCard>();
			CustomCard.BuildCard<AssaultCard>();
			CustomCard.BuildCard<DingCard>();
			CustomCard.BuildCard<DischargeCard>();
			CustomCard.BuildCard<BulkUpCard>();
			CustomCard.BuildCard<QuantumCard>();
			CustomCard.BuildCard<ChargeCard>();
			CustomCard.BuildCard<EnchantCard>();
			CustomCard.BuildCard<ArrayCard>();
			CustomCard.BuildCard<ClearMindCard>(delegate(CardInfo cardInfo)
			{
				Cards.instance.AddHiddenCard(cardInfo);
				CR.hiddenCards.Add(cardInfo);
			});
			CustomCard.BuildCard<SquidCard>();
			CustomCard.BuildCard<AquariusCard>();
			CustomCard.BuildCard<PiscesCard>();
			CustomCard.BuildCard<AriesCard>();
			CustomCard.BuildCard<TaurusCard>();
			CustomCard.BuildCard<GeminiCard>();
			CustomCard.BuildCard<CancerCard>();
			CustomCard.BuildCard<LeoCard>();
			CustomCard.BuildCard<VirgoCard>();
			CustomCard.BuildCard<LibraCard>();
			CustomCard.BuildCard<ScorpioCard>();
			CustomCard.BuildCard<SagittariusCard>();
			CustomCard.BuildCard<CapricornCard>();
			CustomCard.BuildCard<CrushCard>();
			CustomCard.BuildCard<TossCard>();
			CustomCard.BuildCard<ResonateCard>();
			CustomCard.BuildCard<ScarabCard>();
			CustomCard.BuildCard<MoonCard>();
			CustomCard.BuildCard<ReplicateCard>();
			CustomCard.BuildCard<ZodiacCard>();
			CustomCard.BuildCard<ZodiacCardPlus>();
			CustomCard.BuildCard<AquariusCardPlus>(delegate(CardInfo cardInfo)
			{
				Cards.instance.AddHiddenCard(cardInfo);
				CR.hiddenCards.Add(cardInfo);
			});
			CustomCard.BuildCard<PiscesCardPlus>(delegate(CardInfo cardInfo)
			{
				Cards.instance.AddHiddenCard(cardInfo);
				CR.hiddenCards.Add(cardInfo);
			});
			CustomCard.BuildCard<AriesCardPlus>(delegate(CardInfo cardInfo)
			{
				Cards.instance.AddHiddenCard(cardInfo);
				CR.hiddenCards.Add(cardInfo);
			});
			CustomCard.BuildCard<TaurusCardPlus>(delegate(CardInfo cardInfo)
			{
				Cards.instance.AddHiddenCard(cardInfo);
				CR.hiddenCards.Add(cardInfo);
			});
			CustomCard.BuildCard<GeminiCardPlus>(delegate(CardInfo cardInfo)
			{
				Cards.instance.AddHiddenCard(cardInfo);
				CR.hiddenCards.Add(cardInfo);
			});
			CustomCard.BuildCard<CancerCardPlus>(delegate(CardInfo cardInfo)
			{
				Cards.instance.AddHiddenCard(cardInfo);
				CR.hiddenCards.Add(cardInfo);
			});
			CustomCard.BuildCard<LeoCardPlus>(delegate(CardInfo cardInfo)
			{
				Cards.instance.AddHiddenCard(cardInfo);
				CR.hiddenCards.Add(cardInfo);
			});
			CustomCard.BuildCard<VirgoCardPlus>(delegate(CardInfo cardInfo)
			{
				Cards.instance.AddHiddenCard(cardInfo);
				CR.hiddenCards.Add(cardInfo);
			});
			CustomCard.BuildCard<LibraCardPlus>(delegate(CardInfo cardInfo)
			{
				Cards.instance.AddHiddenCard(cardInfo);
				CR.hiddenCards.Add(cardInfo);
			});
			CustomCard.BuildCard<ScorpioCardPlus>(delegate(CardInfo cardInfo)
			{
				Cards.instance.AddHiddenCard(cardInfo);
				CR.hiddenCards.Add(cardInfo);
			});
			CustomCard.BuildCard<SagittariusCardPlus>(delegate(CardInfo cardInfo)
			{
				Cards.instance.AddHiddenCard(cardInfo);
				CR.hiddenCards.Add(cardInfo);
			});
			CustomCard.BuildCard<CapricornCardPlus>(delegate(CardInfo cardInfo)
			{
				Cards.instance.AddHiddenCard(cardInfo);
				CR.hiddenCards.Add(cardInfo);
			});
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000025C2 File Offset: 0x000007C2
		private void GlobalVolAction(float val)
		{
			CR.globalVolMute.Value = val;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000025D0 File Offset: 0x000007D0
		private void GlobalVFXAction(bool val)
		{
			CR.crSpecialVFX.Value = val;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000025E0 File Offset: 0x000007E0
		private void NewGUI(GameObject menu)
		{
			TextMeshProUGUI textMeshProUGUI;
			MenuHandler.CreateText("CR Settings", menu, ref textMeshProUGUI, 60, true, null, null, null, null);
			Slider slider;
			MenuHandler.CreateSlider("SFX Volume", menu, 50, 0f, 1f, CR.globalVolMute.Value, new UnityAction<float>(this.GlobalVolAction), ref slider, false, null, 0, true, null, null, null, null);
			MenuHandler.CreateToggle(CR.crSpecialVFX.Value, "VFX Toggle", menu, new UnityAction<bool>(this.GlobalVFXAction), 50, true, null, null, null, null);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000269B File Offset: 0x0000089B
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000026A2 File Offset: 0x000008A2
		public static CR instance { get; private set; }

		// Token: 0x04000001 RID: 1
		internal static List<CardInfo> hiddenCards = new List<CardInfo>();

		// Token: 0x04000002 RID: 2
		private const string ModId = "com.XAngelMoonX.rounds.CosmicRounds";

		// Token: 0x04000003 RID: 3
		private const string ModName = "CR";

		// Token: 0x04000004 RID: 4
		public const string Version = "2.7.0";

		// Token: 0x04000005 RID: 5
		internal static AssetBundle ArtAsset;

		// Token: 0x04000006 RID: 6
		public static ConfigEntry<float> globalVolMute;

		// Token: 0x04000007 RID: 7
		public static ConfigEntry<bool> crSpecialVFX;

		// Token: 0x04000008 RID: 8
		public static SoundEvent fieldsound;

		// Token: 0x04000009 RID: 9
		private SoundParameterIntensity soundParameterIntensity = new SoundParameterIntensity(1f, 0);

		// Token: 0x0400000A RID: 10
		public int weakassstartuppatch;
	}
}
