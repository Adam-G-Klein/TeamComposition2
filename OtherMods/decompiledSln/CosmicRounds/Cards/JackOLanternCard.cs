using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000CE RID: 206
	public class JackOLanternCard : CustomCard
	{
		// Token: 0x06000663 RID: 1635 RVA: 0x00022AED File Offset: 0x00020CED
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.reflects = 6;
			gun.reloadTimeAdd = 0.25f;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00022B04 File Offset: 0x00020D04
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectielSimulatonSpeed *= 1.25f;
			gun.speedMOnBounce *= 0.85f;
			player.gameObject.AddComponent<DropMono>();
			player.gameObject.AddComponent<PumpkinMono>();
			ObjectsToSpawn item = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(item);
			gun.objectsToSpawn = list.ToArray();
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00022B88 File Offset: 0x00020D88
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00022B8A File Offset: 0x00020D8A
		protected override string GetTitle()
		{
			return "Jack-O-Lantern";
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00022B91 File Offset: 0x00020D91
		protected override string GetDescription()
		{
			return "This is Halloween! Your Pumpkin bullets lose a bit of speed on bounce!";
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00022B98 File Offset: 0x00020D98
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_JackOLantern");
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00022BA9 File Offset: 0x00020DA9
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00022BAC File Offset: 0x00020DAC
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Projectile Speed",
					amount = "+25%",
					simepleAmount = 1
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Bounces",
					amount = "+6",
					simepleAmount = 0
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Time",
					amount = "+0.25s",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00022C43 File Offset: 0x00020E43
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 6;
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x00022C46 File Offset: 0x00020E46
		public override string GetModName()
		{
			return "CR";
		}
	}
}
