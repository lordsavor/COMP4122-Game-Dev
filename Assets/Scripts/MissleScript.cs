﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MissleScript : MonoBehaviour
{
    public Transform missleSpawnPoint;
    public GameObject misslePrefab;
    public float roundsPerMinute = 600;
    public GameObject target;
    private float timeBetweenShots;
    private bool canFire = true;
    public TMP_Text UI;

    void Start()
    {
        timeBetweenShots = 600f / roundsPerMinute;
        UI.text = "Missle\n\n\n1 / 1";
        canFire = true;
    }

    void Update()
    {
        if (Input.GetMouseButton(1) && canFire)
        {
            RaycastHit hit;

            if (Physics.BoxCast(transform.parent.transform.position, transform.parent.transform.localScale*250f, transform.parent.transform.forward, out hit)) {
                if(hit.collider.name == "Enemy") {
                    target = hit.transform.gameObject;
                    StartCoroutine(Firemissles(target));
                }
            }
        }
    }

    IEnumerator Firemissles(GameObject targetObject)
    {
        UI.text = "Missle\n\n\n0 / 1";
        canFire = false;

        if(Input.GetMouseButton(1)) { 
            GameObject missile = Instantiate(misslePrefab, missleSpawnPoint.position, missleSpawnPoint.rotation);

            Tarodev.Missile missileScript = missile.GetComponent<Tarodev.Missile>();

            if (missileScript != null) {
                missileScript.ChangeTarget(targetObject);
                missileScript.SetIsCloned(true);
            }

            yield return new WaitForSeconds(timeBetweenShots);
        }
        UI.text = "Missle\n\n\n1 / 1";
        canFire = true;
    }
}
