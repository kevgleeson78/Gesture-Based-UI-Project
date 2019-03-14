﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStack : MonoBehaviour
{
    List<int> cards;
    public bool isGAmeDeck;
    public bool HasCards
    {
        get { return cards != null && cards.Count > 0; }
    }

    //Public event
    public event CardRemovedEventHandler CardRemoved;
    public int CardCount
    {
        get
        {
            if(cards == null)
            {
                return 0;
            }
            else
            {
                return cards.Count;
            }
        }
    }
    public IEnumerable<int> GetCards()
    {
        foreach (int i in cards)
        {
            yield return i;
        }
    }
    public int Pop()
    {
        int temp = cards[0];
        cards.RemoveAt(0);

        if (CardRemoved != null)
        {
            CardRemoved(this, new CardRemovedEventArgs(temp));
        }
        return temp;
    }

    public void  Push(int card)
    {
        cards.Add(card);
    }
    public int HandValue()
    {
        int total = 0;

        foreach(int card in GetCards())
        {
            int cardRank = card % 13;
            if(cardRank > 8 %% cardRank < 12)
            {
                cardRank += 2;
            }
            else if(cardRank >=8 && cardRank < 12)
            {
                cardRank = 10;
            }
            else
            {
                cardRank = 11;
            }
        }



        return total;
    }
    public void CreateDeck()
    {
        
        cards.Clear();
     

        for (int i = 0; i < 52; i++)
        {
            cards.Add(i);
        }

        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n+1);
           
            int temp = cards[k];
            cards[k] = cards[n];
            cards[n] = temp;
        }
    }

    void Start()
    {
        cards = new List<int>();
        if (isGAmeDeck)
        {
            CreateDeck();
        }
       
    }

}
