using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TeamComposition2
{
    /// <summary>
    /// Tracks which spawn point a team was assigned so respawns stay consistent.
    /// </summary>
    internal static class TeamSpawnManager
    {
        private static readonly Dictionary<int, Vector3> teamSpawns = new Dictionary<int, Vector3>();

        public static void Reset()
        {
            teamSpawns.Clear();
        }

        public static void SetTeamSpawn(int teamId, Vector3 position)
        {
            if (!teamSpawns.ContainsKey(teamId))
            {
                teamSpawns[teamId] = position;
            }
        }

        public static bool TryGetTeamSpawn(int teamId, out Vector3 position)
        {
            return teamSpawns.TryGetValue(teamId, out position);
        }

        public static void SetFromPlayers(IEnumerable<Player> players, IList<Vector2> positions)
        {
            // positions are indexed by playerID in the spawn array
            foreach (Player player in players)
            {
                if (player == null) continue;
                int playerId = player.playerID;
                if (playerId < 0 || playerId >= positions.Count) continue;

                SetTeamSpawn(player.teamID, positions[playerId]);
            }
        }
    }
}

