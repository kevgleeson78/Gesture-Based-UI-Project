using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    List<int> deck;
    public IEnumerable<int> GetCards()
    {
        foreach (int i in deck)
        {
            yield return i;
        }
    }
    public void Shuffle()
    {
        if(deck == null)
        {
            deck = new List<int>();
        }
        else
        {
            deck.Clear();
        }

        for (int i = 0; i < 52; i++)
        {
            deck.Add(i);
        }

        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n+1);
           
            int temp = deck[k];
            deck[k] = deck[n];
            deck[n] = temp;
        }
    }

    void Start()
    {
        Shuffle();
    }

}
