using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000DB RID: 219
	public class EnchantCard : CustomCard
	{
		// Token: 0x060006F3 RID: 1779 RVA: 0x00023861 File Offset: 0x00021A61
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0002386A File Offset: 0x00021A6A
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<EnchantMono>();
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00023878 File Offset: 0x00021A78
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0002387A File Offset: 0x00021A7A
		protected override string GetTitle()
		{
			return "Enchant";
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x00023881 File Offset: 0x00021A81
		protected override string GetDescription()
		{
			return "When your block IS available (after blocking at least once):";
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x00023888 File Offset: 0x00021A88
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Enchant");
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x00023899 File Offset: 0x00021A99
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0002389C File Offset: 0x00021A9C
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+30%",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Projectile Speed",
					amount = "+30%",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bounces",
					amount = "+5",
					simepleAmount = 3
				}
			};
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00023933 File Offset: 0x00021B33
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 8;
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00023936 File Offset: 0x00021B36
		public override string GetModName()
		{
			return "CR";
		}
	}
}
