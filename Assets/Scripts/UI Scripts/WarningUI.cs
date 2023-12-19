using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WarningUI : MonoBehaviour
{
    private bool underAltitudeAlready = false;
    private bool underStallSpeed = false;
    
    public GameObject plane;
    private PlaneMovement planeMovementScript;

    public TMP_Text altitudeTMP;
    public TMP_Text stallTMP;

    private void Start() {
        planeMovementScript = plane.GetComponent<PlaneMovement>();    
    }

    void Update()
    {
        if(!underAltitudeAlready && plane.transform.position.y <= 30f) {
            StartCoroutine(FlashingAltitudeText());
        } 

        //CHANGE THE MIN TAKE OFF SPEED HERE
        //if(!underStallSpeed && planeMovementScript.minTakeOffSpeed <= 20f) {
        if(!underStallSpeed && planeMovementScript.minTakeOffSpeed <= 20f) {
            StartCoroutine(FlashingStallText());
        }  
    }

    IEnumerator FlashingAltitudeText() {
        underAltitudeAlready = true;

        while(plane.transform.position.y <= 30f) {
            altitudeTMP.text = "LOW ALTITUDE, PULL UP";
            yield return new WaitForSeconds(0.4f);

            altitudeTMP.text = "";
            yield return new WaitForSeconds(0.4f);
        }

        underAltitudeAlready = false;
    }

    IEnumerator FlashingStallText() {
        underStallSpeed = true;

        while(planeMovementScript.minTakeOffSpeed <= 20f) {
            stallTMP.text = "LOW STALL SPEED, INCREASE SPEED";
            yield return new WaitForSeconds(0.4f);

            stallTMP.text = "";
            yield return new WaitForSeconds(0.4f);
        }

        underStallSpeed = false;
    }
}
