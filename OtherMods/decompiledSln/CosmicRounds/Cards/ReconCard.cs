using System;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x0200009D RID: 157
	public class ReconCard : CustomCard
	{
		// Token: 0x06000443 RID: 1091 RVA: 0x0001E711 File Offset: 0x0001C911
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001E714 File Offset: 0x0001C914
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileSpeed *= 5f;
			gun.damageAfterDistanceMultiplier *= 2f;
			gun.attackSpeed *= 3f;
			gun.spread = 0f;
			gun.evenSpread = 0f;
			gun.projectileColor = new Color(1f, 1f, 0.1f, 1f);
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001E78C File Offset: 0x0001C98C
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x0001E78E File Offset: 0x0001C98E
		protected override string GetTitle()
		{
			return "Recon";
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x0001E795 File Offset: 0x0001C995
		protected override string GetDescription()
		{
			return "Fire REALLY high velocity bullets!";
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x0001E79C File Offset: 0x0001C99C
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Recon");
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x0001E7AD File Offset: 0x0001C9AD
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0001E7B0 File Offset: 0x0001C9B0
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Spread",
					amount = "Reset",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Speed",
					amount = "+400%",
					simepleAmount = 4
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Damage Growth",
					amount = "+100%",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = false,
					stat = "ATK SPD",
					amount = "-66%",
					simepleAmount = 7
				}
			};
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x0001E873 File Offset: 0x0001CA73
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 1;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0001E876 File Offset: 0x0001CA76
		public override string GetModName()
		{
			return "CR";
		}
	}
}
