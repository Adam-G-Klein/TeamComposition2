using BepInEx.Configuration;
using UnityEngine;

namespace TeamComposition2.GameModes
{
    /// <summary>
    /// Holds all stat modifier settings for the Point Control game mode.
    /// Slider values range from -10 to +10 where:
    /// - 0 = no change (multiplier of 1)
    /// - +10 = multiply stat by 25
    /// - -10 = divide stat by 25
    /// </summary>
    public static class StatModifierSettings
    {
        private const string MenuName = "Point Control Settings";

        // ============================================
        // BASE STAT MODIFIERS
        // ============================================
        public static ConfigEntry<float> BaseMovementSpeed;
        public static ConfigEntry<float> BaseJumpHeight;
        public static ConfigEntry<float> BaseMaxHealth;
        public static ConfigEntry<float> BaseDamage;
        public static ConfigEntry<float> BaseHealing;

        // ============================================
        // TANK CLASS MODIFIERS (per card)
        // ============================================
        public static ConfigEntry<float> TankHealthPerCard;
        public static ConfigEntry<float> TankHealthPerCardMin;
        public static ConfigEntry<float> TankHealthPerCardMax;

        public static ConfigEntry<float> TankMovementSpeedPerCard;
        public static ConfigEntry<float> TankMovementSpeedPerCardMin;
        public static ConfigEntry<float> TankMovementSpeedPerCardMax;

        public static ConfigEntry<float> TankJumpHeightPerCard;
        public static ConfigEntry<float> TankJumpHeightPerCardMin;
        public static ConfigEntry<float> TankJumpHeightPerCardMax;

        // ============================================
        // HEALER CLASS MODIFIERS (per card)
        // ============================================
        public static ConfigEntry<float> HealerHealingPerCard;
        public static ConfigEntry<float> HealerHealingPerCardMin;
        public static ConfigEntry<float> HealerHealingPerCardMax;

        public static ConfigEntry<float> HealerMovementSpeedPerCard;
        public static ConfigEntry<float> HealerMovementSpeedPerCardMin;
        public static ConfigEntry<float> HealerMovementSpeedPerCardMax;

        // ============================================
        // ATTACK CLASS MODIFIERS (per card)
        // ============================================
        public static ConfigEntry<float> AttackDamagePerCard;
        public static ConfigEntry<float> AttackDamagePerCardMin;
        public static ConfigEntry<float> AttackDamagePerCardMax;

        public static ConfigEntry<float> AttackMovementSpeedPerCard;
        public static ConfigEntry<float> AttackMovementSpeedPerCardMin;
        public static ConfigEntry<float> AttackMovementSpeedPerCardMax;

        /// <summary>
        /// Initializes all config entries. Call this from MyPlugin.Awake().
        /// </summary>
        public static void Initialize(ConfigFile config)
        {
            // Base Stats
            BaseMovementSpeed = config.Bind(MenuName, "BaseMovementSpeed", 0f,
                "Base movement speed modifier. 0 = no change, +10 = 25x, -10 = 1/25x");
            BaseJumpHeight = config.Bind(MenuName, "BaseJumpHeight", 0f,
                "Base jump height modifier. 0 = no change, +10 = 25x, -10 = 1/25x");
            BaseMaxHealth = config.Bind(MenuName, "BaseMaxHealth", 0f,
                "Base max health modifier. 0 = no change, +10 = 25x, -10 = 1/25x");
            BaseDamage = config.Bind(MenuName, "BaseDamage", 0f,
                "Base damage modifier applied to all players. 0 = no change, +10 = 25x, -10 = 1/25x");
            BaseHealing = config.Bind(MenuName, "BaseHealing", 0f,
                "Base healing modifier applied to all players. 0 = no change, +10 = 25x, -10 = 1/25x");

            // Tank Class
            TankHealthPerCard = config.Bind(MenuName, "TankHealthPerCard", 0f,
                "Tank health multiplier per tank card. 0 = no change, +10 = 25x, -10 = 1/25x");
            TankHealthPerCardMin = config.Bind(MenuName, "TankHealthPerCardMin", -10f,
                "Minimum value for tank health multiplier");
            TankHealthPerCardMax = config.Bind(MenuName, "TankHealthPerCardMax", 10f,
                "Maximum value for tank health multiplier");

            TankMovementSpeedPerCard = config.Bind(MenuName, "TankMovementSpeedPerCard", 0f,
                "Tank movement speed multiplier per tank card");
            TankMovementSpeedPerCardMin = config.Bind(MenuName, "TankMovementSpeedPerCardMin", -10f,
                "Minimum value for tank movement speed multiplier");
            TankMovementSpeedPerCardMax = config.Bind(MenuName, "TankMovementSpeedPerCardMax", 10f,
                "Maximum value for tank movement speed multiplier");

            TankJumpHeightPerCard = config.Bind(MenuName, "TankJumpHeightPerCard", 0f,
                "Tank jump height multiplier per tank card");
            TankJumpHeightPerCardMin = config.Bind(MenuName, "TankJumpHeightPerCardMin", -10f,
                "Minimum value for tank jump height multiplier");
            TankJumpHeightPerCardMax = config.Bind(MenuName, "TankJumpHeightPerCardMax", 10f,
                "Maximum value for tank jump height multiplier");

            // Healer Class
            HealerHealingPerCard = config.Bind(MenuName, "HealerHealingPerCard", 0f,
                "Healer healing multiplier per healer card");
            HealerHealingPerCardMin = config.Bind(MenuName, "HealerHealingPerCardMin", -10f,
                "Minimum value for healer healing multiplier");
            HealerHealingPerCardMax = config.Bind(MenuName, "HealerHealingPerCardMax", 10f,
                "Maximum value for healer healing multiplier");

            HealerMovementSpeedPerCard = config.Bind(MenuName, "HealerMovementSpeedPerCard", 0f,
                "Healer movement speed multiplier per healer card");
            HealerMovementSpeedPerCardMin = config.Bind(MenuName, "HealerMovementSpeedPerCardMin", -10f,
                "Minimum value for healer movement speed multiplier");
            HealerMovementSpeedPerCardMax = config.Bind(MenuName, "HealerMovementSpeedPerCardMax", 10f,
                "Maximum value for healer movement speed multiplier");

            // Attack Class
            AttackDamagePerCard = config.Bind(MenuName, "AttackDamagePerCard", 0f,
                "Attack damage multiplier per attack card");
            AttackDamagePerCardMin = config.Bind(MenuName, "AttackDamagePerCardMin", -10f,
                "Minimum value for attack damage multiplier");
            AttackDamagePerCardMax = config.Bind(MenuName, "AttackDamagePerCardMax", 10f,
                "Maximum value for attack damage multiplier");

            AttackMovementSpeedPerCard = config.Bind(MenuName, "AttackMovementSpeedPerCard", 0f,
                "Attack movement speed multiplier per attack card");
            AttackMovementSpeedPerCardMin = config.Bind(MenuName, "AttackMovementSpeedPerCardMin", -10f,
                "Minimum value for attack movement speed multiplier");
            AttackMovementSpeedPerCardMax = config.Bind(MenuName, "AttackMovementSpeedPerCardMax", 10f,
                "Maximum value for attack movement speed multiplier");
        }

