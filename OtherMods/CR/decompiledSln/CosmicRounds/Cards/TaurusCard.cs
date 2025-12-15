using System;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000E5 RID: 229
	public class TaurusCard : CustomCard
	{
		// Token: 0x0600076B RID: 1899 RVA: 0x00024353 File Offset: 0x00022553
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.categories = new CardCategory[]
			{
				ZodiacCard.ZodiacClass
			};
			block.cdMultiplier = 0.7f;
			TaurusCard.card = cardInfo;
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0002437B File Offset: 0x0002257B
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.damage *= 1.3f;
			gun.projectileColor = new Color(1f, 0.5f, 1f, 1f);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x000243AE File Offset: 0x000225AE
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x000243B0 File Offset: 0x000225B0
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x000243C8 File Offset: 0x000225C8
		protected override string GetTitle()
		{
			return "Taurus";
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x000243CF File Offset: 0x000225CF
		protected override string GetDescription()
		{
			return "Strong and dependable...";
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x000243D6 File Offset: 0x000225D6
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Taurus");
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x000243E7 File Offset: 0x000225E7
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x000243EC File Offset: 0x000225EC
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+30%"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Block Cooldown",
					amount = "-30%"
				}
			};
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x00024449 File Offset: 0x00022649
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 8;
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0002444C File Offset: 0x0002264C
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004A7 RID: 1191
		internal static CardInfo card;
	}
}
