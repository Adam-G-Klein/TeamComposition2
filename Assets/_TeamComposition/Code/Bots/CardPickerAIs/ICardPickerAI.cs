using System.Collections.Generic;

namespace TeamComposition2.Bots.CardPickerAIs
{
    public interface ICardPickerAI
    {
        List<CardInfo> PickCard(List<CardInfo> cards);
    }
}
