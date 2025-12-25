using System;
using System.Collections.Generic;
using System.Linq;
using UnboundLib.Cards;
using UnityEngine;
using TeamComposition2;

public class MistletoeCard : CustomCard
{
    public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
    {
        gun.reloadTimeAdd = 0.5f;
    }

    public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
    {
        if (characterStats.lifeSteal == 0f)
        {
            characterStats.lifeSteal += 0.3f;
        }
        else
        {
            characterStats.lifeSteal *= 1.3f;
        }
        gun.projectileColor = new Color(0f, 1f, 1f, 1f);
        List<ObjectsToSpawn> list = gun.objectsToSpawn.ToList();
        list.Add(new ObjectsToSpawn
        {
            AddToProjectile = new GameObject("A_Mistletoe", new Type[]
            {
                typeof(MistletoeMono)
            })
        });
        gun.objectsToSpawn = list.ToArray();
    }

    public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
    {
    }

    protected override string GetTitle()
    {
        return "Christmas Cheer";
    }

    protected override string GetDescription()
    {
        return "Spread cheer to enemies you hit, causing them to heal your nearby allies";
    }

    protected override GameObject GetCardArt()
    {
        // You'll need to add the art asset to your asset bundle
        // For now, return null or use a placeholder
        return MyPlugin.asset?.LoadAsset<GameObject>("C_Mistletoe");
    }

    protected override CardInfo.Rarity GetRarity()
    {
        return CardInfo.Rarity.Rare; // 2 = Rare
    }

    protected override CardInfoStat[] GetStats()
    {
        return new CardInfoStat[]
        {
            new CardInfoStat
            {
                positive = true,
                stat = "Lifesteal",
                amount = "+30%",
                simepleAmount = CardInfoStat.SimpleAmount.Some
            },
            new CardInfoStat
            {
                positive = false,
                stat = "Reload Time",
                amount = "+0.5s",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned
            }
        };
    }

    protected override CardThemeColor.CardThemeColorType GetTheme()
    {
        return CardThemeColor.CardThemeColorType.ColdBlue; // 7 = ColdBlue
    }

    public override string GetModName()
    {
        return MyPlugin.modInitials;
    }
}

