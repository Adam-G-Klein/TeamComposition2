using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000BD RID: 189
	public class HiveCard : CustomCard
	{
		// Token: 0x060005A8 RID: 1448 RVA: 0x00021625 File Offset: 0x0001F825
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			block.cdMultiplier = 1.25f;
			cardInfo.allowMultiple = false;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0002163A File Offset: 0x0001F83A
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<HiveMono>();
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00021648 File Offset: 0x0001F848
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0002164A File Offset: 0x0001F84A
		protected override string GetTitle()
		{
			return "Hive";
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00021651 File Offset: 0x0001F851
		protected override string GetDescription()
		{
			return "Blocking turns your next shot into a homing bee swarm! (Cannot stack)";
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x00021658 File Offset: 0x0001F858
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Hive");
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00021669 File Offset: 0x0001F869
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0002166C File Offset: 0x0001F86C
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = false,
					stat = "Block Cooldown",
					amount = "+25%",
					simepleAmount = 4
				}
			};
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x000216A0 File Offset: 0x0001F8A0
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 6;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x000216A3 File Offset: 0x0001F8A3
		public override string GetModName()
		{
			return "CR";
		}
	}
}
