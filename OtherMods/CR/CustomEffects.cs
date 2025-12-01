using System;
using CR.MonoBehaviors;
using UnityEngine;

namespace CR.Extensions
{
	// Token: 0x0200008C RID: 140
	public static class CustomEffects
	{
		// Token: 0x06000391 RID: 913 RVA: 0x0001B633 File Offset: 0x00019833
		public static void DestroyAllEffects(GameObject gameObject)
		{
			CustomEffects.DestroyAllAppliedEffects(gameObject);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0001B63C File Offset: 0x0001983C
		public static void DestroyAllAppliedEffects(GameObject gameObject)
		{
			FearFactorMono[] components = gameObject.GetComponents<FearFactorMono>();
			ShockMono[] components2 = gameObject.GetComponents<ShockMono>();
			StunMono[] components3 = gameObject.GetComponents<StunMono>();
			MosquitoMono[] components4 = gameObject.GetComponents<MosquitoMono>();
			BeetleMono[] components5 = gameObject.GetComponents<BeetleMono>();
			CriticalHitMono[] components6 = gameObject.GetComponents<CriticalHitMono>();
			FlamethrowerMono[] components7 = gameObject.GetComponents<FlamethrowerMono>();
			SyphonMono[] components8 = gameObject.GetComponents<SyphonMono>();
			TaserMono[] components9 = gameObject.GetComponents<TaserMono>();
			HolsterMono[] components10 = gameObject.GetComponents<HolsterMono>();
			FlexMono[] components11 = gameObject.GetComponents<FlexMono>();
			DroneMono[] components12 = gameObject.GetComponents<DroneMono>();
			GoldenGlazeMono[] components13 = gameObject.GetComponents<GoldenGlazeMono>();
			GoldHealthMono[] components14 = gameObject.GetComponents<GoldHealthMono>();
			SugarGlazeMono[] components15 = gameObject.GetComponents<SugarGlazeMono>();
			SugarMoveMono[] components16 = gameObject.GetComponents<SugarMoveMono>();
			MitosisMono[] components17 = gameObject.GetComponents<MitosisMono>();
			MitosisBlockMono[] components18 = gameObject.GetComponents<MitosisBlockMono>();
			MeiosisMono[] components19 = gameObject.GetComponents<MeiosisMono>();
			MeiosisReloadMono[] components20 = gameObject.GetComponents<MeiosisReloadMono>();
			PogoMono[] components21 = gameObject.GetComponents<PogoMono>();
			CloudMono[] components22 = gameObject.GetComponents<CloudMono>();
			PulseMono[] components23 = gameObject.GetComponents<PulseMono>();
			DriveMono[] components24 = gameObject.GetComponents<DriveMono>();
			CriticalMono[] components25 = gameObject.GetComponents<CriticalMono>();
			DropMono[] components26 = gameObject.GetComponents<DropMono>();
			SunMono[] components27 = gameObject.GetComponents<SunMono>();
			MeteorMono[] components28 = gameObject.GetComponents<MeteorMono>();
			UnicornMono[] components29 = gameObject.GetComponents<UnicornMono>();
			RedMono[] components30 = gameObject.GetComponents<RedMono>();
			OrangeMono[] components31 = gameObject.GetComponents<OrangeMono>();
			YellowMono[] components32 = gameObject.GetComponents<YellowMono>();
			GreenMono[] components33 = gameObject.GetComponents<GreenMono>();
			CyanMono[] components34 = gameObject.GetComponents<CyanMono>();
			BlueMono[] components35 = gameObject.GetComponents<BlueMono>();
			PurpleMono[] components36 = gameObject.GetComponents<PurpleMono>();
			PurpleBulletMono[] components37 = gameObject.GetComponents<PurpleBulletMono>();
			PinkMono[] components38 = gameObject.GetComponents<PinkMono>();
			GravityMono[] components39 = gameObject.GetComponents<GravityMono>();
			IgniteMono[] components40 = gameObject.GetComponents<IgniteMono>();
			IgniteEffect[] components41 = gameObject.GetComponents<IgniteEffect>();
			FaeEmbersMono[] components42 = gameObject.GetComponents<FaeEmbersMono>();
			CareenMono[] components43 = gameObject.GetComponents<CareenMono>();
			CometMono[] components44 = gameObject.GetComponents<CometMono>();
			AsteroidMono[] components45 = gameObject.GetComponents<AsteroidMono>();
			PulsarMono[] components46 = gameObject.GetComponents<PulsarMono>();
			GlueMono[] components47 = gameObject.GetComponents<GlueMono>();
			AquaRingMono[] components48 = gameObject.GetComponents<AquaRingMono>();
			QuasarMono[] components49 = gameObject.GetComponents<QuasarMono>();
			CakeMono[] components50 = gameObject.GetComponents<CakeMono>();
			ChlorophyllMono[] components51 = gameObject.GetComponents<ChlorophyllMono>();
			HiveMono[] components52 = gameObject.GetComponents<HiveMono>();
			BarrierMono[] components53 = gameObject.GetComponents<BarrierMono>();
			HeartitionMono[] components54 = gameObject.GetComponents<HeartitionMono>();
			HeartbeatMono[] components55 = gameObject.GetComponents<HeartbeatMono>();
			HeartburnMono[] components56 = gameObject.GetComponents<HeartburnMono>();
			HeartthumpMono[] components57 = gameObject.GetComponents<HeartthumpMono>();
			LoveHertzMono[] components58 = gameObject.GetComponents<LoveHertzMono>();
			SweetheartMono[] components59 = gameObject.GetComponents<SweetheartMono>();
			DarkMono[] components60 = gameObject.GetComponents<DarkMono>();
			RingMono[] components61 = gameObject.GetComponents<RingMono>();
			PingMono[] components62 = gameObject.GetComponents<PingMono>();
			IceShardMono[] components63 = gameObject.GetComponents<IceShardMono>();
			StarMono[] components64 = gameObject.GetComponents<StarMono>();
			PumpkinMono[] components65 = gameObject.GetComponents<PumpkinMono>();
			HeartMono[] components66 = gameObject.GetComponents<HeartMono>();
			BeeSpriteMono[] components67 = gameObject.GetComponents<BeeSpriteMono>();
			BeeShotMono[] components68 = gameObject.GetComponents<BeeShotMono>();
			FireMono[] components69 = gameObject.GetComponents<FireMono>();
			FireballMono[] components70 = gameObject.GetComponents<FireballMono>();
			HolyFireMono[] components71 = gameObject.GetComponents<HolyFireMono>();
			HaloMono[] components72 = gameObject.GetComponents<HaloMono>();
			HolyShotMono[] components73 = gameObject.GetComponents<HolyShotMono>();
			HyperMono[] components74 = gameObject.GetComponents<HyperMono>();
			HyperRegenMono[] components75 = gameObject.GetComponents<HyperRegenMono>();
			AllMono[] components76 = gameObject.GetComponents<AllMono>();
			AllRegenMono[] components77 = gameObject.GetComponents<AllRegenMono>();
			MistletoeMono[] components78 = gameObject.GetComponents<MistletoeMono>();
			IceRing[] components79 = gameObject.GetComponents<IceRing>();
			FrozenMono[] components80 = gameObject.GetComponents<FrozenMono>();
			LoveTapMono[] components81 = gameObject.GetComponents<LoveTapMono>();
			SealMono[] components82 = gameObject.GetComponents<SealMono>();
			SealSealMono[] components83 = gameObject.GetComponents<SealSealMono>();
			BatteryMono[] components84 = gameObject.GetComponents<BatteryMono>();
			AssaultMono[] components85 = gameObject.GetComponents<AssaultMono>();
			DingMono[] components86 = gameObject.GetComponents<DingMono>();
			DischargeMono[] components87 = gameObject.GetComponents<DischargeMono>();
			BulkUpMono[] components88 = gameObject.GetComponents<BulkUpMono>();
			QuantumMono[] components89 = gameObject.GetComponents<QuantumMono>();
			ChargeMono[] components90 = gameObject.GetComponents<ChargeMono>();
			ChargeCDMono[] components91 = gameObject.GetComponents<ChargeCDMono>();
			EnchantMono[] components92 = gameObject.GetComponents<EnchantMono>();
			EnchantCDMono[] components93 = gameObject.GetComponents<EnchantCDMono>();
			CrowSpriteMono[] components94 = gameObject.GetComponents<CrowSpriteMono>();
			HawkSpriteMono[] components95 = gameObject.GetComponents<HawkSpriteMono>();
			DropballSpriteMono[] components96 = gameObject.GetComponents<DropballSpriteMono>();
			DoveSpriteMono[] components97 = gameObject.GetComponents<DoveSpriteMono>();
			VultureSpriteMono[] components98 = gameObject.GetComponents<VultureSpriteMono>();
			SquidMono[] components99 = gameObject.GetComponents<SquidMono>();
			CrushMono[] components100 = gameObject.GetComponents<CrushMono>();
			TossMono[] components101 = gameObject.GetComponents<TossMono>();
			ResonateMono[] components102 = gameObject.GetComponents<ResonateMono>();
			ScarabMono[] components103 = gameObject.GetComponents<ScarabMono>();
			ScarabRegenMono[] components104 = gameObject.GetComponents<ScarabRegenMono>();
			MoonMono[] components105 = gameObject.GetComponents<MoonMono>();
			ReplicateMono[] components106 = gameObject.GetComponents<ReplicateMono>();
			ZodiacMono[] components107 = gameObject.GetComponents<ZodiacMono>();
			foreach (FearFactorMono fearFactorMono in components)
			{
				if (fearFactorMono != null)
				{
					fearFactorMono.componentInChildren.Stop();
					Object.Destroy(fearFactorMono.factorObject);
					Object.Destroy(fearFactorMono.fearEffect);
					Object.Destroy(fearFactorMono.componentInChildren);
					fearFactorMono.Destroy();
				}
			}
			foreach (ShockMono shockMono in components2)
			{
				if (shockMono != null)
				{
					shockMono.Destroy();
				}
			}
			foreach (StunMono stunMono in components3)
			{
				if (stunMono != null)
				{
					stunMono.Destroy();
				}
			}
			foreach (MosquitoMono mosquitoMono in components4)
			{
				if (mosquitoMono != null)
				{
					mosquitoMono.Destroy();
				}
			}
			foreach (BeetleMono beetleMono in components5)
			{
				if (beetleMono != null)
				{
					beetleMono.Destroy();
				}
			}
			foreach (CriticalHitMono criticalHitMono in components6)
			{
				if (criticalHitMono != null)
				{
					criticalHitMono.Destroy();
				}
			}
			foreach (FlamethrowerMono flamethrowerMono in components7)
			{
				if (flamethrowerMono != null)
				{
					flamethrowerMono.Destroy();
				}
			}
			foreach (SyphonMono syphonMono in components8)
			{
				if (syphonMono != null)
				{
					syphonMono.Destroy();
				}
			}
			foreach (TaserMono taserMono in components9)
			{
				if (taserMono != null)
				{
					taserMono.Destroy();
				}
			}
			foreach (HolsterMono holsterMono in components10)
			{
				if (holsterMono != null)
				{
					holsterMono.Destroy();
				}
			}
			foreach (FlexMono flexMono in components11)
			{
				if (flexMono != null)
				{
					flexMono.Destroy();
				}
			}
			foreach (DroneMono droneMono in components12)
			{
				if (droneMono != null)
				{
					droneMono.Destroy();
				}
			}
			foreach (GoldenGlazeMono goldenGlazeMono in components13)
			{
				if (goldenGlazeMono != null)
				{
					goldenGlazeMono.Destroy();
				}
			}
			foreach (GoldHealthMono goldHealthMono in components14)
			{
				if (goldHealthMono != null)
				{
					goldHealthMono.Destroy();
				}
			}
			foreach (SugarGlazeMono sugarGlazeMono in components15)
			{
				if (sugarGlazeMono != null)
				{
					sugarGlazeMono.Destroy();
				}
			}
			foreach (SugarMoveMono sugarMoveMono in components16)
			{
				if (sugarMoveMono != null)
				{
					sugarMoveMono.Destroy();
				}
			}
			foreach (MitosisMono mitosisMono in components17)
			{
				if (mitosisMono != null)
				{
					mitosisMono.Destroy();
				}
			}
			foreach (MitosisBlockMono mitosisBlockMono in components18)
			{
				if (mitosisBlockMono != null)
				{
					mitosisBlockMono.Destroy();
				}
			}
			foreach (MeiosisMono meiosisMono in components19)
			{
				if (meiosisMono != null)
				{
					meiosisMono.Destroy();
				}
			}
			foreach (MeiosisReloadMono meiosisReloadMono in components20)
			{
				if (meiosisReloadMono != null)
				{
					meiosisReloadMono.Destroy();
				}
			}
			foreach (PogoMono pogoMono in components21)
			{
				if (pogoMono != null)
				{
					pogoMono.Destroy();
				}
			}
			foreach (CloudMono cloudMono in components22)
			{
				if (cloudMono != null)
				{
					cloudMono.Destroy();
				}
			}
			foreach (PulseMono pulseMono in components23)
			{
				if (pulseMono != null)
				{
					pulseMono.Destroy();
				}
			}
			foreach (DriveMono driveMono in components24)
			{
				if (driveMono != null)
				{
					driveMono.Destroy();
				}
			}
			foreach (CriticalMono criticalMono in components25)
			{
				if (criticalMono != null)
				{
					criticalMono.Destroy();
				}
			}
			foreach (DropMono dropMono in components26)
			{
				if (dropMono != null)
				{
					dropMono.Destroy();
				}
			}
			foreach (SunMono sunMono in components27)
			{
				if (sunMono != null)
				{
					sunMono.Destroy();
				}
			}
			foreach (MeteorMono meteorMono in components28)
			{
				if (meteorMono != null)
				{
					meteorMono.Destroy();
				}
			}
			foreach (CometMono cometMono in components44)
			{
				if (cometMono != null)
				{
					cometMono.Destroy();
				}
			}
			foreach (UnicornMono unicornMono in components29)
			{
				if (unicornMono != null)
				{
					unicornMono.Destroy();
				}
			}
			foreach (RedMono redMono in components30)
			{
				if (redMono != null)
				{
					redMono.Destroy();
				}
			}
			foreach (OrangeMono orangeMono in components31)
			{
				if (orangeMono != null)
				{
					orangeMono.Destroy();
				}
			}
			foreach (YellowMono yellowMono in components32)
			{
				if (yellowMono != null)
				{
					yellowMono.Destroy();
				}
			}
			foreach (GreenMono greenMono in components33)
			{
				if (greenMono != null)
				{
					greenMono.Destroy();
				}
			}
			foreach (CyanMono cyanMono in components34)
			{
				if (cyanMono != null)
				{
					cyanMono.Destroy();
				}
			}
			foreach (BlueMono blueMono in components35)
			{
				if (blueMono != null)
				{
					blueMono.Destroy();
				}
			}
			foreach (PurpleMono purpleMono in components36)
			{
				if (purpleMono != null)
				{
					purpleMono.Destroy();
				}
			}
			foreach (PurpleBulletMono purpleBulletMono in components37)
			{
				if (purpleBulletMono != null)
				{
					purpleBulletMono.Destroy();
				}
			}
			foreach (PinkMono pinkMono in components38)
			{
				if (pinkMono != null)
				{
					pinkMono.Destroy();
				}
			}
			foreach (GravityMono gravityMono in components39)
			{
				if (gravityMono != null)
				{
					gravityMono.Destroy();
				}
			}
			foreach (IgniteMono igniteMono in components40)
			{
				if (igniteMono != null)
				{
					igniteMono.Destroy();
				}
			}
			foreach (IgniteEffect igniteEffect in components41)
			{
				if (igniteEffect != null)
				{
					igniteEffect.Destroy();
				}
			}
			foreach (FaeEmbersMono faeEmbersMono in components42)
			{
				if (faeEmbersMono != null)
				{
					faeEmbersMono.Destroy();
				}
			}
			foreach (CareenMono careenMono in components43)
			{
				if (careenMono != null)
				{
					careenMono.Destroy();
				}
			}
			foreach (AsteroidMono asteroidMono in components45)
			{
				if (asteroidMono != null)
				{
					asteroidMono.Destroy();
				}
			}
			foreach (PulsarMono pulsarMono in components46)
			{
				if (pulsarMono != null)
				{
					pulsarMono.Destroy();
				}
			}
			foreach (GlueMono glueMono in components47)
			{
				if (glueMono != null)
				{
					glueMono.Destroy();
				}
			}
			foreach (AquaRingMono aquaRingMono in components48)
			{
				if (aquaRingMono != null)
				{
					aquaRingMono.Destroy();
				}
			}
			foreach (QuasarMono quasarMono in components49)
			{
				if (quasarMono != null)
				{
					quasarMono.Destroy();
				}
			}
			foreach (CakeMono cakeMono in components50)
			{
				if (cakeMono != null)
				{
					cakeMono.Destroy();
				}
			}
			foreach (ChlorophyllMono chlorophyllMono in components51)
			{
				if (chlorophyllMono != null)
				{
					chlorophyllMono.Destroy();
				}
			}
			foreach (HiveMono hiveMono in components52)
			{
				if (hiveMono != null)
				{
					hiveMono.Destroy();
				}
			}
			foreach (BarrierMono barrierMono in components53)
			{
				if (barrierMono != null)
				{
					barrierMono.Destroy();
				}
			}
			foreach (HeartitionMono heartitionMono in components54)
			{
				if (heartitionMono != null)
				{
					heartitionMono.Destroy();
				}
			}
			foreach (HeartbeatMono heartbeatMono in components55)
			{
				if (heartbeatMono != null)
				{
					heartbeatMono.Destroy();
				}
			}
			foreach (HeartburnMono heartburnMono in components56)
			{
				if (heartburnMono != null)
				{
					heartburnMono.Destroy();
				}
			}
			foreach (HeartthumpMono heartthumpMono in components57)
			{
				if (heartthumpMono != null)
				{
					heartthumpMono.Destroy();
				}
			}
			foreach (LoveHertzMono loveHertzMono in components58)
			{
				if (loveHertzMono != null)
				{
					loveHertzMono.Destroy();
				}
			}
			foreach (SweetheartMono sweetheartMono in components59)
			{
				if (sweetheartMono != null)
				{
					sweetheartMono.Destroy();
				}
			}
			foreach (DarkMono darkMono in components60)
			{
				if (darkMono != null)
				{
					darkMono.Destroy();
				}
			}
			foreach (RingMono ringMono in components61)
			{
				if (ringMono != null)
				{
					ringMono.Destroy();
				}
			}
			foreach (PingMono pingMono in components62)
			{
				if (pingMono != null)
				{
					pingMono.Destroy();
				}
			}
			foreach (IceShardMono iceShardMono in components63)
			{
				if (iceShardMono != null)
				{
					iceShardMono.Destroy();
				}
			}
			foreach (StarMono starMono in components64)
			{
				if (starMono != null)
				{
					starMono.Destroy();
				}
			}
			foreach (PumpkinMono pumpkinMono in components65)
			{
				if (pumpkinMono != null)
				{
					pumpkinMono.Destroy();
				}
			}
			foreach (HeartMono heartMono in components66)
			{
				if (heartMono != null)
				{
					heartMono.Destroy();
				}
			}
			foreach (BeeSpriteMono beeSpriteMono in components67)
			{
				if (beeSpriteMono != null)
				{
					beeSpriteMono.Destroy();
				}
			}
			foreach (BeeShotMono beeShotMono in components68)
			{
				if (beeShotMono != null)
				{
					beeShotMono.Destroy();
				}
			}
			foreach (FireMono fireMono in components69)
			{
				if (fireMono != null)
				{
					fireMono.Destroy();
				}
			}
			foreach (FireballMono fireballMono in components70)
			{
				if (fireballMono != null)
				{
					fireballMono.Destroy();
				}
			}
			foreach (HolyFireMono holyFireMono in components71)
			{
				if (holyFireMono != null)
				{
					holyFireMono.Destroy();
				}
			}
			foreach (HaloMono haloMono in components72)
			{
				if (haloMono != null)
				{
					haloMono.Destroy();
				}
			}
			foreach (HolyShotMono holyShotMono in components73)
			{
				if (holyShotMono != null)
				{
					holyShotMono.Destroy();
				}
			}
			foreach (HyperMono hyperMono in components74)
			{
				if (hyperMono != null)
				{
					hyperMono.Destroy();
				}
			}
			foreach (HyperRegenMono hyperRegenMono in components75)
			{
				if (hyperRegenMono != null)
				{
					hyperRegenMono.Destroy();
				}
			}
			foreach (AllMono allMono in components76)
			{
				if (allMono != null)
				{
					allMono.Destroy();
				}
			}
			foreach (AllRegenMono allRegenMono in components77)
			{
				if (allRegenMono != null)
				{
					allRegenMono.Destroy();
				}
			}
			foreach (MistletoeMono mistletoeMono in components78)
			{
				if (mistletoeMono != null)
				{
					mistletoeMono.Destroy();
				}
			}
			foreach (IceRing iceRing in components79)
			{
				if (iceRing != null)
				{
					iceRing.Destroy();
				}
			}
			foreach (FrozenMono frozenMono in components80)
			{
				if (frozenMono != null)
				{
					frozenMono.Destroy();
				}
			}
			foreach (LoveTapMono loveTapMono in components81)
			{
				if (loveTapMono != null)
				{
					loveTapMono.Destroy();
				}
			}
			foreach (SealMono sealMono in components82)
			{
				if (sealMono != null)
				{
					sealMono.Destroy();
				}
			}
			foreach (SealSealMono sealSealMono in components83)
			{
				if (sealSealMono != null)
				{
					sealSealMono.Destroy();
				}
			}
			foreach (BatteryMono batteryMono in components84)
			{
				if (batteryMono != null)
				{
					batteryMono.Destroy();
				}
			}
			foreach (AssaultMono assaultMono in components85)
			{
				if (assaultMono != null)
				{
					assaultMono.Destroy();
				}
			}
			foreach (DingMono dingMono in components86)
			{
				if (dingMono != null)
				{
					dingMono.Destroy();
				}
			}
			foreach (DischargeMono dischargeMono in components87)
			{
				if (dischargeMono != null)
				{
					dischargeMono.Destroy();
				}
			}
			foreach (BulkUpMono bulkUpMono in components88)
			{
				if (bulkUpMono != null)
				{
					bulkUpMono.Destroy();
				}
			}
			foreach (QuantumMono quantumMono in components89)
			{
				if (quantumMono != null)
				{
					quantumMono.Destroy();
				}
			}
			foreach (ChargeMono chargeMono in components90)
			{
				if (chargeMono != null)
				{
					chargeMono.Destroy();
				}
			}
			foreach (ChargeCDMono chargeCDMono in components91)
			{
				if (chargeCDMono != null)
				{
					chargeCDMono.Destroy();
				}
			}
			foreach (EnchantMono enchantMono in components92)
			{
				if (enchantMono != null)
				{
					enchantMono.Destroy();
				}
			}
			foreach (EnchantCDMono enchantCDMono in components93)
			{
				if (enchantCDMono != null)
				{
					enchantCDMono.Destroy();
				}
			}
			foreach (CrowSpriteMono crowSpriteMono in components94)
			{
				if (crowSpriteMono != null)
				{
					crowSpriteMono.Destroy();
				}
			}
			foreach (HawkSpriteMono hawkSpriteMono in components95)
			{
				if (hawkSpriteMono != null)
				{
					hawkSpriteMono.Destroy();
				}
			}
			foreach (DropballSpriteMono dropballSpriteMono in components96)
			{
				if (dropballSpriteMono != null)
				{
					dropballSpriteMono.Destroy();
				}
			}
			foreach (DoveSpriteMono doveSpriteMono in components97)
			{
				if (doveSpriteMono != null)
				{
					doveSpriteMono.Destroy();
				}
			}
			foreach (VultureSpriteMono vultureSpriteMono in components98)
			{
				if (vultureSpriteMono != null)
				{
					vultureSpriteMono.Destroy();
				}
			}
			foreach (SquidMono squidMono in components99)
			{
				if (squidMono != null)
				{
					squidMono.Destroy();
				}
			}
			foreach (CrushMono crushMono in components100)
			{
				if (crushMono != null)
				{
					crushMono.Destroy();
				}
			}
			foreach (TossMono tossMono in components101)
			{
				if (tossMono != null)
				{
					tossMono.Destroy();
				}
			}
			foreach (ResonateMono resonateMono in components102)
			{
				if (resonateMono != null)
				{
					resonateMono.Destroy();
				}
			}
			foreach (ScarabMono scarabMono in components103)
			{
				if (scarabMono != null)
				{
					scarabMono.Destroy();
				}
			}
			foreach (ScarabRegenMono scarabRegenMono in components104)
			{
				if (scarabRegenMono != null)
				{
					scarabRegenMono.Destroy();
				}
			}
			foreach (MoonMono moonMono in components105)
			{
				if (moonMono != null)
				{
					moonMono.Destroy();
				}
			}
			foreach (ReplicateMono replicateMono in components106)
			{
				if (replicateMono != null)
				{
					replicateMono.Destroy();
				}
			}
			foreach (ZodiacMono zodiacMono in components107)
			{
				if (zodiacMono != null)
				{
					zodiacMono.Destroy();
				}
			}
		}
	}
}
