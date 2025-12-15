using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000C9 RID: 201
	public class ShootingStarCard : CustomCard
	{
		// Token: 0x0600062C RID: 1580 RVA: 0x000221C9 File Offset: 0x000203C9
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.reflects = 2;
			gun.reloadTimeAdd = 0.5f;
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000221E0 File Offset: 0x000203E0
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = new Color(1f, 1f, 0f, 1f);
			gun.damage *= 1.3f;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_ShootingStar", new Type[]
				{
					typeof(ShootingStarMono)
				})
			});
			ObjectsToSpawn item = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			list.Add(item);
			gun.objectsToSpawn = list.ToArray();
			gun.gravity *= 0.75f;
			player.gameObject.AddComponent<StarMono>();
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x000222A7 File Offset: 0x000204A7
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x000222A9 File Offset: 0x000204A9
		protected override string GetTitle()
		{
			return "Shooting Star";
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x000222B0 File Offset: 0x000204B0
		protected override string GetDescription()
		{
			return "Your bullets accelerate vertically.";
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x000222B7 File Offset: 0x000204B7
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_ShootingStar");
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x000222C8 File Offset: 0x000204C8
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x000222CC File Offset: 0x000204CC
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+30%",
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
					positive = true,
					stat = "Bullet Gravity",
					amount = "-25%",
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

		// Token: 0x06000634 RID: 1588 RVA: 0x00022388 File Offset: 0x00020588
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 6;
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x0002238B File Offset: 0x0002058B
		public override string GetModName()
		{
			return "CR";
		}
	}
}
