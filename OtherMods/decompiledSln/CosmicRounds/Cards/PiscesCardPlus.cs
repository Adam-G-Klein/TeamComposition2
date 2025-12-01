using System;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000E2 RID: 226
	public class PiscesCardPlus : CustomCard
	{
		// Token: 0x06000745 RID: 1861 RVA: 0x0002409F File Offset: 0x0002229F
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			statModifiers.health = 1.6f;
			statModifiers.movementSpeed = 1.6f;
			PiscesCardPlus.card = cardInfo;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x000240BF File Offset: 0x000222BF
		public override bool GetEnabled()
		{
			return false;
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x000240C2 File Offset: 0x000222C2
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = Color.green;
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x000240CF File Offset: 0x000222CF
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x000240D1 File Offset: 0x000222D1
		public override void Callback()
		{
			ExtensionMethods.GetOrAddComponent<ClassNameMono>(base.gameObject, false).className = ZodiacClass.name;
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x000240E9 File Offset: 0x000222E9
		protected override string GetTitle()
		{
			return "Pisces+";
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x000240F0 File Offset: 0x000222F0
		protected override string GetDescription()
		{
			return "Affectionate and empathetic...";
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x000240F7 File Offset: 0x000222F7
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Pisces");
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x00024108 File Offset: 0x00022308
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0002410C File Offset: 0x0002230C
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Health",
					amount = "+60%"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Movement Speed",
					amount = "+60%"
				}
			};
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00024169 File Offset: 0x00022369
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 5;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0002416C File Offset: 0x0002236C
		public override string GetModName()
		{
			return "CR";
		}

		// Token: 0x040004A4 RID: 1188
		internal static CardInfo card;
	}
}
