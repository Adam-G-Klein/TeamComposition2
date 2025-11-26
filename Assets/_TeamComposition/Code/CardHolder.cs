using System.Collections;
using System.Collections.Generic;
using UnboundLib.Cards;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    public List<CardInfo> cards;
    public List<CardInfo> hiddenCards;
    internal void RegisterCards()
    {
        foreach (var card in cards)
        {
            CustomCard.RegisterUnityCard(card.gameObject, MyPlugin.modInitials, card.cardName, true, null);
        }

        foreach (var card in cards)
        {
            CustomCard.RegisterUnityCard(card.gameObject, MyPlugin.modInitials, card.cardName, false, null);
            ModdingUtils.Utils.Cards.instance.AddHiddenCard(card);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
