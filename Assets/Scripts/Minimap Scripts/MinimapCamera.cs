using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    public Transform plane;
    void Update()
    {  
        float x = plane.transform.position.x;
        float z = plane.transform.position.z;

        transform.position = new Vector3(x, 700, z);
    }
}
