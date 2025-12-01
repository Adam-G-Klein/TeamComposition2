using System;
using RarityLib.Utils;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x02000094 RID: 148
	public class OnesKingCard : CustomCard
	{
		// Token: 0x060003E0 RID: 992 RVA: 0x0001D94C File Offset: 0x0001BB4C
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.gravity = 0.5f;
			gun.projectielSimulatonSpeed = 0.5f;
			statModifiers.health = 2.5f;
			statModifiers.gravity = 0.5f;
			statModifiers.jump = 1.5f;
			statModifiers.lifeSteal = 0.5f;
			gun.damage = 1.5f;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0001D9AA File Offset: 0x0001BBAA
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0001D9AC File Offset: 0x0001BBAC
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0001D9AE File Offset: 0x0001BBAE
		protected override string GetTitle()
		{
			return "Ones King";
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0001D9B5 File Offset: 0x0001BBB5
		protected override string GetDescription()
		{
			return "Become the King of Rounds!! (Shoutout to Wunzking!!)";
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0001D9BC File Offset: 0x0001BBBC
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_OnesKing");
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0001D9CD File Offset: 0x0001BBCD
		protected override CardInfo.Rarity GetRarity()
		{
			return RarityUtils.GetRarity("Legendary");
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0001D9DC File Offset: 0x0001BBDC
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Gravity",
					amount = "-50%",
					simepleAmount = 6
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
					amount = "+150%",
					simepleAmount = 2
				},
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
					stat = "Life Steal",
					amount = "+50%",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+50%",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Projectile Speed",
					amount = "-50%",
					simepleAmount = 6
				}
			};
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0001DAF7 File Offset: 0x0001BCF7
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 8;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0001DAFA File Offset: 0x0001BCFA
		public override string GetModName()
		{
			return "CR";
		}
	}
}
