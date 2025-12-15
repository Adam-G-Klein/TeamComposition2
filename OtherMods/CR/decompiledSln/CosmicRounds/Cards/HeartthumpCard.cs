using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000C3 RID: 195
	public class HeartthumpCard : CustomCard
	{
		// Token: 0x060005EA RID: 1514 RVA: 0x00021B35 File Offset: 0x0001FD35
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.reflects = 3;
			gun.reloadTimeAdd = 0.5f;
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00021B4C File Offset: 0x0001FD4C
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = new Color(1f, 0.5f, 1f, 1f);
			gun.dmgMOnBounce *= 1.3f;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			player.gameObject.AddComponent<HeartthumpMono>();
			ObjectsToSpawn item = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			list.Add(item);
			gun.objectsToSpawn = list.ToArray();
			player.gameObject.AddComponent<HeartMono>();
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x00021BDD File Offset: 0x0001FDDD
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x00021BDF File Offset: 0x0001FDDF
		protected override string GetTitle()
		{
			return "Heart Thump";
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00021BE6 File Offset: 0x0001FDE6
		protected override string GetDescription()
		{
			return "Each time your bullets bounce, they gain +30% damage and heal you for 10% of your bullet damage!";
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x00021BED File Offset: 0x0001FDED
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Heartthump");
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00021BFE File Offset: 0x0001FDFE
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00021C04 File Offset: 0x0001FE04
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bounces",
					amount = "+3",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Time",
					amount = "+0.5s"
				}
			};
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00021C68 File Offset: 0x0001FE68
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 8;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00021C6B File Offset: 0x0001FE6B
		public override string GetModName()
		{
			return "CR";
		}
	}
}
