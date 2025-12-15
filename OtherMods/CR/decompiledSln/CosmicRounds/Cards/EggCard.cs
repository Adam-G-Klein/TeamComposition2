using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using ModdingUtils.Extensions;
using ModdingUtils.Utils;
using RarityLib.Utils;
using UnboundLib.Cards;
using UnboundLib.Utils;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000BB RID: 187
	public class EggCard : CustomCard
	{
		// Token: 0x0600058D RID: 1421 RVA: 0x00020F4D File Offset: 0x0001F14D
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
			CardInfoExtension.GetAdditionalData(cardInfo).canBeReassigned = false;
			cardInfo.categories = new CardCategory[]
			{
				CustomCardCategories.instance.CardCategory("CardManipulation")
			};
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x00020F80 File Offset: 0x0001F180
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			for (int i = 0; i < 2; i++)
			{
				this.rand = new Random();
				this.chance = this.rand.Next(1, 101);
				this.leg = this.rand.Next(1, 101);
				if (this.chance < 34)
				{
					if (this.leg < 34)
					{
						CardInfo cardInfo = Cards.instance.NORARITY_GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, new Func<CardInfo, Player, Gun, GunAmmo, CharacterData, HealthHandler, Gravity, Block, CharacterStatModifiers, bool>(this.condition4), 1000);
						if (cardInfo == null)
						{
							CardInfo[] array = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>().Concat((List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToArray<CardInfo>();
							cardInfo = Cards.instance.DrawRandomCardWithCondition(array, player, null, null, null, null, null, null, null, new Func<CardInfo, Player, Gun, GunAmmo, CharacterData, HealthHandler, Gravity, Block, CharacterStatModifiers, bool>(this.condition4), 1000);
						}
						Cards.instance.AddCardToPlayer(player, cardInfo, false, "", 0f, 0f, true);
						CardBarUtils.instance.ShowAtEndOfPhase(player, cardInfo);
					}
					else
					{
						CardInfo cardInfo2 = Cards.instance.NORARITY_GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, new Func<CardInfo, Player, Gun, GunAmmo, CharacterData, HealthHandler, Gravity, Block, CharacterStatModifiers, bool>(this.condition1), 1000);
						if (cardInfo2 == null)
						{
							CardInfo[] array2 = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>().Concat((List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToArray<CardInfo>();
							cardInfo2 = Cards.instance.DrawRandomCardWithCondition(array2, player, null, null, null, null, null, null, null, new Func<CardInfo, Player, Gun, GunAmmo, CharacterData, HealthHandler, Gravity, Block, CharacterStatModifiers, bool>(this.condition1), 1000);
						}
						Cards.instance.AddCardToPlayer(player, cardInfo2, false, "", 0f, 0f, true);
						CardBarUtils.instance.ShowAtEndOfPhase(player, cardInfo2);
					}
				}
				else if (this.chance > 33 && this.chance < 67)
				{
					CardInfo cardInfo3 = Cards.instance.NORARITY_GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, new Func<CardInfo, Player, Gun, GunAmmo, CharacterData, HealthHandler, Gravity, Block, CharacterStatModifiers, bool>(this.condition2), 1000);
					if (cardInfo3 == null)
					{
						CardInfo[] array3 = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>().Concat((List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToArray<CardInfo>();
						cardInfo3 = Cards.instance.DrawRandomCardWithCondition(array3, player, null, null, null, null, null, null, null, new Func<CardInfo, Player, Gun, GunAmmo, CharacterData, HealthHandler, Gravity, Block, CharacterStatModifiers, bool>(this.condition2), 1000);
					}
					Cards.instance.AddCardToPlayer(player, cardInfo3, false, "", 0f, 0f, true);
					CardBarUtils.instance.ShowAtEndOfPhase(player, cardInfo3);
				}
				else if (this.chance > 66)
				{
					CardInfo cardInfo4 = Cards.instance.NORARITY_GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, new Func<CardInfo, Player, Gun, GunAmmo, CharacterData, HealthHandler, Gravity, Block, CharacterStatModifiers, bool>(this.condition3), 1000);
					if (cardInfo4 == null)
					{
						CardInfo[] array4 = ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToList<CardInfo>().Concat((List<CardInfo>)typeof(CardManager).GetField("inactiveCards", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null)).ToArray<CardInfo>();
						cardInfo4 = Cards.instance.DrawRandomCardWithCondition(array4, player, null, null, null, null, null, null, null, new Func<CardInfo, Player, Gun, GunAmmo, CharacterData, HealthHandler, Gravity, Block, CharacterStatModifiers, bool>(this.condition3), 1000);
					}
					Cards.instance.AddCardToPlayer(player, cardInfo4, false, "", 0f, 0f, true);
					CardBarUtils.instance.ShowAtEndOfPhase(player, cardInfo4);
				}
			}
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x0002137A File Offset: 0x0001F57A
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x0002137C File Offset: 0x0001F57C
		protected override string GetTitle()
		{
			return "Egg";
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x00021383 File Offset: 0x0001F583
		protected override string GetDescription()
		{
			return "EGG!";
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0002138A File Offset: 0x0001F58A
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Egg");
		}

		// Token: 0x06000593 RID: 1427 RVA: 0x0002139B File Offset: 0x0001F59B
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x06000594 RID: 1428 RVA: 0x000213A0 File Offset: 0x0001F5A0
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "",
					amount = "E G G"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "E G G",
					amount = ""
				},
				new CardInfoStat
				{
					positive = true,
					stat = "",
					amount = "E   G   G"
				}
			};
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x00021422 File Offset: 0x0001F622
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 3;
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x00021425 File Offset: 0x0001F625
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0002142C File Offset: 0x0001F62C
		public bool condition1(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			return card.rarity == 2 && !card.categories.Intersect(EggCard.noLotteryCategories).Any<CardCategory>();
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x00021451 File Offset: 0x0001F651
		public bool condition2(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			return card.rarity == 1 && !card.categories.Intersect(EggCard.noLotteryCategories).Any<CardCategory>();
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x00021476 File Offset: 0x0001F676
		public bool condition3(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			return card.rarity == null && !card.categories.Intersect(EggCard.noLotteryCategories).Any<CardCategory>();
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0002149A File Offset: 0x0001F69A
		public bool condition4(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			return card.rarity == RarityUtils.GetRarity("Legendary") && !card.categories.Intersect(EggCard.noLotteryCategories).Any<CardCategory>();
		}

		// Token: 0x0400049C RID: 1180
		public static CardCategory[] noLotteryCategories = new CardCategory[]
		{
			CustomCardCategories.instance.CardCategory("CardManipulation"),
			CustomCardCategories.instance.CardCategory("NoRandom")
		};

		// Token: 0x0400049D RID: 1181
		private Random rand;

		// Token: 0x0400049E RID: 1182
		private int chance;

		// Token: 0x0400049F RID: 1183
		private int leg;
	}
}
