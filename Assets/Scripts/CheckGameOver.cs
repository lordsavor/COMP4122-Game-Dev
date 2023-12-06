using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckGameOver : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Plane")) {
            Destroy(other.gameObject);
            StartCoroutine(DeathTimer());
        }
    }

    private IEnumerator DeathTimer() {
        yield return new WaitForSeconds(3f);
        
        //Play Death Animation Here

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
