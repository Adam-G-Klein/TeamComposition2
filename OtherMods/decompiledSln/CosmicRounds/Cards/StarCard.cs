using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x02000098 RID: 152
	public class StarCard : CustomCard
	{
		// Token: 0x0600040C RID: 1036 RVA: 0x0001DEDD File Offset: 0x0001C0DD
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.reloadTimeAdd = 0.25f;
			gun.ammo = 12;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001DEF4 File Offset: 0x0001C0F4
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.numberOfProjectiles += 4;
			gun.bursts = 0;
			gun.timeBetweenBullets = 0f;
			gun.damage *= 0.25f;
			gun.spread = 0f;
			gun.evenSpread = 0f;
			gun.damageAfterDistanceMultiplier += 0.25f;
			if (gun.size <= 0f)
			{
				gun.size += 0.3f;
			}
			else
			{
				gun.size *= 1.3f;
			}
			player.gameObject.AddComponent<StarMono>();
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0001DF9A File Offset: 0x0001C19A
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001DF9C File Offset: 0x0001C19C
		protected override string GetTitle()
		{
			return "Star";
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0001DFA3 File Offset: 0x0001C1A3
		protected override string GetDescription()
		{
			return "Overlap your bullets into a star.";
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0001DFAA File Offset: 0x0001C1AA
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Star");
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0001DFBB File Offset: 0x0001C1BB
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0001DFC0 File Offset: 0x0001C1C0
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullets",
					amount = "+4",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Ammo",
					amount = "+12",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Damage Growth",
					amount = "+25%",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Time",
					amount = "+0.25s",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Damage",
					amount = "-75%",
					simepleAmount = 7
				}
			};
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0001E0AF File Offset: 0x0001C2AF
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 1;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0001E0B2 File Offset: 0x0001C2B2
		public override string GetModName()
		{
			return "CR";
		}
	}
}
