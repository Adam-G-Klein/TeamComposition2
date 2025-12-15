using Photon.Pun;
using RWF.GameModes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TeamComposition2.GameModes.Extensions;
using TeamComposition2.GameModes.Objects;
using UnboundLib;
using UnboundLib.Networking;
using UnityEngine;

namespace TeamComposition2.GameModes
{
    /// <summary>
    /// Crown Control game mode ported from Game Mode Collection.
    /// Players fight over a crown; holding it accumulates time for the holder's team.
    /// Respawns are enabled and the crown respawns if it falls out of bounds or stays unattended.
    /// </summary>
    public class GM_CrownControl : RWFGameMode
    {
        internal static GM_CrownControl instance;

        internal const float secondsNeededToWin = 20f;

        private const float crownAngularVelocityMult = 10f;

        private const float delayPenaltyPerDeath = 1f;
        private const float baseRespawnDelay = 1f;
        private static readonly Vector2 crownSpawn = new Vector2(0.5f, 1f);
        private readonly List<int> awaitingRespawn = new List<int>();

        private CrownHandler crown;

        private Coroutine crownControlRoutine;

        private readonly Dictionary<int, int> deathsThisBattle = new Dictionary<int, int>();
        private Dictionary<int, float> teamHeldFor = new Dictionary<int, float>();

        protected override void Awake()
        {
            instance = this;
            base.Awake();
        }

        protected override void Start()
        {
            _ = CrownPrefab.Crown;
            base.Start();
        }

        private void ResetForBattle()
        {
            crown.Reset();
            ResetDeaths();
            ResetHeldFor();
        }

        private void ResetHeldFor()
        {
            teamHeldFor.Clear();
            foreach (int tID in PlayerManager.instance.players.Select(p => p.teamID).Distinct())
            {
                teamHeldFor[tID] = 0f;
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
                thisDist += Vector2.Distance(crown.transform.position, spawn);
                if (thisDist > dist)
                {
                    dist = thisDist;
                    best = spawn;
                }
            }
            return best;
        }

        public void SetCrown(CrownHandler crownHandler)
        {
            crown = crownHandler;
        }
        public void DestroyCrown()
        {
            if (crown != null)
            {
                UnityEngine.Object.DestroyImmediate(crown);
            }
        }

        public override IEnumerator DoStartGame()
        {
            CrownHandler.DestroyCrown();

            yield return WaitForSyncUp();

            yield return new WaitForEndOfFrame();

            yield return CrownHandler.MakeCrownHandler();

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

            if (killedPlayer.playerID == crown.CrownHolder)
            {
                teamHeldFor[killedPlayer.teamID] += crown.HeldFor;

                crown.GiveCrownToPlayer(-1);
                crown.SetVel((Vector2)killedPlayer.data.playerVel.GetFieldValue("velocity"));
                crown.SetAngularVel(-crownAngularVelocityMult * ((Vector2)killedPlayer.data.playerVel.GetFieldValue("velocity")).x);
                crown.AddRandomAngularVelocity();
            }

            SyncHeldFor();

            awaitingRespawn.Add(killedPlayer.playerID);
            StartCoroutine(IRespawnPlayer(killedPlayer, delayPenaltyPerDeath * (deathsThisBattle[killedPlayer.playerID] - 1) + baseRespawnDelay));
        }

        public IEnumerator IRespawnPlayer(Player player, float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            if (awaitingRespawn.Contains(player.playerID))
            {
                player.transform.position = GetFarthestSpawn(player.teamID);
                player.data.healthHandler.Revive(true);
                player.GetComponent<GeneralInput>().enabled = true;
                awaitingRespawn.Remove(player.playerID);
            }
        }
        public override IEnumerator DoRoundStart()
        {
            ResetForBattle();
            crownControlRoutine = StartCoroutine(DoCrownControl());
            yield return base.DoRoundStart();
            SpawnCrown();
        }
        public override IEnumerator DoPointStart()
        {
            ResetForBattle();
            crownControlRoutine = StartCoroutine(DoCrownControl());
            yield return base.DoPointStart();
            SpawnCrown();
        }

        private IEnumerator DoCrownControl()
        {
            while (true)
            {

                int? winningTeamID = null;

                foreach (int tID in teamHeldFor.Keys.ToList())
                {
                    float time = teamHeldFor[tID] + ((crown.CrownHolder != -1 && PlayerManager.instance.players[crown.CrownHolder].teamID == tID) ? crown.HeldFor : 0f);

                    UIHandler.instance.roundCounterSmall.UpdateClock(
                        tID,
                        Mathf.Clamp(time, 0f, secondsNeededToWin) / secondsNeededToWin);

                    if (time > secondsNeededToWin)
                    {
                        winningTeamID = tID;
                        break;
                    }
                }
                if (winningTeamID == null && crown.TooManyRespawns)
                {
                    winningTeamID = -1;
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
                    CallEndRound();
                    NetworkingManager.RPC(
                        typeof(GM_CrownControl),
                        nameof(RPCA_NextRound),
                        winningTeamID != -1 ? new int[] { (int)winningTeamID } : new int[] { },
                        teamPoints,
                        teamRounds
                    );
                    break;
                }
                yield return null;
            }
        }

        private void CallEndRound()
        {
            if (PhotonNetwork.OfflineMode || PhotonNetwork.IsMasterClient)
            {
                NetworkingManager.RPC(typeof(GM_CrownControl), nameof(RPCA_EndRound));
            }
        }
        [UnboundRPC]
        private static void RPCA_EndRound()
        {
            instance.awaitingRespawn.Clear();
            TimeHandler.instance.DoSlowDown();
            if (instance.crownControlRoutine != null)
            {
                instance.StopCoroutine(instance.crownControlRoutine);
            }
        }

        private void SpawnCrown()
        {
            if (PhotonNetwork.OfflineMode || PhotonNetwork.IsMasterClient)
            {
                NetworkingManager.RPC(typeof(GM_CrownControl), nameof(RPCA_SpawnCrown), crownSpawn + new Vector2(UnityEngine.Random.Range(-0.01f, 0.01f), 0f));
            }
        }

        [UnboundRPC]
        private static void RPCA_ResetCrown()
        {
            instance.crown.Reset();
        }

        [UnboundRPC]
        private static void RPCA_SpawnCrown(Vector2 spawnPos)
        {
            instance.crown.Spawn(spawnPos);
        }
    }
}

