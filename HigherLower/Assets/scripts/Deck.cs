using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    List<int> deck;
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
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    void Start()
    {
        Shuffle();
    }

}
