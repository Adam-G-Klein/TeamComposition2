using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace TeamComposition2.Patches
{
    /// <summary>
    /// Captures the spawn position assigned to each team so respawns reuse it.
    /// </summary>
    [HarmonyPatch]
    internal static class TeamSpawnAssignmentPatch
    {
        private static MethodBase TargetMethod()
        {
            // RWF's internal patch type is not directly accessible; resolve via name.
            return AccessTools.Method("RWF.Patches.PlayerManager_Patch_MovePlayers:RPCA_MovePlayers");
        }

        private static void Postfix(Vector2[] spawnDictionary)
        {
            if (spawnDictionary == null)
            {
                return;
            }

            TeamSpawnManager.Reset();

            // spawnDictionary is indexed by playerID
            TeamSpawnManager.SetFromPlayers(PlayerManager.instance.players, spawnDictionary);
        }
    }
}

