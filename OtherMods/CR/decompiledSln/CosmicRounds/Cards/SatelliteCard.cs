using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000CA RID: 202
	public class SatelliteCard : CustomCard
	{
		// Token: 0x06000637 RID: 1591 RVA: 0x0002239A File Offset: 0x0002059A
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.reflects = 1;
			gun.reloadTimeAdd = 0.25f;
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x000223B0 File Offset: 0x000205B0
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = new Color(1f, 1f, 1f, 1f);
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Satellite", new Type[]
				{
					typeof(SatelliteMono)
				})
			});
			gun.objectsToSpawn = list.ToArray();
			gun.gravity *= 0.5f;
			ObjectsToSpawn item = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			list.Add(item);
			gun.objectsToSpawn = list.ToArray();
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00022465 File Offset: 0x00020665
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00022467 File Offset: 0x00020667
		protected override string GetTitle()
		{
			return "Satellite";
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0002246E File Offset: 0x0002066E
		protected override string GetDescription()
		{
			return "Your bullets stop and then continue their arc occasionally. (Immobile bullets are not visible)";
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00022475 File Offset: 0x00020675
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Satellite");
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00022486 File Offset: 0x00020686
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0002248C File Offset: 0x0002068C
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Gravity",
					amount = "-50%",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bounce",
					amount = "+1",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Time",
					amount = "+0.25s"
				}
			};
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0002251C File Offset: 0x0002071C
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 3;
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x0002251F File Offset: 0x0002071F
		public override string GetModName()
		{
			return "CR";
		}
	}
}
