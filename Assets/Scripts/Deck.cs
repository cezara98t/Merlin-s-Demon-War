using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Deck
{
    public List<CardData> cardDatas = new List<CardData>();

    public void Create()
    {
        // create all the cards from a deck
        List<CardData> allCardDatas = new List<CardData>();
        foreach(CardData cardData in GameController.instance.cards)
        {
            for(int i=0; i<cardData.numberInDeck; i++)
            {
                allCardDatas.Add(cardData);
            }
        }

        // randomize the created cards
        while (allCardDatas.Count > 0)
        {
            int rand = UnityEngine.Random.Range(0, allCardDatas.Count);
            cardDatas.Add(allCardDatas[rand]);
            allCardDatas.RemoveAt(rand);
        }
    }
}
