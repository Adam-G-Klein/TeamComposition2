using System.Collections;
using System.Collections.Generic;
using UnboundLib.Cards;
using UnityEngine;
using TeamComposition2;

public class CardHolder : MonoBehaviour
{
    public List<CardInfo> cards;
    public List<CardInfo> hiddenCards;
    internal void RegisterCards()
    {
        foreach (var card in cards)
        {
            UnityEngine.Debug.Log("Teamcomposition: registered card: " + card.cardName);
            CustomCard.RegisterUnityCard(card.gameObject, MyPlugin.modInitials, card.cardName, true, null);
        }

        foreach (var card in hiddenCards)
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
