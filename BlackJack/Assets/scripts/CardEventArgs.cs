using System;
/*
 * Title: Gesture Based UI Development Project
 * Discription: Script for setting the card index
 *              used in the view for the dealer, player and main deck
 * Adapted from: https://www.youtube.com/watch?v=FxH8FoddkVY&list=PLZo2FfoMkJeE6GXx8cEUeR9KzItTvaKlz
 * @Author: Kevin Gleeson
 * Version: 1.0
 * Date: 1/04/2019
 * 
 */
// Event handler for card indexing
public class CardEventArgs : EventArgs
{
    public int CardIndex { get; private set; }

    public CardEventArgs(int cardIndex)
    {
        CardIndex = cardIndex;
    }

}