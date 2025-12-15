using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000AE RID: 174
	public class CometCard : CustomCard
	{
		// Token: 0x060004FE RID: 1278 RVA: 0x0001FFA7 File Offset: 0x0001E1A7
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.reloadTimeAdd = 0.5f;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0001FFB4 File Offset: 0x0001E1B4
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = new Color(0.4f, 0.4f, 1f, 1f);
			gun.damage *= 1.25f;
			if (gun.slow > 0f)
			{
				gun.slow *= 2f;
			}
			else
			{
				gun.slow += 1f;
			}
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Comet", new Type[]
				{
					typeof(CometMono)
				})
			});
			gun.objectsToSpawn = list.ToArray();
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x0002006D File Offset: 0x0001E26D
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x0002006F File Offset: 0x0001E26F
		protected override string GetTitle()
		{
			return "Comet";
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00020076 File Offset: 0x0001E276
		protected override string GetDescription()
		{
			return "Your bullets accelerate horizontally and slow targets when hitting.";
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0002007D File Offset: 0x0001E27D
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Comet");
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0002008E File Offset: 0x0001E28E
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00020094 File Offset: 0x0001E294
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Slow",
					amount = "+100%"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+25%"
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Time",
					amount = "+0.5s"
				}
			};
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00020116 File Offset: 0x0001E316
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 7;
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00020119 File Offset: 0x0001E319
		public override string GetModName()
		{
			return "CR";
		}
	}
}
