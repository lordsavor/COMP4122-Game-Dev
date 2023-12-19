using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadExplosion : MonoBehaviour
{
    public ParticleSystem destroyEffect;
    public float destroyDuration = 2f;

    private void OnDestroy()
    {
        ParticleSystem effect = Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(effect.gameObject, destroyDuration);
    }
}
