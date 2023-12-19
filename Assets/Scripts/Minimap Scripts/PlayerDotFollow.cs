using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDotFollow : MonoBehaviour
{
    public Transform planeObject;

    void Update()
    {
        float x = planeObject.transform.position.x;
        float z = planeObject.transform.position.z;

        transform.position = new Vector3(x, 50f, z);
    }
}
