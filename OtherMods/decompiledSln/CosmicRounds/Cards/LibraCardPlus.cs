using System;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000F0 RID: 240
	public class LibraCardPlus : CustomCard
	{
		// Token: 0x060007F4 RID: 2036 RVA: 0x00024DE3 File Offset: 0x00022FE3
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			statModifiers.health = 1.6f;
			LibraCardPlus.card = cardInfo;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00024DF7 File Offset: 0x00022FF7
		public override bool GetEnabled()
		{
			return false;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00024DFA File Offset: 0x00022FFA
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gunAmmo.reloadTimeMultiplier *= 0.4f;
			gun.projectileColor = Color.blue;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00024E19 File Offset: 0x00023019
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00024E1B File Offset: 0x0002301B
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00024E33 File Offset: 0x00023033
		protected override string GetTitle()
		{
			return "Libra+";
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00024E3A File Offset: 0x0002303A
		protected override string GetDescription()
		{
			return "Diplomatic and gracious...";
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00024E41 File Offset: 0x00023041
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Libra");
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00024E52 File Offset: 0x00023052
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00024E58 File Offset: 0x00023058
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
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

		// Token: 0x060007FE RID: 2046 RVA: 0x00024EB5 File Offset: 0x000230B5
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 2;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00024EB8 File Offset: 0x000230B8
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004B2 RID: 1202
		internal static CardInfo card;
	}
}
