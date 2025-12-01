using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000C6 RID: 198
	public class DarkMatterCard : CustomCard
	{
		// Token: 0x0600060B RID: 1547 RVA: 0x00021E69 File Offset: 0x00020069
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.reflects = 1;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x00021E74 File Offset: 0x00020074
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = Color.clear;
			gun.projectielSimulatonSpeed *= 0.5f;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Dark", new Type[]
				{
					typeof(DarkMono)
				})
			});
			ObjectsToSpawn item = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			list.Add(item);
			gun.objectsToSpawn = list.ToArray();
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x00021F09 File Offset: 0x00020109
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00021F0B File Offset: 0x0002010B
		protected override string GetTitle()
		{
			return "Dark Matter";
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00021F12 File Offset: 0x00020112
		protected override string GetDescription()
		{
			return "Your bullets become invisible, only having rings around them and dealing damage within their radius over time based on the enemies' max hp!";
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x00021F19 File Offset: 0x00020119
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_DarkMatter");
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00021F2A File Offset: 0x0002012A
		protected override CardInfo.Rarity GetRarity()
		{
			return 2;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00021F30 File Offset: 0x00020130
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bounce",
					amount = "+1",
					simepleAmount = 0
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

		// Token: 0x06000613 RID: 1555 RVA: 0x00021F9B File Offset: 0x0002019B
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 0;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00021F9E File Offset: 0x0002019E
		public override string GetModName()
		{
			return "CR";
		}
	}
}
