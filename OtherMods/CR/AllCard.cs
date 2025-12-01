using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000A9 RID: 169
	public class AllCard : CustomCard
	{
		// Token: 0x060004C7 RID: 1223 RVA: 0x0001F529 File Offset: 0x0001D729
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.numberOfProjectiles = 1;
			gun.reflects = 1;
			block.cdMultiplier = 0.85f;
			block.additionalBlocks = 1;
			gun.ammo = 1;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001F554 File Offset: 0x0001D754
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			data.maxHealth *= 1.15f;
			characterStats.movementSpeed *= 1.15f;
			gun.gravity *= 0.85f;
			gun.projectileSpeed *= 1.15f;
			gun.projectielSimulatonSpeed *= 1.15f;
			if (gun.size <= 0f)
			{
				gun.size += 0.15f;
			}
			else
			{
				gun.size *= 1.15f;
			}
			gun.spread *= 0.85f;
			gun.evenSpread *= 0.85f;
			gun.damage *= 1.15f;
			gun.damageAfterDistanceMultiplier *= 1.15f;
			gun.dmgMOnBounce *= 1.15f;
			if (gun.slow <= 0f)
			{
				gun.slow += 0.15f;
			}
			else
			{
				gun.slow *= 1.15f;
			}
			gun.attackSpeed *= 0.85f;
			gun.knockback *= 1.15f;
			gun.destroyBulletAfter *= 1.15f;
			gravity.gravityForce *= 0.85f;
			if (characterStats.lifeSteal == 0f)
			{
				characterStats.lifeSteal += 0.15f;
			}
			else
			{
				characterStats.lifeSteal *= 1.15f;
			}
			player.gameObject.AddComponent<AllMono>();
			ObjectsToSpawn item = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(item);
			gun.objectsToSpawn = list.ToArray();
			gunAmmo.reloadTimeMultiplier *= 0.85f;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001F755 File Offset: 0x0001D955
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001F757 File Offset: 0x0001D957
		protected override string GetTitle()
		{
			return "ALL";
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001F75E File Offset: 0x0001D95E
		protected override string GetDescription()
		{
			return "Ups your stats that benefit from being higher, and lowers your stats the benefit from being lower!";
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001F765 File Offset: 0x0001D965
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_All");
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0001F776 File Offset: 0x0001D976
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001F77C File Offset: 0x0001D97C
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Positive Stats",
					amount = "+15%",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Negative Stats",
					amount = "-15%",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Ammo",
					amount = "+1",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet",
					amount = "+1",
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
					stat = "Blocks",
					amount = "+1",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0001F897 File Offset: 0x0001DA97
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 8;
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0001F89A File Offset: 0x0001DA9A
		public override string GetModName()
		{
			return "CR";
		}
	}
}
