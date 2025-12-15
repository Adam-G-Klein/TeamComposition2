using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000C8 RID: 200
	public class PingCard : CustomCard
	{
		// Token: 0x06000621 RID: 1569 RVA: 0x000220F1 File Offset: 0x000202F1
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x000220F3 File Offset: 0x000202F3
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<PingMono>();
			block.cdAdd += 0.5f;
			gun.damage *= 1.3f;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00022126 File Offset: 0x00020326
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00022128 File Offset: 0x00020328
		protected override string GetTitle()
		{
			return "Ping";
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0002212F File Offset: 0x0002032F
		protected override string GetDescription()
		{
			return "Blocking does damage to nearby enemies, equal to 75% of your gun damage.";
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00022136 File Offset: 0x00020336
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Ping");
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00022147 File Offset: 0x00020347
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0002214C File Offset: 0x0002034C
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
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

		// Token: 0x06000629 RID: 1577 RVA: 0x000221B7 File Offset: 0x000203B7
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 6;
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x000221BA File Offset: 0x000203BA
		public override string GetModName()
		{
			return "CR";
		}
	}
}
