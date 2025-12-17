using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TeamComposition2
{
    /// <summary>
    /// Tracks player overlaps on the control point trigger and updates its appearance.
    /// </summary>
    public class ControlPointTracker : MonoBehaviour
    {
        private readonly List<Player> overlappingPlayers = new List<Player>();

        private SpriteRenderer indicatorRenderer;

        private ParticleSystem controlPointParticles;

        private BoxCollider2D triggerCollider;

        private bool initialized;

        private static readonly Color neutralColor = new Color(1f, 1f, 1f, 1f);

        private void Awake()
        {
            indicatorRenderer = GetComponentsInChildren<SpriteRenderer>(includeInactive: true)
                .FirstOrDefault(r => r.gameObject != gameObject);

            controlPointParticles = GetComponentsInChildren<ParticleSystem>(includeInactive: true)
                .FirstOrDefault();

            triggerCollider = GetComponentsInChildren<BoxCollider2D>(includeInactive: true)
                .FirstOrDefault(c => c.isTrigger);

            if (triggerCollider == null)
            {
                triggerCollider = gameObject.AddComponent<BoxCollider2D>();
            }

            triggerCollider.isTrigger = true;
            triggerCollider.gameObject.layer = LayerMask.NameToLayer("IgnorePlayer");

            initialized = true;
            overlappingPlayers.Clear();
            UpdateIndicatorColor();

            UnityEngine.Debug.Log("[TeamComposition2] ControlPointTracker initialized with indicator and trigger.");
        }

        private void Update()
        {
            if (!initialized || triggerCollider == null)
            {
                return;
            }

            Vector2 center = triggerCollider.bounds.center;
            Vector2 halfExtents = triggerCollider.bounds.extents;

            ContactFilter2D filter = new ContactFilter2D
            {
                useLayerMask = true,
                layerMask = LayerMask.GetMask("Player")
            };
            overlappingPlayers.Clear();

            Collider2D[] results = new Collider2D[16];
            int hitCount = Physics2D.OverlapBox(center, halfExtents * 2f, 0f, filter, results);

            UnityEngine.Debug.Log($"[TeamComposition2] Control point collider hit {hitCount} players.");
            for (int i = 0; i < hitCount; i++)
            {
                Player player = results[i]?.GetComponentInParent<Player>();
                if (player == null)
                {
                    continue;
                }
                overlappingPlayers.Add(player);
            }

            if (overlappingPlayers.Count == 0)
            {
                ResetColor();
            }
            else if (overlappingPlayers.Count > 0)
            {
                UpdateIndicatorColor();
            }
        }

        private void UpdateIndicatorColor()
        {
            if (indicatorRenderer == null)
            {
                return;
            }

            if (overlappingPlayers.Count == 0)
            {
                ResetColor();
                return;
            }

            Player controllingPlayer = overlappingPlayers[0];
            Color teamColor = PlayerManager.instance.GetPlayersInTeam(controllingPlayer.teamID)
                .First()
                .GetTeamColors()
                .color;
            indicatorRenderer.color = teamColor;
            UpdateParticleColor(teamColor);
            UnityEngine.Debug.Log($"[TeamComposition2] Control point color set to team {controllingPlayer.teamID}.");
        }

        private void ResetColor()
        {
            if (indicatorRenderer != null)
            {
                indicatorRenderer.color = neutralColor;
                UpdateParticleColor(neutralColor);
                UnityEngine.Debug.Log("[TeamComposition2] Control point color reset to neutral.");
            }
        }

        private void UpdateParticleColor(Color color)
        {
            if (controlPointParticles == null)
            {
                return;
            }

            var main = controlPointParticles.main;
            main.startColor = color;

            if (controlPointParticles.colorOverLifetime.enabled)
            {
                var colorModule = controlPointParticles.colorOverLifetime;
                Gradient grad = new Gradient();
                grad.SetKeys(
                    new GradientColorKey[] { new GradientColorKey(color, 0f), new GradientColorKey(color, 1f) },
                    new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(0f, 1f) }
                );
                colorModule.color = grad;
            }
        }
    }
}

