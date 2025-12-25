using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace TeamComposition2.Stats
{
    /// <summary>
    /// Additional data stored per-player for tracking healing effectiveness multiplier.
    /// This affects "pure healing" effects like Healing Field and Christmas Cheer's ally healing,
    /// but NOT lifesteal or regen.
    /// </summary>
    [Serializable]
    public class HealingEffectivenessData
    {
        /// <summary>
        /// Multiplier applied to all healing received by this player from pure healing sources.
        /// 1.0 = normal healing, 2.0 = double healing, etc.
        /// </summary>
        public float HealingReceivedMultiplier { get; set; } = 1f;

        /// <summary>
        /// Multiplier applied to all healing dealt by this player from pure healing sources.
        /// Used by Christmas Cheer's IceRing to boost the healer's output.
        /// </summary>
        public float HealingDealtMultiplier { get; set; } = 1f;

        public void Reset()
        {
            HealingReceivedMultiplier = 1f;
            HealingDealtMultiplier = 1f;
        }
    }

    /// <summary>
    /// Extension methods for accessing HealingEffectivenessData on Player objects.
    /// </summary>
    public static class HealingEffectivenessExtension
    {
        private static readonly ConditionalWeakTable<Player, HealingEffectivenessData> data =
            new ConditionalWeakTable<Player, HealingEffectivenessData>();

        /// <summary>
        /// Gets the healing effectiveness data for a player (creates if not exists).
        /// </summary>
        public static HealingEffectivenessData GetHealingEffectivenessData(this Player player)
        {
            return data.GetOrCreateValue(player);
        }

        /// <summary>
        /// Gets the healing received multiplier for a player.
        /// </summary>
        public static float GetHealingReceivedMultiplier(this Player player)
        {
            return player.GetHealingEffectivenessData().HealingReceivedMultiplier;
        }

        /// <summary>
        /// Sets the healing received multiplier for a player.
        /// </summary>
        public static void SetHealingReceivedMultiplier(this Player player, float multiplier)
        {
            player.GetHealingEffectivenessData().HealingReceivedMultiplier = multiplier;
        }

        /// <summary>
        /// Gets the healing dealt multiplier for a player.
        /// </summary>
        public static float GetHealingDealtMultiplier(this Player player)
        {
            return player.GetHealingEffectivenessData().HealingDealtMultiplier;
        }

        /// <summary>
        /// Sets the healing dealt multiplier for a player.
        /// </summary>
        public static void SetHealingDealtMultiplier(this Player player, float multiplier)
        {
            player.GetHealingEffectivenessData().HealingDealtMultiplier = multiplier;
        }

        /// <summary>
        /// Resets all healing effectiveness data for all players.
        /// Call this at game start.
        /// </summary>
        public static void ResetAllPlayerHealingEffectiveness()
        {
            if (PlayerManager.instance?.players == null) return;

            foreach (var player in PlayerManager.instance.players)
            {
                if (player != null)
                {
                    player.GetHealingEffectivenessData().Reset();
                }
            }
        }
    }
}
