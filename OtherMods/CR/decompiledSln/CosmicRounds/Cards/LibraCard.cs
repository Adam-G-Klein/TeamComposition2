using System;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000EF RID: 239
	public class LibraCard : CustomCard
	{
		// Token: 0x060007E8 RID: 2024 RVA: 0x00024CEF File Offset: 0x00022EEF
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.categories = new CardCategory[]
			{
				ZodiacCard.ZodiacClass
			};
			statModifiers.health = 1.3f;
			LibraCard.card = cardInfo;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00024D17 File Offset: 0x00022F17
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gunAmmo.reloadTimeMultiplier *= 0.7f;
			gun.projectileColor = Color.blue;
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00024D36 File Offset: 0x00022F36
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x00024D38 File Offset: 0x00022F38
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00024D50 File Offset: 0x00022F50
		protected override string GetTitle()
		{
			return "Libra";
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00024D57 File Offset: 0x00022F57
		protected override string GetDescription()
		{
			return "Diplomatic and gracious...";
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00024D5E File Offset: 0x00022F5E
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Libra");
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x00024D6F File Offset: 0x00022F6F
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00024D74 File Offset: 0x00022F74
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
					amount = "+30%"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Reload Time",
					amount = "-30%"
				}
			};
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x00024DD1 File Offset: 0x00022FD1
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 2;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x00024DD4 File Offset: 0x00022FD4
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004B1 RID: 1201
		internal static CardInfo card;
	}
}
