using Photon.Pun;
using RWF.GameModes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TeamComposition2.GameModes.Extensions;
using TeamComposition2;
using UnboundLib;
using UnboundLib.Networking;
using UnityEngine;

namespace TeamComposition2.GameModes
{
    /// <summary>
    /// Point Control game mode.
    /// Teams fight over a static capture zone, gaining progress while their team controls the area.
    /// </summary>
    public class GM_CrownControl : RWFGameMode
    {
        internal static GM_CrownControl instance;

        internal static float secondsNeededToWin = 20f;
        internal const float defaultSecondsNeededToWin = 20f;

        private const float delayPenaltyPerDeath = 1f;
        private const float baseRespawnDelay = 1f;
        private readonly List<int> awaitingRespawn = new List<int>();

        private readonly Dictionary<int, int> deathsThisBattle = new Dictionary<int, int>();
        private Dictionary<int, float> teamHeldFor = new Dictionary<int, float>();
        private int controllingTeam = -1;

        protected override void Awake()
        {
            instance = this;
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();
        }

        private void ResetForBattle()
        {
            ResetDeaths();
            ResetHeldFor();
            controllingTeam = -1;
        }

        private void ResetHeldFor()
        {
            var teams = PlayerManager.instance.players.Select(p => p.teamID).Distinct().ToArray();
            foreach (int team in teams)
            {
                if (teamHeldFor.ContainsKey(team))
                {
                    teamHeldFor[team] = 0f;
                }
            }
        }

        private void SyncHeldFor()
        {
            if (PhotonNetwork.OfflineMode || PhotonNetwork.IsMasterClient)
            {
                NetworkingManager.RPC_Others(typeof(GM_CrownControl), nameof(RPCA_SyncHeldFor), teamHeldFor);
            }
        }

        [UnboundRPC]
        private static void RPCA_SyncHeldFor(Dictionary<int, float> teamHeldFor)
        {
            instance.teamHeldFor = teamHeldFor.ToDictionary(kv => kv.Key, kv => kv.Value);
        }

        private void ResetDeaths()
        {
            deathsThisBattle.Clear();
            for (int i = 0; i < PlayerManager.instance.players.Count(); i++)
            {
                deathsThisBattle[i] = 0;
            }
            awaitingRespawn.Clear();
        }

        private Vector3 GetFarthestSpawn(int teamID)
        {
            Vector3[] spawns = MapManager.instance.GetSpawnPoints().Select(s => s.localStartPos).ToArray();
            float dist = -1f;
            Vector3 best = Vector3.zero;
            foreach (Vector3 spawn in spawns)
            {
                float thisDist = PlayerManager.instance.players.Where(p => !p.data.dead && p.teamID != teamID)
                    .Select(p => Vector3.Distance(p.transform.position, spawn)).Sum();
                if (thisDist > dist)
                {
                    dist = thisDist;
                    best = spawn;
                }
            }
            return best;
        }

        public override IEnumerator DoStartGame()
        {
            yield return WaitForSyncUp();

            yield return new WaitForEndOfFrame();

            yield return WaitForSyncUp();

            ResetForBattle();

            yield return base.DoStartGame();
        }

        public override void PlayerJoined(Player player)
        {
            deathsThisBattle[player.playerID] = 0;
            base.PlayerJoined(player);
        }

        public override void PlayerDied(Player killedPlayer, int teamsAlive)
        {
            if (awaitingRespawn.Contains(killedPlayer.playerID))
            {
                return;
            }

            deathsThisBattle[killedPlayer.playerID]++;

            SyncHeldFor();

            awaitingRespawn.Add(killedPlayer.playerID);
            StartCoroutine(IRespawnPlayer(killedPlayer, delayPenaltyPerDeath * (deathsThisBattle[killedPlayer.playerID] - 1) + baseRespawnDelay));
        }

        public IEnumerator IRespawnPlayer(Player player, float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            if (awaitingRespawn.Contains(player.playerID))
            {
                Vector3 respawnPos;
                if (!TeamSpawnManager.TryGetTeamSpawn(player.teamID, out respawnPos))
                {
                    respawnPos = GetFarthestSpawn(player.teamID);
                }

                player.transform.position = respawnPos;
                player.data.healthHandler.Revive(true);
                player.GetComponent<GeneralInput>().enabled = true;
                awaitingRespawn.Remove(player.playerID);
            }
        }
        public override IEnumerator DoRoundStart()
        {
            ResetForBattle();
            StartCoroutine(DoCrownControl());
            yield return base.DoRoundStart();
        }
        public override IEnumerator DoPointStart()
        {
            ResetForBattle();
            StartCoroutine(DoCrownControl());
            yield return base.DoPointStart();
        }

        private IEnumerator DoCrownControl()
        {
            while (true)
            {

                int? winningTeamID = null;

                var currentTeams = PlayerManager.instance.players.Select(p => p.teamID).Distinct().ToArray();
                foreach (int team in currentTeams)
                {
                    if (!teamHeldFor.ContainsKey(team))
                    {
                        teamHeldFor[team] = 0f;
                    }
                }

                if (controllingTeam >= 0)
                {
                    teamHeldFor[controllingTeam] += TimeHandler.deltaTime;
                }

                foreach (int tID in teamHeldFor.Keys.ToList())
                {
                    float time = teamHeldFor[tID];

                    UIHandler.instance.roundCounterSmall.UpdateClock(
                        tID,
                        Mathf.Clamp(time, 0f, secondsNeededToWin) / secondsNeededToWin);

                    if (time > secondsNeededToWin)
                    {
                        winningTeamID = tID;
                        break;
                    }
                }
                if (winningTeamID != null && (PhotonNetwork.IsMasterClient || PhotonNetwork.OfflineMode))
                {
                    foreach (Player player in PlayerManager.instance.players.Where(p => !p.data.dead && p.teamID != winningTeamID))
                    {
                        player.data.view.RPC("RPCA_Die", RpcTarget.All, new object[]
                        {
                            new Vector2(0, 1)
                        });
                    }
                    yield return null;
                    awaitingRespawn.Clear();
                    TimeHandler.instance.DoSlowDown();
                    ResetHeldFor();
                    SyncHeldFor();
                    NetworkingManager.RPC(
                        typeof(GM_CrownControl),
                        nameof(RPCA_NextRound),
                        new int[] { (int)winningTeamID },
                        teamPoints,
                        teamRounds
                    );
                    break;
                }
                yield return null;
            }
        }

        internal void SetControllingTeam(int teamID)
        {
            controllingTeam = teamID;
        }

    }
}

