using System.Runtime.CompilerServices;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace TeamComposition2.Patches
{
    /// <summary>
    /// Tracks manual block button presses so they can be converted into ability triggers
    /// without enabling the actual block window.
    /// </summary>
    [HarmonyPatch(typeof(GeneralInput), "Update")]
    internal static class BlockInputDisablePatch
    {
        // Cached reflection accessors to work around internal/private members on game types.
        private static readonly FieldInfo SimulatedField = AccessTools.Field(typeof(PlayerVelocity), "simulated");

        private class ManualBlockState
        {
            public bool ManualPressed;
        }

        private static readonly ConditionalWeakTable<GeneralInput, ManualBlockState> ManualPresses = new ConditionalWeakTable<GeneralInput, ManualBlockState>();

        internal static bool WasManualPress(GeneralInput input)
        {
            return ManualPresses.TryGetValue(input, out var state) && state.ManualPressed;
        }

        internal static void ClearManualPress(GeneralInput input)
        {
            if (ManualPresses.TryGetValue(input, out var state))
            {
                state.ManualPressed = false;
            }
        }

        private static void Postfix(GeneralInput __instance)
        {
            var data = __instance.GetComponent<CharacterData>();

            // Record whether the player physically pressed the block button this frame.
            bool manualPressed = data?.playerActions?.Block?.WasPressed == true;
            ManualPresses.GetOrCreateValue(__instance).ManualPressed = manualPressed;
        }

        internal static bool IsSimulated(PlayerVelocity playerVelocity)
        {
            if (playerVelocity == null)
            {
                return false;
            }

            // Field is internal on the vanilla class; reflection keeps us compatible across assemblies.
            if (SimulatedField != null && SimulatedField.GetValue(playerVelocity) is bool simulated)
            {
                return simulated;
            }

            // Fall back to true so we do not incorrectly block gameplay if reflection fails.
            return true;
        }
    }

    /// <summary>
    /// Converts manual block button presses into block-triggered effects only,
    /// preventing the defensive block while still firing block-based ability hooks
    /// and honoring cooldowns.
    /// </summary>
    [HarmonyPatch(typeof(Block), "Update")]
    internal static class BlockInputRedirectPatch
    {
        private static void Prefix(Block __instance)
        {
            var input = __instance.GetComponent<GeneralInput>();
            if (input == null)
            {
                return;
            }

            if (BlockInputDisablePatch.WasManualPress(input))
            {
                // Stop the vanilla block from seeing the manual press.
                input.shieldWasPressed = false;
            }
        }

        private static void Postfix(Block __instance)
        {
            var input = __instance.GetComponent<GeneralInput>();
            if (input == null || !BlockInputDisablePatch.WasManualPress(input))
            {
                return;
            }

            var data = __instance.GetComponent<CharacterData>();
            if (data == null || data.playerVel == null || !BlockInputDisablePatch.IsSimulated(data.playerVel))
            {
                BlockInputDisablePatch.ClearManualPress(input);
                return;
            }

            // Only trigger if block is off cooldown, mirroring TryBlock behavior.
            if (__instance.counter >= __instance.Cooldown())
            {
                // Trigger all block effects (heals, spawned objects, events) without
                // granting the defensive block window.
                __instance.RPCA_DoBlock(true, false, BlockTrigger.BlockTriggerType.Default, default, true);
            }

            BlockInputDisablePatch.ClearManualPress(input);
        }
    }
}
