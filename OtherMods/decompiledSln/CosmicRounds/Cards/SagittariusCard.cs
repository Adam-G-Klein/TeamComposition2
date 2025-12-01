using System;
using System.Collections.Generic;
using System.Linq;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000F3 RID: 243
	public class SagittariusCard : CustomCard
	{
		// Token: 0x0600081A RID: 2074 RVA: 0x0002509F File Offset: 0x0002329F
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.categories = new CardCategory[]
			{
				ZodiacCard.ZodiacClass
			};
			gun.reflects = 2;
			SagittariusCard.card = cardInfo;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x000250C4 File Offset: 0x000232C4
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectielSimulatonSpeed *= 1.5f;
			gun.projectileColor = Color.cyan;
			ObjectsToSpawn item = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(item);
			gun.objectsToSpawn = list.ToArray();
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00025129 File Offset: 0x00023329
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0002512B File Offset: 0x0002332B
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00025143 File Offset: 0x00023343
		protected override string GetTitle()
		{
			return "Sagittarius";
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0002514A File Offset: 0x0002334A
		protected override string GetDescription()
		{
			return "Honest and realistic...";
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00025151 File Offset: 0x00023351
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Sagittarius");
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00025162 File Offset: 0x00023362
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x00025168 File Offset: 0x00023368
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
					stat = "Projectile Speed",
					amount = "+50%"
				}
			};
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x000251C5 File Offset: 0x000233C5
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 7;
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x000251C8 File Offset: 0x000233C8
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004B5 RID: 1205
		internal static CardInfo card;
	}
}
