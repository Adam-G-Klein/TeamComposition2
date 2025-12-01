using System;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000EC RID: 236
	public class LeoCardPlus : CustomCard
	{
		// Token: 0x060007C2 RID: 1986 RVA: 0x00024A3F File Offset: 0x00022C3F
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			LeoCardPlus.card = cardInfo;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00024A47 File Offset: 0x00022C47
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.attackSpeed *= 0.25f;
			gun.projectileSpeed *= 1.6f;
			gun.projectileColor = Color.yellow;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00024A78 File Offset: 0x00022C78
		public override bool GetEnabled()
		{
			return false;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00024A7B File Offset: 0x00022C7B
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00024A7D File Offset: 0x00022C7D
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00024A95 File Offset: 0x00022C95
		protected override string GetTitle()
		{
			return "Leo+";
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00024A9C File Offset: 0x00022C9C
		protected override string GetDescription()
		{
			return "Fiery and courageous...";
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x00024AA3 File Offset: 0x00022CA3
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Leo");
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x00024AB4 File Offset: 0x00022CB4
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x00024AB8 File Offset: 0x00022CB8
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "ATK Speed",
					amount = "+200%"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Speed",
					amount = "+60%"
				}
			};
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00024B15 File Offset: 0x00022D15
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 6;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00024B18 File Offset: 0x00022D18
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004AE RID: 1198
		internal static CardInfo card;
	}
}
