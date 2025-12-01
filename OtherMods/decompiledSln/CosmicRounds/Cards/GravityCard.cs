using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000B1 RID: 177
	public class GravityCard : CustomCard
	{
		// Token: 0x0600051F RID: 1311 RVA: 0x000202BA File Offset: 0x0001E4BA
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x000202BC File Offset: 0x0001E4BC
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<GravityMono>();
			data.maxHealth *= 1.3f;
			block.cdAdd += 0.5f;
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x000202F0 File Offset: 0x0001E4F0
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x000202F2 File Offset: 0x0001E4F2
		protected override string GetTitle()
		{
			return "Gravity";
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x000202F9 File Offset: 0x0001E4F9
		protected override string GetDescription()
		{
			return "Blocking slams nearby targets towards the closest barriers, and damages them!";
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00020300 File Offset: 0x0001E500
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Gravity");
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x00020311 File Offset: 0x0001E511
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00020314 File Offset: 0x0001E514
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
					positive = false,
					stat = "Block Cooldown",
					amount = "+0.5s",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0002037F File Offset: 0x0001E57F
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 4;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00020382 File Offset: 0x0001E582
		public override string GetModName()
		{
			return "CR";
		}
	}
}
