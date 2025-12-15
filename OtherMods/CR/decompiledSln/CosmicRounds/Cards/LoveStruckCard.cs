using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000D1 RID: 209
	public class LoveStruckCard : CustomCard
	{
		// Token: 0x06000684 RID: 1668 RVA: 0x00022EBD File Offset: 0x000210BD
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x00022EC0 File Offset: 0x000210C0
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<LoveTapMono>();
			block.cdAdd += 0.5f;
			data.maxHealth *= 1.3f;
			if (characterStats.lifeSteal == 0f)
			{
				characterStats.lifeSteal += 0.3f;
				return;
			}
			characterStats.lifeSteal *= 1.3f;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x00022F34 File Offset: 0x00021134
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x00022F36 File Offset: 0x00021136
		protected override string GetTitle()
		{
			return "Love Struck";
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x00022F3D File Offset: 0x0002113D
		protected override string GetDescription()
		{
			return "Blocking does damage to nearby enemies, equal to your lifesteal! (100% Lifesteal = 100 DMG)";
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x00022F44 File Offset: 0x00021144
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Lovetap");
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x00022F55 File Offset: 0x00021155
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00022F58 File Offset: 0x00021158
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
					amount = "+30%",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Lifesteal",
					amount = "+30%",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Block Cooldown",
					amount = "+0.5s",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x00022FEF File Offset: 0x000211EF
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 8;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00022FF2 File Offset: 0x000211F2
		public override string GetModName()
		{
			return "CR";
		}
	}
}
