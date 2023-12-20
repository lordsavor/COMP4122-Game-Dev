using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    GameObject[] listOfCheckpoints;
    private SetCheckpoints script;
    static int counter = 1;
    
    private void Start() {
        script = FindObjectOfType<SetCheckpoints>();
        listOfCheckpoints = script.listOfCheckpoints;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Plane") {
            if (counter == listOfCheckpoints.Length) {
            SceneManager.LoadScene("Main Menu");
        } else {
            listOfCheckpoints[counter].SetActive(true);
            counter = counter + 1;
        }  
        }  
    }
}
