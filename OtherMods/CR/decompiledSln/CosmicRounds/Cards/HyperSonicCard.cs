using System;
using CR.MonoBehaviors;
using RarityLib.Utils;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000CC RID: 204
	public class HyperSonicCard : CustomCard
	{
		// Token: 0x0600064D RID: 1613 RVA: 0x0002271D File Offset: 0x0002091D
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x0002271F File Offset: 0x0002091F
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			characterStats.movementSpeed *= 1.5f;
			data.maxHealth *= 3f;
			player.gameObject.AddComponent<HyperMono>();
			gun.destroyBulletAfter = 0f;
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x0002275E File Offset: 0x0002095E
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x00022760 File Offset: 0x00020960
		protected override string GetTitle()
		{
			return "Hyper Sonic";
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00022767 File Offset: 0x00020967
		protected override string GetDescription()
		{
			return "NOW ILL SHOW YOU!!";
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0002276E File Offset: 0x0002096E
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_HyperSonic");
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x0002277F File Offset: 0x0002097F
		protected override CardInfo.Rarity GetRarity()
		{
			return RarityUtils.GetRarity("Legendary");
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x0002278C File Offset: 0x0002098C
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Range",
					amount = "Reset",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Movement Speed",
					amount = "+50%",
					simepleAmount = 4
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
					amount = "+200%",
					simepleAmount = 4
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Life Regen",
					amount = "+10",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x0002284F File Offset: 0x00020A4F
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 2;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00022852 File Offset: 0x00020A52
		public override string GetModName()
		{
			return "CR";
		}
	}
}
