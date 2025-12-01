using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x0200009C RID: 156
	public class DropshotCard : CustomCard
	{
		// Token: 0x06000438 RID: 1080 RVA: 0x0001E501 File Offset: 0x0001C701
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.reflects = 1;
			gun.reloadTimeAdd = 0.25f;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0001E518 File Offset: 0x0001C718
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor += new Color(1f, 0.7f, 0f, 1f);
			gun.gravity *= 1.35f;
			gun.projectielSimulatonSpeed *= 1.35f;
			gun.damage *= 1.25f;
			gun.knockback *= 3f;
			player.gameObject.AddComponent<DropMono>();
			ObjectsToSpawn item = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(item);
			gun.objectsToSpawn = list.ToArray();
			player.gameObject.AddComponent<DropballSpriteMono>();
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0001E5EA File Offset: 0x0001C7EA
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0001E5EC File Offset: 0x0001C7EC
		protected override string GetTitle()
		{
			return "Dropshot";
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0001E5F3 File Offset: 0x0001C7F3
		protected override string GetDescription()
		{
			return "Your bullets have more knockback!";
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001E5FA File Offset: 0x0001C7FA
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Dropshot");
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001E60B File Offset: 0x0001C80B
		protected override CardInfo.Rarity GetRarity()
		{
			return 0;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0001E610 File Offset: 0x0001C810
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Projectile Speed",
					amount = "+35%",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Damage",
					amount = "+25%",
					simepleAmount = 2
				},
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
					stat = "Bullet Gravity",
					amount = "+35%",
					simepleAmount = 2
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

		// Token: 0x06000440 RID: 1088 RVA: 0x0001E6FF File Offset: 0x0001C8FF
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 6;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0001E702 File Offset: 0x0001C902
		public override string GetModName()
		{
			return "CR";
		}
	}
}
