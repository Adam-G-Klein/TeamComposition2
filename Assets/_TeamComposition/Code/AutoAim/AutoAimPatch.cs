using HarmonyLib;
using UnityEngine;

namespace TeamComposition2.AutoAim
{
    [HarmonyPatch(typeof(GeneralInput), "Update")]
    public class AutoAimPatch
    {
        [HarmonyPostfix]
        public static void Postfix(GeneralInput __instance)
        {
            // Get the player from the CharacterData component
            CharacterData data = __instance.GetComponent<CharacterData>();
            if (data == null || data.player == null)
            {
                return;
            }

            Player player = data.player;

            // Check for toggle input
            if (data.playerActions != null)
            {
                var autoAimData = data.playerActions.GetAutoAimData();
                bool wasPressed = autoAimData.toggleAutoAim != null && autoAimData.toggleAutoAim.WasPressed;

                // Only toggle on the rising edge (was not pressed last frame, is pressed this frame)
                if (wasPressed && !autoAimData.toggleAutoAimWasPressed)
                {
                    AutoAimManager.ToggleAutoAim(player);
                }

                autoAimData.toggleAutoAimWasPressed = wasPressed;
            }

            // Check if this player has auto-aim enabled
            if (!AutoAimManager.IsAutoAiming(player))
            {
                return;
            }

            // Get the auto-aim direction
            Vector3 autoAimDirection = AutoAimManager.GetAutoAimDirection(player);

            // Only override if we have a valid target
            if (autoAimDirection != Vector3.zero)
            {
                __instance.aimDirection = autoAimDirection;

                // Also update lastAimDirection so it persists
                if (__instance.aimDirection != Vector3.zero)
                {
                    __instance.lastAimDirection = __instance.aimDirection;
                }
            }
        }
    }
}
