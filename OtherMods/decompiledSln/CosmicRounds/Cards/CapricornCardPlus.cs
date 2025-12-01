using System;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000F6 RID: 246
	public class CapricornCardPlus : CustomCard
	{
		// Token: 0x0600083F RID: 2111 RVA: 0x000253EB File Offset: 0x000235EB
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			statModifiers.secondsToTakeDamageOver = 4f;
			statModifiers.health = 1.6f;
			CapricornCardPlus.card = cardInfo;
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0002540B File Offset: 0x0002360B
		public override bool GetEnabled()
		{
			return false;
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0002540E File Offset: 0x0002360E
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = Color.yellow;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0002541B File Offset: 0x0002361B
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0002541D File Offset: 0x0002361D
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00025435 File Offset: 0x00023635
		protected override string GetTitle()
		{
			return "Capricorn+";
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0002543C File Offset: 0x0002363C
		protected override string GetDescription()
		{
			return "Serious and disciplined...";
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00025443 File Offset: 0x00023643
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Capricorn");
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00025454 File Offset: 0x00023654
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00025458 File Offset: 0x00023658
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
					amount = "+60%"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Decay",
					amount = "+2s"
				}
			};
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x000254B5 File Offset: 0x000236B5
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 6;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x000254B8 File Offset: 0x000236B8
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004B8 RID: 1208
		internal static CardInfo card;
	}
}
