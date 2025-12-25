using HarmonyLib;

namespace TeamComposition2.Patches
{
    /// <summary>
    /// Prevents card selection input (spacebar, clicking) while the game is paused.
    /// </summary>
    [HarmonyPatch(typeof(CardChoice))]
    class CardChoicePausePatch
    {
        [HarmonyPatch("DoPlayerSelect")]
        [HarmonyPrefix]
        static bool Prefix()
        {
            // Block card selection input when escape menu is open (game paused)
            return !EscapeMenuHandler.isEscMenu;
        }
    }
}
