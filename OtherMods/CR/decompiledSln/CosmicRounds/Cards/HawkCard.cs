using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x0200008F RID: 143
	public class HawkCard : CustomCard
	{
		// Token: 0x060003A9 RID: 937 RVA: 0x0001D2BD File Offset: 0x0001B4BD
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.projectileSpeed = 2f;
			gun.damage = 1.25f;
			gun.attackSpeedMultiplier = 1.75f;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0001D2E0 File Offset: 0x0001B4E0
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.spread = 0f;
			gun.evenSpread = 0f;
			gun.projectileColor = Color.yellow;
			player.gameObject.AddComponent<HawkSpriteMono>();
			gunAmmo.reloadTimeMultiplier *= 1.25f;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0001D32C File Offset: 0x0001B52C
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0001D32E File Offset: 0x0001B52E
		protected override string GetTitle()
		{
			return "Hawk";
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0001D335 File Offset: 0x0001B535
		protected override string GetDescription()
		{
			return "Launch high velocity hawk-bullets.";
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0001D33C File Offset: 0x0001B53C
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Hawk");
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0001D34D File Offset: 0x0001B54D
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0001D350 File Offset: 0x0001B550
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Speed",
					amount = "+200%",
					simepleAmount = 4
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Spread",
					amount = "Reset",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+25%",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = false,
					stat = "ATK Speed",
					amount = "-75%",
					simepleAmount = 7
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Time",
					amount = "+25%",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001D43F File Offset: 0x0001B63F
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 1;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0001D442 File Offset: 0x0001B642
		public override string GetModName()
		{
			return "CR";
		}
	}
}
