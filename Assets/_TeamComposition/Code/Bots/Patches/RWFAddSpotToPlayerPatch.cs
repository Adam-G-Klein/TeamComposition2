using HarmonyLib;
using RWF.UI;

namespace TeamComposition2.Bots.Patches
{
    [HarmonyPatch(typeof(PlayerSpotlight))]
    internal class RWFAddSpotToPlayerPatch
    {
        [HarmonyPatch("AddSpotToPlayer")]
        public static bool Prefix(Player player)
        {
            return !player.GetComponent<PlayerAPI>().enabled;
        }
    }
}
