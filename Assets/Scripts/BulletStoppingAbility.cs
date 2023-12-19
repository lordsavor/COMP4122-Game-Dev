using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStoppingAbility : MonoBehaviour
{
    [SerializeField] public LayerMask bulletLayer;
    [SerializeField] public float stoppingRadius = 50f;
    [SerializeField] public float destroyInterval = 0.1f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            StopBullets();
        }
    }

    private void StopBullets()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, stoppingRadius, bulletLayer);

        foreach (Collider collider in colliders)
        {
            Rigidbody bulletRb = collider.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.isKinematic = true;
                collider.enabled = false;
                StartCoroutine(DestroyBulletCoroutine(collider.gameObject));
            }
        }
    }

    private IEnumerator DestroyBulletCoroutine(GameObject bullet)
    {
        yield return new WaitForSeconds(destroyInterval);
        Destroy(bullet);
    }
}
