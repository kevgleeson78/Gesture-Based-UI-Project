using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CardStack))]
public class CardStackView : MonoBehaviour
{
    CardStack deck;
    Dictionary<int, GameObject> fetchCards;
    int lastCount;

    public Vector3 start;
    public float cardOffset;
    public bool faceUp = false;
    public GameObject cardPrefab;

    void Start()
    {
        fetchCards = new Dictionary<int, GameObject>();
        deck = GetComponent<CardStack>();

        ShowCards();
        lastCount = deck.CardCount;
        deck.CardRemoved += Deck_CardRemoved;
    }

    void Deck_CardRemoved(object sender, CardRemovedEventArgs e)
    {
        if (fetchCards.ContainsKey(e.CardIndex))
        {
            Destroy(fetchCards[e.CardIndex]);
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
            return;
        }
        GameObject cardCopy = (GameObject)Instantiate(cardPrefab);
        cardCopy.transform.position = position;

        Game game = cardCopy.GetComponent<Game>();
        game.cardIndex = cardIndex;
        game.ToggleFace(faceUp);

        SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = PositionalIndex;

        fetchCards.Add(cardIndex, cardCopy);
    }
}