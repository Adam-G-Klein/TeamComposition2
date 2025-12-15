using System;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000FD RID: 253
	public class ZodiacCard : CustomCard
	{
		// Token: 0x0600088E RID: 2190 RVA: 0x00025A1D File Offset: 0x00023C1D
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			statModifiers.health = 1.4f;
			cardInfo.allowMultiple = false;
			ZodiacCard.card = cardInfo;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00025A38 File Offset: 0x00023C38
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00025A3A File Offset: 0x00023C3A
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00025A3C File Offset: 0x00023C3C
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false);
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00025A4B File Offset: 0x00023C4B
		protected override string GetTitle()
		{
			return "Zodiac";
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00025A52 File Offset: 0x00023C52
		protected override string GetDescription()
		{
			return "Unlocks simple but sweet Zodiac Cards!";
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00025A59 File Offset: 0x00023C59
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Zodiac");
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00025A6A File Offset: 0x00023C6A
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00025A6D File Offset: 0x00023C6D
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
					amount = "+40%"
				}
			};
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00025A9A File Offset: 0x00023C9A
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 8;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00025A9D File Offset: 0x00023C9D
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004B9 RID: 1209
		internal static CardInfo card;

		// Token: 0x040004BA RID: 1210
		public static string ZodiacClassName = "Zodiac";

		// Token: 0x040004BB RID: 1211
		public static CardCategory ZodiacClass = CustomCardCategories.instance.CardCategory("Zodiac");

		// Token: 0x040004BC RID: 1212
		public static CardCategory[] zodiacClass = new CardCategory[]
		{
			ZodiacCard.ZodiacClass
		};
	}
}
