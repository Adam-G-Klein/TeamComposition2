using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000D4 RID: 212
	public class BatteryCard : CustomCard
	{
		// Token: 0x060006A6 RID: 1702 RVA: 0x000232D9 File Offset: 0x000214D9
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x000232DB File Offset: 0x000214DB
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<BatteryMono>();
			data.maxHealth *= 1.3f;
			gunAmmo.reloadTimeMultiplier *= 0.75f;
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x0002330E File Offset: 0x0002150E
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00023310 File Offset: 0x00021510
		protected override string GetTitle()
		{
			return "Battery";
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00023317 File Offset: 0x00021517
		protected override string GetDescription()
		{
			return "Whenever you start or finish reloading, stun nearby enemies.";
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x0002331E File Offset: 0x0002151E
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Battery");
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0002332F File Offset: 0x0002152F
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00023334 File Offset: 0x00021534
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
					amount = "+30%",
					simepleAmount = 2
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
					stat = "Ability Cooldown",
					amount = "0.5s"
				}
			};
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x000233C4 File Offset: 0x000215C4
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 7;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x000233C7 File Offset: 0x000215C7
		public override string GetModName()
		{
			return "CR";
		}
	}
}
