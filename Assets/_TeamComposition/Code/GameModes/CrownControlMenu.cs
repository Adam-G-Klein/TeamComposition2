using TMPro;
using UnboundLib;
using UnboundLib.Utils.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TeamComposition2.GameModes
{
    public static class CrownControlMenu
    {
        private const string MenuName = "Point Control Settings";

        public static void RegisterMenu()
        {
            Unbound.RegisterMenu(MenuName, () => { }, CreateMenu, null, true);
        }

        private static void CreateMenu(GameObject menu)
        {
            MenuHandler.CreateText("<b>Point Control Settings", menu, out TextMeshProUGUI _, 60);
            AddBlank(menu, 30);

            // Hold Time slider (existing)
            CreateHoldTimeSection(menu);
            AddBlank(menu, 30);

            // Base Stats submenu
            CreateBaseStatsMenu(menu);
            AddBlank(menu, 20);

            // Tank Class submenu
            CreateTankClassMenu(menu);
            AddBlank(menu, 20);

            // Healer Class submenu
            CreateHealerClassMenu(menu);
            AddBlank(menu, 20);

            // Attack Class submenu
            CreateAttackClassMenu(menu);
            AddBlank(menu, 30);

            // Reset All button
            MenuHandler.CreateButton("Reset All Stats to Default", menu, () =>
            {
                StatModifierSettings.ResetAllToDefaults();
                GM_CrownControl.secondsNeededToWin = GM_CrownControl.defaultSecondsNeededToWin;
            }, 30);
        }

        private static void CreateHoldTimeSection(GameObject menu)
        {
            MenuHandler.CreateText("Seconds to Win", menu, out TextMeshProUGUI _, 40);
            MenuHandler.CreateText("Time a team must hold the point to win", menu, out TextMeshProUGUI _, 25, false);
            AddBlank(menu, 10);

            float minValue = 1f;
            float maxValue = GM_CrownControl.defaultSecondsNeededToWin * 5f;

            MenuHandler.CreateSlider(
                "Hold Time (seconds)",
                menu,
                30,
                minValue,
                maxValue,
                GM_CrownControl.secondsNeededToWin,
                value => GM_CrownControl.secondsNeededToWin = value,
                out Slider _,
                wholeNumbers: true
            );
        }

        private static void CreateBaseStatsMenu(GameObject mainMenu)
        {
            GameObject baseStatsMenu = MenuHandler.CreateMenu("Base Stats", () => { }, mainMenu, 40,
                parentForMenu: mainMenu.transform.parent.gameObject);

            MenuHandler.CreateText("<b>Base Stat Modifiers", baseStatsMenu, out TextMeshProUGUI _, 60);
            MenuHandler.CreateText("Applies to all players. 0 = no change, +5 = 5x, -5 = 1/5x", baseStatsMenu, out TextMeshProUGUI _, 25, false);
            AddBlank(baseStatsMenu, 30);

            // Movement Speed
            CreateStatSlider(baseStatsMenu, "Movement Speed",
                StatModifierSettings.BaseMovementSpeed.Value,
                value => StatModifierSettings.BaseMovementSpeed.Value = value);
            AddBlank(baseStatsMenu, 20);

            // Jump Height
            CreateStatSlider(baseStatsMenu, "Jump Height",
                StatModifierSettings.BaseJumpHeight.Value,
                value => StatModifierSettings.BaseJumpHeight.Value = value);
            AddBlank(baseStatsMenu, 20);

            // Maximum Health
            CreateStatSlider(baseStatsMenu, "Maximum Health",
                StatModifierSettings.BaseMaxHealth.Value,
                value => StatModifierSettings.BaseMaxHealth.Value = value);
            AddBlank(baseStatsMenu, 30);

            // Reset button for base stats
            MenuHandler.CreateButton("Reset Base Stats", baseStatsMenu, () =>
            {
                StatModifierSettings.BaseMovementSpeed.Value = 0f;
                StatModifierSettings.BaseJumpHeight.Value = 0f;
                StatModifierSettings.BaseMaxHealth.Value = 0f;
            }, 30);
        }

        private static void CreateTankClassMenu(GameObject mainMenu)
        {
            GameObject tankMenu = MenuHandler.CreateMenu("Tank Class Stats", () => { }, mainMenu, 40,
                parentForMenu: mainMenu.transform.parent.gameObject);

            MenuHandler.CreateText("<b>Tank Class Modifiers", tankMenu, out TextMeshProUGUI _, 60);
            MenuHandler.CreateText("Per-card bonuses for Tank role cards", tankMenu, out TextMeshProUGUI _, 25, false);
            AddBlank(tankMenu, 30);

            // Health per card
            CreateClassStatSection(tankMenu, "Health Per Card",
                StatModifierSettings.TankHealthPerCard,
                StatModifierSettings.TankHealthPerCardMin,
                StatModifierSettings.TankHealthPerCardMax);
            AddBlank(tankMenu, 30);

            // Movement Speed per card
            CreateClassStatSection(tankMenu, "Movement Speed Per Card",
                StatModifierSettings.TankMovementSpeedPerCard,
                StatModifierSettings.TankMovementSpeedPerCardMin,
                StatModifierSettings.TankMovementSpeedPerCardMax);
            AddBlank(tankMenu, 30);

            // Jump Height per card
            CreateClassStatSection(tankMenu, "Jump Height Per Card",
                StatModifierSettings.TankJumpHeightPerCard,
                StatModifierSettings.TankJumpHeightPerCardMin,
                StatModifierSettings.TankJumpHeightPerCardMax);
            AddBlank(tankMenu, 30);

            // Reset button
            MenuHandler.CreateButton("Reset Tank Stats", tankMenu, () =>
            {
                StatModifierSettings.TankHealthPerCard.Value = 0f;
                StatModifierSettings.TankHealthPerCardMin.Value = -5f;
                StatModifierSettings.TankHealthPerCardMax.Value = 5f;
                StatModifierSettings.TankMovementSpeedPerCard.Value = 0f;
                StatModifierSettings.TankMovementSpeedPerCardMin.Value = -5f;
                StatModifierSettings.TankMovementSpeedPerCardMax.Value = 5f;
                StatModifierSettings.TankJumpHeightPerCard.Value = 0f;
                StatModifierSettings.TankJumpHeightPerCardMin.Value = -5f;
                StatModifierSettings.TankJumpHeightPerCardMax.Value = 5f;
            }, 30);
        }

        private static void CreateHealerClassMenu(GameObject mainMenu)
        {
            GameObject healerMenu = MenuHandler.CreateMenu("Healer Class Stats", () => { }, mainMenu, 40,
                parentForMenu: mainMenu.transform.parent.gameObject);

            MenuHandler.CreateText("<b>Healer Class Modifiers", healerMenu, out TextMeshProUGUI _, 60);
            MenuHandler.CreateText("Per-card bonuses for Healer role cards", healerMenu, out TextMeshProUGUI _, 25, false);
            AddBlank(healerMenu, 30);

            // Healing per card
            CreateClassStatSection(healerMenu, "Healing Per Card",
                StatModifierSettings.HealerHealingPerCard,
                StatModifierSettings.HealerHealingPerCardMin,
                StatModifierSettings.HealerHealingPerCardMax);
            AddBlank(healerMenu, 30);

            // Movement Speed per card
            CreateClassStatSection(healerMenu, "Movement Speed Per Card",
                StatModifierSettings.HealerMovementSpeedPerCard,
                StatModifierSettings.HealerMovementSpeedPerCardMin,
                StatModifierSettings.HealerMovementSpeedPerCardMax);
            AddBlank(healerMenu, 30);

            // Reset button
            MenuHandler.CreateButton("Reset Healer Stats", healerMenu, () =>
            {
                StatModifierSettings.HealerHealingPerCard.Value = 0f;
                StatModifierSettings.HealerHealingPerCardMin.Value = -5f;
                StatModifierSettings.HealerHealingPerCardMax.Value = 5f;
                StatModifierSettings.HealerMovementSpeedPerCard.Value = 0f;
                StatModifierSettings.HealerMovementSpeedPerCardMin.Value = -5f;
                StatModifierSettings.HealerMovementSpeedPerCardMax.Value = 5f;
            }, 30);
        }

        private static void CreateAttackClassMenu(GameObject mainMenu)
        {
            GameObject attackMenu = MenuHandler.CreateMenu("Attack Class Stats", () => { }, mainMenu, 40,
                parentForMenu: mainMenu.transform.parent.gameObject);

            MenuHandler.CreateText("<b>Attack Class Modifiers", attackMenu, out TextMeshProUGUI _, 60);
            MenuHandler.CreateText("Per-card bonuses for Attack role cards", attackMenu, out TextMeshProUGUI _, 25, false);
            AddBlank(attackMenu, 30);

            // Damage per card
            CreateClassStatSection(attackMenu, "Damage Per Card",
                StatModifierSettings.AttackDamagePerCard,
                StatModifierSettings.AttackDamagePerCardMin,
                StatModifierSettings.AttackDamagePerCardMax);
            AddBlank(attackMenu, 30);

            // Movement Speed per card
            CreateClassStatSection(attackMenu, "Movement Speed Per Card",
                StatModifierSettings.AttackMovementSpeedPerCard,
                StatModifierSettings.AttackMovementSpeedPerCardMin,
                StatModifierSettings.AttackMovementSpeedPerCardMax);
            AddBlank(attackMenu, 30);

            // Reset button
            MenuHandler.CreateButton("Reset Attack Stats", attackMenu, () =>
            {
                StatModifierSettings.AttackDamagePerCard.Value = 0f;
                StatModifierSettings.AttackDamagePerCardMin.Value = -5f;
                StatModifierSettings.AttackDamagePerCardMax.Value = 5f;
                StatModifierSettings.AttackMovementSpeedPerCard.Value = 0f;
                StatModifierSettings.AttackMovementSpeedPerCardMin.Value = -5f;
                StatModifierSettings.AttackMovementSpeedPerCardMax.Value = 5f;
            }, 30);
        }

        /// <summary>
        /// Creates a simple stat slider with range -5 to +5.
        /// </summary>
        private static void CreateStatSlider(GameObject menu, string label, float currentValue, System.Action<float> onValueChanged)
        {
            MenuHandler.CreateSlider(
                label,
                menu,
                30,
                -5f,
                5f,
                currentValue,
                value => onValueChanged(value),
                out Slider _
            );
        }

        /// <summary>
        /// Creates a class stat section with value, min, and max sliders.
        /// </summary>
        private static void CreateClassStatSection(
            GameObject menu,
            string label,
            BepInEx.Configuration.ConfigEntry<float> valueConfig,
            BepInEx.Configuration.ConfigEntry<float> minConfig,
            BepInEx.Configuration.ConfigEntry<float> maxConfig)
        {
            MenuHandler.CreateText($"<b>{label}", menu, out TextMeshProUGUI _, 35);
            AddBlank(menu, 10);

            // Value slider
            MenuHandler.CreateSlider(
                "Value",
                menu,
                30,
                -5f,
                5f,
                valueConfig.Value,
                value => valueConfig.Value = value,
                out Slider _
            );
            AddBlank(menu, 10);

            // Min slider
            MenuHandler.CreateSlider(
                "Minimum",
                menu,
                30,
                -5f,
                5f,
                minConfig.Value,
                value => minConfig.Value = value,
                out Slider _
            );
            AddBlank(menu, 10);

            // Max slider
            MenuHandler.CreateSlider(
                "Maximum",
                menu,
                30,
                -5f,
                5f,
                maxConfig.Value,
                value => maxConfig.Value = value,
                out Slider _
            );
        }

        private static void AddBlank(GameObject menu, int size = 30)
        {
            MenuHandler.CreateText(" ", menu, out TextMeshProUGUI _, size);
        }
    }
}
