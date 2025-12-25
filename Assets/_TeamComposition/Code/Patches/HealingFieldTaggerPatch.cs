using System;
using HarmonyLib;
using UnityEngine;

namespace TeamComposition2.Patches
{
    /// <summary>
    /// Tags healing field spawns with their owning team so AI can reason about them.
    /// </summary>
    [HarmonyPatch(typeof(SpawnObjects), "ConfigureObject")]
    internal static class HealingFieldTaggerPatch
    {
        private static void Postfix(SpawnObjects __instance, GameObject go)
        {
            if (go == null)
            {
                return;
            }

            // Only care about the Healing Field object (negative damage AOE with "Healing" in the name).
            var explosion = go.GetComponent<Explosion>();
            if (explosion == null || explosion.damage >= 0f)
            {
                return;
            }

            if (go.name.IndexOf("Healing", StringComparison.OrdinalIgnoreCase) < 0)
            {
                return;
            }

            var owner = go.GetComponent<SpawnedAttack>()?.spawner ?? __instance.transform.root.GetComponent<Player>();
            var marker = go.GetComponent<TeamComposition2.HealingFieldTeamMarker>() ?? go.AddComponent<TeamComposition2.HealingFieldTeamMarker>();
            marker.Initialize(owner);
        }
    }
}

