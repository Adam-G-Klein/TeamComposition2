using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000BA RID: 186
	public class CakeCard : CustomCard
	{
		// Token: 0x06000582 RID: 1410 RVA: 0x00020E55 File Offset: 0x0001F055
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00020E5E File Offset: 0x0001F05E
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<CakeMono>();
			gunAmmo.reloadTimeMultiplier *= 1.35f;
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00020E7E File Offset: 0x0001F07E
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00020E80 File Offset: 0x0001F080
		protected override string GetTitle()
		{
			return "Cake";
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00020E87 File Offset: 0x0001F087
		protected override string GetDescription()
		{
			return "(Happy Birthday AngelMoon!) While reloading, you gain:";
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00020E8E File Offset: 0x0001F08E
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Cake");
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00020E9F File Offset: 0x0001F09F
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00020EA4 File Offset: 0x0001F0A4
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Movement Speed",
					amount = "+50%",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Life Regen",
					amount = "+10",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Time",
					amount = "+35%",
					simepleAmount = 2
				}
			};
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x00020F3B File Offset: 0x0001F13B
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 8;
		}

		// Token: 0x0600058B RID: 1419 RVA: 0x00020F3E File Offset: 0x0001F13E
		public override string GetModName()
		{
			return "CR";
		}
	}
}
