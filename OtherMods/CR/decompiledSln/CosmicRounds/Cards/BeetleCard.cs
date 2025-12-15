using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x0200008D RID: 141
	public class BeetleCard : CustomCard
	{
		// Token: 0x06000393 RID: 915 RVA: 0x0001D082 File Offset: 0x0001B282
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			statModifiers.health = 1.3f;
			statModifiers.movementSpeed = 0.7f;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0001D09C File Offset: 0x0001B29C
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<BeetleMono>();
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0001D0AA File Offset: 0x0001B2AA
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0001D0AC File Offset: 0x0001B2AC
		protected override string GetTitle()
		{
			return "Beetle";
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001D0B3 File Offset: 0x0001B2B3
		protected override string GetDescription()
		{
			return "Gain a beetle shell that regenerates your health until you reach max.";
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001D0BA File Offset: 0x0001B2BA
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Beetle");
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0001D0CB File Offset: 0x0001B2CB
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0001D0D0 File Offset: 0x0001B2D0
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
					positive = true,
					stat = "Life Regen",
					amount = "+5",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Movement Speed",
					amount = "-30%",
					simepleAmount = 6
				}
			};
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0001D167 File Offset: 0x0001B367
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 2;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0001D16A File Offset: 0x0001B36A
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x0400049A RID: 1178
		public AssetBundle Asset;
	}
}
