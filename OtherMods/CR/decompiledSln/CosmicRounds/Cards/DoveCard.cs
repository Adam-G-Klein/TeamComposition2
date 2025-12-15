using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000BF RID: 191
	public class DoveCard : CustomCard
	{
		// Token: 0x060005BE RID: 1470 RVA: 0x0002177D File Offset: 0x0001F97D
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.ammo = 2;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00021788 File Offset: 0x0001F988
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.gravity *= -0.05f;
			gun.damage *= 0.5f;
			gun.numberOfProjectiles += 2;
			gun.spread += 0.1f;
			gun.projectileColor = Color.white;
			player.gameObject.AddComponent<DoveSpriteMono>();
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x000217F0 File Offset: 0x0001F9F0
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x000217F2 File Offset: 0x0001F9F2
		protected override string GetTitle()
		{
			return "Dove";
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x000217F9 File Offset: 0x0001F9F9
		protected override string GetDescription()
		{
			return "Set loose a dule of doves that fly upwards.";
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00021800 File Offset: 0x0001FA00
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Dove");
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00021811 File Offset: 0x0001FA11
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00021814 File Offset: 0x0001FA14
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Gravity",
					amount = "-105%",
					simepleAmount = 7
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bullets",
					amount = "+2",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Ammo",
					amount = "+2",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Damage",
					amount = "-50%",
					simepleAmount = 5
				}
			};
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000218D7 File Offset: 0x0001FAD7
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 7;
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000218DA File Offset: 0x0001FADA
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004A0 RID: 1184
		public AssetBundle Asset;
	}
}
