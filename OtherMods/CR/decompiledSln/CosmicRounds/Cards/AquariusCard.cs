using System;
using System.Collections.Generic;
using System.Linq;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000DF RID: 223
	public class AquariusCard : CustomCard
	{
		// Token: 0x06000720 RID: 1824 RVA: 0x00023CE9 File Offset: 0x00021EE9
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.categories = new CardCategory[]
			{
				ZodiacCard.ZodiacClass
			};
			gun.reflects = 2;
			AquariusCard.card = cardInfo;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00023D0C File Offset: 0x00021F0C
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectielSimulatonSpeed *= 0.75f;
			gunAmmo.reloadTimeMultiplier *= 0.7f;
			gun.projectileColor = Color.cyan;
			ObjectsToSpawn item = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(item);
			gun.objectsToSpawn = list.ToArray();
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00023D83 File Offset: 0x00021F83
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00023D85 File Offset: 0x00021F85
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00023D9D File Offset: 0x00021F9D
		protected override string GetTitle()
		{
			return "Aquarius";
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x00023DA4 File Offset: 0x00021FA4
		protected override string GetDescription()
		{
			return "Deep and imaginative...";
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x00023DAB File Offset: 0x00021FAB
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Aquarius");
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00023DBC File Offset: 0x00021FBC
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00023DC0 File Offset: 0x00021FC0
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bounces",
					amount = "+2"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Reload Time",
					amount = "-30%"
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Projectile Speed",
					amount = "-25%"
				}
			};
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00023E42 File Offset: 0x00022042
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 7;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00023E45 File Offset: 0x00022045
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004A1 RID: 1185
		internal static CardInfo card;
	}
}
