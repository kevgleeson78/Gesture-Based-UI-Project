using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
/*
 * Title: Gesture Based UI Development Project
 * Discription: A class to get the card sprite game objects adn set the index of each card to a stack array
 * Adapted from: https://www.youtube.com/watch?v=FxH8FoddkVY&list=PLZo2FfoMkJeE6GXx8cEUeR9KzItTvaKlz
 * @Author: Kevin Gleeson
 * Version: 1.0
 * Date: 1/04/2019
 * 
 */

public class CardModel : MonoBehaviour {
    SpriteRenderer spriteRenderer;
    // And array of the card sprites split from the original pdf in the sprite editor.
    public Sprite[] card;
    // The single card back sprite.
    public Sprite cardBack;
    // Fro applying each card sprite to the card Sprite array.
    public int cardIndex;

    // Unity editor option for showing the front or back of the card
    public void ToggleFace(bool showFace)
    {
        // If check show the card value
        if (showFace)
        {
            spriteRenderer.sprite = card[cardIndex];
        }
        // If not checked show the crad back.
        else
        {
            spriteRenderer.sprite = cardBack;
        }
        
    }
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

}
/* public static string[] suits =  { "C","D","H","S"};
 public static string[] values = {"A","2","3","4","5","6","7","8","9","10","J","Q","K" };
 public List<string> deck;
 // Use this for initialization
 void Start () {
     playCards();

 }

 // Update is called once per frame
 void Update () {

 }
 public void playCards()
 {
     deck = GenerateDeck();
     Shuffle(deck);
     foreach (string card in deck)
     {
         print(card);
     }
 }
 public static List<string> GenerateDeck()
 {
     List<string> newDeck = new List<string>();
     foreach(string s in suits)
     {
         foreach(string v in values)
         {
             newDeck.Add(s + v);
         }

     }
     return newDeck;
 }

 void Shuffle<T>(List<T> list)
 {
     System.Random random = new System.Random();
     int n = list.Count;
     while(n > 1)
     {
         int k = random.Next(n);
         n--;
         T temp = list[k];
         list[k] = list[n];
         list[n] = temp;
     }
 }
 */

