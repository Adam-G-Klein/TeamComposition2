using System;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000A4 RID: 164
	public class FocusCard : CustomCard
	{
		// Token: 0x06000490 RID: 1168 RVA: 0x0001EF71 File Offset: 0x0001D171
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0001EF73 File Offset: 0x0001D173
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.spread = 0f;
			gun.attackSpeed *= 1.5f;
			data.maxHealth *= 1.7f;
			gun.destroyBulletAfter = 0f;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001EFB0 File Offset: 0x0001D1B0
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001EFB2 File Offset: 0x0001D1B2
		protected override string GetTitle()
		{
			return "Focus";
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001EFB9 File Offset: 0x0001D1B9
		protected override string GetDescription()
		{
			return null;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001EFBC File Offset: 0x0001D1BC
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Focus");
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0001EFCD File Offset: 0x0001D1CD
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001EFD0 File Offset: 0x0001D1D0
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
					stat = "Bullet Spread",
					amount = "Reset",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
					amount = "+70%",
					simepleAmount = 3
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

		// Token: 0x06000498 RID: 1176 RVA: 0x0001F093 File Offset: 0x0001D293
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 6;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0001F096 File Offset: 0x0001D296
		public override string GetModName()
		{
			return "CR";
		}
	}
}
