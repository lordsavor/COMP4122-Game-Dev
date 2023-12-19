using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code From: https://www.youtube.com/watch?v=0IrZ3LDJoeM
public class CanSeeEnemy : MonoBehaviour
{
    public GameObject target;

    Vector3 screenPos;

    private void Start() {
        
    }

    private void Update ()
    {
        var targetRender = target.GetComponent<Renderer>();
        if (IsVisible(target)) {
            //screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
            //Debug.Log("Coords in screen are " + screenPos);
        }
        else {
           //your sentence
        }
    }

    private bool IsVisible(GameObject target)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        var point = target.transform.position;

        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point)< 0)
            {
                return false;
            }
        }
        return true;
    }
}
