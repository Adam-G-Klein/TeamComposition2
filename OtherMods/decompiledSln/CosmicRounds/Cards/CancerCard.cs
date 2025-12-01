using System;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000E9 RID: 233
	public class CancerCard : CustomCard
	{
		// Token: 0x0600079D RID: 1949 RVA: 0x0002477C File Offset: 0x0002297C
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.categories = new CardCategory[]
			{
				ZodiacCard.ZodiacClass
			};
			statModifiers.lifeSteal = 0.3f;
			statModifiers.secondsToTakeDamageOver = 2f;
			CancerCard.card = cardInfo;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x000247B0 File Offset: 0x000229B0
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = Color.magenta;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x000247BD File Offset: 0x000229BD
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x000247BF File Offset: 0x000229BF
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x000247D7 File Offset: 0x000229D7
		protected override string GetTitle()
		{
			return "Cancer";
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x000247DE File Offset: 0x000229DE
		protected override string GetDescription()
		{
			return "Sentimental and protective...";
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x000247E5 File Offset: 0x000229E5
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Cancer");
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x000247F6 File Offset: 0x000229F6
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x000247FC File Offset: 0x000229FC
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Lifesteal",
					amount = "+30%"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Decay",
					amount = "+2s"
				}
			};
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00024859 File Offset: 0x00022A59
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 4;
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0002485C File Offset: 0x00022A5C
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004AB RID: 1195
		internal static CardInfo card;
	}
}
