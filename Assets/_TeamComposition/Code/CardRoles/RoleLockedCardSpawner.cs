using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Photon.Pun;
using UnityEngine;
using ModdingUtils.Utils;

namespace TeamComposition2.CardRoles
{
    /// <summary>
    /// Static state for tracking required roles during card spawning.
    /// Ensures that the leftmost N card slots in card selection correspond to
    /// the player's existing card role sequence.
    /// </summary>
    public static class RoleLockedCardSpawner
    {
        /// <summary>
        /// For the current pick, the list of required roles for each slot.
        /// Index 0 = leftmost slot. CardRole.None means no role requirement for that slot.
        /// </summary>
        public static List<CardRole> RequiredRolesForSlots { get; private set; } = new List<CardRole>();

        /// <summary>
        /// The current spawn index (0 = leftmost slot).
        /// Incremented each time SpawnUniqueCard is called.
        /// </summary>
        public static int CurrentSpawnIndex { get; set; } = 0;

        /// <summary>
        /// Setup the required roles before card spawning begins.
        /// Called at the start of ReplaceCards.
        /// </summary>
        public static void SetupRequiredRoles(Player player)
        {
            RequiredRolesForSlots.Clear();
            CurrentSpawnIndex = 0;

            if (player == null)
            {
                UnityEngine.Debug.Log("[TeamComposition2] RoleLockedCardSpawner: No player, skipping role setup");
                return;
            }

            var rolesData = player.GetCardRolesData();
            RequiredRolesForSlots = new List<CardRole>(rolesData.RoleSequence);

            UnityEngine.Debug.Log($"[TeamComposition2] RoleLockedCardSpawner: Setup for player {player.playerID} with {RequiredRolesForSlots.Count} locked slots: [{string.Join(", ", RequiredRolesForSlots)}]");
        }

        /// <summary>
        /// Gets a random card of the specified role that is valid for the player.
        /// Uses weighted rarity selection similar to the base game.
        /// </summary>
        public static CardInfo GetRandomCardOfRole(Player player, CardRole role, CardChoice instance)
        {
            // Get all active cards of this role
            var cardsOfRole = CardChoice.instance.cards
                .Where(c => CardRoleManager.GetCardRole(c) == role)
                .ToArray();

            if (cardsOfRole.Length == 0)
            {
                UnityEngine.Debug.Log($"[TeamComposition2] RoleLockedCardSpawner: No cards found with role {role}");
                return null;
            }

            // Filter by validity (same conditions as CardChoiceSpawnUniqueCardPatch)
            var validCards = cardsOfRole.Where(card =>
                Cards.instance.PlayerIsAllowedCard(player, card) &&
                IsCardValidForSpawn(card, player, instance)
            ).ToArray();

            if (validCards.Length == 0)
            {
                // Allow duplicates if no unique cards are available
                UnityEngine.Debug.Log($"[TeamComposition2] RoleLockedCardSpawner: No unique cards of role {role}, allowing duplicates");
                validCards = cardsOfRole.Where(card =>
                    Cards.instance.PlayerIsAllowedCard(player, card)
                ).ToArray();
            }

            if (validCards.Length == 0)
            {
                UnityEngine.Debug.Log($"[TeamComposition2] RoleLockedCardSpawner: No valid cards of role {role} at all");
                return null;
            }

            // Random weighted selection based on rarity
            return GetWeightedRandomCard(validCards);
        }

