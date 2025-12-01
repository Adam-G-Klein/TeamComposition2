using System;
using CR.MonoBehaviors;
using RarityLib.Utils;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000CF RID: 207
	public class HaloCard : CustomCard
	{
		// Token: 0x0600066E RID: 1646 RVA: 0x00022C55 File Offset: 0x00020E55
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			block.cdMultiplier = 1.25f;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00022C64 File Offset: 0x00020E64
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = new Color(1f, 1f, 1f, 1f);
			player.gameObject.AddComponent<HaloMono>();
			player.gameObject.AddComponent<HolyMono>();
			player.gameObject.AddComponent<FireMono>();
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x00022CB4 File Offset: 0x00020EB4
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x00022CB6 File Offset: 0x00020EB6
		protected override string GetTitle()
		{
			return "Halo";
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x00022CBD File Offset: 0x00020EBD
		protected override string GetDescription()
		{
			return "(Shoutout to Ksick!!) Your bullets become holy fire!! Blocking turns your next shot into targetting halo rays!!";
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00022CC4 File Offset: 0x00020EC4
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Halo");
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00022CD5 File Offset: 0x00020ED5
		protected override CardInfo.Rarity GetRarity()
		{
			return RarityUtils.GetRarity("Legendary");
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00022CE4 File Offset: 0x00020EE4
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
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
					stat = "Block Cooldown",
					amount = "+25%",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00022D4F File Offset: 0x00020F4F
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 0;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00022D52 File Offset: 0x00020F52
		public override string GetModName()
		{
			return "CR";
		}
	}
}
