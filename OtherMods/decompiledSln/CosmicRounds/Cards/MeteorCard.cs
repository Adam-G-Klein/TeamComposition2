using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000AF RID: 175
	public class MeteorCard : CustomCard
	{
		// Token: 0x06000509 RID: 1289 RVA: 0x00020128 File Offset: 0x0001E328
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0002012C File Offset: 0x0001E32C
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = new Color(0.1f, 1f, 0.3f, 1f);
			gun.damage *= 1.5f;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Meteor", new Type[]
				{
					typeof(MeteorMono)
				})
			});
			gun.objectsToSpawn = list.ToArray();
			gunAmmo.reloadTimeMultiplier *= 1.15f;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x000201C4 File Offset: 0x0001E3C4
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x000201C6 File Offset: 0x0001E3C6
		protected override string GetTitle()
		{
			return "Meteor";
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x000201CD File Offset: 0x0001E3CD
		protected override string GetDescription()
		{
			return "Your bullets accelerate when falling and deal 25% additional damage after hitting.";
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x000201D4 File Offset: 0x0001E3D4
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Meteor");
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x000201E5 File Offset: 0x0001E3E5
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x000201E8 File Offset: 0x0001E3E8
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+50%",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload TIme",
					amount = "+15%",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00020253 File Offset: 0x0001E453
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 5;
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00020256 File Offset: 0x0001E456
		public override string GetModName()
		{
			return "CR";
		}
	}
}
