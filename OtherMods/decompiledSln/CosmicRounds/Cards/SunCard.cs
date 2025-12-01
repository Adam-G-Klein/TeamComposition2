using System;
using System.Collections.Generic;
using System.Linq;
using CR.MonoBehaviors;
using RarityLib.Utils;
using UnboundLib.Cards;
using UnityEngine;

namespace CR.Cards
{
	// Token: 0x020000AD RID: 173
	public class SunCard : CustomCard
	{
		// Token: 0x060004F3 RID: 1267 RVA: 0x0001FD19 File Offset: 0x0001DF19
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.reflects = 2;
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001FD24 File Offset: 0x0001DF24
		public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.projectileColor = new Color(1f, 0.8f, 0.1f, 1f);
			gun.destroyBulletAfter = 7f;
			gun.damage *= 0.75f;
			gun.unblockable = true;
			List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList<ObjectsToSpawn>();
			list.Add(new ObjectsToSpawn
			{
				AddToProjectile = new GameObject("A_Sun", new Type[]
				{
					typeof(SunMono)
				})
			});
			GameObject gameObject = Object.Instantiate<GameObject>(((GameObject)Resources.Load("0 cards/Explosive bullet")).GetComponent<Gun>().objectsToSpawn[0].effect);
			gameObject.transform.position = new Vector3(1000f, 0f, 0f);
			gameObject.hideFlags = 61;
			gameObject.name = "Explosion";
			Object.DestroyImmediate(gameObject.GetComponent<RemoveAfterSeconds>());
			gameObject.GetComponent<Explosion>().force = 5000f;
			list.Add(new ObjectsToSpawn
			{
				effect = gameObject,
				normalOffset = 0.1f,
				numberOfSpawns = 1,
				scaleFromDamage = 0.5f,
				scaleStackM = 0.7f,
				scaleStacks = true
			});
			ObjectsToSpawn item = ((GameObject)Resources.Load("0 cards/Mayhem")).GetComponent<Gun>().objectsToSpawn[0];
			list.Add(item);
			gun.objectsToSpawn = list.ToArray();
			player.gameObject.AddComponent<FireMono>();
			gunAmmo.reloadTimeMultiplier *= 1.25f;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001FEB1 File Offset: 0x0001E0B1
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001FEB3 File Offset: 0x0001E0B3
		protected override string GetTitle()
		{
			return "Sun";
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0001FEBA File Offset: 0x0001E0BA
		protected override string GetDescription()
		{
			return "Your bullets become sun beams that penetrate players' blocks!! They also redirect towards the center of the screen when they run out of bounces!!";
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0001FEC1 File Offset: 0x0001E0C1
		protected override GameObject GetCardArt()
		{
			return CR.ArtAsset.LoadAsset<GameObject>("C_Sun");
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0001FED2 File Offset: 0x0001E0D2
		protected override CardInfo.Rarity GetRarity()
		{
			return RarityUtils.GetRarity("Legendary");
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x0001FEE0 File Offset: 0x0001E0E0
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = true,
					stat = "Bounces",
					amount = "+2"
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Range",
					amount = "Fixed"
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Damage",
					amount = "-25%",
					simepleAmount = 5
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

		// Token: 0x060004FB RID: 1275 RVA: 0x0001FF95 File Offset: 0x0001E195
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return 6;
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0001FF98 File Offset: 0x0001E198
		public override string GetModName()
		{
			return "CR";
		}
	}
}
