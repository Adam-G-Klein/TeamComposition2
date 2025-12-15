using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000AA RID: 170
	public class CloudCard : CustomCard
	{
		// Token: 0x060004D2 RID: 1234 RVA: 0x0001F8A9 File Offset: 0x0001DAA9
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0001F8AC File Offset: 0x0001DAAC
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = new Color(0.7f, 0.8f, 1f, 1f);
			gun.damage *= 0.5f;
			gun.projectileSpeed *= 1.6f;
			gun.numberOfProjectiles += 2;
			gun.spread += 0.07f;
			gun.drag += 0.5f;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Cloud", new Type[]
				{
					typeof(CloudMono)
				})
			});
			gun.objectsToSpawn = list.ToArray();
			gunAmmo.reloadTimeMultiplier *= 0.7f;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0001F988 File Offset: 0x0001DB88
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001F98A File Offset: 0x0001DB8A
		protected override string GetTitle()
		{
			return "Cloud";
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001F991 File Offset: 0x0001DB91
		protected override string GetDescription()
		{
			return "Your bullets slow down and drift downwards.";
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0001F998 File Offset: 0x0001DB98
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Cloud");
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001F9A9 File Offset: 0x0001DBA9
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001F9AC File Offset: 0x0001DBAC
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullets",
					amount = "+2",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Speed",
					amount = "+60%",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Reload Time",
					amount = "-30%",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Damage",
					amount = "-50%",
					simepleAmount = 5
				}
			};
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001FA6F File Offset: 0x0001DC6F
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 3;
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x0001FA72 File Offset: 0x0001DC72
		public override string GetModName()
		{
			return "CR";
		}
	}
}
