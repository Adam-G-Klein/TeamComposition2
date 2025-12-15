using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x0200009F RID: 159
	public class HolsterCard : CustomCard
	{
		// Token: 0x06000459 RID: 1113 RVA: 0x0001E951 File Offset: 0x0001CB51
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0001E953 File Offset: 0x0001CB53
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<HolsterMono>();
			block.cdMultiplier *= 0.85f;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0001E974 File Offset: 0x0001CB74
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001E976 File Offset: 0x0001CB76
		protected override string GetTitle()
		{
			return "Holster";
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001E97D File Offset: 0x0001CB7D
		protected override string GetDescription()
		{
			return "Blocking fires an extra bullet with double damage! (Does not consume ammo)";
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0001E984 File Offset: 0x0001CB84
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Holster");
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001E995 File Offset: 0x0001CB95
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0001E998 File Offset: 0x0001CB98
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Block Cooldown",
					amount = "-15%",
					simepleAmount = 6
				}
			};
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0001E9CC File Offset: 0x0001CBCC
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 3;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001E9CF File Offset: 0x0001CBCF
		public override string GetModName()
		{
			return "CR";
		}
	}
}
