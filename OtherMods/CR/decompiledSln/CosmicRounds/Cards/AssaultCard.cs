using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000D5 RID: 213
	public class AssaultCard : CustomCard
	{
		// Token: 0x060006B1 RID: 1713 RVA: 0x000233D6 File Offset: 0x000215D6
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x000233D8 File Offset: 0x000215D8
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<AssaultMono>();
			data.maxHealth *= 1.25f;
			gunAmmo.reloadTimeMultiplier *= 0.75f;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0002340B File Offset: 0x0002160B
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0002340D File Offset: 0x0002160D
		protected override string GetTitle()
		{
			return "Assault";
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00023414 File Offset: 0x00021614
		protected override string GetDescription()
		{
			return "Whenever you start or finish reloading, slow and deal unblockable damage to nearby enemies based on their max health.";
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0002341B File Offset: 0x0002161B
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Assault");
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0002342C File Offset: 0x0002162C
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00023430 File Offset: 0x00021630
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
					amount = "+25%",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Reload Time",
					amount = "-25%",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0002349B File Offset: 0x0002169B
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 0;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0002349E File Offset: 0x0002169E
		public override string GetModName()
		{
			return "CR";
		}
	}
}
