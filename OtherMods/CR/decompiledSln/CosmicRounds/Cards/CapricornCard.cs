using System;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000F5 RID: 245
	public class CapricornCard : CustomCard
	{
		// Token: 0x06000833 RID: 2099 RVA: 0x000252FF File Offset: 0x000234FF
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.categories = new CardCategory[]
			{
				ZodiacCard.ZodiacClass
			};
			statModifiers.secondsToTakeDamageOver = 2f;
			statModifiers.health = 1.3f;
			CapricornCard.card = cardInfo;
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x00025333 File Offset: 0x00023533
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = Color.yellow;
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x00025340 File Offset: 0x00023540
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00025342 File Offset: 0x00023542
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0002535A File Offset: 0x0002355A
		protected override string GetTitle()
		{
			return "Capricorn";
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00025361 File Offset: 0x00023561
		protected override string GetDescription()
		{
			return "Serious and disciplined...";
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00025368 File Offset: 0x00023568
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Capricorn");
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00025379 File Offset: 0x00023579
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0002537C File Offset: 0x0002357C
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
					amount = "+30%"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Decay",
					amount = "+2s"
				}
			};
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x000253D9 File Offset: 0x000235D9
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 6;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x000253DC File Offset: 0x000235DC
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004B7 RID: 1207
		internal static CardInfo card;
	}
}
