using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    public float maxSpeed;
    public float acceleration;
    private float relativeResponse;

    //input variables

    float pitchMouse;
    float pitchKey;

    float rollMouse;
    float rollKey;

    float yawKey;

    float thrustKey;

    //final axis input variables

    float roll;
    float pitch;
    float yaw;
    float thrust;
    float currentSpeed = 10f;

    Rigidbody planeRB;
    public float minTakeOffSpeed;

    public float pitchSpeed;
    public float rollSpeed;
    public float yawSpeed;


    private void Awake() {
        planeRB = GetComponent<Rigidbody>();
        relativeResponse = planeRB.mass;
    }

    private void Start() {
        currentSpeed = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update() {

        // movement

        pitchMouse = Input.GetAxis("Mouse Y");
        pitchKey = Input.GetAxis("PitchKey");

        rollMouse = Input.GetAxis("Mouse X");
        rollKey = Input.GetAxis("RollKey");
 
        yawKey = Input.GetAxis("Yaw");

        thrustKey = Input.GetAxis("Thrust");
    }

    private void FixedUpdate() {


        // process input sources
        pitch = (pitchKey == 0) ? pitchMouse : pitchKey;    // keyboard > mouse input
        roll = (rollKey == 0) ? rollMouse : rollKey;
        yaw = yawKey;
        thrust = thrustKey;

        // input -> movement

        currentSpeed += thrust * acceleration;

        Vector3 lForward = Camera.main.transform.forward;
        planeRB.AddForce(lForward * maxSpeed * currentSpeed);

        planeRB.AddRelativeTorque(Vector3.forward * pitch * pitchSpeed);
        planeRB.AddRelativeTorque(Vector3.up * yaw * yawSpeed);
        planeRB.AddRelativeTorque(Vector3.right * roll * rollSpeed);

        // lift calculation
        planeRB.AddForce(Vector3.up * planeRB.velocity.magnitude * minTakeOffSpeed);

        //Limiting the max and min speed
        if (currentSpeed <= 10f) currentSpeed = 10f;
        else if (currentSpeed >= 100.5f) currentSpeed = 100f;
    }
}
