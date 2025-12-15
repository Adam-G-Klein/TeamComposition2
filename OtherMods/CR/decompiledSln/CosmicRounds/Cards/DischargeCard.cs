using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000D7 RID: 215
	public class DischargeCard : CustomCard
	{
		// Token: 0x060006C7 RID: 1735 RVA: 0x00023585 File Offset: 0x00021785
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00023587 File Offset: 0x00021787
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<DischargeMono>();
			data.maxHealth *= 1.25f;
			gunAmmo.reloadTimeMultiplier *= 0.75f;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x000235BA File Offset: 0x000217BA
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x000235BC File Offset: 0x000217BC
		protected override string GetTitle()
		{
			return "Discharge";
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x000235C3 File Offset: 0x000217C3
		protected override string GetDescription()
		{
			return "Whenever you start or finish reloading, deal lingering damage to nearby enemies for 3s equal to 25% of their max health.";
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x000235CA File Offset: 0x000217CA
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Discharge");
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x000235DB File Offset: 0x000217DB
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x000235E0 File Offset: 0x000217E0
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

		// Token: 0x060006CF RID: 1743 RVA: 0x0002364B File Offset: 0x0002184B
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 1;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0002364E File Offset: 0x0002184E
		public override string GetModName()
		{
			return "CR";
		}
	}
}
