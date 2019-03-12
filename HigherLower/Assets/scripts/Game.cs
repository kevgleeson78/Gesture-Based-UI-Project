using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class Game : MonoBehaviour {
    SpriteRenderer spriteRenderer;
    public Sprite[] card;
    public Sprite cardBack;

    public int cardIndex;


    public void ToggleFace(bool showFace)
    {

        if (showFace)
        {
            spriteRenderer.sprite = card[cardIndex];
        }
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

