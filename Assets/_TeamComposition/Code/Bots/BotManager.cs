using BepInEx;
using BepInEx.Configuration;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using HarmonyLib;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TeamComposition2.Bots.UI;
using TeamComposition2.Bots.Utils;
using TeamComposition2.Patches;
using UnboundLib;
using UnboundLib.Networking;
using UnboundLib.Utils;
using UnityEngine;

namespace TeamComposition2.Bots
{
    public class BotManager : MonoBehaviour
    {
        private const string ModId = "com.adamklein.teamcomposition.bots";

        public static BotManager Instance { get; private set; }
        internal static List<BaseUnityPlugin> Plugins { get; private set; }
        internal static AssetBundle Assets { get; private set; }

        public static void Initialize(ConfigFile config, AssetBundle assets = null)
        {
            Assets = assets;

            // Create the manager GameObject
            var managerObject = new GameObject("TC2_BotManager");
            Instance = managerObject.AddComponent<BotManager>();
            DontDestroyOnLoad(managerObject);

            // Apply Harmony patches
            var harmony = new Harmony(ModId);
            harmony.PatchAll(typeof(Patches.PlayerAssignerPatch));
            harmony.PatchAll(typeof(Patches.PlayerAIPhilipPatch));
            harmony.PatchAll(typeof(Patches.MapPatch));
            harmony.PatchAll(typeof(Patches.CharacterSelectionInstancePatch));
            harmony.PatchAll(typeof(Patches.KeybindHintsPatch));
            harmony.PatchAll(typeof(Patches.RWFAddSpotToPlayerPatch));
            harmony.PatchAll(typeof(HealingFieldTaggerPatch));
        }

        public static void RegisterMenuAndHandshake(ConfigFile config, AssetBundle assets = null)
        {
            if (assets != null)
            {
                Assets = assets;
            }

            Plugins = (List<BaseUnityPlugin>)typeof(BepInEx.Bootstrap.Chainloader).GetField("_plugins", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);

            BotMenu.RegisterMenu(config);

            Unbound.RegisterHandshake(ModId, OnHandShakeCompleted);

            // Load BotMenu prefab from asset bundle
            if (Assets != null)
            {
                var botMenuPrefab = Assets.LoadAsset<GameObject>("BotMenu");
                if (botMenuPrefab != null)
                {
                    BotMenuUIHandler.SetPrefab(botMenuPrefab);
                    Debug.Log("[TC2-Bots] BotMenu prefab loaded from asset bundle");
                }
                else
                {
                    Debug.LogWarning("[TC2-Bots] BotMenu prefab not found in asset bundle");
                }
            }

            // Add card validation to exclude certain cards from bots
            ModdingUtils.Utils.Cards.instance.AddCardValidationFunction((player, card) =>
            {
                if (player.GetComponent<PlayerAPI>().enabled
                   && card.blacklistedCategories.Contains(CustomCardCategories.instance.CardCategory("NotForBots"))
                ) return false;

                return true;
            });

            // Exclude certain cards from bots
            CardExclusiveUtils.ExcludeCardsFromBots(CardManager.GetCardInfoWithName("Remote"));
            CardExclusiveUtils.ExcludeCardsFromBots(CardManager.GetCardInfoWithName("Teleport"));
            CardExclusiveUtils.ExcludeCardsFromBots(CardManager.GetCardInfoWithName("Shield Charge"));

            // Create the BotAIManager
            BotAIManager.Instance = new GameObject("TC2_BotAIManager").AddComponent<BotAIManager>();
            DontDestroyOnLoad(BotAIManager.Instance.gameObject);
        }

        private static void OnHandShakeCompleted()
        {
            if (PhotonNetwork.IsMasterClient)
            {
                NetworkingManager.RPC_Others(
                    typeof(BotManager),
                    nameof(RPCA_SyncSettings),

                    BotMenu.StalemateTimer.Value,
                    BotMenu.StalemateDamageCooldown.Value,
                    BotMenu.StalemateDamageDuration.Value,

                    BotMenu.CycleDelay.Value,
                    BotMenu.PreCycleDelay.Value,
                    BotMenu.GoToCardDelay.Value,
                    BotMenu.PickDelay.Value
                );
            }
        }

        [UnboundRPC]
        private static void RPCA_SyncSettings(
            float stalemateTimer,
            float stalemateDamageCooldown,
            float stalemateDamageDuration,
            float cycleDelay,
            float preCycleDelay,
            float goToCardDelay,
            float pickDelay
        )
        {
            BotMenu.StalemateTimer.Value = stalemateTimer;
            BotMenu.StalemateDamageCooldown.Value = stalemateDamageCooldown;
            BotMenu.StalemateDamageDuration.Value = stalemateDamageDuration;

            BotMenu.CycleDelay.Value = cycleDelay;
            BotMenu.PreCycleDelay.Value = preCycleDelay;
            BotMenu.GoToCardDelay.Value = goToCardDelay;
            BotMenu.PickDelay.Value = pickDelay;
        }
    }
}
