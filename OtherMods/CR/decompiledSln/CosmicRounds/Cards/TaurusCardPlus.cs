using System;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000E6 RID: 230
	public class TaurusCardPlus : CustomCard
	{
		// Token: 0x06000777 RID: 1911 RVA: 0x0002445B File Offset: 0x0002265B
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			block.cdMultiplier = 0.4f;
			TaurusCardPlus.card = cardInfo;
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0002446F File Offset: 0x0002266F
		public override bool GetEnabled()
		{
			return false;
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x00024472 File Offset: 0x00022672
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.damage *= 1.6f;
			gun.projectileColor = new Color(1f, 0.5f, 1f, 1f);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x000244A5 File Offset: 0x000226A5
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x000244A7 File Offset: 0x000226A7
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x000244BF File Offset: 0x000226BF
		protected override string GetTitle()
		{
			return "Taurus+";
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x000244C6 File Offset: 0x000226C6
		protected override string GetDescription()
		{
			return "Strong and dependable...";
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x000244CD File Offset: 0x000226CD
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Taurus");
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x000244DE File Offset: 0x000226DE
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x000244E4 File Offset: 0x000226E4
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+60%"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Block Cooldown",
					amount = "-60%"
				}
			};
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00024541 File Offset: 0x00022741
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 8;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00024544 File Offset: 0x00022744
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004A8 RID: 1192
		internal static CardInfo card;
	}
}
