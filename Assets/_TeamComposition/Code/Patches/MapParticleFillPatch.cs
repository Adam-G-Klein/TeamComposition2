using HarmonyLib;
using UnityEngine;

namespace TeamComposition2.Patches
{
    /// <summary>
    /// Fills the MapParticle sorting layer with a flat white sprite inside each map mask
    /// and disables any particle systems on that layer.
    /// </summary>
    [HarmonyPatch(typeof(Map), "Start")]
    internal static class MapParticleFillPatch
    {
        private const string FillObjectName = "TC2_MapParticleFill";

        private static readonly int MapParticleLayerId = SortingLayer.NameToID("MapParticle");

        private static readonly Material DefaultSpriteMaterial = new Material(Shader.Find("Sprites/Default"));

        private static void Postfix(Map __instance)
        {
            // Disable particle systems under the map (particularly those on MapParticle layer)
            foreach (ParticleSystemRenderer psr in __instance.GetComponentsInChildren<ParticleSystemRenderer>(true))
            {
                if (psr == null)
                {
                    continue;
                }

                if (psr.sortingLayerID == MapParticleLayerId || psr.gameObject.layer == MapParticleLayerId)
                {
                    ParticleSystem ps = psr.GetComponent<ParticleSystem>();
                    if (ps != null)
                    {
                        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                        ps.Clear(true);
                    }
                    psr.enabled = false;
                    psr.gameObject.SetActive(false);
                }
            }

            // For every SpriteMask, add a flat white fill sprite in the MapParticle layer
            SpriteMask[] masks = __instance.GetComponentsInChildren<SpriteMask>(true);
            foreach (SpriteMask mask in masks)
            {
                if (mask == null || mask.gameObject.CompareTag("NoMask"))
                {
                    continue;
                }

                // Avoid duplicating fill renderers
                Transform existingFill = mask.transform.Find(FillObjectName);
                if (existingFill != null)
                {
                    continue;
                }

                SpriteMask maskComponent = mask;
                Sprite sprite = maskComponent.sprite;
                if (sprite == null)
                {
                    continue;
                }

                GameObject fill = new GameObject(FillObjectName);
                fill.transform.SetParent(mask.transform, false);
                fill.transform.localPosition = Vector3.zero;
                fill.transform.localRotation = Quaternion.identity;
                fill.transform.localScale = Vector3.one;

                SpriteRenderer renderer = fill.AddComponent<SpriteRenderer>();
                renderer.sprite = sprite;
                renderer.color = Color.white;
                renderer.sortingLayerID = MapParticleLayerId;
                renderer.sortingOrder = 0;
                renderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                renderer.material = DefaultSpriteMaterial;
            }
        }
    }
}

