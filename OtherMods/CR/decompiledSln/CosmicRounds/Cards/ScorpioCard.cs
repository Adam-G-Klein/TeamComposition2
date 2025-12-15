using System;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000F1 RID: 241
	public class ScorpioCard : CustomCard
	{
		// Token: 0x06000801 RID: 2049 RVA: 0x00024EC7 File Offset: 0x000230C7
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.categories = new CardCategory[]
			{
				ZodiacCard.ZodiacClass
			};
			statModifiers.lifeSteal = 0.3f;
			ScorpioCard.card = cardInfo;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00024EEF File Offset: 0x000230EF
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gunAmmo.reloadTimeMultiplier *= 0.7f;
			gun.projectileColor = Color.magenta;
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00024F0E File Offset: 0x0002310E
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00024F10 File Offset: 0x00023110
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00024F28 File Offset: 0x00023128
		protected override string GetTitle()
		{
			return "Scorpio";
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00024F2F File Offset: 0x0002312F
		protected override string GetDescription()
		{
			return "Brave and resourceful...";
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00024F36 File Offset: 0x00023136
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Scorpio");
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00024F47 File Offset: 0x00023147
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00024F4C File Offset: 0x0002314C
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Lifesteal",
					amount = "+30%"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Reload Time",
					amount = "-30%"
				}
			};
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00024FA9 File Offset: 0x000231A9
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 4;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00024FAC File Offset: 0x000231AC
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004B3 RID: 1203
		internal static CardInfo card;
	}
}
