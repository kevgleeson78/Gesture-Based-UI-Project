using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeadGuestures : MonoBehaviour
{
    private Vector3[] angles;
    private int index;
    private Vector3 centerAngle;
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
            if (angles[i].x < centerAngle.x - 20.0f && !up)
            {
                up = true;
            }else if (angles[i].x > centerAngle.x + 20.0f && !down)
            {
                down = true;
            }
            if (angles[i].y < centerAngle.y - 20.0f && !left)
            {
                left = true;
            }
            else if (angles[i].y > centerAngle.y + 20.0f && !right)
            {
                right = true;
            }
        }
        if (left && right && !(up && down))
        {
            Debug.Log("gesture = NO");
        }
        if (up && down && !(left && right))
        {
            Debug.Log("Gesture =  YES");
        }
    }
    void RestGesture()
    {
        angles = new Vector3[80];
        index = 0;
        centerAngle = Camera.main.transform.eulerAngles;
    }
}
