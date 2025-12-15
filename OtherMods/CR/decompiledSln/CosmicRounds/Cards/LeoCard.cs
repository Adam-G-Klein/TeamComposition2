using System;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000EB RID: 235
	public class LeoCard : CustomCard
	{
		// Token: 0x060007B6 RID: 1974 RVA: 0x00024947 File Offset: 0x00022B47
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.categories = new CardCategory[]
			{
				ZodiacCard.ZodiacClass
			};
			LeoCard.card = cardInfo;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x00024963 File Offset: 0x00022B63
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.attackSpeed *= 0.5f;
			gun.projectileSpeed *= 1.3f;
			gun.projectileColor = Color.yellow;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x00024994 File Offset: 0x00022B94
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00024996 File Offset: 0x00022B96
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x000249AE File Offset: 0x00022BAE
		protected override string GetTitle()
		{
			return "Leo";
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x000249B5 File Offset: 0x00022BB5
		protected override string GetDescription()
		{
			return "Fiery and courageous...";
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x000249BC File Offset: 0x00022BBC
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Leo");
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x000249CD File Offset: 0x00022BCD
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x000249D0 File Offset: 0x00022BD0
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "ATK Speed",
					amount = "+100%"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Speed",
					amount = "+30%"
				}
			};
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00024A2D File Offset: 0x00022C2D
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 6;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00024A30 File Offset: 0x00022C30
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004AD RID: 1197
		internal static CardInfo card;
	}
}
