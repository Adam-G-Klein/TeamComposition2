using System;
using System.Collections.Generic;
using System.Linq;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000F4 RID: 244
	public class SagittariusCardPlus : CustomCard
	{
		// Token: 0x06000826 RID: 2086 RVA: 0x000251D7 File Offset: 0x000233D7
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.reflects = 4;
			SagittariusCardPlus.card = cardInfo;
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x000251E6 File Offset: 0x000233E6
		public override bool GetEnabled()
		{
			return false;
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x000251EC File Offset: 0x000233EC
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectielSimulatonSpeed *= 2f;
			gun.projectileColor = Color.cyan;
			ObjectsToSpawn item = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(item);
			gun.objectsToSpawn = list.ToArray();
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00025251 File Offset: 0x00023451
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00025253 File Offset: 0x00023453
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x0002526B File Offset: 0x0002346B
		protected override string GetTitle()
		{
			return "Sagittarius+";
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00025272 File Offset: 0x00023472
		protected override string GetDescription()
		{
			return "Honest and realistic...";
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00025279 File Offset: 0x00023479
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Sagittarius");
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x0002528A File Offset: 0x0002348A
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x00025290 File Offset: 0x00023490
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
					stat = "Projectile Speed",
					amount = "+100%"
				}
			};
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x000252ED File Offset: 0x000234ED
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 7;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x000252F0 File Offset: 0x000234F0
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004B6 RID: 1206
		internal static CardInfo card;
	}
}
