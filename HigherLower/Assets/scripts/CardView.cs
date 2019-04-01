

using UnityEngine;
// Just a simple game object class to toggle cards face up or face down
public  class CardView
    {
    // The crad prefab in unity editor
    public GameObject Card { get; private set; }
    // Unity editor for face up or down
    // USed for the dealer
    public bool IsFaceUp { get; set; }
    // Unity editor adding the card sprite to the game object 
    public CardView(GameObject card)
    {
        Card = card;
        //initail value of false for the check box.
        IsFaceUp = false;
    }
}

