using System;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000DC RID: 220
	public class ArrayCard : CustomCard
	{
		// Token: 0x060006FE RID: 1790 RVA: 0x00023945 File Offset: 0x00021B45
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.ammo = 3;
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x0002394E File Offset: 0x00021B4E
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.attackSpeed *= 0.2f;
			gun.spread += 0.1f;
			gun.damage *= 0.6f;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x00023986 File Offset: 0x00021B86
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x00023988 File Offset: 0x00021B88
		protected override string GetTitle()
		{
			return "Array";
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0002398F File Offset: 0x00021B8F
		protected override string GetDescription()
		{
			return null;
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x00023992 File Offset: 0x00021B92
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Array");
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x000239A3 File Offset: 0x00021BA3
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000239A8 File Offset: 0x00021BA8
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "ATK Speed",
					amount = "+300%",
					simepleAmount = 3
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Ammo",
					amount = "+3",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Damage",
					amount = "-40%",
					simepleAmount = 6
				}
			};
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00023A3F File Offset: 0x00021C3F
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 1;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00023A42 File Offset: 0x00021C42
		public override string GetModName()
		{
			return "CR";
		}
	}
}
