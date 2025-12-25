using UnboundLib.Cards;
using UnityEngine;
using TeamComposition2;

/// <summary>
/// Empty Zen – reloading creates a weak healing field at the player's position.
/// The field heals for 50% of the standard Healing Field card's healing.
/// </summary>
public class EmptyZenCard : CustomCard
{
    public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
    {
        // Slightly slower reload to pay for the free heal field
        gun.reloadTimeAdd = 0.25f;
    }

    public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
    {
        var effect = player.gameObject.GetComponent<EmptyZenEffect>() ?? player.gameObject.AddComponent<EmptyZenEffect>();
        effect.Initialize(player, gun, characterStats);
    }

    public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
    {
        var effect = player.gameObject.GetComponent<EmptyZenEffect>();
        if (effect != null)
        {
            GameObject.Destroy(effect);
        }
    }

    protected override string GetTitle() => "Empty Zen";

    protected override string GetDescription() => "Reloading creates a weak healing field at your position (50% of Healing Field's healing).";

    protected override GameObject GetCardArt() => MyPlugin.asset?.LoadAsset<GameObject>("C_HealingField");

    protected override CardInfo.Rarity GetRarity() => CardInfo.Rarity.Uncommon;

    protected override CardInfoStat[] GetStats()
    {
        return new CardInfoStat[]
        {
            new CardInfoStat
            {
                positive = true,
                stat = "On reload",
                amount = "Creates weak healing field (50%)",
                simepleAmount = CardInfoStat.SimpleAmount.Some
            },
            new CardInfoStat
            {
                positive = false,
                stat = "Reload time",
                amount = "+0.25s",
                simepleAmount = CardInfoStat.SimpleAmount.Some
            }
        };
    }

    protected override CardThemeColor.CardThemeColorType GetTheme() => CardThemeColor.CardThemeColorType.ColdBlue;

    public override string GetModName() => MyPlugin.modInitials;
}

/// <summary>
/// Handles spawning the weakened healing field when the player reloads.
/// </summary>
public class EmptyZenEffect : MonoBehaviour
{
    private const float healMultiplier = 0.5f; // 50% of the standard Healing Field heal amount

    private Player player;
    private Gun gun;
    private CharacterStatModifiers stats;
    private GameObject fieldPrefab;

    public void Initialize(Player owner, Gun ownerGun, CharacterStatModifiers ownerStats)
    {
        player = owner;
        gun = ownerGun;
        stats = ownerStats;

        // Ensure we don't double-subscribe if the component already exists
        stats.OnReloadDoneAction -= OnReload;
        stats.OnReloadDoneAction += OnReload;

        fieldPrefab = MyPlugin.asset?.LoadAsset<GameObject>("E_HealingCircle");
    }

    private void OnDisable()
    {
        if (stats != null)
        {
            stats.OnReloadDoneAction -= OnReload;
        }
    }

    private void OnDestroy()
    {
        if (stats != null)
        {
            stats.OnReloadDoneAction -= OnReload;
        }
    }

    private void OnReload(int _)
    {
        if (player == null)
        {
            return;
        }

        SpawnHealingField();
    }

    private void SpawnHealingField()
    {
        if (fieldPrefab == null)
        {
            fieldPrefab = MyPlugin.asset?.LoadAsset<GameObject>("E_HealingCircle");
            if (fieldPrefab == null)
            {
                UnityEngine.Debug.LogWarning("[EmptyZen] Could not load E_HealingCircle prefab from asset bundle.");
                return;
            }
        }

        GameObject instance = Instantiate(fieldPrefab, player.transform.position, Quaternion.identity);
        instance.name = "EmptyZen_HealingField";

        // Attach ownership for healing effectiveness and AI tracking
        var spawnedAttack = instance.GetComponent<SpawnedAttack>() ?? instance.AddComponent<SpawnedAttack>();
        spawnedAttack.spawner = player;

        // Tag for bot navigation
        var marker = instance.GetComponent<HealingFieldTeamMarker>() ?? instance.AddComponent<HealingFieldTeamMarker>();
        marker.Initialize(player);

        // Reduce heal strength to 50% of the baseline Healing Field
        var explosion = instance.GetComponentInChildren<Explosion>();
        if (explosion != null)
        {
            explosion.damage *= healMultiplier;
        }
    }
}

