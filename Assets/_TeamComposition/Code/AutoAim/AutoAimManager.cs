using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TeamComposition2.AutoAim
{
    public static class AutoAimManager
    {
        private static readonly HashSet<int> autoAimingPlayerIds = new HashSet<int>();

        public static void EnableAutoAim(Player player)
        {
            if (player != null)
            {
                autoAimingPlayerIds.Add(player.playerID);
                UnityEngine.Debug.Log($"[TeamComposition2] Auto-aim enabled for player {player.playerID}");
            }
        }

        public static void DisableAutoAim(Player player)
        {
            if (player != null)
            {
                autoAimingPlayerIds.Remove(player.playerID);
                UnityEngine.Debug.Log($"[TeamComposition2] Auto-aim disabled for player {player.playerID}");
            }
        }

        public static void ToggleAutoAim(Player player)
        {
            if (player == null) return;

            if (IsAutoAiming(player))
            {
                DisableAutoAim(player);
            }
            else
            {
                EnableAutoAim(player);
            }
        }

        public static bool IsAutoAiming(Player player)
        {
            return player != null && autoAimingPlayerIds.Contains(player.playerID);
        }

        public static void ClearAll()
        {
            autoAimingPlayerIds.Clear();
        }

        /// <summary>
        /// Gets the closest valid target for the given player.
        /// Valid targets are all players who are NOT on the same team and are alive.
        /// </summary>
        public static Player GetClosestTarget(Player sourcePlayer)
        {
            if (sourcePlayer == null || PlayerManager.instance == null)
            {
                return null;
            }

            Player closestTarget = null;
            float closestDistance = float.MaxValue;

            foreach (Player potentialTarget in PlayerManager.instance.players)
            {
                // Skip self
                if (potentialTarget.playerID == sourcePlayer.playerID)
                {
                    continue;
                }

                // Skip teammates
                if (potentialTarget.teamID == sourcePlayer.teamID)
                {
                    continue;
                }

                // Skip dead players
                if (potentialTarget.data.dead)
                {
                    continue;
                }

                // Calculate distance
                float distance = Vector3.Distance(sourcePlayer.transform.position, potentialTarget.transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestTarget = potentialTarget;
                }
            }

            return closestTarget;
        }

        /// <summary>
        /// Gets the aim direction towards the closest valid target.
        /// Returns Vector3.zero if no valid target exists.
        /// </summary>
        public static Vector3 GetAutoAimDirection(Player sourcePlayer)
        {
            Player target = GetClosestTarget(sourcePlayer);

            if (target == null)
            {
                return Vector3.zero;
            }

            Vector3 direction = target.transform.position - sourcePlayer.transform.position;
            direction.z = 0f;
            return direction.normalized;
        }
    }
}
