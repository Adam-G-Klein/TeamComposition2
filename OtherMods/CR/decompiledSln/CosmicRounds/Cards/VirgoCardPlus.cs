using System;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000EE RID: 238
	public class VirgoCardPlus : CustomCard
	{
		// Token: 0x060007DB RID: 2011 RVA: 0x00024C13 File Offset: 0x00022E13
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			block.cdMultiplier = 0.4f;
			gun.attackSpeed = 0.25f;
			VirgoCardPlus.card = cardInfo;
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00024C32 File Offset: 0x00022E32
		public override bool GetEnabled()
		{
			return false;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00024C35 File Offset: 0x00022E35
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = Color.white;
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00024C42 File Offset: 0x00022E42
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00024C44 File Offset: 0x00022E44
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00024C5C File Offset: 0x00022E5C
		protected override string GetTitle()
		{
			return "Virgo+";
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00024C63 File Offset: 0x00022E63
		protected override string GetDescription()
		{
			return "Loyal and gentle...";
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00024C6A File Offset: 0x00022E6A
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Virgo");
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00024C7B File Offset: 0x00022E7B
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00024C80 File Offset: 0x00022E80
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
					stat = "Block Cooldown",
					amount = "-60%"
				}
			};
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00024CDD File Offset: 0x00022EDD
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 3;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00024CE0 File Offset: 0x00022EE0
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004B0 RID: 1200
		internal static CardInfo card;
	}
}