        /// <summary>
        /// Converts a slider value (-10 to +10) to a multiplier.
        /// 0 = 1x (no change), +10 = 25x, -10 = 0.04x (1/25)
        /// Uses exponential scaling: 5^(value/5)
        /// </summary>
        public static float SliderValueToMultiplier(float sliderValue)
        {
            return Mathf.Pow(5f, sliderValue / 5f);
        }

        /// <summary>
        /// Converts a multiplier back to a slider value.
        /// </summary>
        public static float MultiplierToSliderValue(float multiplier)
        {
            if (multiplier <= 0f) return -10f;
            return 5f * Mathf.Log(multiplier) / Mathf.Log(5f);
        }

        /// <summary>
        /// Gets a formatted display string for a slider value showing the effective multiplier.
        /// </summary>
        public static string GetMultiplierDisplayString(float sliderValue)
        {
            float multiplier = SliderValueToMultiplier(sliderValue);
            if (Mathf.Approximately(sliderValue, 0f))
                return "1x (no change)";
            else if (sliderValue > 0)
                return $"{multiplier:F2}x";
            else
                return $"{multiplier:F2}x (1/{1f / multiplier:F1})";
        }

        /// <summary>
        /// Resets all settings to their default values.
        /// </summary>
        public static void ResetAllToDefaults()
        {
            // Base Stats
            BaseMovementSpeed.Value = 0f;
            BaseJumpHeight.Value = 0f;
            BaseMaxHealth.Value = 0f;
            BaseDamage.Value = 0f;
            BaseHealing.Value = 0f;

            // Tank Class
            TankHealthPerCard.Value = 0f;
            TankHealthPerCardMin.Value = -10f;
            TankHealthPerCardMax.Value = 10f;

            TankMovementSpeedPerCard.Value = 0f;
            TankMovementSpeedPerCardMin.Value = -10f;
            TankMovementSpeedPerCardMax.Value = 10f;

            TankJumpHeightPerCard.Value = 0f;
            TankJumpHeightPerCardMin.Value = -10f;
            TankJumpHeightPerCardMax.Value = 10f;

            // Healer Class
            HealerHealingPerCard.Value = 0f;
            HealerHealingPerCardMin.Value = -10f;
            HealerHealingPerCardMax.Value = 10f;

            HealerMovementSpeedPerCard.Value = 0f;
            HealerMovementSpeedPerCardMin.Value = -10f;
            HealerMovementSpeedPerCardMax.Value = 10f;

            // Attack Class
            AttackDamagePerCard.Value = 0f;
            AttackDamagePerCardMin.Value = -10f;
            AttackDamagePerCardMax.Value = 10f;

            AttackMovementSpeedPerCard.Value = 0f;
            AttackMovementSpeedPerCardMin.Value = -10f;
            AttackMovementSpeedPerCardMax.Value = 10f;
        }
    }
}
