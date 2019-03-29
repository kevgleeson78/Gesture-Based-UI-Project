using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeadGuestures : MonoBehaviour
{
    public GameController gc;
    private Vector3[] angles;
    private int index;
    private Vector3 centerAngle;
    private float deg = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
       
        RestGesture();
    }

    // Update is called once per frame
    void Update()
    {

        angles[index] = Camera.main.transform.eulerAngles;
        
        index++;
        if (index  == 80)
        {
            
            CheckMovement();
            RestGesture();


        }
    }

    private void CheckMovement()
    {
       // Debug.Log("Method Called...");
        bool right = false, left = false, up = false, down = false;

        for (int i = 0; i < 80; i++)
        {
            if (angles[i].x < centerAngle.x - deg && !up)
            {
                up = true;
            }else if (angles[i].x > centerAngle.x + deg && !down)
            {
                down = true;
            }
            if (angles[i].y < centerAngle.y - deg && !left)
            {
                left = true;
            }
            else if (angles[i].y > centerAngle.y + deg && !right)
            {
                right = true;
            }
        }
        if (left && right && !(up && down))
        {
            Debug.Log("gesture = NO");
            if (gc.noddable)
            {
                gc.Stick();
            }
            
            //GvrCardboardHelpers.Recenter();
        }
        if (up && down && !(left && right))
        {
            Debug.Log("Gesture =  YES");
            // GvrCardboardHelpers.Recenter();
            if (gc.noddable)
            {
                gc.Hit();
            }
        }
    }
    void RestGesture()
    {
        angles = new Vector3[80];
        index = 0;
        centerAngle = Camera.main.transform.eulerAngles;
       
    }
}
