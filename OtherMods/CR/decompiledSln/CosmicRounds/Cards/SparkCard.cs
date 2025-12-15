using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000A2 RID: 162
	public class SparkCard : CustomCard
	{
		// Token: 0x0600047A RID: 1146 RVA: 0x0001EBF9 File Offset: 0x0001CDF9
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.ammo = 8;
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0001EC04 File Offset: 0x0001CE04
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectielSimulatonSpeed *= 3.5f;
			gun.spread += 0.05f;
			gun.attackSpeed *= 0.2f;
			gun.damage *= 0.6f;
			gun.knockback *= 0.9f;
			gun.projectileColor = Color.yellow;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Spark", new Type[]
				{
					typeof(StunMono)
				})
			});
			gun.objectsToSpawn = list.ToArray();
			gunAmmo.reloadTimeMultiplier *= 0.65f;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0001ECD0 File Offset: 0x0001CED0
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0001ECD2 File Offset: 0x0001CED2
		protected override string GetTitle()
		{
			return "Spark";
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0001ECD9 File Offset: 0x0001CED9
		protected override string GetDescription()
		{
			return "Your bullets become fast lightning sparks that may stun targets for a bit!";
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0001ECE0 File Offset: 0x0001CEE0
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Spark");
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0001ECF1 File Offset: 0x0001CEF1
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0001ECF4 File Offset: 0x0001CEF4
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "ATK SPD",
					amount = "+400",
					simepleAmount = 4
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Ammo",
					amount = "+8",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Reload Time",
					amount = "-35%",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Damage",
					amount = "-40%",
					simepleAmount = 6
				}
			};
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0001EDB7 File Offset: 0x0001CFB7
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 3;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0001EDBA File Offset: 0x0001CFBA
		public override string GetModName()
		{
			return "CR";
		}
	}
}
