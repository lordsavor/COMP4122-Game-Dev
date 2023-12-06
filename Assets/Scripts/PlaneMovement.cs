using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovement : MonoBehaviour
{
    public float maxSpeed = 150f;
    public float increaseSpeed = 0.1f;
    private float relativeResponse;

    float roll;
    float pitch;
    float yaw;
    float currentSpeed = 10f;

    Rigidbody planeRB;
    public float minTakeOffSpeed = 100f;

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
        //front to back
        roll = Input.GetAxis("Mouse Y");
        //side to side
        pitch = Input.GetAxis("Mouse X");
        //rotation around vertical
        yaw = Input.GetAxis("Yaw");

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

        planeRB.AddRelativeTorque(Vector3.forward * roll * 300f);
        planeRB.AddRelativeTorque(Vector3.up * pitch * 300f);
        planeRB.AddRelativeTorque(Vector3.right * yaw * 300f);

        planeRB.AddForce(Vector3.up * planeRB.velocity.magnitude * minTakeOffSpeed);
    }
}
