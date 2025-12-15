using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000CB RID: 203
	public class VultureCard : CustomCard
	{
		// Token: 0x06000642 RID: 1602 RVA: 0x0002252E File Offset: 0x0002072E
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.ammo = 8;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00022538 File Offset: 0x00020738
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectielSimulatonSpeed *= 1.5f;
			gun.numberOfProjectiles++;
			gun.timeBetweenBullets += 0.15f;
			gun.bursts += 2;
			if (gun.spread <= 0f)
			{
				gun.spread += 0.1f;
			}
			else
			{
				gun.spread *= 1.1f;
			}
			gun.damage *= 0.4f;
			gun.projectileColor = new Color(1f, 0.25f, 1f, 0.35f);
			player.gameObject.AddComponent<VultureSpriteMono>();
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x000225F5 File Offset: 0x000207F5
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x000225F7 File Offset: 0x000207F7
		protected override string GetTitle()
		{
			return "Vulture";
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x000225FE File Offset: 0x000207FE
		protected override string GetDescription()
		{
			return "Launch a wake of lethal vultures that cut through the air!";
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x00022605 File Offset: 0x00020805
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Vulture");
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x00022616 File Offset: 0x00020816
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0002261C File Offset: 0x0002081C
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
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
					stat = "Bullets",
					amount = "+1",
					simepleAmount = 0
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
					stat = "Projectile Speed",
					amount = "+50%",
					simepleAmount = 4
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Damage",
					amount = "-60%",
					simepleAmount = 7
				}
			};
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0002270B File Offset: 0x0002090B
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 4;
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x0002270E File Offset: 0x0002090E
		public override string GetModName()
		{
			return "CR";
		}
	}
}
