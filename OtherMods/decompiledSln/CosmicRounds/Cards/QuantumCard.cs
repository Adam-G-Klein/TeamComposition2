using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000D9 RID: 217
	public class QuantumCard : CustomCard
	{
		// Token: 0x060006DD RID: 1757 RVA: 0x000236EC File Offset: 0x000218EC
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			block.cdMultiplier = 1.1f;
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x000236FA File Offset: 0x000218FA
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<QuantumMono>();
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00023708 File Offset: 0x00021908
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0002370A File Offset: 0x0002190A
		protected override string GetTitle()
		{
			return "Quantum";
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00023711 File Offset: 0x00021911
		protected override string GetDescription()
		{
			return "Blocking near an enemy reactivates your block abilities directly over the nearest enemy!";
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00023718 File Offset: 0x00021918
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Quantum");
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00023729 File Offset: 0x00021929
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0002372C File Offset: 0x0002192C
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = false,
					stat = "Block Cooldown",
					amount = "+10%",
					simepleAmount = 0
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

		// Token: 0x060006E5 RID: 1765 RVA: 0x00023797 File Offset: 0x00021997
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 2;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0002379A File Offset: 0x0002199A
		public override string GetModName()
		{
			return "CR";
		}
	}
}
