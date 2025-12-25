using System.Collections;
using System.Collections.Generic;
using UnboundLib.GameModes;
using UnityEngine;
using TeamComposition2.CardRoles;
using TeamComposition2.Stats;

namespace TeamComposition2.GameModes
{
    /// <summary>
    /// Applies stat modifiers from StatModifierSettings to players during gameplay.
    /// Base stats are applied to all players, class-based stats scale with card role counts.
    /// </summary>
    public static class StatModifierApplicator
    {
        /// <summary>
        /// Tracks applied modifiers per player so they can be removed/reapplied correctly.
        /// Key is player ID, value contains the inverse multipliers to undo previous application.
        /// </summary>
        private static Dictionary<int, AppliedModifiers> appliedModifiers = new Dictionary<int, AppliedModifiers>();

        private class AppliedModifiers
        {
            public float HealthMultiplier = 1f;
            public float MovementSpeedMultiplier = 1f;
            public float JumpMultiplier = 1f;
            public float DamageMultiplier = 1f;
            public float HealingMultiplier = 1f;
        }

        /// <summary>
        /// Registers all necessary hooks for stat application.
        /// Call this from MyPlugin.Awake().
        /// </summary>
        public static void RegisterHooks()
        {
            // Apply stats at battle start (after cards are picked, before fighting)
            GameModeManager.AddHook(GameModeHooks.HookBattleStart, ApplyAllPlayerStats);
            // Apply at round/point start as well to cover custom modes that may skip HookBattleStart
            GameModeManager.AddHook(GameModeHooks.HookRoundStart, ApplyAllPlayerStats);
            GameModeManager.AddHook(GameModeHooks.HookPointStart, ApplyAllPlayerStats);

            // Reset tracking at game start
            GameModeManager.AddHook(GameModeHooks.HookGameStart, ResetAppliedModifiers);

            // Also reset at game end for cleanup
            GameModeManager.AddHook(GameModeHooks.HookGameEnd, ResetAppliedModifiers);
        }

        /// <summary>
        /// Resets the applied modifiers tracking.
        /// </summary>
        public static IEnumerator ResetAppliedModifiers(IGameModeHandler gm)
        {
            appliedModifiers.Clear();
            yield break;
        }

        /// <summary>
        /// Applies stat modifiers to all players based on settings and their card roles.
        /// </summary>
        public static IEnumerator ApplyAllPlayerStats(IGameModeHandler gm)
        {
            if (PlayerManager.instance?.players == null)
                yield break;

            foreach (var player in PlayerManager.instance.players)
            {
                if (player == null) continue;
                ApplyPlayerStats(player);
            }
            yield break;
        }

        /// <summary>
        /// Applies stat modifiers to a single player.
        /// </summary>
        private static void ApplyPlayerStats(Player player)
        {
            var characterStats = player.GetComponent<CharacterStatModifiers>();
            var gun = player.GetComponent<Holding>()?.holdable?.GetComponent<Gun>();
            var healthHandler = player.GetComponent<HealthHandler>();
            var characterData = player.GetComponent<CharacterData>();

            if (characterStats == null)
            {
                Debug.LogWarning($"[StatModifierApplicator] Player {player.playerID} missing CharacterStatModifiers");
                return;
            }

            // First, undo any previously applied modifiers
            UndoPreviousModifiers(player, characterStats, gun, healthHandler, characterData);

            // Calculate new modifiers
            var newModifiers = CalculateModifiers(player);

            // Apply new modifiers
            ApplyModifiers(player, characterStats, gun, healthHandler, characterData, newModifiers);

            // Store for later reversal
            appliedModifiers[player.playerID] = newModifiers;

            // Log application
            var rolesData = player.GetCardRolesData();
            Debug.Log($"[StatModifierApplicator] Applied stats to player {player.playerID}: " +
                $"Health={newModifiers.HealthMultiplier:F2}x, " +
                $"Speed={newModifiers.MovementSpeedMultiplier:F2}x, " +
                $"Jump={newModifiers.JumpMultiplier:F2}x, " +
                $"Damage={newModifiers.DamageMultiplier:F2}x, " +
                $"Healing={newModifiers.HealingMultiplier:F2}x " +
                $"(Tank:{rolesData.TankCount}, Atk:{rolesData.AtkCount}, Heal:{rolesData.HealCount})");
        }

