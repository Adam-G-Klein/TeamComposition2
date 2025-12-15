using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x02000091 RID: 145
	public class MosquitoCard : CustomCard
	{
		// Token: 0x060003BF RID: 959 RVA: 0x0001D54D File Offset: 0x0001B74D
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.projectielSimulatonSpeed = 1.3f;
			statModifiers.lifeSteal = 0.5f;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0001D568 File Offset: 0x0001B768
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = Color.red;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Mosquito", new Type[]
				{
					typeof(MosquitoMono)
				})
			});
			gun.objectsToSpawn = list.ToArray();
			gunAmmo.reloadTimeMultiplier *= 1.25f;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001D5DA File Offset: 0x0001B7DA
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0001D5DC File Offset: 0x0001B7DC
		protected override string GetTitle()
		{
			return "Mosquito";
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0001D5E3 File Offset: 0x0001B7E3
		protected override string GetDescription()
		{
			return "Your bullets become mosquitos, dealing damage each second over 4s.";
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0001D5EA File Offset: 0x0001B7EA
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Mosquito");
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001D5FB File Offset: 0x0001B7FB
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001D600 File Offset: 0x0001B800
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Projectile Speed",
					amount = "+30%",
					simepleAmount = 2
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Lifesteal",
					amount = "+50%",
					simepleAmount = 2
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

		// Token: 0x060003C7 RID: 967 RVA: 0x0001D697 File Offset: 0x0001B897
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 0;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0001D69A File Offset: 0x0001B89A
		public override string GetModName()
		{
			return "CR";
		}
	}
}