        /// <summary>
        /// Checks if a card is valid for spawning (not already spawned, not blacklisted, etc.)
        /// </summary>
        private static bool IsCardValidForSpawn(CardInfo card, Player player, CardChoice instance)
        {
            var spawnedCards = (List<GameObject>)Traverse.Create(instance).Field("spawnedCards").GetValue();

            // Check for duplicates in already spawned cards
            foreach (var spawned in spawnedCards)
            {
                if (spawned == null) continue;
                var spawnedCardInfo = spawned.GetComponent<CardInfo>();
                if (spawnedCardInfo == null) continue;

                if (spawnedCardInfo.gameObject.name.Replace("(Clone)", "") == card.gameObject.name)
                {
                    return false;
                }
            }

            // Check gun lock conflicts
            if (instance.pickrID != -1)
            {
                Holdable holdable = player.data.GetComponent<Holding>().holdable;
                if (holdable != null)
                {
                    Gun component2 = holdable.GetComponent<Gun>();
                    Gun component3 = card.GetComponent<Gun>();
                    if (component3 != null && component2 != null && component3.lockGunToDefault && component2.lockGunToDefault)
                    {
                        return false;
                    }
                }
            }

            // Check blacklisted categories from player's existing cards
            if (player.data?.currentCards != null)
            {
                foreach (var existingCard in player.data.currentCards)
                {
                    if (existingCard == null) continue;

                    if (existingCard.blacklistedCategories != null && card.categories != null)
                    {
                        foreach (var blacklisted in existingCard.blacklistedCategories)
                        {
                            if (card.categories.Contains(blacklisted))
                            {
                                return false;
                            }
                        }
                    }

                    if (!existingCard.allowMultiple && card.gameObject.name == existingCard.gameObject.name)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Selects a random card using weighted rarity (same weights as base game).
        /// </summary>
        private static CardInfo GetWeightedRandomCard(CardInfo[] cards)
        {
            float totalWeight = 0;
            foreach (var card in cards)
            {
                totalWeight += GetRarityWeight(card.rarity);
            }

            float roll = UnityEngine.Random.Range(0f, totalWeight);
            foreach (var card in cards)
            {
                roll -= GetRarityWeight(card.rarity);
                if (roll <= 0)
                {
                    return card;
                }
            }

            return cards[cards.Length - 1];
        }

        /// <summary>
        /// Gets the spawn weight for a given rarity (matches base game).
        /// </summary>
        private static float GetRarityWeight(CardInfo.Rarity rarity)
        {
            switch (rarity)
            {
                case CardInfo.Rarity.Common: return 10f;
                case CardInfo.Rarity.Uncommon: return 4f;
                case CardInfo.Rarity.Rare: return 1f;
                default: return 1f;
            }
        }
    }

    /// <summary>
    /// Patch ReplaceCards to set up required roles before spawning cards.
    /// </summary>
    [HarmonyPatch(typeof(CardChoice), "ReplaceCards")]
    internal class RoleLockedReplaceCardsPatch
    {
        [HarmonyPrefix]
        static void Prefix(CardChoice __instance)
        {
            if (__instance.pickrID == -1)
            {
                RoleLockedCardSpawner.SetupRequiredRoles(null);
                return;
            }

            Player player = null;
            var pickerType = (PickerType)Traverse.Create(__instance).Field("pickerType").GetValue();

            if (pickerType == PickerType.Team)
            {
                var playersInTeam = PlayerManager.instance.GetPlayersInTeam(__instance.pickrID);
                if (playersInTeam != null && playersInTeam.Count() > 0)
                {
                    player = playersInTeam[0];
                }
            }
            else
            {
                if (__instance.pickrID >= 0 && __instance.pickrID < PlayerManager.instance.players.Count)
                {
                    player = PlayerManager.instance.players[__instance.pickrID];
                }
            }

            RoleLockedCardSpawner.SetupRequiredRoles(player);
        }
    }

    /// <summary>
    /// Patch SpawnUniqueCard to force role-specific cards for locked slots.
    /// Runs after the base CardChoiceSpawnUniqueCardPatch so we can clean up any
    /// card that was spawned there before we replace it with the role-locked one.
    /// </summary>
    [HarmonyPatch(typeof(CardChoice), "SpawnUniqueCard")]
    [HarmonyAfter("pykess.rounds.plugins.cardchoicespawnuniquecardpatch")]
    [HarmonyPriority(Priority.Low)]
    internal class RoleLockedSpawnPatch
    {
        [HarmonyPrefix]
        static bool Prefix(ref GameObject __result, CardChoice __instance, Vector3 pos, Quaternion rot)
        {
            int slotIndex = RoleLockedCardSpawner.CurrentSpawnIndex;
            var requiredRoles = RoleLockedCardSpawner.RequiredRolesForSlots;

            // Increment the spawn index for the next spawn
            RoleLockedCardSpawner.CurrentSpawnIndex++;

            // If this slot doesn't have a required role, let the other patches handle it
            if (slotIndex >= requiredRoles.Count)
            {
                UnityEngine.Debug.Log($"[TeamComposition2] RoleLockedSpawnPatch: Slot {slotIndex} has no role requirement, using default spawn");
                return true; // Continue to next patch / original
            }

            var requiredRole = requiredRoles[slotIndex];
            if (requiredRole == CardRole.None || requiredRole == CardRole.Disabled)
            {
                UnityEngine.Debug.Log($"[TeamComposition2] RoleLockedSpawnPatch: Slot {slotIndex} requires {requiredRole}, using default spawn");
                return true; // No role requirement, continue normally
            }

            // Get the player
            Player player = null;
            var pickerType = (PickerType)Traverse.Create(__instance).Field("pickerType").GetValue();
            if (pickerType == PickerType.Team)
            {
                var playersInTeam = PlayerManager.instance.GetPlayersInTeam(__instance.pickrID);
                if (playersInTeam != null && playersInTeam.Count() > 0)
                {
                    player = playersInTeam[0];
                }
            }
            else
            {
                if (__instance.pickrID >= 0 && __instance.pickrID < PlayerManager.instance.players.Count)
                {
                    player = PlayerManager.instance.players[__instance.pickrID];
                }
            }

            if (player == null)
            {
                UnityEngine.Debug.Log($"[TeamComposition2] RoleLockedSpawnPatch: Could not find player, using default spawn");
                return true;
            }

            // Destroy any card spawned by earlier prefixes (e.g. the default unique-card patch)
            if (__result != null)
            {
                TryDestroySpawnedCard(__result);
                __result = null;
            }

            // Try to get a card of the required role
            var roleCard = RoleLockedCardSpawner.GetRandomCardOfRole(player, requiredRole, __instance);

            if (roleCard == null)
            {
                // No cards of this role available, fall back to normal behavior
                UnityEngine.Debug.Log($"[TeamComposition2] RoleLockedSpawnPatch: No cards of role {requiredRole} available, using default spawn");
                return true;
            }

            UnityEngine.Debug.Log($"[TeamComposition2] RoleLockedSpawnPatch: Slot {slotIndex} locked to role {requiredRole}, spawning '{roleCard.cardName}'");

            // Spawn the role-specific card using the private Spawn method
            GameObject gameObject = (GameObject)typeof(CardChoice).InvokeMember("Spawn",
                BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic,
                null, __instance, new object[] { roleCard.gameObject, pos, rot });
            gameObject.GetComponent<CardInfo>().sourceCard = roleCard;
            gameObject.GetComponentInChildren<DamagableEvent>().GetComponent<Collider2D>().enabled = false;

            __result = gameObject;
            return false; // Skip original and other patches
        }

        private static void TryDestroySpawnedCard(GameObject cardObject)
        {
            try
            {
                if (cardObject == null)
                {
                    return;
                }

                var photonView = cardObject.GetComponent<PhotonView>();
                if (photonView != null)
                {
                    PhotonNetwork.Destroy(cardObject);
                }
                else
                {
                    UnityEngine.Object.Destroy(cardObject);
                }
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogWarning($"[TeamComposition2] RoleLockedSpawnPatch: Failed to destroy previously spawned card: {ex}");
            }
        }
    }
}
