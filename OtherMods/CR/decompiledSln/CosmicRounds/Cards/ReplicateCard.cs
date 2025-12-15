using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000FC RID: 252
	public class ReplicateCard : CustomCard
	{
		// Token: 0x06000883 RID: 2179 RVA: 0x00025950 File Offset: 0x00023B50
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00025959 File Offset: 0x00023B59
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<ReplicateMono>();
			block.cdMultiplier *= 1.15f;
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x0002597A File Offset: 0x00023B7A
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0002597C File Offset: 0x00023B7C
		protected override string GetTitle()
		{
			return "Replicate";
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00025983 File Offset: 0x00023B83
		protected override string GetDescription()
		{
			return "After 4s pass, you may block an additional time without it going on cooldown.";
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x0002598A File Offset: 0x00023B8A
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Replicate");
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0002599B File Offset: 0x00023B9B
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x000259A0 File Offset: 0x00023BA0
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = false,
					stat = "Block Cooldown",
					amount = "+15%",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Ability Cooldown",
					amount = "4s",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x00025A0B File Offset: 0x00023C0B
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 2;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00025A0E File Offset: 0x00023C0E
		public override string GetModName()
		{
			return "CR";
		}
	}
}
