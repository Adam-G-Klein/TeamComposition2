using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000C1 RID: 193
	public class HeartbeatCard : CustomCard
	{
		// Token: 0x060005D4 RID: 1492 RVA: 0x000219B5 File Offset: 0x0001FBB5
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x000219BE File Offset: 0x0001FBBE
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<HeartbeatMono>();
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000219CC File Offset: 0x0001FBCC
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x000219CE File Offset: 0x0001FBCE
		protected override string GetTitle()
		{
			return "Heartbeat";
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x000219D5 File Offset: 0x0001FBD5
		protected override string GetDescription()
		{
			return "Heal for 25% of your max health and regain your block every 5 seconds!";
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x000219DC File Offset: 0x0001FBDC
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Heartbeat");
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x000219ED File Offset: 0x0001FBED
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x000219F0 File Offset: 0x0001FBF0
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[0];
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000219F8 File Offset: 0x0001FBF8
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 8;
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x000219FB File Offset: 0x0001FBFB
		public override string GetModName()
		{
			return "CR";
		}
	}
}
