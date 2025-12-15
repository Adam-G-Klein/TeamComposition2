using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000B7 RID: 183
	public class GlueCard : CustomCard
	{
		// Token: 0x06000561 RID: 1377 RVA: 0x00020B19 File Offset: 0x0001ED19
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.reloadTimeAdd = 0.25f;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00020B28 File Offset: 0x0001ED28
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = new Color(1f, 1f, 1f, 1f);
			gun.reflects = 0;
			gun.attackSpeed *= 0.5f;
			player.gameObject.AddComponent<GlueMono>();
			if (gun.slow > 0f)
			{
				gun.slow *= 1.5f;
				return;
			}
			gun.slow += 0.5f;
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00020BAB File Offset: 0x0001EDAB
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00020BAD File Offset: 0x0001EDAD
		protected override string GetTitle()
		{
			return "Glue";
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x00020BB4 File Offset: 0x0001EDB4
		protected override string GetDescription()
		{
			return "Your bullets splat when hitting to slow targets.";
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00020BBB File Offset: 0x0001EDBB
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Glue");
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00020BCC File Offset: 0x0001EDCC
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00020BD0 File Offset: 0x0001EDD0
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Slow",
					amount = "+50%",
					simepleAmount = 3
				},
				new CardInfoStat
				{
					positive = true,
					stat = "ATK Speed",
					amount = "+100%",
					simepleAmount = 3
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Bounces",
					amount = "Reset",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload TIme",
					amount = "+0.25s",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00020C93 File Offset: 0x0001EE93
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 3;
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00020C96 File Offset: 0x0001EE96
		public override string GetModName()
		{
			return "CR";
		}
	}
}
