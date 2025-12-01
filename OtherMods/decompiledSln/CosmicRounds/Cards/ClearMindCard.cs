using System;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000DD RID: 221
	public class ClearMindCard : CustomCard
	{
		// Token: 0x06000709 RID: 1801 RVA: 0x00023A51 File Offset: 0x00021C51
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00023A53 File Offset: 0x00021C53
		public override bool GetEnabled()
		{
			return false;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00023A56 File Offset: 0x00021C56
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.data.stats.lifeSteal = 0f;
			player.data.stats.respawns = 0;
			player.data.stats.secondsToTakeDamageOver = 0f;
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x00023A93 File Offset: 0x00021C93
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x00023A95 File Offset: 0x00021C95
		protected override string GetTitle()
		{
			return "Clear Mind";
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x00023A9C File Offset: 0x00021C9C
		protected override string GetDescription()
		{
			return "Everyone ought to be equal, no?";
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x00023AA3 File Offset: 0x00021CA3
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_ClearMind");
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x00023AB4 File Offset: 0x00021CB4
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x06000711 RID: 1809 RVA: 0x00023AB8 File Offset: 0x00021CB8
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = false,
					stat = "Lifesteal",
					amount = "Reset"
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Revives",
					amount = "Reset"
				}
			};
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x00023B15 File Offset: 0x00021D15
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 7;
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x00023B18 File Offset: 0x00021D18
		public override string GetModName()
		{
			return "CR";
		}
	}
}
