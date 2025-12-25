using UnboundLib.Cards;
using UnityEngine;
using TeamComposition2;

/// <summary>
/// Float Like a Butterfly – lower jump height but lets you keep jumping midair without touching the ground.
/// </summary>
public class FloatLikeAButterflyCard : CustomCard
{
    // Reduce jump height so the infinite hops stay controlled
    private const float JumpHeightMultiplier = 0.7f; // -30%

    public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
    {
        statModifiers.jump *= JumpHeightMultiplier;
    }

    public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
    {
        var effect = player.gameObject.GetComponent<FloatLikeAButterflyEffect>() ?? player.gameObject.AddComponent<FloatLikeAButterflyEffect>();
        effect.SetMinTimeBetweenJumps(0.1f);
    }

    public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
    {
        var effect = player.gameObject.GetComponent<FloatLikeAButterflyEffect>();
        if (effect != null)
        {
            GameObject.Destroy(effect);
        }
    }

    protected override string GetTitle() => "Float Like a Butterfly";

    protected override string GetDescription() => "Jump height is reduced, but you can chain jumps without landing.";

    protected override GameObject GetCardArt() => MyPlugin.asset?.LoadAsset<GameObject>("C_FloatLikeAButterfly");

    protected override CardInfo.Rarity GetRarity() => CardInfo.Rarity.Uncommon;

    protected override CardInfoStat[] GetStats()
    {
        return new[]
        {
            new CardInfoStat
            {
                positive = true,
                stat = "Air control",
                amount = "Jump again without landing",
                simepleAmount = CardInfoStat.SimpleAmount.Some
            },
            new CardInfoStat
            {
                positive = false,
                stat = "Jump height",
                amount = "-30%",
                simepleAmount = CardInfoStat.SimpleAmount.Some
            }
        };
    }

    protected override CardThemeColor.CardThemeColorType GetTheme() => CardThemeColor.CardThemeColorType.TechWhite;

    public override string GetModName() => MyPlugin.modInitials;
}

/// <summary>
/// Keeps at least one jump available while airborne so the player never needs to touch ground to jump again.
/// </summary>
public class FloatLikeAButterflyEffect : MonoBehaviour
{
    private CharacterData data;
    private float minTimeBetweenJumps = 0.1f;

    private void Awake()
    {
        data = GetComponent<CharacterData>();
    }

    public void SetMinTimeBetweenJumps(float minTime)
    {
        minTimeBetweenJumps = Mathf.Max(0f, minTime);
    }

    private void Update()
    {
        if (data == null)
        {
            Destroy(this);
            return;
        }

        // Only intervene while airborne; grounded/wall states already restore jumps normally.
        if (data.isGrounded || data.isWallGrab)
        {
            return;
        }

        if (data.currentJumps <= 0 && data.sinceJump >= minTimeBetweenJumps)
        {
            data.currentJumps = Mathf.Max(1, data.currentJumps);
        }
    }
}


