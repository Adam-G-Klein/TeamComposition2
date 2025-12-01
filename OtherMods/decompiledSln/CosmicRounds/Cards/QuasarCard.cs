using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000B9 RID: 185
	public class QuasarCard : CustomCard
	{
		// Token: 0x06000577 RID: 1399 RVA: 0x00020D7D File Offset: 0x0001EF7D
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00020D7F File Offset: 0x0001EF7F
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<QuasarMono>();
			characterStats.movementSpeed *= 0.8f;
			block.cdMultiplier *= 1.25f;
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00020DB3 File Offset: 0x0001EFB3
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00020DB5 File Offset: 0x0001EFB5
		protected override string GetTitle()
		{
			return "Quasar";
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00020DBC File Offset: 0x0001EFBC
		protected override string GetDescription()
		{
			return "Blocking conjures a black hole that sucks in and rips opponents apart!";
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00020DC3 File Offset: 0x0001EFC3
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Quasar");
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00020DD4 File Offset: 0x0001EFD4
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00020DD8 File Offset: 0x0001EFD8
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = false,
					stat = "Movement Speed",
					amount = "-20%",
					simepleAmount = 1
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

		// Token: 0x0600057F RID: 1407 RVA: 0x00020E43 File Offset: 0x0001F043
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 0;
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00020E46 File Offset: 0x0001F046
		public override string GetModName()
		{
			return "CR";
		}
	}
}
