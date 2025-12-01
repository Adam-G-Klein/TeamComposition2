using System;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x02000090 RID: 144
	public class SpeedUpCard : CustomCard
	{
		// Token: 0x060003B4 RID: 948 RVA: 0x0001D451 File Offset: 0x0001B651
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			block.cdMultiplier = 0.8f;
			statModifiers.movementSpeed = 1.35f;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0001D46B File Offset: 0x0001B66B
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gunAmmo.reloadTimeMultiplier *= 0.8f;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0001D47F File Offset: 0x0001B67F
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001D481 File Offset: 0x0001B681
		protected override string GetTitle()
		{
			return "Speed Up";
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001D488 File Offset: 0x0001B688
		protected override string GetDescription()
		{
			return "Come on, step it up!";
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0001D48F File Offset: 0x0001B68F
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_SpeedUp");
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0001D4A0 File Offset: 0x0001B6A0
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0001D4A4 File Offset: 0x0001B6A4
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Movement Speed",
					amount = "+35%",
					simepleAmount = 1
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Reload Time",
					amount = "-20%",
					simepleAmount = 5
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Block Cooldown",
					amount = "-20%",
					simepleAmount = 5
				}
			};
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001D53B File Offset: 0x0001B73B
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 2;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0001D53E File Offset: 0x0001B73E
		public override string GetModName()
		{
			return "CR";
		}
	}
}
