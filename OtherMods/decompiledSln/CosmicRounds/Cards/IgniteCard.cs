using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000B2 RID: 178
	public class IgniteCard : CustomCard
	{
		// Token: 0x0600052A RID: 1322 RVA: 0x00020391 File Offset: 0x0001E591
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00020394 File Offset: 0x0001E594
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<IgniteMono>();
			characterStats.movementSpeed *= 0.85f;
			gun.damage *= 1.3f;
			gun.projectileColor = new Color(1f, 0.3f, 0f, 1f);
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Flamethrower", new Type[]
				{
					typeof(FlamethrowerMono)
				})
			});
			gun.objectsToSpawn = list.ToArray();
			player.gameObject.AddComponent<BurnMono>();
			player.gameObject.AddComponent<FireMono>();
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00020451 File Offset: 0x0001E651
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x00020453 File Offset: 0x0001E653
		protected override string GetTitle()
		{
			return "Ignite";
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0002045A File Offset: 0x0001E65A
		protected override string GetDescription()
		{
			return "Your bullets and block ignite targets for 2 seconds! Igniting through blocking deals 33% of targets' max hp as damage!";
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x00020461 File Offset: 0x0001E661
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Ignite");
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00020472 File Offset: 0x0001E672
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00020478 File Offset: 0x0001E678
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+30%",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Objects",
					amount = "Ignite",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Movement Speed",
					amount = "-25%",
					simepleAmount = 5
				}
			};
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0002050F File Offset: 0x0001E70F
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 0;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x00020512 File Offset: 0x0001E712
		public override string GetModName()
		{
			return "CR";
		}
	}
}
