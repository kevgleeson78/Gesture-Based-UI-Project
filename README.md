# Gesture-Based-UI-Project
This project is a 4th year assignment for the module Gesture Based UI Development.
## Link to youtube demo [HERE](https://www.youtube.com/watch?v=qdQGHUeOEsk)
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
 IF the player chooses to twist the player score is updated and checked if they have gone above 21.
 If it has a score greater than 21 the dealers turn method is acalled and the players head gertures are turned off.

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
IF the player chooses to stick the noddable valiable is set to false to prevent the player from twisting fort he remainder of the game.
The the dealers turn is then called as acorutine to allow for a delay of one second between cards being shown to tue screen.

```C#
public void Stick()
    {
        // If the player sticks (no gesture)
        // Turn off head recognition
        noddable = false;
        // Start the dealers turn
        StartCoroutine(DealersTurn());
    }
```
#### Dealers turn
When it is the dealers turn the first card is turned over and the dealer begins to twist.
Each time the dealer twists for a new card the dealers score is updated and checked aghainst the player score.
The dealer also keeps twisting while there hand valkue is less than 17.
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
 
 This keeps repoeating until the dealer has gone over the had value of 21 or the dealers hand is equal or better then the player hand..
 The below game over conditions are then checked with a message to the screen if they evaluate to true.
#### The dealers first card face down and twist dealer

When a new game is created the dealers first card must not be shown to the player.

This is achieved by a boolean function that gets the card index of the dealers first card and hides the card by placing a card back sprite over the first card.

When it is the dealers turn this card is toggled and show to the player in the screen.
```C#
void HitDealer()
    {
        
        int card = deck.Pop();
        // set the dealers first card to face down
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
```

cardStackView script (Toggle method) to control the dealers cards show at the at the start of a new game.
```C#
    // Toggle the card face up or face down
    // From the fectchCards dict
    public void Toggle(int card, bool isFaceUp)
    {
        fetchCards[card].IsFaceUp = isFaceUp;
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
The paly again function simply clears all of the player, dealer and main deck views for a new game.

Head gesture input gets activated for a new game as it is always the players turn first.

The createDeck method is called to shuffle a new deck and deal the cards to the player and the dealer with the dealer firts card face down.

The dealer score and game over message are set to an empty string to clear the screen of any old values that were there from a previous game.

```C#
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
        dealerScore.text = "";
        // reset the dealers first card to face down
        dealersFirstCard = -1;
        
        // Start a new game
        StartGame();
      
    }
