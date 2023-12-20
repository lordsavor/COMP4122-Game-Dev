using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStoppingAbility : MonoBehaviour
{
    public float destroyInterval = 5f;
    public LayerMask bulletLayer;
    public float stoppingRadius = 50f;
    public float stoppingTime = 1f;
    public GameObject sphereEffectPrefab;
    public float abilityCooldown = 10f; // Cooldown time in seconds

    private Material sphereEffectMaterial;
    private bool isCooldown = false; // Tracks whether the ability is on cooldown
    private bool isAbilityActive = true; // Tracks whether the ability can currently be used

    private void Start()
    {
        Renderer sphereRenderer = sphereEffectPrefab.GetComponent<Renderer>();
        if (sphereRenderer != null)
        {
            sphereEffectMaterial = sphereRenderer.sharedMaterial;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isAbilityActive && !isCooldown)
        {
            StopBullets();
            StartCooldown();
        }
    }

    private void StopBullets()
    {
        StartCoroutine(ExpandSphereEffectCoroutine());
    }

    private IEnumerator ExpandSphereEffectCoroutine()
    {
        GameObject sphereEffect = Instantiate(sphereEffectPrefab, transform.position, Quaternion.identity);
        float elapsedTime = 0f;

        while (elapsedTime < stoppingTime)
        {
            float t = elapsedTime / stoppingTime;
            float currentScale = Mathf.Lerp(0f, stoppingRadius * 2f, t);
            sphereEffect.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            sphereEffectMaterial.SetFloat("_AlphaScale", 1f - t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(sphereEffect);
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