using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000A7 RID: 167
	public class MeiosisCard : CustomCard
	{
		// Token: 0x060004B1 RID: 1201 RVA: 0x0001F35E File Offset: 0x0001D55E
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001F367 File Offset: 0x0001D567
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<MeiosisMono>();
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001F375 File Offset: 0x0001D575
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001F377 File Offset: 0x0001D577
		protected override string GetTitle()
		{
			return "Meiosis";
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001F37E File Offset: 0x0001D57E
		protected override string GetDescription()
		{
			return "While reloading, you gain:";
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001F385 File Offset: 0x0001D585
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Meiosis");
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001F396 File Offset: 0x0001D596
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001F39C File Offset: 0x0001D59C
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Movement Speed",
					amount = "+50%",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Block Cooldown",
					amount = "-50%",
					simepleAmount = 6
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Block",
					amount = "+1",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001F433 File Offset: 0x0001D633
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 2;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001F436 File Offset: 0x0001D636
		public override string GetModName()
		{
			return "CR";
		}
	}
}
