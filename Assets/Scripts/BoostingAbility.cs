using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostingAbility : MonoBehaviour
{
    public float boostSpeed = 20f; // Desired speed when boosting
    public float boostDuration = 5f; // Duration of the boost in seconds
    public float normalSpeed = 10f; // Normal speed value
    public float speedDropTime = 5f; // Time it takes to gradually drop speed to normal

    private Rigidbody rb;
    private float originalSpeed;
    private float boostEndTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalSpeed = rb.velocity.magnitude;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            BoostSpeed();
        }

        if (Time.time >= boostEndTime)
        {
            DropSpeed();
        }
    }

    private void BoostSpeed()
    {
        rb.velocity = rb.velocity.normalized * boostSpeed;
        boostEndTime = Time.time + boostDuration;
    }

    private void DropSpeed()
    {
        float timePassed = Time.time - boostEndTime;
        float t = Mathf.Clamp01(timePassed / speedDropTime);
        rb.velocity = Vector3.Lerp(rb.velocity, rb.velocity.normalized * normalSpeed, t);
    }
}
