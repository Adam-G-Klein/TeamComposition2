using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000A3 RID: 163
	public class GoldenGlazeCard : CustomCard
	{
		// Token: 0x06000485 RID: 1157 RVA: 0x0001EDC9 File Offset: 0x0001CFC9
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.ammo = 1;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0001EDD4 File Offset: 0x0001CFD4
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = new Color(0.8f, 1f, 0f, 1f);
			gun.attackSpeed *= 0.5f;
			gun.spread = 0f;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Golden", new Type[]
				{
					typeof(GoldenGlazeMono)
				})
			});
			gun.objectsToSpawn = list.ToArray();
			gunAmmo.reloadTimeMultiplier *= 1.15f;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0001EE77 File Offset: 0x0001D077
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0001EE79 File Offset: 0x0001D079
		protected override string GetTitle()
		{
			return "Golden Glaze";
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0001EE80 File Offset: 0x0001D080
		protected override string GetDescription()
		{
			return "Your bullets slow targets and decrease their hp by 30% for 3 seconds. (Shoutout to Kane4Days!) ";
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0001EE87 File Offset: 0x0001D087
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_GoldenGlaze");
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0001EE98 File Offset: 0x0001D098
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0001EE9C File Offset: 0x0001D09C
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
					amount = "+15%",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001EF5F File Offset: 0x0001D15F
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 1;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001EF62 File Offset: 0x0001D162
		public override string GetModName()
		{
			return "CR";
		}
	}
}
