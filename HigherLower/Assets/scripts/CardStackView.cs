using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CardStack))]
public class CardStackView : MonoBehaviour
{
    CardStack deck;
    Dictionary<int, CardView> fetchCards;
    
    int lastCount;

    public Vector3 start;
    public float cardOffset;
    public bool faceUp = false;
    public bool reverseLayerOrder = false;
    public GameObject cardPrefab;


    public void Toggle(int card, bool isFaceUp)
    {
        fetchCards[card].IsFaceUp = isFaceUp;
    }
    void Awake()
    {
        fetchCards = new Dictionary<int, CardView>();
        deck = GetComponent<CardStack>();

        ShowCards();
        lastCount = deck.CardCount;
        deck.CardRemoved += deck_CardRemoved;
        deck.cardAdded += deck_CardAdded;
    }

    private void deck_CardAdded(object sender, CardEventArgs e)
    {
        float co = cardOffset * deck.CardCount;
        Vector3 temp = start + new Vector3(co, 0f);
        AddCard(temp, e.CardIndex, deck.CardCount);

    }

    void deck_CardRemoved(object sender, CardEventArgs e)
    {
        if (fetchCards.ContainsKey(e.CardIndex))
        {
            Destroy(fetchCards[e.CardIndex].Card);
            fetchCards.Remove(e.CardIndex);
        }
    }

    private void Update()
    {
        if (lastCount != deck.CardCount)
        {
            lastCount = deck.CardCount;
            ShowCards();
        }
        
    }
    void ShowCards()
    {
        int cardCount = 0;
        if (deck.HasCards)
        {
            foreach (int i in deck.GetCards())
            {
                float co = cardOffset * cardCount;
               

                Vector3 temp = start + new Vector3(co, 0f);
                AddCard(temp, i, cardCount);


                cardCount++;
            }
        }

    }

    void AddCard(Vector3 position, int cardIndex, int PositionalIndex)
    {

        
        if (fetchCards.ContainsKey(cardIndex))
        {
            if (!faceUp)
            {
                CardModel cardModel = fetchCards[cardIndex].Card.GetComponent<CardModel>();
                cardModel.ToggleFace(fetchCards[cardIndex].IsFaceUp);
            }
            return;
        }
        GameObject cardCopy = (GameObject)Instantiate(cardPrefab);
        cardCopy.transform.position = position;

        CardModel model = cardCopy.GetComponent<CardModel>();
        model.cardIndex = cardIndex;

        model.ToggleFace(faceUp);
      
        SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer>();
        if (reverseLayerOrder)
        {
            spriteRenderer.sortingOrder = 51 - PositionalIndex;
        }
        else
        {
            spriteRenderer.sortingOrder = PositionalIndex;
        }
        

        fetchCards.Add(cardIndex, new CardView(cardCopy));
        Debug.Log("Hand value  = " + deck.HandValue());
    }
}