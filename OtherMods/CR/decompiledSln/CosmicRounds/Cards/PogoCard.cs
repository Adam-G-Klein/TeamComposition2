using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000A8 RID: 168
	public class PogoCard : CustomCard
	{
		// Token: 0x060004BC RID: 1212 RVA: 0x0001F445 File Offset: 0x0001D645
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			block.cdMultiplier = 0.5f;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001F453 File Offset: 0x0001D653
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<PogoMono>();
			gravity.gravityForce /= 2f;
			characterStats.jump *= 1.5f;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001F487 File Offset: 0x0001D687
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001F489 File Offset: 0x0001D689
		protected override string GetTitle()
		{
			return "Pogo";
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001F490 File Offset: 0x0001D690
		protected override string GetDescription()
		{
			return "You jump every 4 seconds, even if you are in mid-air! (Weee!!)";
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001F497 File Offset: 0x0001D697
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Pogo");
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001F4A8 File Offset: 0x0001D6A8
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001F4AC File Offset: 0x0001D6AC
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Gravity",
					amount = "-50%",
					simepleAmount = 6
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Block Cooldown",
					amount = "-50%",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001F517 File Offset: 0x0001D717
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 4;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001F51A File Offset: 0x0001D71A
		public override string GetModName()
		{
			return "CR";
		}
	}
}
