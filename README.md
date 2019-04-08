# Gesture-Based-UI-Project
This project is a 4th year assignment for the module Gesture Based UI Development.
## Link to youtube demo [HERE](https://www.youtube.com/watch?v=Uiy2A6vALLQ)
## Overview
For this module we were given an open ended project that integrates gesture based user interatcion with an application.
Uisng voice commands hand or head gestures to control an object via a software framework.
The choice of programming language and hardware was up to us to choose along with the type to gesture control to imnplement.

I have chosen to develop a blackjack game in VR using head guestures, unity, google cardboard and my android phone.
The player controls the game via head movements nodding yes or no to choose between the options available to the user.
The two options are Stick (nod no) or twist (nod yes).

Once the game has been completed (win or loose) a message is displayed to the user along with an option to play again.

The option to play again is in the form of a button in the users view that can be activated via gaze input.
When the user points the reticle at the button for more than 1 second it will create a click event to restart the game.

However before I came to decide on developing this system there were other ideas that were considered.


### Initial ideas.

The first idea I had was to develop a 3D game that controls a game object via a mobile phone accellerometer. 

This user would controll the object via hand gestures of:
Tilt Left: move left.
Tilt Right: move right.
Tilt Up: Move forward.
Tilt down: Move backwards.

The players game object would have to collect certain shapes to gain a positive score.
There woulsd also be certain shapes that would have a negative impact on the players score.

The objects that the player would collect and avoid would be dropped into the playing area for interaction.
In researching this proposal i had my phone dispalying the accellorometer and gyroscope data along with moving a basic game objest in a 3d environment.

