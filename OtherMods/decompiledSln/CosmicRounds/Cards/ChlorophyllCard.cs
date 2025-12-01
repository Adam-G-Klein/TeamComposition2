using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000BC RID: 188
	public class ChlorophyllCard : CustomCard
	{
		// Token: 0x0600059D RID: 1437 RVA: 0x00021501 File Offset: 0x0001F701
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0002150A File Offset: 0x0001F70A
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<ChlorophyllMono>();
			gunAmmo.reloadTimeMultiplier *= 2f;
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0002152A File Offset: 0x0001F72A
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0002152C File Offset: 0x0001F72C
		protected override string GetTitle()
		{
			return "Chlorophyll";
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00021533 File Offset: 0x0001F733
		protected override string GetDescription()
		{
			return "Every second while reloading, for up to 5 stacks, you gain:";
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0002153A File Offset: 0x0001F73A
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Chlorophyll");
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0002154B File Offset: 0x0001F74B
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x060005A4 RID: 1444 RVA: 0x00021550 File Offset: 0x0001F750
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Movement Speed",
					amount = "+15%",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Life Regen",
					amount = "+8",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
					amount = "+30%",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Time",
					amount = "+100%",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00021613 File Offset: 0x0001F813
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 5;
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00021616 File Offset: 0x0001F816
		public override string GetModName()
		{
			return "CR";
		}
	}
}
