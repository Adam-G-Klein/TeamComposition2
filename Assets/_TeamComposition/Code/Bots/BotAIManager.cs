using HarmonyLib;
using ModdingUtils.GameModes;
using ModdingUtils.Utils;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TeamComposition2.Bots.CardPickerAIs;
using TeamComposition2.Bots.Extensions;
using TeamComposition2.Bots.Utils;
using TMPro;
using UnboundLib;
using UnboundLib.GameModes;
using UnboundLib.Networking;
using UnityEngine;

namespace TeamComposition2.Bots
{
    public class BotAIManager : MonoBehaviour, IPlayerPickStartHookHandler, IGameStartHookHandler, IPointStartHookHandler, IRoundEndHookHandler
    {
        public static BotAIManager Instance;

        public Dictionary<int, CardPickerAI> PickerAIs = new Dictionary<int, CardPickerAI>();

        private Coroutine stalemateHandlerCoroutine;

        void Start()
        {
            InterfaceGameModeHooksManager.instance.RegisterHooks(this);

            DontDestroyOnLoad(this);
            Instance = this;
        }

        public void SetBotsId()
        {
            BotLoggerUtils.Log("Getting bots player.");

            PickerAIs.Clear();
            List<int> botsIds = PlayerManager.instance.players
                     .Where(player => player.data.GetAdditionalData().IsBot)
                     .Select(player => player.playerID)
                     .ToList();

            PickerAIs = botsIds.ToDictionary(id => id, id => new CardPickerAI());
            if (PickerAIs.Count > 1)
            {
                PickerAIs[0].cardPickerAI = new WeightedCardsPicker();
            }

            botsIds.ForEach(id => BotLoggerUtils.Log($"Bot '{id}' has been added to the list of bots id."));
            BotLoggerUtils.Log("Successfully get list of bots player.");
        }

        public void OnPlayerPickStart()
        {
            StartCoroutine(Instance.AiPickCard());
        }

        public void OnGameStart()
        {
            Instance.SetBotsId();
            foreach (var bot in PickerAIs)
            {
                Player player = PlayerManager.instance.players.First(p => p.playerID == bot.Key);
                player.GetComponentInChildren<PlayerName>().GetComponent<TextMeshProUGUI>().text = "<#07e0f0>[BOT]";
            }
        }

        public void OnPointStart()
        {
            if (stalemateHandlerCoroutine != null)
            {
                StopCoroutine(stalemateHandlerCoroutine);
            }

            stalemateHandlerCoroutine = StartCoroutine(StalemateHandler.HandleStalemate());
        }

        public void OnRoundEnd()
        {
            int maxRounds = (int)GameModeManager.CurrentHandler.Settings["roundsToWinGame"];
            var teams = PlayerManager.instance.players.Select(p => p.teamID).Distinct();
            int? winnerTeam = teams.Select(id => (int?)id).FirstOrDefault(id => GameModeManager.CurrentHandler.GetTeamScore(id.Value).rounds >= maxRounds);

            bool isAllBot = PlayerManager.instance.players.All(p => p.data.GetAdditionalData().IsBot
                || ModdingUtils.AIMinion.Extensions.CharacterDataExtension.GetAdditionalData(p.data).isAIMinion);

            if (winnerTeam != null && isAllBot)
            {
                StartCoroutine(Rematch());
            }
        }

        public IEnumerator Rematch()
        {
            yield return new WaitForSeconds(1f);

            if (GameManager.instance.isPlaying)
            {
                // RoundEndHandler lives in the RoundsWithFriends assembly and is internal there,
                // so we trigger its static Rematch method via reflection to avoid a hard compile-time reference.
                var roundEndHandlerType = AccessTools.TypeByName("RWF.RoundEndHandler");
                var rematchMethod = roundEndHandlerType != null ? AccessTools.Method(roundEndHandlerType, "Rematch") : null;

                if (rematchMethod != null)
                {
                    rematchMethod.Invoke(null, new object[0]);
                }
                else
                {
                    BotLoggerUtils.Log("Could not find RWF.RoundEndHandler.Rematch; skipping auto-rematch.");
                }
            }
        }

        #region Card Picking

        public List<GameObject> GetSpawnCards()
        {
            BotLoggerUtils.Log("Getting spawn cards");
            return (List<GameObject>)AccessTools.Field(typeof(CardChoice), "spawnedCards").GetValue(CardChoice.instance);
        }

