using BepInEx.Configuration;
using Photon.Pun;
using TMPro;
using TeamComposition2.Bots.Extensions;
using UnboundLib;
using UnboundLib.Utils.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TeamComposition2.Bots
{
    public static class BotMenu
    {
        private const string MenuName = "Rounds With Bots";

        public static ConfigEntry<bool> DebugMode;
        public static ConfigEntry<bool> RandomizationFace;
        public static ConfigEntry<int> SelectedFace;
        public static ConfigEntry<float> StalemateTimer;
        public static ConfigEntry<float> StalemateDamageCooldown;
        public static ConfigEntry<float> StalemateDamageDuration;
        public static ConfigEntry<float> CycleDelay;
        public static ConfigEntry<float> PreCycleDelay;
        public static ConfigEntry<float> GoToCardDelay;
        public static ConfigEntry<float> PickDelay;
        public static ConfigEntry<bool> PeacefulBots;

        public static GameObject SelectedFaceObject;

        public static void RegisterMenu(ConfigFile config)
        {
            Unbound.RegisterMenu(MenuName, () => { }, CreateBotMenu, null, true);

            DebugMode = config.Bind(MenuName, "DebugMode", false, "Enable or disable debug mode for additional logging and debugging features.");
            RandomizationFace = config.Bind(MenuName, "RandomizationFace", true, "Enable or disable randomization of bot faces.");
            SelectedFace = config.Bind(MenuName, "SelectedFace", 0, "Select a specific bot face when Randomize Bot Faces is disabled.");

            StalemateTimer = config.Bind(MenuName, "StalemateTimer", 10f, "The time in seconds before a stalemate is declared.");
            StalemateDamageCooldown = config.Bind(MenuName, "StalemateDamageCooldown", 1f, "The time in seconds before a player can take damage again after a stalemate.");
            StalemateDamageDuration = config.Bind(MenuName, "StalemateDamageDuration", 10f, "The time in seconds that a player takes damage after a stalemate.");

            CycleDelay = config.Bind(MenuName, "CycleDelay", 0.3f, "The delay between cycling through cards.");
            PreCycleDelay = config.Bind(MenuName, "PreCycleDelay", 1f, "The delay before cycling through cards.");
            GoToCardDelay = config.Bind(MenuName, "GoToCardDelay", 0.2f, "The delay between going to a specific card.");
            PickDelay = config.Bind(MenuName, "PickDelay", 0.5f, "The delay before picking a card.");

            PeacefulBots = config.Bind(MenuName, "PeacefulBots", false, "When enabled, bots will not fire at players.");
        }

        private static void CreateBotMenu(GameObject mainMenu)
        {
            bool isPauseMenu = IsPauseMenu(mainMenu);

            MenuHandler.CreateText("<b>Rounds With Bots", mainMenu, out TextMeshProUGUI _, 70);
            AddBlank(mainMenu, 50);

            if (isPauseMenu)
            {
                MenuHandler.CreateToggle(PeacefulBots.Value, "<#90EE90>Peaceful Bots", mainMenu, value => PeacefulBots.Value = value, 30);
                AddBlank(mainMenu, 15);

                var oneHealthButton = MenuHandler.CreateButton("Set Bot Health To 1 HP", mainMenu, null, 35);
                oneHealthButton.GetComponent<Button>().onClick.AddListener(LowerBotsToOneHealth);
                AddBlank(mainMenu, 20);
            }
            else
            {
                MenuHandler.CreateText("Peaceful Bots can now be toggled from the in-game pause menu.", mainMenu, out TextMeshProUGUI _, 30, false);
                AddBlank(mainMenu, 20);
            }

            CreateDetailsMenu(mainMenu);
            AddBlank(mainMenu, 20);

            CreateStalemateMenu(mainMenu);
            AddBlank(mainMenu, 20);

            CreateCardsPickerMenu(mainMenu);
            AddBlank(mainMenu, 20);

            MenuHandler.CreateToggle(DebugMode.Value, "<#c41010>Debug Mode", mainMenu, value => DebugMode.Value = value, 30);
        }

        private static void CreateDetailsMenu(GameObject mainMenu)
        {
            GameObject facesMenu = MenuHandler.CreateMenu("Bot Details", () => { }, mainMenu, 40, parentForMenu: mainMenu.transform.parent.gameObject);

            MenuHandler.CreateText("<b>Rounds With Bots | Bot Details", facesMenu, out TextMeshProUGUI _, 70);
            AddBlank(facesMenu, 50);

            MenuHandler.CreateToggle(RandomizationFace.Value, "Randomize Bot Faces", facesMenu, (value) =>
            {
                RandomizationFace.Value = value;
                SelectedFaceObject.SetActive(!value);
            }, 30);

            AddBlank(facesMenu, 20);

            SelectedFaceObject = MenuHandler.CreateSlider("Selected Bot Face", facesMenu, 30, 0, 7, SelectedFace.Value, value => SelectedFace.Value = (int)value, out UnityEngine.UI.Slider _, true);
            SelectedFaceObject.SetActive(!RandomizationFace.Value);
        }

        private static void CreateStalemateMenu(GameObject mainMenu)
        {
            GameObject stalemateMenu = MenuHandler.CreateMenu("Stalemate Options", () => { }, mainMenu, 40, parentForMenu: mainMenu.transform.parent.gameObject);

            MenuHandler.CreateText("<b>Rounds With Bots | Stalemate Options", stalemateMenu, out TextMeshProUGUI _, 70);
            AddBlank(stalemateMenu, 50);

            MenuHandler.CreateSlider("Stalemate Timer", stalemateMenu, 30, 0, 60, StalemateTimer.Value, value => StalemateTimer.Value = value, out UnityEngine.UI.Slider _);
            AddBlank(stalemateMenu, 20);

            MenuHandler.CreateSlider("Stalemate Damage Cooldown", stalemateMenu, 30, 0, 60, StalemateDamageCooldown.Value, value => StalemateDamageCooldown.Value = value, out UnityEngine.UI.Slider _);
            AddBlank(stalemateMenu, 20);

            MenuHandler.CreateSlider("Stalemate Damage Duration", stalemateMenu, 30, 0, 60, StalemateDamageDuration.Value, value => StalemateDamageDuration.Value = value, out UnityEngine.UI.Slider _);
        }

        private static void CreateCardsPickerMenu(GameObject mainMenu)
        {
            GameObject cardsPickerMenu = MenuHandler.CreateMenu("Cards Picker", () => { }, mainMenu, 40, parentForMenu: mainMenu.transform.parent.gameObject);

            MenuHandler.CreateText("<b>Rounds With Bots | Cards Picker", cardsPickerMenu, out TextMeshProUGUI _, 70);
            AddBlank(cardsPickerMenu, 50);

            MenuHandler.CreateSlider("Cycle Delay", cardsPickerMenu, 30, 0, 1, CycleDelay.Value, value => CycleDelay.Value = value, out UnityEngine.UI.Slider _);
            AddBlank(cardsPickerMenu, 20);

            MenuHandler.CreateSlider("Pre-Cycle Delay", cardsPickerMenu, 30, 0, 5, PreCycleDelay.Value, value => PreCycleDelay.Value = value, out UnityEngine.UI.Slider _);
            AddBlank(cardsPickerMenu, 20);

            MenuHandler.CreateSlider("Go To Card Delay", cardsPickerMenu, 30, 0, 1, GoToCardDelay.Value, value => GoToCardDelay.Value = value, out UnityEngine.UI.Slider _);
            AddBlank(cardsPickerMenu, 20);

            MenuHandler.CreateSlider("Pick Delay", cardsPickerMenu, 30, 0, 5, PickDelay.Value, value => PickDelay.Value = value, out UnityEngine.UI.Slider _);
        }

        private static void AddBlank(GameObject menu, int size = 30)
        {
            MenuHandler.CreateText(" ", menu, out TextMeshProUGUI _, size);
        }

        private static bool IsPauseMenu(GameObject menu)
        {
            var escapeMenu = UIHandler.instance?.transform.Find("Canvas/EscapeMenu");
            return escapeMenu != null && menu != null && menu.transform.IsChildOf(escapeMenu);
        }

        private static void LowerBotsToOneHealth()
        {
            if (!PhotonNetwork.IsMasterClient && !PhotonNetwork.OfflineMode)
            {
                Unbound.BuildInfoPopup("Only the host can lower bot health mid-game.");
                return;
            }

            if (PlayerManager.instance?.players == null)
            {
                Unbound.BuildInfoPopup("No players found to adjust.");
                return;
            }

            int adjusted = 0;
            foreach (var player in PlayerManager.instance.players)
            {
                if (player == null || player.data == null || !player.data.GetAdditionalData().IsBot)
                {
                    continue;
                }

                float damageToApply = Mathf.Max(0f, player.data.health - 1f);
                if (damageToApply <= 0f)
                {
                    continue;
                }

                player.data.healthHandler.CallTakeDamage(Vector2.down * damageToApply, player.transform.position, null, null, true);
                adjusted++;
            }

            string message = adjusted > 0
                ? $"Lowered {adjusted} bot(s) to 1 HP for healing tests."
                : "No bots needed adjustment (already at or below 1 HP).";

            Unbound.BuildInfoPopup(message);
        }
    }
}
