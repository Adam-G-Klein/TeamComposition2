using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using RarityLib.Utils;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000FB RID: 251
	public class MoonCard : CustomCard
	{
		// Token: 0x06000878 RID: 2168 RVA: 0x0002583D File Offset: 0x00023A3D
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			cardInfo.allowMultiple = false;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00025848 File Offset: 0x00023A48
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = Color.cyan;
			gun.projectielSimulatonSpeed *= 0.5f;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Dark", new Type[]
				{
					typeof(MoonMono)
				})
			});
			ObjectsToSpawn item = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			list.Add(item);
			gun.objectsToSpawn = list.ToArray();
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x000258DD File Offset: 0x00023ADD
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x000258DF File Offset: 0x00023ADF
		protected override string GetTitle()
		{
			return "Moon";
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x000258E6 File Offset: 0x00023AE6
		protected override string GetDescription()
		{
			return "Your bullets have a gravitational pull to them!!";
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x000258ED File Offset: 0x00023AED
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Moon");
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x000258FE File Offset: 0x00023AFE
		protected override CardInfo.Rarity GetRarity()
		{
			return RarityUtils.GetRarity("Legendary");
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0002590A File Offset: 0x00023B0A
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = false,
					stat = "Projectile Speed",
					amount = "-50%",
					simepleAmount = 7
				}
			};
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0002593E File Offset: 0x00023B3E
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 7;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00025941 File Offset: 0x00023B41
		public override string GetModName()
		{
			return "CR";
		}
	}
}
