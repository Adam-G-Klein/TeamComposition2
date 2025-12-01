using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000AB RID: 171
	public class PulseCard : CustomCard
	{
		// Token: 0x060004DD RID: 1245 RVA: 0x0001FA81 File Offset: 0x0001DC81
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.reflects = 1;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x0001FA8C File Offset: 0x0001DC8C
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor += new Color(1f, 0.8f, 0.7f, 1f);
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Pulse", new Type[]
				{
					typeof(PulseMono)
				})
			});
			ObjectsToSpawn item = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			list.Add(item);
			gun.objectsToSpawn = list.ToArray();
			gunAmmo.reloadTimeMultiplier *= 1.25f;
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0001FB40 File Offset: 0x0001DD40
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0001FB42 File Offset: 0x0001DD42
		protected override string GetTitle()
		{
			return "Pulse";
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0001FB49 File Offset: 0x0001DD49
		protected override string GetDescription()
		{
			return "Your bullets slow down and speed up. When they speed up, they explode up to 2 times.";
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x0001FB50 File Offset: 0x0001DD50
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Pulse");
		}

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001FB61 File Offset: 0x0001DD61
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x0001FB64 File Offset: 0x0001DD64
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
					stat = "Reload Time",
					amount = "+25%",
					simepleAmount = 0
				}
			};
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x0001FBCF File Offset: 0x0001DDCF
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 3;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0001FBD2 File Offset: 0x0001DDD2
		public override string GetModName()
		{
			return "CR";
		}
	}
}
