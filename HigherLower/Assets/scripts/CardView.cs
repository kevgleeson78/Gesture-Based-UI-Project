

using UnityEngine;
// Just a simple game object class to toggle cards face up or face down
public  class CardView
    {
    public GameObject Card { get; private set; }
    public bool IsFaceUp { get; set; }
    public CardView(GameObject card)
    {
        Card = card;
        IsFaceUp = false;
    }
}

