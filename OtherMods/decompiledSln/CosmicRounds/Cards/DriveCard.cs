using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000AC RID: 172
	public class DriveCard : CustomCard
	{
		// Token: 0x060004E8 RID: 1256 RVA: 0x0001FBE1 File Offset: 0x0001DDE1
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0001FBE4 File Offset: 0x0001DDE4
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = new Color(0.7f, 1f, 0.7f, 1f);
			gun.damage *= 0.4f;
			gun.destroyBulletAfter = 0f;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Drive", new Type[]
				{
					typeof(DriveMono)
				})
			});
			gun.objectsToSpawn = list.ToArray();
		}

		// Token: 0x060004EA RID: 1258 RVA: 0x0001FC75 File Offset: 0x0001DE75
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x0001FC77 File Offset: 0x0001DE77
		protected override string GetTitle()
		{
			return "Drive";
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0001FC7E File Offset: 0x0001DE7E
		protected override string GetDescription()
		{
			return "Your bullets fix their trajectory once to where a close and visible enemy is.";
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x0001FC85 File Offset: 0x0001DE85
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Drive");
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001FC96 File Offset: 0x0001DE96
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001FC9C File Offset: 0x0001DE9C
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
					positive = false,
					stat = "Damage",
					amount = "-60%",
					simepleAmount = 6
				}
			};
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0001FD07 File Offset: 0x0001DF07
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 3;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001FD0A File Offset: 0x0001DF0A
		public override string GetModName()
		{
			return "CR";
		}
	}
}