        public IEnumerator CycleThroughCards(float delay, List<GameObject> spawnedCards)
        {
            BotLoggerUtils.Log("Cycling through cards");

            CardInfo lastCardInfo = null;
            int index = 0;

            foreach (var cardObject in spawnedCards)
            {
                CardInfo cardInfo = cardObject.GetComponent<CardInfo>();

                BotLoggerUtils.Log($"Cycling through '${cardInfo.cardName}' card");
                if (lastCardInfo != null)
                {
                    lastCardInfo.RPCA_ChangeSelected(false);
                }
                cardInfo.RPCA_ChangeSelected(true);
                AccessTools.Field(typeof(CardChoice), "currentlySelectedCard").SetValue(CardChoice.instance, index);

                lastCardInfo = cardInfo;
                index++;
                yield return new WaitForSeconds(delay);
            }
            BotLoggerUtils.Log("Successfully gone through all cards");
            yield break;
        }

        public IEnumerator GoToCards(GameObject selectedCards, List<GameObject> spawnedCards, float delay)
        {
            BotLoggerUtils.Log($"Going to '${selectedCards}' card");

            int selectedCardIndex = spawnedCards.IndexOf(selectedCards);
            int handIndex = int.Parse(AccessTools.Field(typeof(CardChoice), "currentlySelectedCard").GetValue(CardChoice.instance).ToString());

            while (handIndex != selectedCardIndex)
            {
                CardInfo cardInfo = spawnedCards[handIndex].GetComponent<CardInfo>();
                cardInfo.RPCA_ChangeSelected(false);
                BotLoggerUtils.Log($"Currently on '${cardInfo}' card");
                if (handIndex > selectedCardIndex)
                {
                    handIndex--;
                }
                else if (handIndex < selectedCardIndex)
                {
                    handIndex++;
                }
                cardInfo = spawnedCards[handIndex].GetComponent<CardInfo>();
                cardInfo.RPCA_ChangeSelected(true);
                AccessTools.Field(typeof(CardChoice), "currentlySelectedCard").SetValue(CardChoice.instance, handIndex);

                yield return new WaitForSeconds(delay);
            }
            BotLoggerUtils.Log($"Successfully got to '${selectedCards}' card");
            yield break;
        }

        public void PickCard(List<GameObject> spawnCards)
        {
            CardChoice.instance.Pick(spawnCards[(int)CardChoice.instance.GetFieldValue("currentlySelectedCard")], true);
        }

        public IEnumerator AiPickCard()
        {
            if (PhotonNetwork.IsMasterClient || PhotonNetwork.OfflineMode)
            {
                yield return new WaitUntil(() =>
                {
                    return CardChoice.instance.IsPicking &&
                   ((List<GameObject>)CardChoice.instance.GetFieldValue("spawnedCards")).Count == ((Transform[])CardChoice.instance.GetFieldValue("children")).Count() &&
                   !((List<GameObject>)CardChoice.instance.GetFieldValue("spawnedCards")).Any(card => { return card == null; });
                });

                for (int i = 0; i < PlayerManager.instance.players.Count; i++)
                {
                    Player player = PlayerManager.instance.players[i];
                    if (PickerAIs.ContainsKey(CardChoice.instance.pickrID))
                    {
                        BotLoggerUtils.Log("AI picking card");
                        List<GameObject> spawnCards = GetSpawnCards();
                        spawnCards[0].GetComponent<CardInfo>().GetComponent<PhotonView>().RPC("RPCA_ChangeSelected", RpcTarget.All, true);

                        ICardPickerAI botCardPickerAI = PickerAIs[CardChoice.instance.pickrID].cardPickerAI;
                        StartCardsPicking(spawnCards, botCardPickerAI, PickerAIs[CardChoice.instance.pickrID].pickerInfo);

                        break;
                    }
                }
                yield break;
            }
        }

        public void StartCardsPicking(List<GameObject> spawnCards, ICardPickerAI botCardPickerAI, PickerInfo pickerInfo)
        {
            if (botCardPickerAI == null)
            {
                BotLoggerUtils.Log("Bot card picker AI is null, Skipping card picking");
                return;
            }

            List<CardInfo> cards = spawnCards.Select(card => card.GetComponent<CardInfo>()).ToList();
            List<CardInfo> pickCards = botCardPickerAI.PickCard(cards);

            CardInfo pickCard = pickCards.ElementAt(Random.Range(0, pickCards.Count));
            int pickCardIndex = cards.IndexOf(pickCard);

            NetworkingManager.RPC(typeof(BotAIManager), nameof(RPCA_PickCardsAtPosition), pickCardIndex, pickerInfo.CycleDelay, pickerInfo.PreCycleDelay, pickerInfo.GoToCardDelay, pickerInfo.PickDelay);
        }

