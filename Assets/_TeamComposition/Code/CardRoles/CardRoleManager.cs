using System;
using System.Collections.Generic;

namespace TeamComposition2.CardRoles
{
    /// <summary>
    /// Manages card role assignments. Cards are identified by their display name (cardName).
    /// Use this to define which cards belong to which roles.
    /// </summary>
    public static class CardRoleManager
    {
        /// <summary>
        /// Static mapping of card display names to their roles.
        /// Add entries here to categorize cards.
        /// Each card has exactly one role.
        /// Cards NOT in this map will be disabled. Cards with the Disabled role will also be disabled.
        /// </summary>
        private static readonly Dictionary<string, CardRole> cardRoles = new Dictionary<string, CardRole>(StringComparer.OrdinalIgnoreCase)
        {
            // ============================================
            // TANK CARDS - Defensive/survivability focused
            // ============================================
            /*
            { "Defender", CardRole.Tank },
            { "Huge", CardRole.Tank },
            { "Shields up", CardRole.Tank },
            { "Shield Charge", CardRole.Tank },
            { "Tank", CardRole.Tank },
            */
            { "Pristine Perserverance", CardRole.Tank },

            // ============================================
            // ATK CARDS - Offensive/damage focused
            // ============================================
            /*
            { "Brawler", CardRole.Atk },
            { "Glass cannon", CardRole.Atk },
            { "Big bullet", CardRole.Atk },
            { "Buckshot", CardRole.Atk },
            { "Burst", CardRole.Atk },
            { "Explosive Bullet", CardRole.Atk },
            { "Drill Ammo", CardRole.Atk },
            { "Fastball", CardRole.Atk },
            { "Homing", CardRole.Atk },
            { "Poison", CardRole.Atk },
            { "Quick shot", CardRole.Atk },
            { "Spray", CardRole.Atk },
            { "Barrage", CardRole.Atk },
            { "Bouncy", CardRole.Atk },
            { "Riccochet", CardRole.Atk },
            { "Target bounce", CardRole.Atk },
            { "Wind up", CardRole.Atk },
            { "Overpower", CardRole.Atk },
            { "Saw", CardRole.Atk },
            { "Mayhem", CardRole.Atk },
            { "Toxic cloud", CardRole.Atk },
            { "Supernova", CardRole.Atk },
            { "Implode", CardRole.Atk },
            { "Emp", CardRole.Atk },
            { "Echo", CardRole.Atk },
            { "Dazzle", CardRole.Atk },
            { "Cold Bullets", CardRole.Atk },
            { "Frost slam", CardRole.Atk },
            { "Shockwave", CardRole.Atk },
            { "Silence", CardRole.Atk },
            { "Static field", CardRole.Atk },
            { "Decay", CardRole.Atk },
            { "Abyssal Countdown", CardRole.Atk },
            { "Radar shot", CardRole.Atk },
            */
            { "Steady shot", CardRole.Atk },

            // ============================================
            // HEAL CARDS - Healing/regeneration focused
            // ============================================
            /*
            { "Leech", CardRole.Heal },
            { "Life stealer", CardRole.Heal },
            { "Parasite", CardRole.Heal },
            { "Taste of blood", CardRole.Heal },
            { "Radiance", CardRole.Heal },
            { "Refresh", CardRole.Heal },
            { "Christmas Cheer", CardRole.Heal },
            { "Scavenger", CardRole.Heal },
            */
            { "Healing Field", CardRole.Heal },
            { "Phoenix", CardRole.Heal },

            // ============================================
            // ADDITIONAL ATK CARDS
            // ============================================
            /*
            { "Chilling Presence", CardRole.Atk },
            { "Chase", CardRole.Atk },

            // ============================================
            // UTILITY CARDS - No specific combat role
            // ============================================
            { "Teleport", CardRole.None },
            { "Trickster", CardRole.None },
            { "Thruster", CardRole.None },
            { "Careful Planning", CardRole.None },
            { "Fast Forward", CardRole.None },
            { "Grow", CardRole.None },
            { "Quick Reload", CardRole.None },
            */
            { "tactical reload", CardRole.None },
        };

