using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStoppingAbility : MonoBehaviour
{
    public float destroyInterval = 5f;
    public LayerMask bulletLayer;
    public float stoppingRadius = 50f;
    public float maxStoppingRange = 2f;
    public float stoppingTime = 1f;
    public GameObject sphereEffectPrefab;
    public float abilityCooldown = 10f; // Cooldown time in seconds

    private GameObject sphereEffect;
    private Material sphereEffectMaterial;
    public LayerMask sphereEffectLayer;
    private bool isCooldown = false; // Tracks whether the ability is on cooldown
    private bool isAbilityActive = true; // Tracks whether the ability can currently be used

    private void Start()
    {
        sphereEffect = Instantiate(sphereEffectPrefab, transform.position, Quaternion.identity);
        sphereEffect.layer = sphereEffectLayer;
        sphereEffectMaterial = sphereEffect.GetComponent<Renderer>().material;
        sphereEffect.transform.localScale = Vector3.zero;
        sphereEffect.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isAbilityActive && !isCooldown)
        {
            StopBullets();
            StartCooldown();
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
                Destroy(collider.gameObject);
            }
        }

        sphereEffect = Instantiate(sphereEffectPrefab, transform.position, Quaternion.identity);
        sphereEffect.layer = sphereEffectLayer;
        sphereEffectMaterial = sphereEffect.GetComponent<Renderer>().material;
        sphereEffect.transform.localScale = Vector3.zero;
        sphereEffect.SetActive(false);

        StartCoroutine(ExpandSphereEffectCoroutine());
    }

    private IEnumerator DestroyBulletCoroutine(GameObject bullet)
    {
        yield return new WaitForSeconds(destroyInterval);
        Destroy(bullet);
    }

    private IEnumerator ExpandSphereEffectCoroutine()
    {
        sphereEffect.SetActive(true);
        float elapsedTime = 0f;

        while (elapsedTime < stoppingTime)
        {
            float t = elapsedTime / stoppingTime;
            float currentScale = Mathf.Lerp(0f, maxStoppingRange * 2f, t);
            sphereEffect.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            sphereEffectMaterial.SetFloat("_Alpha", 1f - t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        sphereEffect.SetActive(false);
    }

    private void StartCooldown()
    {
        isCooldown = true;
        isAbilityActive = false;
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(abilityCooldown);
        isCooldown = false;
        isAbilityActive = true;
    }
}