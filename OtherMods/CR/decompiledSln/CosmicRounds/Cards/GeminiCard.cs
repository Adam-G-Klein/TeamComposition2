using System;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000E7 RID: 231
	public class GeminiCard : CustomCard
	{
		// Token: 0x06000784 RID: 1924 RVA: 0x00024553 File Offset: 0x00022753
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.categories = new CardCategory[]
			{
				ZodiacCard.ZodiacClass
			};
			gun.numberOfProjectiles = 1;
			gun.ammo = 3;
			gun.spread = 0.1f;
			gun.damage = 0.8f;
			GeminiCard.card = cardInfo;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00024593 File Offset: 0x00022793
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = Color.yellow;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x000245A0 File Offset: 0x000227A0
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x000245A2 File Offset: 0x000227A2
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x000245BA File Offset: 0x000227BA
		protected override string GetTitle()
		{
			return "Gemini";
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x000245C1 File Offset: 0x000227C1
		protected override string GetDescription()
		{
			return "Versatile and kind...";
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x000245C8 File Offset: 0x000227C8
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Gemini");
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x000245D9 File Offset: 0x000227D9
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x000245DC File Offset: 0x000227DC
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet",
					amount = "+1"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Ammo",
					amount = "+3"
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Damage",
					amount = "-20%"
				}
			};
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0002465E File Offset: 0x0002285E
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 1;
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x00024661 File Offset: 0x00022861
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004A9 RID: 1193
		internal static CardInfo card;
	}
}
