using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using RarityLib.Utils;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x0200009A RID: 154
	public class FlamethrowerCard : CustomCard
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x0001E195 File Offset: 0x0001C395
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.ammo = 8;
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0001E1A0 File Offset: 0x0001C3A0
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.spread += 0.1f;
			gun.damage *= 0.7f;
			gun.attackSpeed *= 0.2f;
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
			gunAmmo.reloadTimeMultiplier *= 0.7f;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0001E274 File Offset: 0x0001C474
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0001E276 File Offset: 0x0001C476
		protected override string GetTitle()
		{
			return "Flamethrower";
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0001E27D File Offset: 0x0001C47D
		protected override string GetDescription()
		{
			return "Absolutely incinerate anyone in your way!!";
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001E284 File Offset: 0x0001C484
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Flamethrower");
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0001E295 File Offset: 0x0001C495
		protected override CardInfo.Rarity GetRarity()
		{
			return RarityUtils.GetRarity("Legendary");
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0001E2A4 File Offset: 0x0001C4A4
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "ATK Speed",
					amount = "+400%",
					simepleAmount = 4
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Ammo",
					amount = "+8",
					simepleAmount = 0
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
					positive = true,
					stat = "Reload Time",
					amount = "-30%",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Damage",
					amount = "-30%",
					simepleAmount = 6
				}
			};
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0001E393 File Offset: 0x0001C593
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 0;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001E396 File Offset: 0x0001C596
		public override string GetModName()
		{
			return "CR";
		}
	}
}