        /// <summary>
        /// Undoes previously applied modifiers by dividing out the old multipliers.
        /// </summary>
        private static void UndoPreviousModifiers(Player player, CharacterStatModifiers characterStats, Gun gun, HealthHandler healthHandler, CharacterData characterData)
        {
            if (!appliedModifiers.TryGetValue(player.playerID, out var previous))
                return;

            // Divide out the previous multipliers
            if (previous.HealthMultiplier != 1f && previous.HealthMultiplier != 0f)
            {
                characterStats.health /= previous.HealthMultiplier;
                if (characterData != null)
                {
                    characterData.maxHealth /= previous.HealthMultiplier;
                    characterData.health /= previous.HealthMultiplier;
                }
            }

            if (previous.MovementSpeedMultiplier != 1f && previous.MovementSpeedMultiplier != 0f)
                characterStats.movementSpeed /= previous.MovementSpeedMultiplier;

            if (previous.JumpMultiplier != 1f && previous.JumpMultiplier != 0f)
                characterStats.jump /= previous.JumpMultiplier;

            if (gun != null && previous.DamageMultiplier != 1f && previous.DamageMultiplier != 0f)
                gun.damage /= previous.DamageMultiplier;

            // Note: HealingMultiplier is now stored in player extension data, not on lifeSteal/regen
            // The undo happens automatically when we set the new multiplier
        }

        /// <summary>
        /// Calculates the total modifiers for a player based on base stats and class bonuses.
        /// </summary>
        private static AppliedModifiers CalculateModifiers(Player player)
        {
            var modifiers = new AppliedModifiers();
            var rolesData = player.GetCardRolesData();

            // Start with base stat modifiers (apply to all players)
            float baseHealth = StatModifierSettings.SliderValueToMultiplier(StatModifierSettings.BaseMaxHealth.Value);
            float baseSpeed = StatModifierSettings.SliderValueToMultiplier(StatModifierSettings.BaseMovementSpeed.Value);
            float baseJump = StatModifierSettings.SliderValueToMultiplier(StatModifierSettings.BaseJumpHeight.Value);
            float baseDamage = StatModifierSettings.SliderValueToMultiplier(StatModifierSettings.BaseDamage.Value);
            float baseHealing = StatModifierSettings.SliderValueToMultiplier(StatModifierSettings.BaseHealing.Value);

            modifiers.HealthMultiplier = baseHealth;
            modifiers.MovementSpeedMultiplier = baseSpeed;
            modifiers.JumpMultiplier = baseJump;
            modifiers.DamageMultiplier = baseDamage;
            modifiers.HealingMultiplier = baseHealing;

            // Apply Tank class bonuses (per card)
            int tankCards = rolesData.TankCount;
            if (tankCards > 0)
            {
                modifiers.HealthMultiplier *= CalculateClassBonus(
                    StatModifierSettings.TankHealthPerCard.Value,
                    StatModifierSettings.TankHealthPerCardMin.Value,
                    StatModifierSettings.TankHealthPerCardMax.Value,
                    tankCards);

                modifiers.MovementSpeedMultiplier *= CalculateClassBonus(
                    StatModifierSettings.TankMovementSpeedPerCard.Value,
                    StatModifierSettings.TankMovementSpeedPerCardMin.Value,
                    StatModifierSettings.TankMovementSpeedPerCardMax.Value,
                    tankCards);

                modifiers.JumpMultiplier *= CalculateClassBonus(
                    StatModifierSettings.TankJumpHeightPerCard.Value,
                    StatModifierSettings.TankJumpHeightPerCardMin.Value,
                    StatModifierSettings.TankJumpHeightPerCardMax.Value,
                    tankCards);
            }

            // Apply Healer class bonuses (per card)
            int healCards = rolesData.HealCount;
            if (healCards > 0)
            {
                modifiers.HealingMultiplier *= CalculateClassBonus(
                    StatModifierSettings.HealerHealingPerCard.Value,
                    StatModifierSettings.HealerHealingPerCardMin.Value,
                    StatModifierSettings.HealerHealingPerCardMax.Value,
                    healCards);

                modifiers.MovementSpeedMultiplier *= CalculateClassBonus(
                    StatModifierSettings.HealerMovementSpeedPerCard.Value,
                    StatModifierSettings.HealerMovementSpeedPerCardMin.Value,
                    StatModifierSettings.HealerMovementSpeedPerCardMax.Value,
                    healCards);
            }

            // Apply Attack class bonuses (per card)
            int atkCards = rolesData.AtkCount;
            if (atkCards > 0)
            {
                modifiers.DamageMultiplier *= CalculateClassBonus(
                    StatModifierSettings.AttackDamagePerCard.Value,
                    StatModifierSettings.AttackDamagePerCardMin.Value,
                    StatModifierSettings.AttackDamagePerCardMax.Value,
                    atkCards);

                modifiers.MovementSpeedMultiplier *= CalculateClassBonus(
                    StatModifierSettings.AttackMovementSpeedPerCard.Value,
                    StatModifierSettings.AttackMovementSpeedPerCardMin.Value,
                    StatModifierSettings.AttackMovementSpeedPerCardMax.Value,
                    atkCards);
            }

            return modifiers;
        }

