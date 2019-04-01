using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Title: Gesture Based UI Development Project
 * Discription: A scrript to control the in game view of the card sprites.
 * Adapted from: https://www.youtube.com/watch?v=FxH8FoddkVY&list=PLZo2FfoMkJeE6GXx8cEUeR9KzItTvaKlz
 * @Author: Kevin Gleeson
 * Version: 1.0
 * Date: 1/04/2019
 * 
 */
// Enforce the need for the CardStack object
[RequireComponent(typeof(CardStack))]
public class CardStackView : MonoBehaviour
{

    CardStack deck;
    // Key value dict for the cards
    Dictionary<int, CardView> fetchCards;

    // Position of each starting card stack
    public Vector3 start;
    // Offset used to fan the cards in the view
    public float cardOffset;

    // To display face up or down in the view
    public bool faceUp = false;
    // To fan the main deck in the corect way
    public bool reverseLayerOrder = false;
    // Add the card prefab to this script from unity editor.
    public GameObject cardPrefab;

    // Toggle the card face up or face down
    // From the fectchCards dict
    public void Toggle(int card, bool isFaceUp)
    {
        fetchCards[card].IsFaceUp = isFaceUp;
    }
    // To empty the dictionary
    public void Clear()
    {
        deck.Reset();
        foreach (CardView view in fetchCards.Values)
        {
            // remove cloned sprites
            Destroy(view.Card);

        }
        // Clear the dict
        fetchCards.Clear();
    }
    void Awake()
    {
        // new instance of the fetchCrads dict on new game
        fetchCards = new Dictionary<int, CardView>();
        deck = GetComponent<CardStack>();
        // Dicrtiption of function below
        ShowCards();
        deck.CardRemoved += deck_CardRemoved;
        deck.cardAdded += deck_CardAdded;
    }
    // Adding the new cards to the view with the x, y positioning and offset fanning of cards
    // For the main deck in the middle
    private void deck_CardAdded(object sender, CardEventArgs e)
    {
        float co = cardOffset * deck.CardCount;
        Vector3 temp = start + new Vector3(co, 0f);
        AddCard(temp, e.CardIndex, deck.CardCount);

    }
    // Removing a card from the main deck
    void deck_CardRemoved(object sender, CardEventArgs e)
    {
        // Getting the key vlaue from the dictionary
        if (fetchCards.ContainsKey(e.CardIndex))
        {
            // remove the sprite from the game view
            // The main deck as each card is delt to the player and dealer 
            //unitl the game is over and reset.
            Destroy(fetchCards[e.CardIndex].Card);
            fetchCards.Remove(e.CardIndex);
        }
    }

    private void Update()
    {
            ShowCards();   
    }
    // Used for showing the dealerand players cards.
   public void ShowCards()
    {
        int cardCount = 0;
        // ensure the deck has enough cards
        if (deck.HasCards)
        {
            //Get all cards in the array
            foreach (int i in deck.GetCards())
            {
                // Set the x,y and fanning offset of the cards for the player and dealer
                float co = cardOffset * cardCount;
                Vector3 temp = start + new Vector3(co, 0f);
                // Add the sprites to the view method below
                AddCard(temp, i, cardCount);
                cardCount++;
            }
        }

    }
    // Method to add the cards to the game view
    void AddCard(Vector3 position, int cardIndex, int PositionalIndex)
    {

        // Get the card index 
        if (fetchCards.ContainsKey(cardIndex))
        {
            // if its face down selected in unity editor
            if (!faceUp)
            {
                CardModel cardModel = fetchCards[cardIndex].Card.GetComponent<CardModel>();
                // Set the first deal;ers card to face down
                cardModel.ToggleFace(fetchCards[cardIndex].IsFaceUp);
            }
            return;
        }
        // Get the card sprite and place to the position of the method input.
        // Set in the unity editor
        GameObject cardCopy = (GameObject)Instantiate(cardPrefab);
        // Treansform the position on screen
        cardCopy.transform.position = position;
        // Use the card model class getters
        CardModel model = cardCopy.GetComponent<CardModel>();
        model.cardIndex = cardIndex;
        // Set the cards to be face up.
        model.ToggleFace(faceUp);
      
        SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer>();
       // model.transform.parent = Camera.main.transform;
       // FAn the cards from right to left 
       // LEft to right by default
        if (reverseLayerOrder)
        {
            
            spriteRenderer.sortingOrder = 51 - PositionalIndex;
        }
        else
        {
            spriteRenderer.sortingOrder = PositionalIndex;
        }
        
        // Add card to the dict 
        fetchCards.Add(cardIndex, new CardView(cardCopy));
      
    }
}