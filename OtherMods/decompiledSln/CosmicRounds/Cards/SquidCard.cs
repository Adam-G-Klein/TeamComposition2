using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000DE RID: 222
	public class SquidCard : CustomCard
	{
		// Token: 0x06000715 RID: 1813 RVA: 0x00023B27 File Offset: 0x00021D27
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.reflects = 2;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x00023B30 File Offset: 0x00021D30
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = new Color(0.2f, 0.2f, 1f, 1f);
			gun.damage *= 0.4f;
			gun.gravity = 0f;
			gun.destroyBulletAfter = 0f;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Squid", new Type[]
				{
					typeof(SquidMono)
				})
			});
			ObjectsToSpawn item = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			list.Add(item);
			gun.objectsToSpawn = list.ToArray();
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x00023BEF File Offset: 0x00021DEF
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x00023BF1 File Offset: 0x00021DF1
		protected override string GetTitle()
		{
			return "Squid";
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x00023BF8 File Offset: 0x00021DF8
		protected override string GetDescription()
		{
			return "Your bullets fix their trajectory to where a close and visible enemy is multiple times.";
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00023BFF File Offset: 0x00021DFF
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Squid");
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x00023C10 File Offset: 0x00021E10
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00023C14 File Offset: 0x00021E14
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
					stat = "Gravity",
					amount = "Zero",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bounces",
					amount = "+2",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Damage",
					amount = "-60%",
					simepleAmount = 7
				}
			};
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00023CD7 File Offset: 0x00021ED7
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 7;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00023CDA File Offset: 0x00021EDA
		public override string GetModName()
		{
			return "CR";
		}
	}
}
