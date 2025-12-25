using TMPro;
using UnboundLib;
using UnboundLib.Utils.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TeamComposition2.GameModes
{
    /// <summary>
    /// In-game pause menu for stat sliders. Provides a flat, streamlined
    /// interface for adjusting stats during gameplay.
    /// </summary>
    public static class InGameStatMenu
    {
        private const string MenuName = "Stat Settings";
        private const float SliderMin = -10f;
        private const float SliderMax = 10f;

        public static void RegisterMenu()
        {
            // Register only for pause menu (showInPauseMenu = true)
            Unbound.RegisterMenu(MenuName, () => { }, CreateMenu, null, true);
        }

        private static void CreateMenu(GameObject menu)
        {
            MenuHandler.CreateText("<b>In-Game Stat Settings</b>", menu, out TextMeshProUGUI _, 50);
            MenuHandler.CreateText("0 = no change, +10 = 25x, -10 = 1/25x", menu, out TextMeshProUGUI _, 22, false);
            AddBlank(menu, 20);

            // Hold Time slider
            CreateHoldTimeSection(menu);
            AddBlank(menu, 25);

            // Base Stats Section
            MenuHandler.CreateText("<b>--- Base Stats ---</b>", menu, out TextMeshProUGUI _, 35);
            AddBlank(menu, 10);

            CreateStatSlider(menu, "Base Movement Speed",
                StatModifierSettings.BaseMovementSpeed.Value,
                value => StatModifierSettings.BaseMovementSpeed.Value = value);

            CreateStatSlider(menu, "Base Jump Height",
                StatModifierSettings.BaseJumpHeight.Value,
                value => StatModifierSettings.BaseJumpHeight.Value = value);

            CreateStatSlider(menu, "Base Max Health",
                StatModifierSettings.BaseMaxHealth.Value,
                value => StatModifierSettings.BaseMaxHealth.Value = value);

            AddBlank(menu, 25);

            // Tank Class Section
            MenuHandler.CreateText("<b>--- Tank Class ---</b>", menu, out TextMeshProUGUI _, 35);
            AddBlank(menu, 10);

            CreateStatSlider(menu, "Tank Health/Card",
                StatModifierSettings.TankHealthPerCard.Value,
                value => StatModifierSettings.TankHealthPerCard.Value = value);

            CreateStatSlider(menu, "Tank Move Speed/Card",
                StatModifierSettings.TankMovementSpeedPerCard.Value,
                value => StatModifierSettings.TankMovementSpeedPerCard.Value = value);

            CreateStatSlider(menu, "Tank Jump/Card",
                StatModifierSettings.TankJumpHeightPerCard.Value,
                value => StatModifierSettings.TankJumpHeightPerCard.Value = value);

            AddBlank(menu, 25);

            // Healer Class Section
            MenuHandler.CreateText("<b>--- Healer Class ---</b>", menu, out TextMeshProUGUI _, 35);
            AddBlank(menu, 10);

            CreateStatSlider(menu, "Healer Healing/Card",
                StatModifierSettings.HealerHealingPerCard.Value,
                value => StatModifierSettings.HealerHealingPerCard.Value = value);

            CreateStatSlider(menu, "Healer Move Speed/Card",
                StatModifierSettings.HealerMovementSpeedPerCard.Value,
                value => StatModifierSettings.HealerMovementSpeedPerCard.Value = value);

            AddBlank(menu, 25);

            // Attack Class Section
            MenuHandler.CreateText("<b>--- Attack Class ---</b>", menu, out TextMeshProUGUI _, 35);
            AddBlank(menu, 10);

            CreateStatSlider(menu, "Attack Damage/Card",
                StatModifierSettings.AttackDamagePerCard.Value,
                value => StatModifierSettings.AttackDamagePerCard.Value = value);

            CreateStatSlider(menu, "Attack Move Speed/Card",
                StatModifierSettings.AttackMovementSpeedPerCard.Value,
                value => StatModifierSettings.AttackMovementSpeedPerCard.Value = value);

            AddBlank(menu, 25);

            // Reset All button
            MenuHandler.CreateButton("Reset All Stats to Default", menu, () =>
            {
                StatModifierSettings.ResetAllToDefaults();
                GM_CrownControl.secondsNeededToWin = GM_CrownControl.defaultSecondsNeededToWin;
                StatModifierApplicator.RefreshAllPlayerStats();
            }, 30);
        }

        private static void CreateHoldTimeSection(GameObject menu)
        {
            float minValue = 1f;
            float maxValue = GM_CrownControl.defaultSecondsNeededToWin * 5f;

            MenuHandler.CreateSlider(
                "Seconds to Win",
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

        private static void CreateStatSlider(GameObject menu, string label, float currentValue, System.Action<float> onValueChanged)
        {
            MenuHandler.CreateSlider(
                label,
                menu,
                25,
                SliderMin,
                SliderMax,
                currentValue,
                value => UpdateStatAndRefresh(() => onValueChanged(value)),
                out Slider _
            );
        }

        private static void AddBlank(GameObject menu, int size = 30)
        {
            MenuHandler.CreateText(" ", menu, out TextMeshProUGUI _, size);
        }

        private static void UpdateStatAndRefresh(System.Action applyChange)
        {
            applyChange?.Invoke();
            StatModifierApplicator.RefreshAllPlayerStats();
        }
    }
}
