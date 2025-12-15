using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x0200009E RID: 158
	public class TaserCard : CustomCard
	{
		// Token: 0x0600044E RID: 1102 RVA: 0x0001E885 File Offset: 0x0001CA85
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0001E88E File Offset: 0x0001CA8E
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<TaserMono>();
			block.cdMultiplier *= 0.7f;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0001E8AF File Offset: 0x0001CAAF
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001E8B1 File Offset: 0x0001CAB1
		protected override string GetTitle()
		{
			return "Taser";
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0001E8B8 File Offset: 0x0001CAB8
		protected override string GetDescription()
		{
			return "Blocking Stuns nearby targets, immobilizing them!";
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001E8BF File Offset: 0x0001CABF
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Taser");
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0001E8D0 File Offset: 0x0001CAD0
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0001E8D4 File Offset: 0x0001CAD4
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Block Cooldown",
					amount = "-30%",
					simepleAmount = 6
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Ability Cooldown",
					amount = "1s",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0001E93F File Offset: 0x0001CB3F
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 3;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001E942 File Offset: 0x0001CB42
		public override string GetModName()
		{
			return "CR";
		}
	}
}
