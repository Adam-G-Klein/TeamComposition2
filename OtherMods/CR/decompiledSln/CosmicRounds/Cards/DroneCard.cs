using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using RarityLib.Utils;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000A1 RID: 161
	public class DroneCard : CustomCard
	{
		// Token: 0x0600046F RID: 1135 RVA: 0x0001EAA5 File Offset: 0x0001CCA5
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001EAB0 File Offset: 0x0001CCB0
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor += new Color(0.5f, 0.5f, 0.5f, 1f);
			gun.damage *= 0.3f;
			gun.destroyBulletAfter = 0f;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Drone", new Type[]
				{
					typeof(DroneMono)
				})
			});
			gun.objectsToSpawn = list.ToArray();
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0001EB4C File Offset: 0x0001CD4C
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0001EB4E File Offset: 0x0001CD4E
		protected override string GetTitle()
		{
			return "Drone";
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0001EB55 File Offset: 0x0001CD55
		protected override string GetDescription()
		{
			return "Infusing highly intelligent AI into your bullets allows them to adjust trajectory to visible enemies on the fly!!";
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0001EB5C File Offset: 0x0001CD5C
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Drone");
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001EB6D File Offset: 0x0001CD6D
		protected override CardInfo.Rarity GetRarity()
		{
			return RarityUtils.GetRarity("Legendary");
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0001EB7C File Offset: 0x0001CD7C
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Range",
					amount = "Reset",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Damage",
					amount = "-70%",
					simepleAmount = 7
				}
			};
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0001EBE7 File Offset: 0x0001CDE7
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 3;
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0001EBEA File Offset: 0x0001CDEA
		public override string GetModName()
		{
			return "CR";
		}
	}
}
