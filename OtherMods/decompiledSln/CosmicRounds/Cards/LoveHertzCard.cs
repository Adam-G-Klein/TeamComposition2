using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000C4 RID: 196
	public class LoveHertzCard : CustomCard
	{
		// Token: 0x060005F5 RID: 1525 RVA: 0x00021C7A File Offset: 0x0001FE7A
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00021C83 File Offset: 0x0001FE83
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<LoveHertzMono>();
			data.maxHealth *= 1.3f;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00021CA4 File Offset: 0x0001FEA4
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00021CA6 File Offset: 0x0001FEA6
		protected override string GetTitle()
		{
			return "Love Hertz";
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00021CAD File Offset: 0x0001FEAD
		protected override string GetDescription()
		{
			return "Stun and Silence enemies that are nearby every 3 seconds! You heal a bit of health when this happens as well!";
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00021CB4 File Offset: 0x0001FEB4
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Lovehertz");
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00021CC5 File Offset: 0x0001FEC5
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00021CC8 File Offset: 0x0001FEC8
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
				}
			};
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00021CFC File Offset: 0x0001FEFC
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 8;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00021CFF File Offset: 0x0001FEFF
		public override string GetModName()
		{
			return "CR";
		}
	}
}
