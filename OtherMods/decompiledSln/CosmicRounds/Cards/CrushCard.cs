using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000F7 RID: 247
	public class CrushCard : CustomCard
	{
		// Token: 0x0600084C RID: 2124 RVA: 0x000254C7 File Offset: 0x000236C7
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x000254C9 File Offset: 0x000236C9
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<CrushMono>();
			data.maxHealth *= 1.3f;
			block.cdAdd += 0.5f;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x000254FD File Offset: 0x000236FD
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x000254FF File Offset: 0x000236FF
		protected override string GetTitle()
		{
			return "Crush";
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x00025506 File Offset: 0x00023706
		protected override string GetDescription()
		{
			return "Blocking crushes enemies, damaging them and increasing their gravity for a couple of seconds.";
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0002550D File Offset: 0x0002370D
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Crush");
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x0002551E File Offset: 0x0002371E
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x00025524 File Offset: 0x00023724
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
					amount = "+30%",
					simepleAmount = 1
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

		// Token: 0x06000854 RID: 2132 RVA: 0x0002558F File Offset: 0x0002378F
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 0;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00025592 File Offset: 0x00023792
		public override string GetModName()
		{
			return "CR";
		}
	}
}
