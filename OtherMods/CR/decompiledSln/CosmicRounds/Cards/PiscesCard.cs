using System;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000E1 RID: 225
	public class PiscesCard : CustomCard
	{
		// Token: 0x06000739 RID: 1849 RVA: 0x00023FB0 File Offset: 0x000221B0
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.categories = new CardCategory[]
			{
				ZodiacCard.ZodiacClass
			};
			statModifiers.health = 1.3f;
			statModifiers.movementSpeed = 1.3f;
			PiscesCard.card = cardInfo;
		}

		// Token: 0x0600073A RID: 1850 RVA: 0x00023FE4 File Offset: 0x000221E4
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = Color.green;
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x00023FF1 File Offset: 0x000221F1
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x00023FF3 File Offset: 0x000221F3
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0002400B File Offset: 0x0002220B
		protected override string GetTitle()
		{
			return "Pisces";
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x00024012 File Offset: 0x00022212
		protected override string GetDescription()
		{
			return "Affectionate and empathetic...";
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00024019 File Offset: 0x00022219
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Pisces");
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0002402A File Offset: 0x0002222A
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x00024030 File Offset: 0x00022230
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
					stat = "Movement Speed",
					amount = "+30%"
				}
			};
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0002408D File Offset: 0x0002228D
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 5;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x00024090 File Offset: 0x00022290
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004A3 RID: 1187
		internal static CardInfo card;
	}
}
