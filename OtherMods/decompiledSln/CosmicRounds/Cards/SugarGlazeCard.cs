using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000A5 RID: 165
	public class SugarGlazeCard : CustomCard
	{
		// Token: 0x0600049B RID: 1179 RVA: 0x0001F0A5 File Offset: 0x0001D2A5
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.ammo = 1;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001F0B0 File Offset: 0x0001D2B0
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor += new Color(0.8f, 0f, 1f, 1f);
			gun.attackSpeed *= 0.5f;
			gun.spread = 0f;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Sugar", new Type[]
				{
					typeof(SugarGlazeMono)
				})
			});
			gun.objectsToSpawn = list.ToArray();
			gunAmmo.reloadTimeMultiplier *= 1.2f;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001F15E File Offset: 0x0001D35E
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001F160 File Offset: 0x0001D360
		protected override string GetTitle()
		{
			return "Sugar Glaze";
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001F167 File Offset: 0x0001D367
		protected override string GetDescription()
		{
			return "Your bullets make targets jump and makes them uncontrollably faster for 2 seconds.";
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0001F16E File Offset: 0x0001D36E
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_SugarGlaze");
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0001F17F File Offset: 0x0001D37F
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001F184 File Offset: 0x0001D384
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Spread",
					amount = "Reset",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Ammo",
					amount = "+1",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = true,
					stat = "ATK SPD",
					amount = "+100%",
					simepleAmount = 3
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Time",
					amount = "+20%",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001F247 File Offset: 0x0001D447
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 8;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001F24A File Offset: 0x0001D44A
		public override string GetModName()
		{
			return "CR";
		}
	}
}
