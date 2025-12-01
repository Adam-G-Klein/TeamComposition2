using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000DA RID: 218
	public class ChargeCard : CustomCard
	{
		// Token: 0x060006E8 RID: 1768 RVA: 0x000237A9 File Offset: 0x000219A9
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x000237B2 File Offset: 0x000219B2
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<ChargeMono>();
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x000237C0 File Offset: 0x000219C0
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x000237C2 File Offset: 0x000219C2
		protected override string GetTitle()
		{
			return "Charge";
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x000237C9 File Offset: 0x000219C9
		protected override string GetDescription()
		{
			return "When your block ISN'T available:";
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x000237D0 File Offset: 0x000219D0
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Charge");
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x000237E1 File Offset: 0x000219E1
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x000237E4 File Offset: 0x000219E4
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+40%",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Movement Speed",
					amount = "+50%",
					simepleAmount = 2
				}
			};
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0002384F File Offset: 0x00021A4F
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 1;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00023852 File Offset: 0x00021A52
		public override string GetModName()
		{
			return "CR";
		}
	}
}