```
### Yes no head gesture development

### Google VR library for unity

The Google VR library for unity was used to track the users head movement when the application is running in their phone and the phone is atttached to a VR headset.

#### Boolean triggers and CheckMovement method

To achieve triggering the head gesture of yes and no  there are four boolen variable of up, down, left and right initialised to false .
These four variables are held locally within the CeckMovement Method.
```C#
private void CheckMovement()
    {
        // Debug.Log("Method Called...");
        // Boolean control for yes no recognition
        bool right = false, left = false, up = false, down = false;
```
The check movement method is responsible for setting the above four variables to true if conditions are met.
These conditions will be explianed in the next sections.

#### Camera angle 
The angle of the camera view is used to get the angle of rotation based on the positon of the users head (Where they are looking in the VR space)
Teh cameras euler angles are gather by usuing the following method:
```C#
Camera.main.transform.eulerAngles;
```
This can then be used to measure how far the head is away from a fixed point on either the x or y axis.

First variables are globally declared to hold:
- An array of Vector3 cordiantes 
- an index variable to set the amount of time that is to pass before the head positon is read and then reset.
- A Vector3 cordinate centerAngle variable, a fixed position that will be used to measure how far away the cameras rotation along either the x or y axis is.
- A float variable named dist that will be used to set the accepted distance where once passed will set the boolean values of up, down, left, right to true.

```C#
 // Vector3 array to angle of device
    private Vector3[] angles;
    // Index for update function
    private int index;
    // Center angle of device
    // For resetting after head gesture has been recognised.
    private Vector3 centerAngle;
    // The amount of up/down - left/right movement from the center angle needed to trigger yes/ no
    private float dist = 7.0f;
```
#### Storing euler angles of the users head position 
To access the users head position over time an array is used to store the angle of rotation inside unitys update function.

Each tiome update function is called the users head position is stored in the globally declared angles array.

The index of this array is incremented each time the update function is called and set to the angles array.

```C#
void Update()
    {
        // Get the angle of the device relative to the the camera position
        // New to the latest version of GVR.
        angles[index] = Camera.main.transform.eulerAngles;
        // Increment the index for every update.
        // The gesture has to register in this time frame
        index++;
        // check state every 30 frames
        if (index == 30)
        {
            // Check movement function
            CheckMovement();

            // reset the gesture to zero.
            ResetGesture();


        }
    }
```

This gives us an array of angles that we can check against to see if the head ahas passed the pre-set treshold that will set boolean values of up, down, left or right to true.
#### Yes head gesture:
The yes head gesture is triggered if the up and down boolean variables are true.

This is achieved by looping through the angles array populated above by the update function andf comparing the distance between the center fixed angle and the head position angle in the array.

This is measured along the x axis.

If the  pre-set distacnce - center angle is grater than the angle of the head position angkes array the treshoild has been passed and down gets set to true.


The oposite is checked for upward motion:

If the pe-set distance  + the fixed center angle  is les than the index of the array of head positions teh treshold for up has been passed and up gets set to true.

```C#
for (int i = 0; i < 80; i++)
        {
            // Conditions for up and down gesture "Yes"
            // true if the distance is greater than the 
            // pre-defined dist variable
            //!up to ensure that ther has been no other up triggered.
            if (angles[i].x < centerAngle.x - dist && !down)
            {
                down = true;
            }
            else if (angles[i].x > centerAngle.x + dist && !up)
            {
                up = true;
            }
```

 The combination of up and down are evaluated within an if statement and if true we have a yes gesture.
 
 This can then be used to call the methods we need.
 
 ```C#
 // Yes gesture and not NO.
        if (up && down && !(left && right))
        {
            
            //Debug.Log("Gesture =  YES");
            // GvrCardboardHelpers.Recenter();
            // Condition to check if the game is still in play 
            // and accepting noddable gestures
            if (gc.noddable && !gc.gameOver)
            {
                // Twist option with nodding yes.
                gc.Hit();
               
            }
            // If its game over 
            if (gc.gameOver)
            {
                // Play again if yes gesture us detected from above condition.
                gc.PlayAgain();
                
            }
        }
 ```
#### No head gesture:
The no head gesture of left and right movement is measured along the y axis and compared to the fixed center euler angle.

For left to be true:

The fixed center angle on the y axis - the pre-set distance should be greater than one of the angles with the euler angle (angles array).

For right to be true:
The fixed center angle on the y axis - the pre-set distance should be less than one of the angles with the euler angle (angles array).
```C#
  // Check the position of rotaion 
        for (int i = 0; i < 80; i++)
        {
         
            // Conditions for Left/Right movement "No" Gesture.
            if (angles[i].y < centerAngle.y - dist && !left)
            {
                left = true;
            }
            else if (angles[i].y > centerAngle.y + dist && !right)
            {
                right = true;
            }
        }
```

Finally a combination of both left and right boolean values are checked, if they are both true this is a no gesture.

This can then be used to call what ever method we neeed inside this conditional:

```C#
  // If gesture is NO and not yes
        // Stop mulitple gestures being recognised
        if (left && right && !(up && down))
        {
            //Debug.Log("gesture = NO");
            // Check if the noddable boolean is set to true.
            // From the GameController script
            if (gc.noddable)
            {   //Call the Stick() function from the GameController script
                gc.Stick();
                // Its now the dealers turn...
            }
            if (gc.gameOver)
            {
                // Android close icon or back button tapped.
                Application.Quit();
            }
            //GvrCardboardHelpers.Recenter();
        }
```

#### Resetting the gestures:

####  Bringing the two elements together

### Architecture for the solution

### Conclusions & Recommendations

### Problems while developing

### Further development 

### Resources






Author: Kevin Gleeson
