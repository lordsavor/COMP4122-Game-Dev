using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleScript : MonoBehaviour
{
    public Transform missleSpawnPoint;
    public GameObject misslePrefab;
    public float roundsPerMinute = 600;
    public GameObject target;
    private float timeBetweenShots;
    private bool canFire = true;

    void Start()
    {
        timeBetweenShots = 600f / roundsPerMinute;
    }

    void Update()
    {
        if (Input.GetMouseButton(1) && canFire)
        {
            StartCoroutine(Firemissles(target));
        }
    }

    IEnumerator Firemissles(GameObject targetObject)
    {
        canFire = false;

        GameObject missile = Instantiate(misslePrefab, missleSpawnPoint.position, missleSpawnPoint.rotation);
        Tarodev.Missile missileScript = missile.GetComponent<Tarodev.Missile>();
        if (missileScript != null)
        {
            missileScript.ChangeTarget(targetObject);
            missileScript.SetIsCloned(true);
        }

        yield return new WaitForSeconds(timeBetweenShots);

        canFire = true;
    }
}
