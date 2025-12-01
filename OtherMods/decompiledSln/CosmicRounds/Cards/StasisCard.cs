using System;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x02000093 RID: 147
	public class StasisCard : CustomCard
	{
		// Token: 0x060003D5 RID: 981 RVA: 0x0001D815 File Offset: 0x0001BA15
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.projectielSimulatonSpeed = 0.65f;
			gun.damage = 1.35f;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0001D82D File Offset: 0x0001BA2D
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.gravity = 0f;
			gun.projectileColor = Color.cyan;
			gun.destroyBulletAfter = 0f;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0001D850 File Offset: 0x0001BA50
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0001D852 File Offset: 0x0001BA52
		protected override string GetTitle()
		{
			return "Stasis";
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0001D859 File Offset: 0x0001BA59
		protected override string GetDescription()
		{
			return "Your bullets float through the air!";
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0001D860 File Offset: 0x0001BA60
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Stasis");
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001D871 File Offset: 0x0001BA71
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0001D874 File Offset: 0x0001BA74
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
					stat = "Damage",
					amount = "+35%",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Gravity",
					amount = "Zero",
					simepleAmount = 7
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Projectile Speed",
					amount = "-35%",
					simepleAmount = 6
				}
			};
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0001D937 File Offset: 0x0001BB37
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 8;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0001D93A File Offset: 0x0001BB3A
		public override string GetModName()
		{
			return "CR";
		}
	}
}
