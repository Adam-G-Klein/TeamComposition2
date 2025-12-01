using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000B5 RID: 181
	public class AsteroidCard : CustomCard
	{
		// Token: 0x0600054B RID: 1355 RVA: 0x000207DD File Offset: 0x0001E9DD
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.reflects = 2;
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x000207E8 File Offset: 0x0001E9E8
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = new Color(1f, 0.3f, 1f, 1f);
			gun.damage *= 1.3f;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			ObjectsToSpawn item = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Asteroid", new Type[]
				{
					typeof(AsteroidMono)
				})
			});
			list.Add(item);
			gun.objectsToSpawn = list.ToArray();
			gunAmmo.reloadTimeMultiplier *= 1.3f;
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x000208A3 File Offset: 0x0001EAA3
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x000208A5 File Offset: 0x0001EAA5
		protected override string GetTitle()
		{
			return "Asteroid";
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x000208AC File Offset: 0x0001EAAC
		protected override string GetDescription()
		{
			return "Your bullets accelerate both horizontally and vertically.";
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x000208B3 File Offset: 0x0001EAB3
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Asteroid");
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x000208C4 File Offset: 0x0001EAC4
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x000208C8 File Offset: 0x0001EAC8
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
					amount = "+2"
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload TIme",
					amount = "+30%",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00020958 File Offset: 0x0001EB58
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 4;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x0002095B File Offset: 0x0001EB5B
		public override string GetModName()
		{
			return "CR";
		}
	}
}
