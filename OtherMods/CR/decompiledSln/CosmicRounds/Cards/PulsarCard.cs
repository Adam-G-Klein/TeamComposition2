using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000B6 RID: 182
	public class PulsarCard : CustomCard
	{
		// Token: 0x06000556 RID: 1366 RVA: 0x0002096A File Offset: 0x0001EB6A
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.reflects = 2;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00020974 File Offset: 0x0001EB74
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = new Color(1f, 0.6f, 1f, 1f);
			gun.gravity *= 1.25f;
			gun.damage *= 1.3f;
			player.gameObject.AddComponent<PulsarMono>();
			ObjectsToSpawn item = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(item);
			gun.objectsToSpawn = list.ToArray();
			gunAmmo.reloadTimeMultiplier *= 1.3f;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00020A1D File Offset: 0x0001EC1D
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00020A1F File Offset: 0x0001EC1F
		protected override string GetTitle()
		{
			return "Pulsar";
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00020A26 File Offset: 0x0001EC26
		protected override string GetDescription()
		{
			return "Your bullets create a pulsar on bounce that redriects and slows targets! Pulsars deal 9% of bullet damage and are unblockable!";
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00020A2D File Offset: 0x0001EC2D
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Pulsar");
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00020A3E File Offset: 0x0001EC3E
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x0600055D RID: 1373 RVA: 0x00020A44 File Offset: 0x0001EC44
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+30%",
					simepleAmount = 2
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
					stat = "Bullet Gravity",
					amount = "+25%",
					simepleAmount = 1
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload TIme",
					amount = "+30%",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x0600055E RID: 1374 RVA: 0x00020B07 File Offset: 0x0001ED07
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 8;
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00020B0A File Offset: 0x0001ED0A
		public override string GetModName()
		{
			return "CR";
		}
	}
}
