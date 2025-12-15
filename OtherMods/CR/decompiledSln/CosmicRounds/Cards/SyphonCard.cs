using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x0200009B RID: 155
	public class SyphonCard : CustomCard
	{
		// Token: 0x0600042D RID: 1069 RVA: 0x0001E3A5 File Offset: 0x0001C5A5
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.reloadTimeAdd = 0.5f;
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0001E3B4 File Offset: 0x0001C5B4
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
			gun.projectileColor = new Color(1f, 0f, 0.4f, 1f);
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Syphon", new Type[]
				{
					typeof(SyphonMono)
				})
			});
			gun.objectsToSpawn = list.ToArray();
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0001E45E File Offset: 0x0001C65E
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0001E460 File Offset: 0x0001C660
		protected override string GetTitle()
		{
			return "Syphon";
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001E467 File Offset: 0x0001C667
		protected override string GetDescription()
		{
			return "Your bullets have a 50% chance to Silence targets, preventing them from shooting or blocking.";
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0001E46E File Offset: 0x0001C66E
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Syphon");
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0001E47F File Offset: 0x0001C67F
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0001E484 File Offset: 0x0001C684
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Life Steal",
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

		// Token: 0x06000435 RID: 1077 RVA: 0x0001E4EF File Offset: 0x0001C6EF
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 8;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0001E4F2 File Offset: 0x0001C6F2
		public override string GetModName()
		{
			return "CR";
		}
	}
}
