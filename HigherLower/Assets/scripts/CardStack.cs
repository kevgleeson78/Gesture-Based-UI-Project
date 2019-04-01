using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Title: Gesture Based UI Development Project
 * Discription: Script to handle the card array and shuffling the deck
 *              along with the cards that are face up or down.
 * Adapted from: https://www.youtube.com/watch?v=FxH8FoddkVY&list=PLZo2FfoMkJeE6GXx8cEUeR9KzItTvaKlz
 * @Author: Kevin Gleeson
 * Version: 1.0
 * Date: 1/04/2019
 * 
 */
public class CardStack : MonoBehaviour
{
    // List to hold card index
    List<int> cards;
    // unity editor checkbox to select the main deck
    public bool isGameDeck;
    public bool HasCards
    {
        get { return cards != null && cards.Count > 0; }
    }

    //Public event card removed from main deck
    public event CardEventHandler CardRemoved;
    // Card added to new deck, player aor dealer hand
    public event CardEventHandler cardAdded;
    // GEt the amount of cards in the list
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
    // Get adn return the cards in the cards list
    public IEnumerable<int> GetCards()
    {
        foreach (int i in cards)
        {
            yield return i;
        }
    }
    // To remove a card from teh main deck
    public int Pop()
    {
        int temp = cards[0];
        // Remove first card from the stack
        cards.RemoveAt(0);

        if (CardRemoved != null)
        {
            CardRemoved(this, new CardEventArgs(temp));
        }
        return temp;
    }
    // to put a new card to the player or dealer hand
    public void  Push(int card)
    {
        cards.Add(card);
        if (cardAdded != null)
        {
            cardAdded(this, new CardEventArgs(card));
        }
    }
    // Set the value of the cards
    public int HandValue()
    {
        // Keep track of the cards
        int total = 0;
        int aces = 0;
        //loop through the card array.
        foreach(int card in GetCards())
        {
            // All card are stored from ace to king in each suit
            // % 13 will split them up into indvidual values
            int cardRank = card % 13;
            // 0 = ace 
            // Get values from 2 - 9 excluding ace, king, queen  and jack
            if(cardRank <= 9 && cardRank > 0)
            {
                cardRank += 1;
                // total the value
                // This is use to calculate the player and dealer score
                total = total + cardRank;
            }
            // set the values for king, queen , jack (all = 10).
            else if(cardRank >9 && cardRank <= 12)
            {
                cardRank = 10;
                // Add to the total score
                total = total + cardRank;
            }
            else
            {
                // Count the aces
                aces++;
            }

            
        }
        // for the different possible values the ace can have either a one or eleven.
        for (int i = 0; i < aces; i++)
        {
            // check if the the current score is less than or equal to 21
            if (total+11 <= 21)
            {
                // Ace == 11 if above condition is true
                total = total + 11;
            }
            else
            {
                // Ace is worth one if false.
                total = total + 1;
            }
        }

        // The final score
        return total;
    }
    // Creating the shuffled deck of cards
    public void CreateDeck()
    {
        // Clear the cards array
        cards.Clear();
     
        //Add the 1 - 52 to the cards array
        for (int i = 0; i < 52; i++)
        {
            cards.Add(i);
        }

        int n = cards.Count;
        // Fisher yates shuffle
        while (n > 1)
        {
            //decrement the counter by one
            n--;
            // Pick a ranndom number between the current size of the array.
            int k = Random.Range(0, n+1);
            //store random value 
            int temp = cards[k];
            // assign to 52nd slot on first pass the random value to n. 
            //(n will decrease by one after ech recursive call)  
            cards[k] = cards[n];
            // Finally add the random index to the cards array
            cards[n] = temp;
        }
    }
    // Reset for new gamme
    public void Reset()
    {
        cards.Clear();
    }
    void Awake()
    {
        // Create new list on new game
        cards = new List<int>();
        if (isGameDeck)
        {
            // Create a new deck.
            CreateDeck();
        }
       
    }

}
