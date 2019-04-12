

using UnityEngine;
/*
 * Title: Gesture Based UI Development Project
 * Discription: GEtters and setters for the crad game objects in the unity editor
 * Adapted from: https://www.youtube.com/watch?v=FxH8FoddkVY&list=PLZo2FfoMkJeE6GXx8cEUeR9KzItTvaKlz
 * @Author: Kevin Gleeson
 * Version: 1.0
 * Date: 1/04/2019
 * 
 */
// Just a simple game object class to toggle cards face up or face down
public class CardView
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

