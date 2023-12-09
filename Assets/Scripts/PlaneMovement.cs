using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    public float maxSpeed = 150f;
    public float increaseSpeed = 0.1f;
    private float relativeResponse;

    //input variables

    float pitchMouse;
    float pitchKey;

    float rollMouse;
    float rollKey;

    float yawKey;

    //final axis input variables

    float roll;
    float pitch;
    float yaw;
    float currentSpeed = 10f;

    Rigidbody planeRB;
    public float minTakeOffSpeed = 100f;

    public float pitchSpeed = 100f;
    public float rollSpeed = 10f;
    public float yawSpeed = 300f;


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

        pitchMouse = Input.GetAxis("Mouse Y");
        pitchKey = Input.GetAxis("PitchKey");

        rollMouse = Input.GetAxis("Mouse X");
        rollKey = Input.GetAxis("RollKey");
 
        yawKey = Input.GetAxis("Yaw");

        if(Input.GetKey(KeyCode.LeftShift)) currentSpeed += increaseSpeed;
        //Brake? Idk or slow down
        if(Input.GetKey(KeyCode.LeftControl)) currentSpeed -= increaseSpeed;

        //Limiting the max and min speed
        if(currentSpeed <= 10f) currentSpeed = 10f;
        else if (currentSpeed >= 100.5f) currentSpeed = 100f;
    }

    private void FixedUpdate() {
        Vector3 lForward = Camera.main.transform.forward;
        planeRB.AddForce(lForward * maxSpeed * currentSpeed);

        // prioritize keyboard input over mouse input

        pitch = (pitchKey == 0) ? pitchMouse : pitchKey;
        roll = (rollKey == 0) ? rollMouse : rollKey;

        yaw = yawKey;

        planeRB.AddRelativeTorque(Vector3.forward * pitch * pitchSpeed);
        planeRB.AddRelativeTorque(Vector3.up * yaw * yawSpeed);
        planeRB.AddRelativeTorque(Vector3.right * roll * rollSpeed);

        planeRB.AddForce(Vector3.up * planeRB.velocity.magnitude * minTakeOffSpeed);
    }
}
