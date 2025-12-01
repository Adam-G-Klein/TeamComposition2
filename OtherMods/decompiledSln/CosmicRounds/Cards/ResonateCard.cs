using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000F9 RID: 249
	public class ResonateCard : CustomCard
	{
		// Token: 0x06000862 RID: 2146 RVA: 0x00025679 File Offset: 0x00023879
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0002567B File Offset: 0x0002387B
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<ResonateMono>();
			block.cdMultiplier *= 1.25f;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0002569C File Offset: 0x0002389C
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0002569E File Offset: 0x0002389E
		protected override string GetTitle()
		{
			return "Resonate";
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x000256A5 File Offset: 0x000238A5
		protected override string GetDescription()
		{
			return "Blocking conjures a resonating field, where your block abilities will reactivate multiple times! (Only one can exist at a time)";
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000256AC File Offset: 0x000238AC
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Resonate");
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x000256BD File Offset: 0x000238BD
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x000256C0 File Offset: 0x000238C0
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = false,
					stat = "Block Cooldown",
					amount = "+25%",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x000256F4 File Offset: 0x000238F4
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 3;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x000256F7 File Offset: 0x000238F7
		public override string GetModName()
		{
			return "CR";
		}
	}
}
