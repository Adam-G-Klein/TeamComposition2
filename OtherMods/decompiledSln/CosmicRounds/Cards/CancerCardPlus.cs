using System;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000EA RID: 234
	public class CancerCardPlus : CustomCard
	{
		// Token: 0x060007A9 RID: 1961 RVA: 0x0002486B File Offset: 0x00022A6B
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			statModifiers.lifeSteal = 0.6f;
			statModifiers.secondsToTakeDamageOver = 4f;
			CancerCardPlus.card = cardInfo;
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0002488B File Offset: 0x00022A8B
		public override bool GetEnabled()
		{
			return false;
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0002488E File Offset: 0x00022A8E
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = Color.magenta;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0002489B File Offset: 0x00022A9B
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0002489D File Offset: 0x00022A9D
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x000248B5 File Offset: 0x00022AB5
		protected override string GetTitle()
		{
			return "Cancer+";
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x000248BC File Offset: 0x00022ABC
		protected override string GetDescription()
		{
			return "Sentimental and protective...";
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x000248C3 File Offset: 0x00022AC3
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Cancer");
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x000248D4 File Offset: 0x00022AD4
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x000248D8 File Offset: 0x00022AD8
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
					stat = "Decay",
					amount = "+4s"
				}
			};
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00024935 File Offset: 0x00022B35
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 4;
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x00024938 File Offset: 0x00022B38
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004AC RID: 1196
		internal static CardInfo card;
	}
}
