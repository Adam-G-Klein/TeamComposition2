using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000B8 RID: 184
	public class AquaRingCard : CustomCard
	{
		// Token: 0x0600056C RID: 1388 RVA: 0x00020CA5 File Offset: 0x0001EEA5
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00020CA7 File Offset: 0x0001EEA7
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<AquaRingMono>();
			data.maxHealth *= 1.3f;
			block.cdAdd += 0.5f;
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00020CDB File Offset: 0x0001EEDB
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00020CDD File Offset: 0x0001EEDD
		protected override string GetTitle()
		{
			return "Aqua Ring";
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00020CE4 File Offset: 0x0001EEE4
		protected override string GetDescription()
		{
			return "Blocking conjures a pool of water that heals you over time, based on your max health.";
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x00020CEB File Offset: 0x0001EEEB
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Aqua");
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00020CFC File Offset: 0x0001EEFC
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00020D00 File Offset: 0x0001EF00
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

		// Token: 0x06000574 RID: 1396 RVA: 0x00020D6B File Offset: 0x0001EF6B
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 7;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00020D6E File Offset: 0x0001EF6E
		public override string GetModName()
		{
			return "CR";
		}
	}
}
