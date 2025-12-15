using System;
using ClassesManagerReborn.Util;
using CR.MonoBehaviors;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000FE RID: 254
	public class ZodiacCardPlus : CustomCard
	{
		// Token: 0x0600089B RID: 2203 RVA: 0x00025ADF File Offset: 0x00023CDF
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			statModifiers.health = 1.4f;
			cardInfo.allowMultiple = false;
			ZodiacCardPlus.card = cardInfo;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00025AFA File Offset: 0x00023CFA
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			if (player.gameObject.GetComponent<ZodiacMono>() == null)
			{
				player.gameObject.AddComponent<ZodiacMono>();
			}
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00025B1B File Offset: 0x00023D1B
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00025B1D File Offset: 0x00023D1D
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00025B35 File Offset: 0x00023D35
		protected override string GetTitle()
		{
			return "Zodiac+";
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00025B3C File Offset: 0x00023D3C
		protected override string GetDescription()
		{
			return "Upgrades your Zodiac Cards, doubling their effects!";
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00025B43 File Offset: 0x00023D43
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Zodiac");
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00025B54 File Offset: 0x00023D54
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00025B57 File Offset: 0x00023D57
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

		// Token: 0x060008A4 RID: 2212 RVA: 0x00025B84 File Offset: 0x00023D84
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 8;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00025B87 File Offset: 0x00023D87
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004BD RID: 1213
		internal static CardInfo card;
	}
}
