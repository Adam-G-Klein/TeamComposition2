using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x02000096 RID: 150
	public class StunCard : CustomCard
	{
		// Token: 0x060003F6 RID: 1014 RVA: 0x0001DC55 File Offset: 0x0001BE55
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0001DC58 File Offset: 0x0001BE58
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = Color.yellow;
			if (gunAmmo.maxAmmo > 2)
			{
				gunAmmo.maxAmmo -= 2;
			}
			else
			{
				gunAmmo.maxAmmo = 1;
			}
			if (gun.slow > 0f)
			{
				gun.slow *= 1.5f;
			}
			else
			{
				gun.slow += 0.5f;
			}
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Shock", new Type[]
				{
					typeof(ShockMono)
				})
			});
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Stun", new Type[]
				{
					typeof(StunMono)
				})
			});
			gunAmmo.reloadTimeMultiplier *= 0.75f;
			gun.objectsToSpawn = list.ToArray();
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0001DD4D File Offset: 0x0001BF4D
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001DD4F File Offset: 0x0001BF4F
		protected override string GetTitle()
		{
			return "Stun";
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001DD56 File Offset: 0x0001BF56
		protected override string GetDescription()
		{
			return "Your bullets shock targets for 1s and have a 25% chance to stun!";
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0001DD5D File Offset: 0x0001BF5D
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Stun");
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001DD6E File Offset: 0x0001BF6E
		protected override CardInfo.Rarity GetRarity()
		{
			return 1;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001DD74 File Offset: 0x0001BF74
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bullet Slow",
					amount = "+50%"
				},
				new CardInfoStat
				{
					positive = true,
					stat = "Reload Time",
					amount = "-25%",
					simepleAmount = 6
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Ammo",
					amount = "-2"
				}
			};
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001DDFD File Offset: 0x0001BFFD
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 7;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001DE00 File Offset: 0x0001C000
		public override string GetModName()
		{
			return "CR";
		}
	}
}
