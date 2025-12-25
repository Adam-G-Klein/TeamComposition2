using HarmonyLib;
using UnityEngine;

namespace TeamComposition2.Patches
{
    /// <summary>
    /// Replaces the SFShadowed materials on map objects with a flat white DefaultSprite material
    /// to remove the crazy visual effects from map objects.
    /// </summary>
    [HarmonyPatch(typeof(Map), "Start")]
    internal static class MapMaterialPatch
    {
        private static Material _defaultSpriteMaterial;

        /// <summary>
        /// Loads the DefaultSprite material from the asset bundle.
        /// </summary>
        internal static void Initialize()
        {
            if (MyPlugin.asset != null)
            {
                _defaultSpriteMaterial = MyPlugin.asset.LoadAsset<Material>("DefaultSprite");
                if (_defaultSpriteMaterial == null)
                {
                    UnityEngine.Debug.LogWarning("[TeamComposition2] MapMaterialPatch: Could not load DefaultSprite material from asset bundle.");
                }
                else
                {
                    UnityEngine.Debug.Log("[TeamComposition2] MapMaterialPatch: DefaultSprite material loaded successfully.");
                }
            }
        }

        private static void Postfix(Map __instance)
        {
            if (_defaultSpriteMaterial == null)
            {
                return;
            }

            // Get all SpriteRenderers in the map (including inactive ones)
            SpriteRenderer[] renderers = __instance.GetComponentsInChildren<SpriteRenderer>(true);

            foreach (SpriteRenderer renderer in renderers)
            {
                if (renderer == null)
                {
                    continue;
                }

                // Check if this renderer is using the SFShadowed material (or any material with SFShadowStencil shader)
                Material currentMat = renderer.sharedMaterial;
                if (currentMat != null &&
                    (currentMat.name.Contains("SFShadow") ||
                     (currentMat.shader != null && currentMat.shader.name.Contains("SFShadow"))))
                {
                    renderer.material = _defaultSpriteMaterial;
                }
            }
        }
    }
}
