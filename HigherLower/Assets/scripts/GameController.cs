using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
 * Title: Gesture Based UI Development Project
 * Discription: A script to controll the game logic 
 * Adapted from: https://www.youtube.com/watch?v=FxH8FoddkVY&list=PLZo2FfoMkJeE6GXx8cEUeR9KzItTvaKlz
 * @Author: Kevin Gleeson
 * Version: 1.0
 * Date: 1/04/2019
 * 
 */
public class GameController : MonoBehaviour
{
    // used to turn the first card face down
    int dealersFirstCard = -1;
    // THree card stacks for the player, dealer and the deck
    public CardStack player;
    public CardStack dealer;
    public CardStack deck;
    // Boolean to controll the gesture input
    public bool noddable = true;
    // Displays the message to the user after the game has ended
    public Text winnerText;
    // Boolean to controll head gesture input on game over
    public bool gameOver = false;

    // Hit (Twist) function for the player
    public void Hit()
    {
        // Push a card from the stack to the players cards
        // Remove the card from the deck stack
        player.Push(deck.Pop());
        // Check if the player has gone bust
        if (player.HandValue() > 21)
        {
            // Trun off the head gesture 
            noddable = false;
           // Start the deealers turn to show there cards
            StartCoroutine(DealersTurn());
        }
    }
    // Stick function
    public void Stick()
    {
        // If the player sticks (no gesture)
        // Turn off head recognition
        noddable = false;
        // Start the dealers turn
        StartCoroutine(DealersTurn());
    }
    // Play again function
    public void PlayAgain()
    {
        // Trun on head gesture recognition
        gameOver = false;
        noddable = true;
        // Clear the palyer, dealer and deck views
        player.GetComponent<CardStackView>().Clear();
        dealer.GetComponent<CardStackView>().Clear();
        deck.GetComponent<CardStackView>().Clear();
        // Shuffle a new deck
        deck.CreateDeck();
        // clear the end of game text
        winnerText.text = "";
        // reset the dealers first card to face down
        dealersFirstCard = -1;

        // Start a new game
        StartGame();
      
    }
    private void Start()
    {
        StartGame();
    }
    void StartGame()
    {
        // Deal two card each to the player and dealer form the shuffeld deck
        for (int i = 0; i < 2; i++)
        {
            player.Push(deck.Pop());
            HitDealer();
        }
    }
    void HitDealer()
    {
        int card = deck.Pop();
        // set the dealser first card to face down
        // Coverd with the card back sprite
        if (dealersFirstCard < 0)
        {
            dealersFirstCard = card;
        }
        dealer.Push(card);
        // Show the first card if the dealers turn has more that two cards in the stack view.
        if (dealer.CardCount >=2)
        {
            CardStackView view = dealer.GetComponent<CardStackView>();
            view.Toggle(card, true);
        }
    }
    // use the game logic here....
    IEnumerator DealersTurn()
    {
        // Turn off head gestures for the dealers turn
        noddable = false;
       
        CardStackView view = dealer.GetComponent<CardStackView>();
        // Show the dealers first card.
        view.Toggle(dealersFirstCard, true);
        view.ShowCards();
        // Delay showing the dealers card every one second for a new card
        yield return new WaitForSeconds(1f);
        // Keep going while teh dealers hand is worth at least 16.
        // And keep going while the dealers scroe is less than the players score.
        while (dealer.HandValue() < 17 || dealer.HandValue() < player.HandValue())
        {
            // New card for the dealer
            HitDealer();
            // Wait for one second
            yield return new WaitForSeconds(1f);
        }
        // End game conditions Loose
        if (player.HandValue() > 21 || (dealer.HandValue() >= player.HandValue() && dealer.HandValue() <=21))
        {
            winnerText.text = "You have lost!!!!\n Play again Yes / No";
        }
        // End game conditions Win
        else if (dealer.HandValue() > 21 || (player.HandValue() <=21 && player.HandValue() > dealer.HandValue()))
        {
            winnerText.text = "You Have Won!!!\n Play again Yes / No";
        }
        // End game conditions Fallback
        else
        {
            winnerText.text = "House wins\n Play again Yes / No";
        }
        // Delay one second
        yield return new WaitForSeconds(1f);
       // Set game over to true
        gameOver = true;
        

    }
}
