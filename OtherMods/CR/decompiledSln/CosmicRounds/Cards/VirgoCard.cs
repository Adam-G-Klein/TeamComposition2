using System;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000ED RID: 237
	public class VirgoCard : CustomCard
	{
		// Token: 0x060007CF RID: 1999 RVA: 0x00024B27 File Offset: 0x00022D27
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.categories = new CardCategory[]
			{
				ZodiacCard.ZodiacClass
			};
			block.cdMultiplier = 0.7f;
			gun.attackSpeed = 0.5f;
			VirgoCard.card = cardInfo;
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00024B5A File Offset: 0x00022D5A
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = Color.white;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00024B67 File Offset: 0x00022D67
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00024B69 File Offset: 0x00022D69
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00024B81 File Offset: 0x00022D81
		protected override string GetTitle()
		{
			return "Virgo";
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00024B88 File Offset: 0x00022D88
		protected override string GetDescription()
		{
			return "Loyal and gentle...";
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00024B8F File Offset: 0x00022D8F
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Virgo");
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00024BA0 File Offset: 0x00022DA0
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00024BA4 File Offset: 0x00022DA4
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "ATK Speed",
					amount = "+100%"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Block Cooldown",
					amount = "-30%"
				}
			};
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00024C01 File Offset: 0x00022E01
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 3;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00024C04 File Offset: 0x00022E04
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004AF RID: 1199
		internal static CardInfo card;
	}
}
