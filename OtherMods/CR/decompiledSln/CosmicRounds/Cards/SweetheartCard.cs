using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000C5 RID: 197
	public class SweetheartCard : CustomCard
	{
		// Token: 0x06000600 RID: 1536 RVA: 0x00021D0E File Offset: 0x0001FF0E
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00021D10 File Offset: 0x0001FF10
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = new Color(1f, 0.5f, 1f);
			if (characterStats.lifeSteal == 0f)
			{
				characterStats.lifeSteal += 1f;
			}
			else
			{
				characterStats.lifeSteal *= 2f;
			}
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Sweet", new Type[]
				{
					typeof(SweetheartMono)
				})
			});
			gun.objectsToSpawn = list.ToArray();
			gunAmmo.reloadTimeMultiplier *= 0.75f;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00021DC7 File Offset: 0x0001FFC7
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00021DC9 File Offset: 0x0001FFC9
		protected override string GetTitle()
		{
			return "Sweet Heart";
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00021DD0 File Offset: 0x0001FFD0
		protected override string GetDescription()
		{
			return "Your bullets deal damage each second over 6s, making the target jump each second!";
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00021DD7 File Offset: 0x0001FFD7
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Sweetheart");
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00021DE8 File Offset: 0x0001FFE8
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00021DEC File Offset: 0x0001FFEC
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Life Steal",
					amount = "+100%",
					simepleAmount = 3
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Reload Time",
					amount = "-25%",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00021E57 File Offset: 0x00020057
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 8;
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x00021E5A File Offset: 0x0002005A
		public override string GetModName()
		{
			return "CR";
		}
	}
}
