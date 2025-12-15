using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000D3 RID: 211
	public class SealCard : CustomCard
	{
		// Token: 0x0600069B RID: 1691 RVA: 0x000231A3 File Offset: 0x000213A3
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.reloadTimeAdd = 0.25f;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x000231B0 File Offset: 0x000213B0
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.damage *= 1.25f;
			gun.projectileColor = new Color(1f, 0.1f, 0f, 1f);
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Seal", new Type[]
				{
					typeof(SealMono)
				})
			});
			gun.objectsToSpawn = list.ToArray();
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00023236 File Offset: 0x00021436
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00023238 File Offset: 0x00021438
		protected override string GetTitle()
		{
			return "Seal";
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0002323F File Offset: 0x0002143F
		protected override string GetDescription()
		{
			return "Hitting a target has a 50% chance to disable their lifesteal, decay and revives for 4s.";
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00023246 File Offset: 0x00021446
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Seal");
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00023257 File Offset: 0x00021457
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0002325C File Offset: 0x0002145C
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+25%",
					simepleAmount = 1
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Time",
					amount = "+0.25s",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x000232C7 File Offset: 0x000214C7
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 0;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x000232CA File Offset: 0x000214CA
		public override string GetModName()
		{
			return "CR";
		}
	}
}
