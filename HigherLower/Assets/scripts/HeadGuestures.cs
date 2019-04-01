using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeadGuestures : MonoBehaviour
{
    /*
     * Access GameController script
     * Call Hit, stick, playAgaian functions.
     * Mapped to head gestures. 
     * 
     * */
    public GameController gc;
    // Vector3 array to angle of device
    private Vector3[] angles;
    // Index for update function
    private int index;
    // Center angle of device
    // For resetting after head gesture has been recognised.
    private Vector3 centerAngle;
    // The amount of up/down - left/right movement needed to trigger yes/ no
    private float deg = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        // reset the gesture after trigger
        RestGesture();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the angle of the device relative to the the camera position
        // New to the latest version of GVR.
        angles[index] = Camera.main.transform.eulerAngles;
        // Increment the index for every update.
        // The gesture has to register in this time frame
        index++;
        // check state every 60 frames
        if (index  == 60)
        {
            // Check movement function
            CheckMovement();

            // reset the gesture to zero.
            RestGesture();


        }
    }

    private void CheckMovement()
    {
       // Debug.Log("Method Called...");
       // Boolean control for yes no recognition
        bool right = false, left = false, up = false, down = false;

        for (int i = 0; i < 60; i++)
        {
            // Conditions for up and down gesture "Yes"
            if (angles[i].x < centerAngle.x - deg && !up)
            {
                up = true;
            }else if (angles[i].x > centerAngle.x + deg && !down)
            {
                down = true;
            }
            // Conditions for Left/Right movement "No" Gesture.
            if (angles[i].y < centerAngle.y - deg && !left)
            {
                left = true;
            }
            else if (angles[i].y > centerAngle.y + deg && !right)
            {
                right = true;
            }
        }
        // If gesture is NO and not yes
        // Stop mulitple gestures being recognised
        if (left && right && !(up && down))
        {
            Debug.Log("gesture = NO");
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
        // Yes gesture and not NO.
        if (up && down && !(left && right))
        {
            Debug.Log("Gesture =  YES");
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
    }
    void RestGesture()
    {
        // Reset the angle of the camera to listen out fro a new gesture
        angles = new Vector3[60];
        // reset the index from the update function.
        index = 0;
        // Reset the center angle of the camera.
        centerAngle = Camera.main.transform.eulerAngles;
       
    }
}
