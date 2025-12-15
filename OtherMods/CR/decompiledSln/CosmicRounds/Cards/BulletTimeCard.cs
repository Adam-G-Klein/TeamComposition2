using System;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x02000095 RID: 149
	public class BulletTimeCard : CustomCard
	{
		// Token: 0x060003EB RID: 1003 RVA: 0x0001DB09 File Offset: 0x0001BD09
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001DB0C File Offset: 0x0001BD0C
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectielSimulatonSpeed *= 0.5f;
			gun.damageAfterDistanceMultiplier *= 1.5f;
			gun.destroyBulletAfter = 0f;
			gunAmmo.reloadTimeMultiplier *= 0.65f;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0001DB5A File Offset: 0x0001BD5A
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001DB5C File Offset: 0x0001BD5C
		protected override string GetTitle()
		{
			return "Bullet Time";
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x0001DB63 File Offset: 0x0001BD63
		protected override string GetDescription()
		{
			return "Your bullets move slower!";
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0001DB6A File Offset: 0x0001BD6A
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_BulletTIme");
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0001DB7B File Offset: 0x0001BD7B
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0001DB80 File Offset: 0x0001BD80
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Range",
					amount = "Reset",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Damage Growth",
					amount = "+50%",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Reload Time",
					amount = "-35%",
					simepleAmount = 6
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Projectile Speed",
					amount = "-50%",
					simepleAmount = 7
				}
			};
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0001DC43 File Offset: 0x0001BE43
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 8;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0001DC46 File Offset: 0x0001BE46
		public override string GetModName()
		{
			return "CR";
		}
	}
}
