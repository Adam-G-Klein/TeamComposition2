using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000B3 RID: 179
	public class FaeEmbersCard : CustomCard
	{
		// Token: 0x06000535 RID: 1333 RVA: 0x00020521 File Offset: 0x0001E721
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0002052C File Offset: 0x0001E72C
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			player.gameObject.AddComponent<FaeEmbersMono>();
			characterStats.movementSpeed *= 1.3f;
			gunAmmo.reloadTimeMultiplier *= 1.5f;
			data.maxHealth *= 1.3f;
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0002057D File Offset: 0x0001E77D
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0002057F File Offset: 0x0001E77F
		protected override string GetTitle()
		{
			return "Fae Embers";
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x00020586 File Offset: 0x0001E786
		protected override string GetDescription()
		{
			return "Emit small, scorching fae embers while reloading, these fae embers deal damage based on the enemies' max health.";
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0002058D File Offset: 0x0001E78D
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_FaeEmbers");
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0002059E File Offset: 0x0001E79E
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x000205A4 File Offset: 0x0001E7A4
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

		// Token: 0x0600053D RID: 1341 RVA: 0x0002063B File Offset: 0x0001E83B
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 8;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0002063E File Offset: 0x0001E83E
		public override string GetModName()
		{
			return "CR";
		}
	}
}
