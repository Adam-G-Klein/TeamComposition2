using System;
using CR.MonoBehaviors;
using RarityLib.Utils;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000D8 RID: 216
	public class BulkUpCard : CustomCard
	{
		// Token: 0x060006D2 RID: 1746 RVA: 0x0002365D File Offset: 0x0002185D
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			block.cdMultiplier = 2f;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0002366B File Offset: 0x0002186B
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<BulkUpMono>();
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00023679 File Offset: 0x00021879
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0002367B File Offset: 0x0002187B
		protected override string GetTitle()
		{
			return "Bulk Up";
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00023682 File Offset: 0x00021882
		protected override string GetDescription()
		{
			return "Activate your block every second while it's on cooldown!!";
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00023689 File Offset: 0x00021889
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_BulkUp");
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0002369A File Offset: 0x0002189A
		protected override CardInfo.Rarity GetRarity()
		{
			return RarityUtils.GetRarity("Legendary");
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x000236A6 File Offset: 0x000218A6
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = false,
					stat = "Block Cooldown",
					amount = "+100%",
					simepleAmount = 4
				}
			};
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x000236DA File Offset: 0x000218DA
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 0;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x000236DD File Offset: 0x000218DD
		public override string GetModName()
		{
			return "CR";
		}
	}
}
