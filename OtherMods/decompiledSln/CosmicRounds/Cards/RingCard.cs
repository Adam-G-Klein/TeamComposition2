using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000C7 RID: 199
	public class RingCard : CustomCard
	{
		// Token: 0x06000616 RID: 1558 RVA: 0x00021FAD File Offset: 0x000201AD
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.reflects = 1;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x00021FB8 File Offset: 0x000201B8
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = Color.yellow;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Ring", new Type[]
				{
					typeof(RingMono)
				})
			});
			ObjectsToSpawn item = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			list.Add(item);
			gun.objectsToSpawn = list.ToArray();
			gun.projectielSimulatonSpeed *= 0.75f;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0002204D File Offset: 0x0002024D
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0002204F File Offset: 0x0002024F
		protected override string GetTitle()
		{
			return "Ring";
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x00022056 File Offset: 0x00020256
		protected override string GetDescription()
		{
			return "Your bullets gain a ring around them, capable of hurting and bouncing off enemies.";
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0002205D File Offset: 0x0002025D
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Ring");
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0002206E File Offset: 0x0002026E
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00022074 File Offset: 0x00020274
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bounce",
					amount = "+1",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Projectile Speed",
					amount = "-25%",
					simepleAmount = 5
				}
			};
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x000220DF File Offset: 0x000202DF
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 6;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000220E2 File Offset: 0x000202E2
		public override string GetModName()
		{
			return "CR";
		}
	}
}
