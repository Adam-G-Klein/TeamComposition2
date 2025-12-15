using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000B0 RID: 176
	public class UnicornCard : CustomCard
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x00020265 File Offset: 0x0001E465
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0002026E File Offset: 0x0001E46E
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<UnicornMono>();
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0002027C File Offset: 0x0001E47C
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0002027E File Offset: 0x0001E47E
		protected override string GetTitle()
		{
			return "Unicorn";
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00020285 File Offset: 0x0001E485
		protected override string GetDescription()
		{
			return "Happy (Belated) Pride Month! Change colors every 8s, gain unique effects with each color!";
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0002028C File Offset: 0x0001E48C
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Unicorn");
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0002029D File Offset: 0x0001E49D
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x000202A0 File Offset: 0x0001E4A0
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[0];
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000202A8 File Offset: 0x0001E4A8
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 8;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x000202AB File Offset: 0x0001E4AB
		public override string GetModName()
		{
			return "CR";
		}
	}
}
