using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TeamComposition2
{
    public static class MapControlPointSpawner
    {
        private static GameObject controlPointInstance;

        private const string ControlPointPrefabName = "ControlPoint";

        private static readonly string backgroundLayerName = "BackgroundObject";

        private static readonly string backgroundBoxComponentName = "SpriteRenderer";

        public static System.Collections.IEnumerator EnsureControlPointExists(UnboundLib.GameModes.IGameModeHandler gm)
        {
            UnityEngine.Debug.Log("[TeamComposition2] Ensuring control point exists for new round.");
            // Destroy any previous point first
            if (controlPointInstance != null)
            {
                UnityEngine.Debug.Log("[TeamComposition2] Destroying existing control point instance.");
                Object.Destroy(controlPointInstance);
                controlPointInstance = null;
            }

            GameObject controlPointPrefab = MyPlugin.asset.LoadAsset<GameObject>(ControlPointPrefabName);
            if (controlPointPrefab == null)
            {
                UnityEngine.Debug.LogError($"[TeamComposition2] ControlPoint prefab '{ControlPointPrefabName}' missing from asset bundle.");
                yield break;
            }
            UnityEngine.Debug.Log("[TeamComposition2] Loaded ControlPoint prefab from asset bundle.");

            if (!TryFindBackgroundBox(out Transform backgroundBox))
            {
                UnityEngine.Debug.LogWarning("[TeamComposition2] No background box found for control point; skipping spawn.");
                yield break;
            }
            UnityEngine.Debug.Log($"[TeamComposition2] Background box found at {backgroundBox.position} with lossy scale {backgroundBox.lossyScale}.");

            controlPointInstance = Object.Instantiate(controlPointPrefab);
            controlPointInstance.name = controlPointPrefab.name;
            controlPointInstance.transform.position = backgroundBox.position;
            controlPointInstance.transform.rotation = backgroundBox.rotation;
            controlPointInstance.transform.localScale = Vector3.one;
            Scene backgroundScene = backgroundBox.gameObject.scene;
            if (backgroundScene.IsValid())
            {
                SceneManager.MoveGameObjectToScene(controlPointInstance, backgroundScene);
                UnityEngine.Debug.Log($"[TeamComposition2] Moved ControlPoint into scene '{backgroundScene.name}'.");
            }
            else
            {
                UnityEngine.Debug.LogWarning("[TeamComposition2] Background box scene invalid; control point remains in default scene.");
            }
            SetLayerRecursive(controlPointInstance, backgroundLayerName);
            UnityEngine.Debug.Log($"[TeamComposition2] Spawned ControlPoint at {controlPointInstance.transform.position}.");

            yield break;
        }

        private static bool TryFindBackgroundBox(out Transform backgroundBox)
        {
            backgroundBox = null;
            int backgroundLayer = LayerMask.NameToLayer(backgroundLayerName);
            if (backgroundLayer == -1)
            {
                UnityEngine.Debug.LogWarning($"[TeamComposition2] Layer '{backgroundLayerName}' not defined; cannot locate background box.");
                return false;
            }

            SpriteRenderer[] backgroundRenderers = Object.FindObjectsOfType<SpriteRenderer>();
            UnityEngine.Debug.Log($"[TeamComposition2] Searching {backgroundRenderers.Length} SpriteRenderers for background layer {backgroundLayer}.");

            foreach (var renderer in backgroundRenderers)
            {
                if (renderer.gameObject.layer != backgroundLayer)
                {
                    continue;
                }

                UnityEngine.Debug.Log($"[TeamComposition2] Background box candidate '{renderer.gameObject.name}' selected.");
                backgroundBox = renderer.transform;
                return true;
            }

            UnityEngine.Debug.LogWarning("[TeamComposition2] No SpriteRenderer found on background layer; control point not spawned.");
            return false;
        }

        private static void SetLayerRecursive(GameObject obj, string layerName)
        {
            int layer = LayerMask.NameToLayer(layerName);
            if (layer == -1)
            {
                return;
            }
            obj.layer = layer;
            foreach (Transform child in obj.transform)
            {
                SetLayerRecursive(child.gameObject, layerName);
            }
        }

    }
}

