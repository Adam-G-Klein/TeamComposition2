using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000D6 RID: 214
	public class DingCard : CustomCard
	{
		// Token: 0x060006BC RID: 1724 RVA: 0x000234AD File Offset: 0x000216AD
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x000234AF File Offset: 0x000216AF
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<DingMono>();
			data.maxHealth *= 1.25f;
			gunAmmo.reloadTimeMultiplier *= 0.75f;
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x000234E2 File Offset: 0x000216E2
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x000234E4 File Offset: 0x000216E4
		protected override string GetTitle()
		{
			return "Ding";
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x000234EB File Offset: 0x000216EB
		protected override string GetDescription()
		{
			return "Whenever you start or finish reloading, deal damage to nearby enemies equal to 50% your gun damage.";
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x000234F2 File Offset: 0x000216F2
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Ding");
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00023503 File Offset: 0x00021703
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00023508 File Offset: 0x00021708
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

		// Token: 0x060006C4 RID: 1732 RVA: 0x00023573 File Offset: 0x00021773
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 6;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00023576 File Offset: 0x00021776
		public override string GetModName()
		{
			return "CR";
		}
	}
}
