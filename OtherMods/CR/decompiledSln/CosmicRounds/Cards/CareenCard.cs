using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000B4 RID: 180
	public class CareenCard : CustomCard
	{
		// Token: 0x06000540 RID: 1344 RVA: 0x0002064D File Offset: 0x0001E84D
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.reflects = 2;
			gun.reloadTimeAdd = 0.25f;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00020664 File Offset: 0x0001E864
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = new Color(0.2f, 0.7f, 1f, 1f);
			gun.damage *= 1.25f;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Careen", new Type[]
				{
					typeof(CareenMono)
				})
			});
			ObjectsToSpawn item = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			list.Add(item);
			gun.objectsToSpawn = list.ToArray();
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0002070D File Offset: 0x0001E90D
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0002070F File Offset: 0x0001E90F
		protected override string GetTitle()
		{
			return "Careen";
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00020716 File Offset: 0x0001E916
		protected override string GetDescription()
		{
			return "Your bullets hardly fall until they hit something. (Unless fired downwards)";
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0002071D File Offset: 0x0001E91D
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Careen");
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x0002072E File Offset: 0x0001E92E
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00020734 File Offset: 0x0001E934
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+25%",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bounces",
					amount = "+2",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Time",
					amount = "+0.25s",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x000207CB File Offset: 0x0001E9CB
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 7;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x000207CE File Offset: 0x0001E9CE
		public override string GetModName()
		{
			return "CR";
		}
	}
}
