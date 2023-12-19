using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vel : MonoBehaviour
{
    public float velCapacity;
    public float velRegenRate; // natural regen rate

    [SerializeField]
    float currentVel;

    public bool velSpin = false;
    public float velSpinCost;

    public bool velBoost = false;
    public float velBoostCost;

    // Start is called before the first frame update
    void Start()
    {
        currentVel = velCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentVel <= velCapacity) currentVel += velRegenRate * Time.deltaTime;
        velSpin = (Input.GetButton("VelManeuver"))? ActivateVel(velSpinCost) : false;
        velBoost = (Input.GetButton("VelBoost")) ? ActivateVel(velSpinCost) : false;
    }

    bool ActivateVel(float cost)
    {
        if (currentVel > cost)
        {
            currentVel -= (cost + velRegenRate) * Time.deltaTime;
            return true;
        }
        else return false;
    }

}
