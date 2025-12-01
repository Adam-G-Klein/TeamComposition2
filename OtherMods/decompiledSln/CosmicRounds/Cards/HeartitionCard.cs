using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000C0 RID: 192
	public class HeartitionCard : CustomCard
	{
		// Token: 0x060005C9 RID: 1481 RVA: 0x000218E9 File Offset: 0x0001FAE9
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x000218F2 File Offset: 0x0001FAF2
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<HeartitionMono>();
			block.cdAdd += 0.25f;
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00021913 File Offset: 0x0001FB13
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00021915 File Offset: 0x0001FB15
		protected override string GetTitle()
		{
			return "Heartition";
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0002191C File Offset: 0x0001FB1C
		protected override string GetDescription()
		{
			return "Blocking conjures a barrier that heals you for 25% of your health for each bullet destroyed!";
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00021923 File Offset: 0x0001FB23
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Heartition");
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00021934 File Offset: 0x0001FB34
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00021938 File Offset: 0x0001FB38
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
					amount = "2.5s",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000219A3 File Offset: 0x0001FBA3
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 8;
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000219A6 File Offset: 0x0001FBA6
		public override string GetModName()
		{
			return "CR";
		}
	}
}
