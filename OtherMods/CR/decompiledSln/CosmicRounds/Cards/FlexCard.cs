using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000A0 RID: 160
	public class FlexCard : CustomCard
	{
		// Token: 0x06000464 RID: 1124 RVA: 0x0001E9DE File Offset: 0x0001CBDE
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001E9E0 File Offset: 0x0001CBE0
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<FlexMono>();
			block.cdAdd += 0.25f;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001EA01 File Offset: 0x0001CC01
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0001EA03 File Offset: 0x0001CC03
		protected override string GetTitle()
		{
			return "Flex";
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0001EA0A File Offset: 0x0001CC0A
		protected override string GetDescription()
		{
			return "Damaging enemies makes you do an extra block.";
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0001EA11 File Offset: 0x0001CC11
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Flex");
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0001EA22 File Offset: 0x0001CC22
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0001EA28 File Offset: 0x0001CC28
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = false,
					stat = "Block Cooldown",
					amount = "+0.25s",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Ability Cooldown",
					amount = "3s",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0001EA93 File Offset: 0x0001CC93
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 0;
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0001EA96 File Offset: 0x0001CC96
		public override string GetModName()
		{
			return "CR";
		}
	}
}
