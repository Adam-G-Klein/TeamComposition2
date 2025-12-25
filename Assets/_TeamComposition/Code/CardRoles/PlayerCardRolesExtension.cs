using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using HarmonyLib;
using UnboundLib.GameModes;

namespace TeamComposition2.CardRoles
{
    /// <summary>
    /// Additional data stored per-player for tracking card role counts.
    /// </summary>
    [Serializable]
    public class PlayerCardRolesData
    {
        /// <summary>
        /// Count of Tank role cards the player has.
        /// </summary>
        public int TankCount { get; set; }

        /// <summary>
        /// Count of Atk role cards the player has.
        /// </summary>
        public int AtkCount { get; set; }

        /// <summary>
        /// Count of Heal role cards the player has.
        /// </summary>
        public int HealCount { get; set; }

        /// <summary>
        /// The sequence of roles the player has picked, in order.
        /// Each entry corresponds to a card picked, storing its role.
        /// </summary>
        public List<CardRole> RoleSequence { get; private set; }

        public PlayerCardRolesData()
        {
            TankCount = 0;
            AtkCount = 0;
            HealCount = 0;
            RoleSequence = new List<CardRole>();
        }

        /// <summary>
        /// Resets all role counts to zero.
        /// </summary>
        public void Reset()
        {
            TankCount = 0;
            AtkCount = 0;
            HealCount = 0;
            RoleSequence.Clear();
        }

        /// <summary>
        /// Gets the count for a specific role.
        /// </summary>
        public int GetCount(CardRole role)
        {
            switch (role)
            {
                case CardRole.Tank:
                    return TankCount;
                case CardRole.Atk:
                    return AtkCount;
                case CardRole.Heal:
                    return HealCount;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Checks if the player has at least one card of the specified role.
        /// </summary>
        public bool HasRole(CardRole role)
        {
            return GetCount(role) > 0;
        }

        /// <summary>
        /// Increments the count for the given role and adds it to the sequence.
        /// </summary>
        public void AddRole(CardRole role)
        {
            // Add to sequence for all roles (including None for tracking purposes)
            RoleSequence.Add(role);

            switch (role)
            {
                case CardRole.Tank:
                    TankCount++;
                    break;
                case CardRole.Atk:
                    AtkCount++;
                    break;
                case CardRole.Heal:
                    HealCount++;
                    break;
            }
        }

        /// <summary>
        /// Decrements the count for the given role.
        /// Note: Does not modify RoleSequence - use RecalculateFromCards for full reset.
        /// </summary>
        public void RemoveRole(CardRole role)
        {
            switch (role)
            {
                case CardRole.Tank:
                    TankCount = Math.Max(0, TankCount - 1);
                    break;
                case CardRole.Atk:
                    AtkCount = Math.Max(0, AtkCount - 1);
                    break;
                case CardRole.Heal:
                    HealCount = Math.Max(0, HealCount - 1);
                    break;
            }
        }
    }

    /// <summary>
    /// Extension methods for accessing PlayerCardRolesData on Player objects.
    /// </summary>
    public static class PlayerCardRolesExtension
    {
        private static readonly ConditionalWeakTable<Player, PlayerCardRolesData> data =
            new ConditionalWeakTable<Player, PlayerCardRolesData>();

        /// <summary>
        /// Gets the card roles data for a player (creates if not exists).
        /// </summary>
        public static PlayerCardRolesData GetCardRolesData(this Player player)
        {
            return data.GetOrCreateValue(player);
        }

        /// <summary>
        /// Recalculates the role counts and sequence from the player's current cards.
        /// Call this after cards are added/removed to ensure counts are accurate.
        /// This rebuilds the RoleSequence from the player's currentCards list order.
        /// </summary>
        public static void RecalculateCardRoles(this Player player)
        {
            var rolesData = player.GetCardRolesData();
            rolesData.Reset();

            if (player.data?.currentCards == null)
                return;

            foreach (var card in player.data.currentCards)
            {
                if (card == null) continue;
                var role = CardRoleManager.GetCardRole(card);
                rolesData.AddRole(role);
            }

            UnityEngine.Debug.Log($"[TeamComposition2] Player {player.playerID} role sequence recalculated: [{string.Join(", ", rolesData.RoleSequence)}]");
        }

        /// <summary>
        /// Checks if the player has at least one card of the specified role.
        /// </summary>
        public static bool HasCardRole(this Player player, CardRole role)
        {
            return player.GetCardRolesData().HasRole(role);
        }

        /// <summary>
        /// Gets the count of cards with the specified role.
        /// </summary>
        public static int GetCardRoleCount(this Player player, CardRole role)
        {
            return player.GetCardRolesData().GetCount(role);
        }

        /// <summary>
        /// Hook to reset card role data at game start.
        /// Register this with GameModeManager.AddHook(GameModeHooks.HookGameStart, ...)
        /// </summary>
        public static System.Collections.IEnumerator ResetAllPlayerCardRoles(IGameModeHandler gm)
        {
            foreach (var player in PlayerManager.instance.players)
            {
                player.GetCardRolesData().Reset();
            }
            yield break;
        }

        /// <summary>
        /// Hook to recalculate card roles after picks.
        /// Register this with GameModeManager.AddHook(GameModeHooks.HookPlayerPickEnd, ...)
        /// </summary>
        public static System.Collections.IEnumerator RecalculateAllPlayerCardRoles(IGameModeHandler gm)
        {
            foreach (var player in PlayerManager.instance.players)
            {
                player.RecalculateCardRoles();
            }
            yield break;
        }
    }

    /// <summary>
    /// Harmony patches to automatically update card role counts when cards are added.
    /// </summary>
    [HarmonyPatch]
    internal static class PlayerCardRolesPatches
    {
        /// <summary>
        /// Patch ModdingUtils.Utils.Cards.AddCardToPlayer to update role counts.
        /// </summary>
        [HarmonyPatch(typeof(ModdingUtils.Utils.Cards), nameof(ModdingUtils.Utils.Cards.AddCardToPlayer))]
        [HarmonyPostfix]
        private static void OnCardAdded(Player player, CardInfo card)
        {
            if (player == null || card == null) return;

            var role = CardRoleManager.GetCardRole(card);
            if (role != CardRole.None)
            {
                player.GetCardRolesData().AddRole(role);
                UnityEngine.Debug.Log($"[TeamComposition2] Player {player.playerID} gained card '{card.cardName}' with role {role}. " +
                    $"Counts - Tank: {player.GetCardRolesData().TankCount}, Atk: {player.GetCardRolesData().AtkCount}, Heal: {player.GetCardRolesData().HealCount}");
            }
        }
    }
}
