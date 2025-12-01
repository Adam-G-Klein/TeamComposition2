using System;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000F2 RID: 242
	public class ScorpioCardPlus : CustomCard
	{
		// Token: 0x0600080D RID: 2061 RVA: 0x00024FBB File Offset: 0x000231BB
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			statModifiers.lifeSteal = 0.6f;
			ScorpioCardPlus.card = cardInfo;
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00024FCF File Offset: 0x000231CF
		public override bool GetEnabled()
		{
			return false;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00024FD2 File Offset: 0x000231D2
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gunAmmo.reloadTimeMultiplier *= 0.4f;
			gun.projectileColor = Color.magenta;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00024FF1 File Offset: 0x000231F1
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00024FF3 File Offset: 0x000231F3
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0002500B File Offset: 0x0002320B
		protected override string GetTitle()
		{
			return "Scorpio+";
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00025012 File Offset: 0x00023212
		protected override string GetDescription()
		{
			return "Brave and resourceful...";
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00025019 File Offset: 0x00023219
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Scorpio");
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0002502A File Offset: 0x0002322A
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00025030 File Offset: 0x00023230
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Lifesteal",
					amount = "+60%"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Reload Time",
					amount = "-60%"
				}
			};
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0002508D File Offset: 0x0002328D
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 4;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00025090 File Offset: 0x00023290
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004B4 RID: 1204
		internal static CardInfo card;
	}
}
