using System.Collections.Generic;
using System.Linq;
using TeamComposition2.Bots.Utils;

namespace TeamComposition2.Bots.CardPickerAIs
{
    public class WeightedCardsPicker : ICardPickerAI
    {
        public List<IWeightedCardProcessor> CardProcessors { get; set; }

        public WeightedCardsPicker(List<IWeightedCardProcessor> cardProcessors)
        {
            CardProcessors = cardProcessors;
        }

        public WeightedCardsPicker() : this(GetDefaultWeightedCardProcessors()) { }

        public List<CardInfo> PickCard(List<CardInfo> cards)
        {
            Player player = PlayerManager.instance.players.Find(p => p.playerID == CardChoice.instance.pickrID);
            Dictionary<CardInfo, float> cardWeights = new Dictionary<CardInfo, float>();

            foreach (var card in cards)
            {
                float weight = 1f;
                foreach (var processor in CardProcessors)
                {
                    weight *= processor.GetWeight(card, player);
                    BotLoggerUtils.Log($"Card '{card.cardName}' processed by '{processor.GetType().Name}' with weight: {weight}");
                }
                cardWeights[card] = weight;
                BotLoggerUtils.Log($"Card '{card.cardName}' has weight: {weight}");
            }

            List<CardInfo> sortedCards = new List<CardInfo>(cardWeights.Keys);
            sortedCards.Sort((a, b) => cardWeights[b].CompareTo(cardWeights[a]));

            if (sortedCards.Count > 0)
            {
                return new List<CardInfo> { sortedCards[0] };
            }
            else
            {
                return new List<CardInfo>();
            }
        }

        public static List<IWeightedCardProcessor> GetDefaultWeightedCardProcessors()
        {
            var weightedCardProcessors = new List<IWeightedCardProcessor>
            {
                new RarityWeightedCardProcessor(0.25f),
                new StatsWeightedCardProcessor(1.25f),
                new ThemedWeightedCardProcessor(1.5f, 0.5f),
                new CurseWeightedCardProcessor(0.01f)
            };

            return weightedCardProcessors;
        }
    }
}
