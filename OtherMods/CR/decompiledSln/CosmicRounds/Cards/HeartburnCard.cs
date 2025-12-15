using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000C2 RID: 194
	public class HeartburnCard : CustomCard
	{
		// Token: 0x060005DF RID: 1503 RVA: 0x00021A0A File Offset: 0x0001FC0A
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00021A14 File Offset: 0x0001FC14
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<HeartburnMono>();
			characterStats.movementSpeed *= 1.3f;
			gunAmmo.reloadTimeMultiplier *= 1.5f;
			data.maxHealth *= 1.3f;
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00021A65 File Offset: 0x0001FC65
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00021A67 File Offset: 0x0001FC67
		protected override string GetTitle()
		{
			return "Heartburn";
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00021A6E File Offset: 0x0001FC6E
		protected override string GetDescription()
		{
			return "Emit scorching heart embers that heal you and damage enemies while reloading! Heart embers get faster over time and deal damage based on enemy max health!";
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00021A75 File Offset: 0x0001FC75
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Heartburn");
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00021A86 File Offset: 0x0001FC86
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00021A8C File Offset: 0x0001FC8C
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
					amount = "+30%",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Movement Speed",
					amount = "+30%",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Time",
					amount = "+50%",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00021B23 File Offset: 0x0001FD23
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 0;
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00021B26 File Offset: 0x0001FD26
		public override string GetModName()
		{
			return "CR";
		}
	}
}
