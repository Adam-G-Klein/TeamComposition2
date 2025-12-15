using System;
using System.Linq;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using ModdingUtils.Utils;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000D2 RID: 210
	public class TabulaRasaCard : CustomCard
	{
		// Token: 0x0600068F RID: 1679 RVA: 0x00023001 File Offset: 0x00021201
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
			cardInfo.categories = new CardCategory[]
			{
				CustomCardCategories.instance.CardCategory("CardManipulation")
			};
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00023028 File Offset: 0x00021228
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			foreach (Player player2 in PlayerManager.instance.players)
			{
				CardInfo cardInfo = CR.hiddenCards.First<CardInfo>();
				Cards.instance.AddCardToPlayer(player2, cardInfo, false, "", 0f, 0f, true);
			}
			data.maxHealth *= 2f;
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x000230B4 File Offset: 0x000212B4
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000230B6 File Offset: 0x000212B6
		protected override string GetTitle()
		{
			return "Tabula Rasa";
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x000230BD File Offset: 0x000212BD
		protected override string GetDescription()
		{
			return "A clean slate...";
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x000230C4 File Offset: 0x000212C4
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_TabulaRasa");
		}

		// Token: 0x06000695 RID: 1685 RVA: 0x000230D5 File Offset: 0x000212D5
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x06000696 RID: 1686 RVA: 0x000230D8 File Offset: 0x000212D8
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
					amount = "+100%"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Everyone's Lifesteal",
					amount = "Reset"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Everyone's Decay",
					amount = "Reset"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Everyone's Revives",
					amount = "Reset"
				}
			};
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x0002317F File Offset: 0x0002137F
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 4;
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00023182 File Offset: 0x00021382
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00023189 File Offset: 0x00021389
		public bool condition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			return card.cardName == "Clear Mind";
		}
	}
}
