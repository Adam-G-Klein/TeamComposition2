using System;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000A6 RID: 166
	public class MitosisCard : CustomCard
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x0001F259 File Offset: 0x0001D459
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
			gun.ammo = 1;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0001F26C File Offset: 0x0001D46C
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor += new Color(0f, 1f, 0f, 1f);
			player.gameObject.AddComponent<MitosisMono>();
			block.cdAdd += 0.5f;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0001F2C2 File Offset: 0x0001D4C2
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001F2C4 File Offset: 0x0001D4C4
		protected override string GetTitle()
		{
			return "Mitosis";
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0001F2CB File Offset: 0x0001D4CB
		protected override string GetDescription()
		{
			return "Blocking adds +1 Bullet, +25% Damage and +75% Projectile Speed to your next shot! (Cannot stack)";
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0001F2D2 File Offset: 0x0001D4D2
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Mitosis");
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001F2E3 File Offset: 0x0001D4E3
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0001F2E8 File Offset: 0x0001D4E8
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Ammo",
					amount = "+1"
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Block Cooldown",
					amount = "+0.5s",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0001F34C File Offset: 0x0001D54C
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 5;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0001F34F File Offset: 0x0001D54F
		public override string GetModName()
		{
			return "CR";
		}
	}
}
