using System;
using System.Collections.Generic;
using UnboundLib.Utils;

namespace TeamComposition2
{
    public static class CardToggleManager
    {
        private static HashSet<string> enabledCardNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "Brawler",
            "Dazzle",
            "Chase",
            "Leech",
            "Chilling Presence",
            "Cold Bullets",
            "Decay",
            "Defender",
            "Echo",
            "Emp",
            "Frost slam",
            "Glass cannon",
            "Huge",
            "Implode",
            "Life stealer",
            "Overpower",
            "Parasite",
            "Pristine Perserverance",
            "Radiance",
            "Saw",
            "Shield Charge",
            "Shields up",
            "Shockwave",
            "Silence",
            "Static field",
            "Supernova",
            "Tank",
            "Taste of blood",
            "Barrage",
            "Big bullet",
            "Bouncy",
            "Abyssal Countdown",
            "Buckshot",
            "Burst",
            "Careful Planning",
            "Explosive Bullet",
            "Drill Ammo",
            "Fastball",
            "Fast Forward",
            "Grow",
            "Homing",
            "Mayhem",
            "Poison",
            "Quick shot",
            "Radar shot",
            "Riccochet",
            "Scavenger",
            "Spray",
            "Steady shot",
            "Target bounce",
            "Thruster",
            "Toxic cloud",
            "Trickster",
            "Wind up",
            "Quick Reload",
            "Refresh",
            "Teleport",
            "tactical reload"
        };

        /// <summary>
        /// Call this in Awake() to register the card toggle callback.
        /// </summary>
        public static void Initialize()
        {
            UnityEngine.Debug.Log($"[TeamComposition2] CardToggleManager.Initialize called");
            UnityEngine.Debug.Log($"[TeamComposition2] enabledCardNames has {enabledCardNames.Count} entries");
            UnityEngine.Debug.Log($"[TeamComposition2] Registering OnAllCardsLoaded callback");
            CardManager.AddAllCardsCallback(OnAllCardsLoaded);
        }

        private static void OnAllCardsLoaded(CardInfo[] allCards)
        {
            UnityEngine.Debug.Log($"[TeamComposition2] OnAllCardsLoaded callback fired with {allCards.Length} cards");
            ApplyCardToggles();
        }

        /// <summary>
        /// Applies the card toggles: disables all Vanilla cards, then enables only those in the list.
        /// Sets config values so RestoreCardToggles() will apply our settings.
        /// </summary>
        public static void ApplyCardToggles()
        {
            UnityEngine.Debug.Log($"[TeamComposition2] ApplyCardToggles called. enabledCardNames has {enabledCardNames.Count} entries");
            int enabledCount = 0;
            int disabledCount = 0;

            foreach (var cardEntry in CardManager.cards)
            {
                var card = cardEntry.Value;
                if (card.category != "Vanilla") continue;

                var cardDisplayName = card.cardInfo.cardName;
                bool shouldBeEnabled = enabledCardNames.Contains(cardDisplayName);

                // Set the config value - RestoreCardToggles() will read this
                card.config.Value = shouldBeEnabled;
                card.enabled = shouldBeEnabled;

                if (shouldBeEnabled)
                {
                    CardManager.EnableCard(card.cardInfo, false);
                    UnityEngine.Debug.Log($"[TeamComposition2] Enabling card: {cardDisplayName}");
                    enabledCount++;
                }
                else
                {
                    CardManager.DisableCard(card.cardInfo, false);
                    UnityEngine.Debug.Log($"[TeamComposition2] Disabling card: {cardDisplayName}");
                    disabledCount++;
                }
            }

            UnityEngine.Debug.Log($"[TeamComposition2] Card toggles applied: {enabledCount} enabled, {disabledCount} disabled");
        }

        /// <summary>
        /// Enable a card by its display name at runtime.
        /// </summary>
        public static bool EnableCard(string displayName)
        {
            var cardInfo = FindCardByDisplayName(displayName);
            if (cardInfo != null)
            {
                enabledCardNames.Add(displayName);
                CardManager.EnableCard(cardInfo, false);
                return true;
            }
            UnityEngine.Debug.LogWarning($"[TeamComposition2] Card not found: {displayName}");
            return false;
        }

        /// <summary>
        /// Disable a card by its display name at runtime.
        /// </summary>
        public static bool DisableCard(string displayName)
        {
            var cardInfo = FindCardByDisplayName(displayName);
            if (cardInfo != null)
            {
                enabledCardNames.Remove(displayName);
                CardManager.DisableCard(cardInfo, false);
                return true;
            }
            UnityEngine.Debug.LogWarning($"[TeamComposition2] Card not found: {displayName}");
            return false;
        }

        /// <summary>
        /// Check if a card is in the enabled list.
        /// </summary>
        public static bool IsCardInEnabledList(string displayName)
        {
            return enabledCardNames.Contains(displayName);
        }

        /// <summary>
        /// Get a copy of all currently enabled card names.
        /// </summary>
        public static HashSet<string> GetEnabledCardNames()
        {
            return new HashSet<string>(enabledCardNames, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Set the enabled cards list and apply immediately.
        /// </summary>
        public static void SetEnabledCards(IEnumerable<string> cardNames)
        {
            enabledCardNames.Clear();
            foreach (var name in cardNames)
            {
                enabledCardNames.Add(name);
            }
            ApplyCardToggles();
        }

        private static CardInfo FindCardByDisplayName(string displayName)
        {
            foreach (var cardEntry in CardManager.cards.Values)
            {
                if (string.Equals(cardEntry.cardInfo.cardName, displayName, StringComparison.OrdinalIgnoreCase))
                {
                    return cardEntry.cardInfo;
                }
            }
            return null;
        }
    }
}
