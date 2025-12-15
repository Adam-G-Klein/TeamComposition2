using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x02000099 RID: 153
	public class CriticalHitCard : CustomCard
	{
		// Token: 0x06000417 RID: 1047 RVA: 0x0001E0C1 File Offset: 0x0001C2C1
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0001E0C4 File Offset: 0x0001C2C4
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Critical", new Type[]
				{
					typeof(CriticalHitMono)
				})
			});
			gun.objectsToSpawn = list.ToArray();
			gunAmmo.reloadTimeMultiplier *= 1.1f;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0001E12B File Offset: 0x0001C32B
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0001E12D File Offset: 0x0001C32D
		protected override string GetTitle()
		{
			return "Critical Hit";
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001E134 File Offset: 0x0001C334
		protected override string GetDescription()
		{
			return "Your bullets have a 50% chance to deal double damage, targets glow yellow upon taking critical damage.";
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001E13B File Offset: 0x0001C33B
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_CriticalHit");
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0001E14C File Offset: 0x0001C34C
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0001E14F File Offset: 0x0001C34F
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Time",
					amount = "+10%",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0001E183 File Offset: 0x0001C383
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 1;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0001E186 File Offset: 0x0001C386
		public override string GetModName()
		{
			return "CR";
		}
	}
}
