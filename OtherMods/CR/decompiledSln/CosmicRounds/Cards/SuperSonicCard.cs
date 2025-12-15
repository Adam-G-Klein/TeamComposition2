using System;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x02000092 RID: 146
	public class SuperSonicCard : CustomCard
	{
		// Token: 0x060003CA RID: 970 RVA: 0x0001D6A9 File Offset: 0x0001B8A9
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.attackSpeedMultiplier = 0.5f;
			statModifiers.movementSpeed = 1.65f;
			statModifiers.health = 0.75f;
			block.cdMultiplier = 0.5f;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0001D6DA File Offset: 0x0001B8DA
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gunAmmo.reloadTimeMultiplier *= 0.5f;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0001D6EE File Offset: 0x0001B8EE
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0001D6F0 File Offset: 0x0001B8F0
		protected override string GetTitle()
		{
			return "Super Sonic";
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0001D6F7 File Offset: 0x0001B8F7
		protected override string GetDescription()
		{
			return "You're too slow!";
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0001D6FE File Offset: 0x0001B8FE
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_SuperSonic");
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0001D70F File Offset: 0x0001B90F
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0001D714 File Offset: 0x0001B914
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "ATK SPD",
					amount = "+100%",
					simepleAmount = 4
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Movement Speed",
					amount = "+65%",
					simepleAmount = 3
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Reload Time",
					amount = "-50%",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Block Cooldown",
					amount = "-50%",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Health",
					amount = "-25%",
					simepleAmount = 6
				}
			};
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0001D803 File Offset: 0x0001BA03
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 6;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0001D806 File Offset: 0x0001BA06
		public override string GetModName()
		{
			return "CR";
		}
	}
}
