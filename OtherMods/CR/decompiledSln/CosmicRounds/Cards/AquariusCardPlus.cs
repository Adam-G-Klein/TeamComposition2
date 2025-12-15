using System;
using System.Collections.Generic;
using System.Linq;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000E0 RID: 224
	public class AquariusCardPlus : CustomCard
	{
		// Token: 0x0600072C RID: 1836 RVA: 0x00023E54 File Offset: 0x00022054
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.reflects = 4;
			AquariusCardPlus.card = cardInfo;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00023E63 File Offset: 0x00022063
		public override bool GetEnabled()
		{
			return false;
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00023E68 File Offset: 0x00022068
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectielSimulatonSpeed *= 0.7f;
			gunAmmo.reloadTimeMultiplier *= 0.4f;
			gun.projectileColor = Color.cyan;
			ObjectsToSpawn item = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(item);
			gun.objectsToSpawn = list.ToArray();
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00023EDF File Offset: 0x000220DF
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00023EE1 File Offset: 0x000220E1
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00023EF9 File Offset: 0x000220F9
		protected override string GetTitle()
		{
			return "Aquarius+";
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x00023F00 File Offset: 0x00022100
		protected override string GetDescription()
		{
			return "Deep and imaginative...";
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x00023F07 File Offset: 0x00022107
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Aquarius");
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x00023F18 File Offset: 0x00022118
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00023F1C File Offset: 0x0002211C
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bounces",
					amount = "+4"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Reload Time",
					amount = "-60%"
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Projectile Speed",
					amount = "-30%"
				}
			};
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00023F9E File Offset: 0x0002219E
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 7;
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x00023FA1 File Offset: 0x000221A1
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004A2 RID: 1186
		internal static CardInfo card;
	}
}
