using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000D0 RID: 208
	public class MistletoeCard : CustomCard
	{
		// Token: 0x06000679 RID: 1657 RVA: 0x00022D61 File Offset: 0x00020F61
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.reloadTimeAdd = 0.5f;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00022D70 File Offset: 0x00020F70
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			if (characterStats.lifeSteal == 0f)
			{
				characterStats.lifeSteal += 0.3f;
			}
			else
			{
				characterStats.lifeSteal *= 1.3f;
			}
			gun.projectileColor = new Color(0f, 1f, 1f, 1f);
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Mistletoe", new Type[]
				{
					typeof(MistletoeMono)
				})
			});
			gun.objectsToSpawn = list.ToArray();
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00022E1A File Offset: 0x0002101A
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00022E1C File Offset: 0x0002101C
		protected override string GetTitle()
		{
			return "Mistletoe";
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00022E23 File Offset: 0x00021023
		protected override string GetDescription()
		{
			return "Hitting an enemy spawns an ice field that slows enemies and heals yourself!";
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00022E2A File Offset: 0x0002102A
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Mistletoe");
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00022E3B File Offset: 0x0002103B
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00022E40 File Offset: 0x00021040
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Lifesteal",
					amount = "+30%",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Time",
					amount = "+0.5s",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00022EAB File Offset: 0x000210AB
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 7;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00022EAE File Offset: 0x000210AE
		public override string GetModName()
		{
			return "CR";
		}
	}
}
