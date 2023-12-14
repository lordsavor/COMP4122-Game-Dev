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

    //unlocks certain rigid body elements
    public Vector3 tensor;
    public float maxAngularVelocity;
    //private Vector3 angularVelocity = new Vector3();

    private Vector3 tensorBrake = new Vector3 (9999,9999,9999);
    private Vector3 currentTensor = new Vector3 (1,1,1);

    private void Awake() {
        planeRB = GetComponent<Rigidbody>();
        relativeResponse = planeRB.mass;
    }

    private void Start() {
        currentSpeed = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        //unlocks inertia tensor
        planeRB.inertiaTensor = tensor;
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

        // Determine maxAngularVeloctiy, might change depending on special abilities (e.g. plane go spin ability)
        // Would've liked it more if I can just max pitch,roll, and yaw separately. So this is a bandaid solution
        planeRB.maxAngularVelocity = maxAngularVelocity;

        // process input sources
        pitch = (pitchKey == 0) ? pitchMouse : pitchKey;    // keyboard > mouse input
        roll = (rollKey == 0) ? rollMouse : rollKey;
        yaw = yawKey;
        thrust = thrustKey;

        // experimental: playing with tensors to increase plane stability

        currentTensor.Set(
            (pitch == 0) ?  tensorBrake.x : tensor.x,
            (yaw == 0) ? tensorBrake.y : tensor.y,
            (roll == 0) ? tensorBrake.z : tensor.z
        );
        planeRB.inertiaTensor = currentTensor;
        planeRB.angularDrag = (pitch == 0 && yaw == 0 && roll == 0)  ? 10 : 1;

        // input -> movement

        currentSpeed += thrust * acceleration;

        Vector3 lForward = Camera.main.transform.forward;
        planeRB.AddForce(lForward * maxSpeed * currentSpeed);

        ForceMode mode = ForceMode.Impulse;
        planeRB.AddRelativeTorque(Vector3.right * pitch * pitchSpeed, mode);
        planeRB.AddRelativeTorque(Vector3.up * yaw * yawSpeed, mode);
        planeRB.AddRelativeTorque(-Vector3.forward * roll * rollSpeed, mode);

        /*
        angularVelocity.Set(pitch * pitchSpeed, yaw * yawSpeed, roll * rollSpeed);
        planeRB.angularVelocity = angularVelocity;
        */

        // lift calculation
        planeRB.AddForce(Vector3.up * planeRB.velocity.magnitude * minTakeOffSpeed);

        //Limiting the max and min speed
        if (currentSpeed <= 10f) currentSpeed = 10f;
        else if (currentSpeed >= 100.5f) currentSpeed = 100f;
    }

}
