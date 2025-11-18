using System.Collections;
using UnityEngine;

public class PelletManager : MonoBehaviour
{
    private GameObject impactEffectPrefab;
    private float lifetime;

    public float explosionSize = 0.5f;


    public void Initialize(float life, GameObject effectPrefab)
    {
        lifetime = life;
        impactEffectPrefab = effectPrefab;

        StartCoroutine(LifetimeCoroutine());
    }

    private IEnumerator LifetimeCoroutine()
    {
        yield return new WaitForSeconds(lifetime);

        if (impactEffectPrefab != null)
        {
            GameObject impactEffect = Instantiate(impactEffectPrefab, transform.position, Quaternion.Euler(90f, 0f, 0f));

            impactEffect.transform.localScale *= explosionSize;

            Destroy(impactEffect, 0.1f);
        }

        Destroy(gameObject);
    }
}