namespace TeamComposition2.Bots.CardPickerAIs
{
    public interface IWeightedCardProcessor
    {
        float GetWeight(CardInfo card, Player player);
    }
}
