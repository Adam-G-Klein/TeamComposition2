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
                out Slider holdTimeSlider,
                wholeNumbers: true
            );

            AddBlank(menu, 20);

            var resetButton = MenuHandler.CreateButton("Reset to Default", menu, () =>
            {
                GM_CrownControl.secondsNeededToWin = GM_CrownControl.defaultSecondsNeededToWin;
                holdTimeSlider.value = GM_CrownControl.defaultSecondsNeededToWin;
            }, 30);
        }

        private static void AddBlank(GameObject menu, int size = 30)
        {
            MenuHandler.CreateText(" ", menu, out TextMeshProUGUI _, size);
        }
    }
}
