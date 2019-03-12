using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilt : MonoBehaviour {
    
    void Update()
    {
        GetYRotation();
    }
    void GetYRotation()
    {
        Quaternion referenceRotation = Quaternion.identity;
        Quaternion deviceRotation = new Quaternion(0.5f, 0.5f, -0.5f, 0.5f) * Input.gyro.attitude * new Quaternion(0, 1, 0, 0);
        Quaternion eliminationOfXZ = Quaternion.Inverse(
            Quaternion.FromToRotation(referenceRotation * Vector3.up,
                                  deviceRotation * Vector3.up)
            );
        Quaternion rotationY = eliminationOfXZ * deviceRotation;
        float roll = rotationY.eulerAngles.y;

        print(roll);
    }
}
    
