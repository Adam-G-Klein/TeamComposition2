using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace TeamComposition2.Patches
{
    /// <summary>
    /// Intercepts TryBlock calls and redirects them to trigger ability effects only,
    /// preventing the defensive block window (sinceBlock) from activating while still
    /// firing all block-based ability hooks and respecting cooldowns.
    /// </summary>
    [HarmonyPatch(typeof(Block), "TryBlock")]
    internal static class BlockTryBlockPatch
    {
        // Cached reflection accessor for internal simulated field.
        private static readonly FieldInfo SimulatedField = AccessTools.Field(typeof(PlayerVelocity), "simulated");

        private static bool Prefix(Block __instance)
        {
            var data = __instance.GetComponent<CharacterData>();
            if (data == null || data.playerVel == null)
            {
                return false; // Skip original
            }

            // Only allow if player is simulated (i.e., actively playing).
            if (!IsSimulated(data.playerVel))
            {
                return false; // Skip original
            }

            // Only trigger if block is off cooldown (same check as original TryBlock).
            if (__instance.counter >= __instance.Cooldown())
            {
                // Trigger all block effects (heals, spawned objects, events) without
                // granting the defensive block window (onlyBlockEffects = true prevents
                // sinceBlock from being set to 0).
                __instance.RPCA_DoBlock(true, false, BlockTrigger.BlockTriggerType.Default, default, true);

                // Reset counter to put block on cooldown (same as original TryBlock).
                __instance.counter = 0f;
            }

            // Skip the original TryBlock to prevent the defensive block window.
            return false;
        }

        private static bool IsSimulated(PlayerVelocity playerVelocity)
        {
            if (playerVelocity == null)
            {
                return false;
            }

            // Field is internal on the vanilla class; reflection keeps us compatible.
            if (SimulatedField != null && SimulatedField.GetValue(playerVelocity) is bool simulated)
            {
                return simulated;
            }

            // Fall back to true so we do not incorrectly block gameplay if reflection fails.
            return true;
        }
    }
}
