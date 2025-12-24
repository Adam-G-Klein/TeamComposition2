using System;
using System.Collections.Generic;
using TeamComposition2.CardRoles;
using UnboundLib.Utils;

namespace TeamComposition2
{
    public static class CardToggleManager
    {

        /// <summary>
        /// Call this in Awake() to register the card toggle callback.
        /// </summary>
        public static void Initialize()
        {
            UnityEngine.Debug.Log($"[TeamComposition2] CardToggleManager.Initialize called");
            UnityEngine.Debug.Log($"[TeamComposition2] Registering OnAllCardsLoaded callback");
            CardManager.AddAllCardsCallback(OnAllCardsLoaded);
        }

        private static void OnAllCardsLoaded(CardInfo[] allCards)
        {
            UnityEngine.Debug.Log($"[TeamComposition2] OnAllCardsLoaded callback fired with {allCards.Length} cards");
            ApplyCardToggles();
        }

        /// <summary>
        /// Applies the card toggles based on CardRoleManager.
        /// Cards not in the role map or with the Disabled role are disabled.
        /// </summary>
        public static void ApplyCardToggles()
        {
            UnityEngine.Debug.Log($"[TeamComposition2] ApplyCardToggles called");
            int enabledCount = 0;
            int disabledCount = 0;

            foreach (var cardEntry in CardManager.cards)
            {
                var card = cardEntry.Value;

                var cardDisplayName = card.cardInfo.cardName;
                bool shouldBeEnabled = CardRoleManager.IsCardEnabled(cardDisplayName);

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
        /// Sets the card role to None if it was Disabled or not in the map.
        /// </summary>
        public static bool EnableCard(string displayName)
        {
            var cardInfo = FindCardByDisplayName(displayName);
            if (cardInfo != null)
            {
                // If card is disabled or not in map, set to None to enable it
                var currentRole = CardRoleManager.GetCardRole(displayName);
                if (currentRole == CardRole.Disabled || !CardRoleManager.IsCardInRoleMap(displayName))
                {
                    CardRoleManager.SetCardRole(displayName, CardRole.None);
                }
                CardManager.EnableCard(cardInfo, false);
                return true;
            }
            UnityEngine.Debug.LogWarning($"[TeamComposition2] Card not found: {displayName}");
            return false;
        }

        /// <summary>
        /// Disable a card by its display name at runtime.
        /// Sets the card role to Disabled.
        /// </summary>
        public static bool DisableCard(string displayName)
        {
            var cardInfo = FindCardByDisplayName(displayName);
            if (cardInfo != null)
            {
                CardRoleManager.SetCardRole(displayName, CardRole.Disabled);
                CardManager.DisableCard(cardInfo, false);
                return true;
            }
            UnityEngine.Debug.LogWarning($"[TeamComposition2] Card not found: {displayName}");
            return false;
        }

        /// <summary>
        /// Check if a card is enabled (in the role map and not disabled).
        /// </summary>
        public static bool IsCardInEnabledList(string displayName)
        {
            return CardRoleManager.IsCardEnabled(displayName);
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
