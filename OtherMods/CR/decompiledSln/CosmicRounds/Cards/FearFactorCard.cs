using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x02000097 RID: 151
	public class FearFactorCard : CustomCard
	{
		// Token: 0x06000401 RID: 1025 RVA: 0x0001DE0F File Offset: 0x0001C00F
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0001DE18 File Offset: 0x0001C018
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<FearFactorMono>();
			data.maxHealth *= 1.5f;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001DE39 File Offset: 0x0001C039
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0001DE3B File Offset: 0x0001C03B
		protected override string GetTitle()
		{
			return "Fear Factor";
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0001DE42 File Offset: 0x0001C042
		protected override string GetDescription()
		{
			return "Sporadically shoot bullets when an ENEMY BULLET is near.";
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001DE49 File Offset: 0x0001C049
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_FearFactor");
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0001DE5A File Offset: 0x0001C05A
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0001DE60 File Offset: 0x0001C060
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
					amount = "+50%",
					simepleAmount = 3
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

		// Token: 0x06000409 RID: 1033 RVA: 0x0001DECB File Offset: 0x0001C0CB
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 0;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0001DECE File Offset: 0x0001C0CE
		public override string GetModName()
		{
			return "CR";
		}
	}
}
