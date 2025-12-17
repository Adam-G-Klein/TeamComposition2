using RWF.GameModes;

namespace TeamComposition2.GameModes
{
    public class CrownControlHandler : RWFGameModeHandler<GM_CrownControl>
    {
        internal const string GameModeName = "Point Control";
        internal const string GameModeID = "Point Control";
        public CrownControlHandler() : base(
            name: GameModeName,
            gameModeId: GameModeID,
            allowTeams: false,
            pointsToWinRound: 2,
            roundsToWinGame: 3,
            playersRequiredToStartGame: null,
            maxPlayers: null,
            maxTeams: null,
            maxClients: null,
            description: $"Free for all. Control the capture point for {UnityEngine.Mathf.RoundToInt(GM_CrownControl.secondsNeededToWin)} seconds to win. Respawns enabled.")
        {
        }
    }

    public class TeamCrownControlHandler : RWFGameModeHandler<GM_CrownControl>
    {
        internal const string GameModeName = "Team Point Control";
        internal const string GameModeID = "Team Point Control";
        public TeamCrownControlHandler() : base(
            name: GameModeName,
            gameModeId: GameModeID,
            allowTeams: true,
            pointsToWinRound: 2,
            roundsToWinGame: 5,
            playersRequiredToStartGame: null,
            maxPlayers: null,
            maxTeams: null,
            maxClients: null,
            description: $"Help your team hold the capture point for {UnityEngine.Mathf.RoundToInt(GM_CrownControl.secondsNeededToWin)} seconds to win. Respawns enabled.")
        {
        }
    }
}

