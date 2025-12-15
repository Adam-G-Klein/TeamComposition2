using System;
using CR.MonoBehaviors;
using RarityLib.Utils;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000FA RID: 250
	public class ScarabCard : CustomCard
	{
		// Token: 0x0600086D RID: 2157 RVA: 0x00025706 File Offset: 0x00023906
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			statModifiers.secondsToTakeDamageOver = 5f;
			block.cdMultiplier = 0.5f;
			statModifiers.movementSpeed = 0.7f;
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0002572C File Offset: 0x0002392C
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<ScarabMono>();
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0002573A File Offset: 0x0002393A
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0002573C File Offset: 0x0002393C
		protected override string GetTitle()
		{
			return "Scarab";
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00025743 File Offset: 0x00023943
		protected override string GetDescription()
		{
			return "Gain a scarab shell that regenerates your health until you reach max!";
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0002574A File Offset: 0x0002394A
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Scarab");
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0002575B File Offset: 0x0002395B
		protected override CardInfo.Rarity GetRarity()
		{
			return RarityUtils.GetRarity("Legendary");
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00025768 File Offset: 0x00023968
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Decay",
					amount = "+5s",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Life Regen",
					amount = "+20",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Block Cooldown",
					amount = "-50%",
					simepleAmount = 7
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Movement Speed",
					amount = "-30%",
					simepleAmount = 7
				}
			};
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0002582B File Offset: 0x00023A2B
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 1;
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0002582E File Offset: 0x00023A2E
		public override string GetModName()
		{
			return "CR";
		}
	}
}
