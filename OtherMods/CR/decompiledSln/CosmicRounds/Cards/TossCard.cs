using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000F8 RID: 248
	public class TossCard : CustomCard
	{
		// Token: 0x06000857 RID: 2135 RVA: 0x000255A1 File Offset: 0x000237A1
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x000255A3 File Offset: 0x000237A3
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<TossMono>();
			data.maxHealth *= 1.3f;
			block.cdAdd += 0.5f;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x000255D7 File Offset: 0x000237D7
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x000255D9 File Offset: 0x000237D9
		protected override string GetTitle()
		{
			return "Toss";
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x000255E0 File Offset: 0x000237E0
		protected override string GetDescription()
		{
			return "Blocking tosses enemies, damaging them and decreases their gravity for a couple of seconds.";
		}

		// Token: 0x0600085C RID: 2140 RVA: 0x000255E7 File Offset: 0x000237E7
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Toss");
		}

		// Token: 0x0600085D RID: 2141 RVA: 0x000255F8 File Offset: 0x000237F8
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x0600085E RID: 2142 RVA: 0x000255FC File Offset: 0x000237FC
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

		// Token: 0x0600085F RID: 2143 RVA: 0x00025667 File Offset: 0x00023867
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 8;
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0002566A File Offset: 0x0002386A
		public override string GetModName()
		{
			return "CR";
		}
	}
}
