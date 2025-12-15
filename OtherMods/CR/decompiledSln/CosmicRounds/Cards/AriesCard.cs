using System;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000E3 RID: 227
	public class AriesCard : CustomCard
	{
		// Token: 0x06000752 RID: 1874 RVA: 0x0002417B File Offset: 0x0002237B
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.categories = new CardCategory[]
			{
				ZodiacCard.ZodiacClass
			};
			statModifiers.movementSpeed = 1.3f;
			AriesCard.card = cardInfo;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x000241A3 File Offset: 0x000223A3
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.damage *= 1.3f;
			gun.projectileColor = Color.red;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x000241C2 File Offset: 0x000223C2
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x000241C4 File Offset: 0x000223C4
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x000241DC File Offset: 0x000223DC
		protected override string GetTitle()
		{
			return "Aries";
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x000241E3 File Offset: 0x000223E3
		protected override string GetDescription()
		{
			return "Quick and competitive...";
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x000241EA File Offset: 0x000223EA
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Aries");
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x000241FB File Offset: 0x000223FB
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x00024200 File Offset: 0x00022400
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+30%"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Movement Speed",
					amount = "+30%"
				}
			};
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0002425D File Offset: 0x0002245D
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 0;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00024260 File Offset: 0x00022460
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004A5 RID: 1189
		internal static CardInfo card;
	}
}
