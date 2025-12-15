using System;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000E8 RID: 232
	public class GeminiCardPlus : CustomCard
	{
		// Token: 0x06000790 RID: 1936 RVA: 0x00024670 File Offset: 0x00022870
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.numberOfProjectiles = 2;
			gun.ammo = 6;
			gun.spread = 0.1f;
			gun.damage = 0.8f;
			GeminiCardPlus.card = cardInfo;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x0002469C File Offset: 0x0002289C
		public override bool GetEnabled()
		{
			return false;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x0002469F File Offset: 0x0002289F
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = Color.yellow;
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x000246AC File Offset: 0x000228AC
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x000246AE File Offset: 0x000228AE
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x000246C6 File Offset: 0x000228C6
		protected override string GetTitle()
		{
			return "Gemini+";
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x000246CD File Offset: 0x000228CD
		protected override string GetDescription()
		{
			return "Versatile and kind...";
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x000246D4 File Offset: 0x000228D4
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Gemini");
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x000246E5 File Offset: 0x000228E5
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x000246E8 File Offset: 0x000228E8
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullets",
					amount = "+2"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Ammo",
					amount = "+6"
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Damage",
					amount = "-20%"
				}
			};
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0002476A File Offset: 0x0002296A
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 1;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0002476D File Offset: 0x0002296D
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004AA RID: 1194
		internal static CardInfo card;
	}
}