However there was a issue with the data being processed by my phone specific to my model and an alternative proposal was needed for this project.
Here is an article on unity forums explaining the problem [Link Here](https://answers.unity.com/questions/1273323/gyroscope-samsung-s7-doesnt-work-properly.html).

### New Plan
After researching what my model of phone was capable of for gesture bsed development I foud Google Cardboard was a possibility.
I had a talk with my lecturer and we discussed VR and head gestures controlling a card game.
From this conversation I had the idea of creating a blackjack game that would be controlled by head guestures.

In the sections below I will discuss how I went about developing the game along with implementing the head gesture recognition for the system.

### Purpose of the application
The purpose of this application is to allow a user to control a card game while using only head gestures.

<img src="images/New Mockup 1.png">

The card game will be presented to the user via an app on their mobile phone.
This app is a virtual reallity environment using Google VR.
With the phone attached to a Google cardboard or similar device the users head positon can be tracked.
Tracking the users head is achieved by getting the angle of tilt relative to the phone position (More about this below).
With this data we can then capture head gestures and map them to actions within the game.



### Gestures identified as appropriate for this application
The gestures chosen for this application are yes and no head movements.

Yes moving head up and down and No by moving head left and right.

These head movements will map to the actions of stick or twist within the game environment.

The head gesture of yes triggering the twist function and giving the player a new card.

The head gesture of no triggering the stick function and activating the dealers turn function.

When the game of black jack has finished the user is presented with a message if they have won or not and the option to play again or exit the application. 

The yes head gesture will play again and the no getsure will exit the application.


### Hardware used in creating the application
The application is compiled and built to an andriod device with unity.

A VR headset is used to view the game when the application is run on the device.


### Development

When first developing the blackjack portion of the application buttons were used for click events of stick, twist and play again.
<img src="images/buttons.png">
These buttons triggering the stick, twist and play again will eventually be removed once the game functions have been verified as working.

These functions will then be mapped to yes and no head gestures.
### Black Jack game development

#### Sprites:
A PDF file containing images of a deck of cards was used to represent the playing cards in the game environment.
The sprite editor was then used to cut each card from the pdf as individual sprites.
This gave us a complete deck of cards to use for the development of the game.
Later values were attached to each card image which will be discussed in the below section "Adding values to the cards"

### Game controlller
The game controller script in the application handles all of the game logic and each of the player, dealer and deck prefabs.
The three card stacks of player, dealer and deck are added within the unity editor.
These are made publicaly available from the Game controller script.
```C#
 // Three card stacks for the player, dealer and the deck
    public CardStack player;
    public CardStack dealer;
    public CardStack deck;
```

A boolean variable of noddable is used to control if the player is allowed to used head gestures.
If the player has chosen to stick or there score is greater than 21 they should not be able to twist a card.
```C#
// Boolean to controll the gesture input
    public bool noddable = true;
```

A Boolean variable of gameover is used to check if the game has ended to allow the player to choose to play again or quit the application.

```C#    
    // Boolean to controll head gesture input on game over
    public bool gameOver = false;
 ```
 
 #### Update player/dealer score functions
 
 When the player chooses to twist the score UI gets updated based on the totlat value of the cards they have in their hand.
 
 The UpdatePlayerScore/UpdateDealerScore functions:
 ```C#
 void UpdatePlayerScore()
    {
        playerScore.text = "Player Score: "+player.HandValue().ToString();
    }
    void UpdateDealerScore()
    {
        dealerScore.text = "Dealer Score: "+dealer.HandValue().ToString();
    }
 ```
 These functions get called in the player and dealer turn functions described below in the palyer and dealer turn sections.
 
#### UI messages

Three UI text variables are used to display to the user the:
- player score
- dealer score
- Winning/loosing text message.
```C#
 // Displays the message to the user after the game has ended
    public Text winnerText;
    public Text playerScore;
    public Text dealerScore;
```


#### Shuffling the deck
To shuffle the deck of cards the fisher yates shuffle algorithm is used.

The algorithm works by starting at the beginning of an array in our case it will be the array holding the deck of cards.

A random number is generated from 0 - 51.

Suppose 34 is generated the card at index 34 is swaped with the card at index 0.

The algorithm then moves to index 1.

A random number is then generated between 0 - 51.

If 22 is randomly chosen the card at index 22 is swapped with the card at index 1.

This repeats unitl we have moved to the last element in the array at 51.

```C#
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
```


#### Adding Values to the cards
Teh card stack script controlls the values of each card.
The full list of cards are held in an array.
THe cards are ordered from (ace, 2, 3 ... , King).

Therefore a mod 13 operator can be used to get each card within the array.
The first card from each suit is ignored as they are aces.

We will deal with giving the value to an ace further down this function.

First off the cards between 2 and 9 are givne there face values.

Then the Jack, Queen and King are given a value of ten.




```C#
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
```

#### Handling the ace card value
The aces have to be accounted fro as a value of 1 if the totla score of the player or dealer hand is less than 21.

IF the player or dealer hand is greater than 21 the ace gets a value of one.

The total score is recorder by the total variable.
```C#
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
```
#### Players turn

 At the start of each game the cards are delt to the player and dealer.
 The StartGame function is used to deal two cards to the player and dealer with push and pop methods use to take a card from the main deck and place them in the dealer and player deck array.
 
 ```C#
 void StartGame()
    {
        
        // Deal two card each to the player and dealer form the shuffeld deck
        for (int i = 0; i < 2; i++)
        {
            player.Push(deck.Pop());
            UpdatePlayerScore();
            HitDealer();
        }
    }
 ```
 The push method:
 ```C#
 // to put a new card to the player or dealer hand
    public void  Push(int card)
    {
        cards.Add(card);
        if (cardAdded != null)
        {
            cardAdded(this, new CardEventArgs(card));
        }
    }
 ```
 
 The pop method:
 ```C#
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
 ```
 The player has control of the game first with the option of sticking or twisting.

#### Twist:

 ```C#
 public void Hit()
    {
        
        // Push a card from the stack to the players cards
        // Remove the card from the deck stack
        player.Push(deck.Pop());
        UpdatePlayerScore();

        // Check if the player has gone bust
        if (player.HandValue() > 21)
        {
           
            // Trun off the head gesture 
            noddable = false;
           // Start the deealers turn to show there cards
            StartCoroutine(DealersTurn());
        }
    }
 ```
#### Stick:

#### Dealers turn
 ```C#
 IEnumerator DealersTurn()
    {
        // Turn off head gestures for the dealers turn
        noddable = false;
       
        CardStackView view = dealer.GetComponent<CardStackView>();
        // Show the dealers first card.
        view.Toggle(dealersFirstCard, true);
        view.ShowCards();
        UpdateDealerScore();
        // Delay showing the dealers card every one second for a new card
        yield return new WaitForSeconds(1f);
        // Keep going while teh dealers hand is worth at least 16.
        // And keep going while the dealers scroe is less than the players score.
        while (dealer.HandValue() < 17 || dealer.HandValue() < player.HandValue())
        {
            // New card for the dealer
            HitDealer();
            UpdateDealerScore();
            // Wait for one second
            yield return new WaitForSeconds(1f);
        }
 ```
#### Game over conditions
The game checked inside the DealersTurn() function with conditional checks to see if the player hand value is gretaer than the dealer.
- The dealer must twist as long as habd value is at 16 or below.
- The dealer must attempt to get a higher or equal value of the playuer hand.
- The message is finally desplayed to the use once the dealer has completed their turn.
- Once the dealers turn is over the gameover vairable is set to true to allow the user to choose to play again or quit the game.

```C#
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
        
       // Set game over to true
        gameOver = true;
```
#### Play again

### Yes no head gesture development

#### Camera angle 

#### Boolean triggers

#### Yes head gesture:

#### No head gesture:

####  Bringing the two elements together

### Architecture for the solution

### Conclusions & Recommendations

### Problems while developing

### Further development 

### Resources






Author: Kevin Gleeson