        private IEnumerator PickCardsAtPosition(int position, float cycleDelay, float preCycleDelay, float goToCardDelay, float pickDelay)
        {
            List<GameObject> spawnCards = GetSpawnCards();

            yield return CycleThroughCards(cycleDelay, spawnCards);
            yield return new WaitForSeconds(preCycleDelay);

            yield return GoToCards(spawnCards[position], spawnCards, goToCardDelay);
            yield return new WaitForSeconds(pickDelay);

            PickCard(spawnCards);

            yield break;
        }

        [UnboundRPC]
        private static void RPCA_PickCardsAtPosition(int position, float cycleDelay, float preCycleDelay, float goToCardDelay, float pickDelay)
        {
            Instance.StartCoroutine(Instance.PickCardsAtPosition(position, cycleDelay, preCycleDelay, goToCardDelay, pickDelay));
        }

        #endregion

        #region Stalemate Handler

        internal static class StalemateHandler
        {
            public static bool IsStalemate
            {
                get
                {
                    bool isPlayersAlive = PlayerManager.instance.players
                        .Any(player => !player.data.GetAdditionalData().IsBot
                        && !ModdingUtils.AIMinion.Extensions.CharacterDataExtension.GetAdditionalData(player.data).isAIMinion
                        && PlayerStatus.PlayerAliveAndSimulated(player));

                    return !isPlayersAlive;
                }
            }

            public static IEnumerator HandleStalemate()
            {
                if (PhotonNetwork.IsMasterClient || PhotonNetwork.OfflineMode)
                {
                    yield return new WaitForSeconds(2f);

                    while (!IsStalemate)
                    {
                        yield return new WaitForSeconds(0.5f);
                    }

                    yield return new WaitForSeconds(BotMenu.StalemateTimer.Value);

                    while (IsStalemate)
                    {
                        yield return new WaitForSeconds(BotMenu.StalemateDamageCooldown.Value);

                        Player[] aliveBots = PlayerManager.instance.players
                            .Where(player => PlayerStatus.PlayerAliveAndSimulated(player))
                            .ToArray();

                        if (aliveBots.Length == 0)
                        {
                            yield break;
                        }

                        Player targetBot = aliveBots[Random.Range(0, aliveBots.Length)];
                        NetworkingManager.RPC(typeof(StalemateHandler), nameof(RPCA_SendTakeDamageOverTime), targetBot.data.view.ControllerActorNr, targetBot.playerID, targetBot.data.maxHealth, BotMenu.StalemateDamageDuration.Value);
                    }
                }
                yield break;
            }

            [UnboundRPC]
            public static void RPCA_SendTakeDamageOverTime(int actorID, int playerID, float damage, float duration)
            {
                Player player = FindPlayer.GetPlayerWithActorAndPlayerIDs(actorID, playerID);
                player.data.healthHandler.TakeDamageOverTime(damage * Vector2.down, player.gameObject.transform.position, duration, 0.5f, Color.white);
            }
        }

        #endregion
    }

    public class PickerInfo
    {
        public float CycleDelay;
        public float PreCycleDelay;
        public float GoToCardDelay;
        public float PickDelay;

        public PickerInfo(float cycleDelay, float preCycleDelay, float goToCardDelay, float pickDelay)
        {
            CycleDelay = cycleDelay;
            PreCycleDelay = preCycleDelay;
            GoToCardDelay = goToCardDelay;
            PickDelay = pickDelay;
        }

        public PickerInfo()
        {
            CycleDelay = BotMenu.CycleDelay.Value;
            PreCycleDelay = BotMenu.PreCycleDelay.Value;
            GoToCardDelay = BotMenu.GoToCardDelay.Value;
            PickDelay = BotMenu.PickDelay.Value;
        }
    }

    public class CardPickerAI
    {
        public ICardPickerAI cardPickerAI;
        public PickerInfo pickerInfo;

        public CardPickerAI(ICardPickerAI cardPickerAI, PickerInfo pickerInfo)
        {
            this.cardPickerAI = cardPickerAI;
            this.pickerInfo = pickerInfo;
        }

        public CardPickerAI()
        {
            cardPickerAI = new WeightedCardsPicker();
            pickerInfo = new PickerInfo();
        }
    }
}
