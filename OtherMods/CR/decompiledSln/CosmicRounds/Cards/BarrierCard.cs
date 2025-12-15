using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000BE RID: 190
	public class BarrierCard : CustomCard
	{
		// Token: 0x060005B3 RID: 1459 RVA: 0x000216B2 File Offset: 0x0001F8B2
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x000216BB File Offset: 0x0001F8BB
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<BarrierMono>();
			block.cdAdd += 0.25f;
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x000216DC File Offset: 0x0001F8DC
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x000216DE File Offset: 0x0001F8DE
		protected override string GetTitle()
		{
			return "Barrier";
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x000216E5 File Offset: 0x0001F8E5
		protected override string GetDescription()
		{
			return "Blocking conjures a barrier that destroys enemy bullets.";
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x000216EC File Offset: 0x0001F8EC
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Barrier");
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x000216FD File Offset: 0x0001F8FD
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00021700 File Offset: 0x0001F900
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
					amount = "2s",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x0002176B File Offset: 0x0001F96B
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 2;
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0002176E File Offset: 0x0001F96E
		public override string GetModName()
		{
			return "CR";
		}
	}
}
