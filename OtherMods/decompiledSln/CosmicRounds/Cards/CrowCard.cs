using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x0200008E RID: 142
	public class CrowCard : CustomCard
	{
		// Token: 0x0600039E RID: 926 RVA: 0x0001D179 File Offset: 0x0001B379
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.gravity = 0.7f;
			gun.numberOfProjectiles = 3;
			gun.spread = 0.1f;
			gun.damage = 0.3f;
			gun.ammo = 5;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0001D1AA File Offset: 0x0001B3AA
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = Color.magenta;
			player.gameObject.AddComponent<CrowSpriteMono>();
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001D1C3 File Offset: 0x0001B3C3
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0001D1C5 File Offset: 0x0001B3C5
		protected override string GetTitle()
		{
			return "Crow";
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001D1CC File Offset: 0x0001B3CC
		protected override string GetDescription()
		{
			return "Launch a 'murder' of bullets to pelt targets.";
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0001D1D3 File Offset: 0x0001B3D3
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Crow");
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001D1E4 File Offset: 0x0001B3E4
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0001D1E8 File Offset: 0x0001B3E8
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Gravity",
					amount = "-70%",
					simepleAmount = 7
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bullets",
					amount = "+3",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Ammo",
					amount = "+5",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Damage",
					amount = "-70%",
					simepleAmount = 7
				}
			};
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0001D2AB File Offset: 0x0001B4AB
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 4;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001D2AE File Offset: 0x0001B4AE
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x0400049B RID: 1179
		public AssetBundle Asset;
	}
}
