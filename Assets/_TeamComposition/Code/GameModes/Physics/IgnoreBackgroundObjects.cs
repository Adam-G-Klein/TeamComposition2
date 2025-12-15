using UnityEngine;

namespace TeamComposition2.GameModes.Physics
{
    /// <summary>
    /// Simple helper that mirrors the collider transform so we can hide objects behind the background shader,
    /// mirroring the behavior from Game Mode Collection.
    /// </summary>
    class IgnoreBackgroundObjects : MonoBehaviour
    {
        private Collider2D trackedCollider;

        public void SetColliderToTrack(Collider2D collider)
        {
            trackedCollider = collider;
        }

        private void LateUpdate()
        {
            if (trackedCollider == null)
            {
                return;
            }

            transform.position = trackedCollider.transform.position;
            transform.rotation = trackedCollider.transform.rotation;
        }
    }
}

