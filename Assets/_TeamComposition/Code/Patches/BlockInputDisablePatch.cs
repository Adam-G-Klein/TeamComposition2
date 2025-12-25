using HarmonyLib;

namespace TeamComposition2.Patches
{
    /// <summary>
    /// Disables the block input (mouse/keyboard and controller) so players cannot
    /// activate blocking by pressing a button. Blocking can still be triggered
    /// programmatically through abilities by setting shieldWasPressed directly.
    /// </summary>
    [HarmonyPatch(typeof(GeneralInput), "Update")]
    internal static class BlockInputDisablePatch
    {
        private static void Postfix(GeneralInput __instance)
        {
            // Clear the block input flag after normal input processing
            // This prevents button-triggered blocks while allowing abilities
            // to trigger blocks by setting shieldWasPressed = true after this runs
            __instance.shieldWasPressed = false;
        }
    }
}
