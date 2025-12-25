using HarmonyLib;
using TeamComposition2.Stats;
using UnityEngine;

namespace TeamComposition2.Patches
{
    /// <summary>
    /// Patches for applying healing effectiveness multipliers to pure healing effects.
    /// This affects Healing Field and Christmas Cheer (IceRing) healing, but NOT lifesteal or regen.
    /// </summary>
    [HarmonyPatch]
    internal static class HealingEffectivenessPatches
    {
        /// <summary>
        /// Patches Explosion.DoExplosionEffects to apply healing effectiveness when damage is negative (healing).
        /// The spawner's HealingDealtMultiplier is applied to the heal amount.
        /// </summary>
        [HarmonyPatch(typeof(Explosion), "DoExplosionEffects")]
        [HarmonyPrefix]
        private static bool ExplosionHealingPrefix(Explosion __instance, Collider2D hitCol, ref float ___damage)
        {
            // Only modify if this is a healing explosion (negative damage)
            if (___damage >= 0f)
                return true;

            // Block the healer from benefiting from their own healing field
            // (identified by the marker added in HealingFieldTaggerPatch).
            if (__instance.GetComponent<TeamComposition2.HealingFieldTeamMarker>() != null)
            {
                var targetData = hitCol != null ? hitCol.gameObject.GetComponentInParent<CharacterData>() : null;
                var spawnedAttack = __instance.GetComponent<SpawnedAttack>();
                var spawner = spawnedAttack != null ? spawnedAttack.spawner : null;

                if (targetData != null && spawner != null && targetData.player == spawner)
                {
                    // Allow self-healing only when explicitly enabled (e.g., via Self-Sufficient card)
                    if (!spawner.CanHealSelfWithHealingFields())
                    {
                        return false;
                    }
                }
            }

            // Get the spawner to apply their healing multiplier
            var spawned = __instance.GetComponent<SpawnedAttack>();
            if (spawned == null || spawned.spawner == null)
                return true;

            float healingMultiplier = spawned.spawner.GetHealingDealtMultiplier();
            if (healingMultiplier != 1f)
            {
                // damage is negative, so multiplying it makes the heal larger (more negative = more healing)
                ___damage *= healingMultiplier;
                Debug.Log($"[HealingEffectiveness] Applied {healingMultiplier:F2}x healing multiplier to Healing Field from player {spawned.spawner.playerID}");
            }

            return true;
        }
    }
}
