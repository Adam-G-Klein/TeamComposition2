using System;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000E4 RID: 228
	public class AriesCardPlus : CustomCard
	{
		// Token: 0x0600075E RID: 1886 RVA: 0x0002426F File Offset: 0x0002246F
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			statModifiers.movementSpeed = 1.6f;
			AriesCardPlus.card = cardInfo;
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x00024283 File Offset: 0x00022483
		public override bool GetEnabled()
		{
			return false;
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x00024286 File Offset: 0x00022486
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.damage *= 1.6f;
			gun.projectileColor = Color.red;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x000242A5 File Offset: 0x000224A5
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x000242A7 File Offset: 0x000224A7
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x000242BF File Offset: 0x000224BF
		protected override string GetTitle()
		{
			return "Aries+";
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x000242C6 File Offset: 0x000224C6
		protected override string GetDescription()
		{
			return "Quick and competitive...";
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x000242CD File Offset: 0x000224CD
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Aries");
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x000242DE File Offset: 0x000224DE
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x000242E4 File Offset: 0x000224E4
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+60%"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Movement Speed",
					amount = "+60%"
				}
			};
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x00024341 File Offset: 0x00022541
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 0;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x00024344 File Offset: 0x00022544
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004A6 RID: 1190
		internal static CardInfo card;
	}
}
