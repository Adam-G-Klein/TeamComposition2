using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000CD RID: 205
	public class IceShardCard : CustomCard
	{
		// Token: 0x06000658 RID: 1624 RVA: 0x00022861 File Offset: 0x00020A61
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.reflects = 1;
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x0002286C File Offset: 0x00020A6C
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("Ice_Mono", new Type[]
				{
					typeof(IceTrailMono)
				})
			});
			ObjectsToSpawn item = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			list.Add(item);
			gun.objectsToSpawn = list.ToArray();
			gun.projectileColor = new Color(0f, 1f, 1f, 1f);
			gun.gravity = 0f;
			gun.spread += 0.1f;
			gun.projectielSimulatonSpeed *= 4f;
			gun.timeBetweenBullets = 0.08f;
			gun.destroyBulletAfter = 0.15f;
			gun.bursts += 3;
			gun.damage *= 0.65f;
			gunAmmo.reloadTimeMultiplier *= 0.75f;
			if (gun.slow > 0f)
			{
				gun.slow *= 2f;
			}
			else
			{
				gun.slow += 1f;
			}
			player.gameObject.AddComponent<FrostMono>();
			player.gameObject.AddComponent<IceShardMono>();
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x000229C5 File Offset: 0x00020BC5
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x000229C7 File Offset: 0x00020BC7
		protected override string GetTitle()
		{
			return "Ice Shard";
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x000229CE File Offset: 0x00020BCE
		protected override string GetDescription()
		{
			return "Your bullets become Ice Shards that move quickly, but melt fast!";
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x000229D5 File Offset: 0x00020BD5
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_IceShard");
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x000229E6 File Offset: 0x00020BE6
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x000229EC File Offset: 0x00020BEC
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Slow",
					amount = "+100%",
					simepleAmount = 3
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bursts",
					amount = "+2",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bounce",
					amount = "+1",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Reload Time",
					amount = "-25%",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Damage",
					amount = "-35%",
					simepleAmount = 6
				}
			};
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x00022ADB File Offset: 0x00020CDB
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 7;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x00022ADE File Offset: 0x00020CDE
		public override string GetModName()
		{
			return "CR";
		}
	}
}
