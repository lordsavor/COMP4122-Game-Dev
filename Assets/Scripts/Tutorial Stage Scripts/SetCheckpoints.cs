using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCheckpoints : MonoBehaviour
{
    public GameObject[] listOfCheckpoints;

    void Start()
    {
        for(int i = 1; i < listOfCheckpoints.Length; i++) {
            listOfCheckpoints[i].SetActive(false);
        }
    }
}