        /// <summary>
        /// Gets the role(s) assigned to a card by its display name.
        /// Returns CardRole.None if the card is not in the mapping.
        /// </summary>
        public static CardRole GetCardRole(string cardDisplayName)
        {
            if (string.IsNullOrEmpty(cardDisplayName))
                return CardRole.None;

            return cardRoles.TryGetValue(cardDisplayName, out var role) ? role : CardRole.None;
        }

        /// <summary>
        /// Gets the role(s) assigned to a card.
        /// Returns CardRole.None if the card is null or not in the mapping.
        /// </summary>
        public static CardRole GetCardRole(CardInfo card)
        {
            if (card == null)
                return CardRole.None;

            return GetCardRole(card.cardName);
        }

        /// <summary>
        /// Checks if a card has a specific role.
        /// </summary>
        public static bool HasRole(string cardDisplayName, CardRole role)
        {
            return GetCardRole(cardDisplayName) == role;
        }

        /// <summary>
        /// Checks if a card has a specific role.
        /// </summary>
        public static bool HasRole(CardInfo card, CardRole role)
        {
            return GetCardRole(card) == role;
        }

        /// <summary>
        /// Gets the abbreviated string for a card role.
        /// Returns "ATK", "TANK", "HEAL", or empty string for None/Disabled.
        /// </summary>
        public static string GetRoleAbbreviation(CardRole role)
        {
            switch (role)
            {
                case CardRole.Atk:
                    return "ATK";
                case CardRole.Tank:
                    return "TANK";
                case CardRole.Heal:
                    return "HEAL";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Gets the abbreviated string for a card's role.
        /// </summary>
        public static string GetRoleAbbreviation(string cardDisplayName)
        {
            return GetRoleAbbreviation(GetCardRole(cardDisplayName));
        }

        /// <summary>
        /// Gets the abbreviated string for a card's role.
        /// </summary>
        public static string GetRoleAbbreviation(CardInfo card)
        {
            return GetRoleAbbreviation(GetCardRole(card));
        }

        /// <summary>
        /// Registers or updates a card's role mapping at runtime.
        /// </summary>
        public static void SetCardRole(string cardDisplayName, CardRole role)
        {
            if (string.IsNullOrEmpty(cardDisplayName))
                return;

            cardRoles[cardDisplayName] = role;
        }

        /// <summary>
        /// Gets all card names that have a specific role.
        /// </summary>
        public static IEnumerable<string> GetCardsWithRole(CardRole role)
        {
            foreach (var kvp in cardRoles)
            {
                if (kvp.Value == role)
                    yield return kvp.Key;
            }
        }

        /// <summary>
        /// Checks if a card is in the role map.
        /// </summary>
        public static bool IsCardInRoleMap(string cardDisplayName)
        {
            if (string.IsNullOrEmpty(cardDisplayName))
                return false;

            return cardRoles.ContainsKey(cardDisplayName);
        }

        /// <summary>
        /// Checks if a card should be enabled.
        /// A card is enabled if it is in the role map AND does not have the Disabled role.
        /// </summary>
        public static bool IsCardEnabled(string cardDisplayName)
        {
            if (string.IsNullOrEmpty(cardDisplayName))
                return false;

            if (!cardRoles.TryGetValue(cardDisplayName, out var role))
                return false; // Not in map = disabled

            return role != CardRole.Disabled; // Enabled if not Disabled
        }

        /// <summary>
        /// Checks if a card should be enabled.
        /// A card is enabled if it is in the role map AND does not have the Disabled role.
        /// </summary>
        public static bool IsCardEnabled(CardInfo card)
        {
            if (card == null)
                return false;

            return IsCardEnabled(card.cardName);
        }
    }
}