        /// <summary>
        /// Calculates the total bonus for a class stat based on per-card value, min, max, and card count.
        /// The formula is: multiplier^cardCount, clamped between min and max multipliers.
        /// </summary>
        private static float CalculateClassBonus(float perCardValue, float minValue, float maxValue, int cardCount)
        {
            if (cardCount <= 0)
                return 1f;

            // Convert per-card slider value to multiplier
            float perCardMultiplier = StatModifierSettings.SliderValueToMultiplier(perCardValue);

            // Calculate total bonus: multiplier raised to the power of card count
            // e.g., if per-card multiplier is 1.2 and player has 3 cards: 1.2^3 = 1.728
            float totalMultiplier = Mathf.Pow(perCardMultiplier, cardCount);

            // Convert min/max slider values to multipliers for clamping
            float minMultiplier = StatModifierSettings.SliderValueToMultiplier(minValue);
            float maxMultiplier = StatModifierSettings.SliderValueToMultiplier(maxValue);

            // Clamp the result
            totalMultiplier = Mathf.Clamp(totalMultiplier, minMultiplier, maxMultiplier);

            return totalMultiplier;
        }

        /// <summary>
        /// Applies the calculated modifiers to the player's stats.
        /// </summary>
        private static void ApplyModifiers(Player player, CharacterStatModifiers characterStats, Gun gun, HealthHandler healthHandler, CharacterData characterData, AppliedModifiers modifiers)
        {
            // Apply stat multipliers
            characterStats.health *= modifiers.HealthMultiplier;
            UnityEngine.Debug.Log($"[StatModifierApplicator] Player {player.playerID} health set to {characterStats.health}, multiplier applied: {modifiers.HealthMultiplier}");
            if (characterData != null)
            {
                characterData.maxHealth *= modifiers.HealthMultiplier;
                characterData.health *= modifiers.HealthMultiplier;
            }
            characterStats.movementSpeed *= modifiers.MovementSpeedMultiplier;
            characterStats.jump *= modifiers.JumpMultiplier;

            // Apply damage multiplier to gun
            if (gun != null)
            {
                gun.damage *= modifiers.DamageMultiplier;
            }

            // Apply healing effectiveness multiplier to player extension data
            // This affects pure healing (Healing Field, Christmas Cheer) but NOT lifesteal/regen
            player.SetHealingDealtMultiplier(modifiers.HealingMultiplier);
        }

        /// <summary>
        /// Forces recalculation of stats for a specific player.
        /// Call this if card roles change mid-game.
        /// </summary>
        public static void RefreshPlayerStats(Player player)
        {
            if (player == null) return;
            ApplyPlayerStats(player);
        }

        /// <summary>
        /// Forces recalculation of stats for all players.
        /// </summary>
        public static void RefreshAllPlayerStats()
        {
            if (PlayerManager.instance?.players == null) return;

            foreach (var player in PlayerManager.instance.players)
            {
                if (player != null)
                    ApplyPlayerStats(player);
            }
        }
    }
}
