using System.Collections;
using UnityEngine;

public class PelletManager : MonoBehaviour
{
    private GameObject impactEffectPrefab;
    private float lifetime;
    public float explosionSize = 0.5f;
    private bool hasHit = false;

    // Initialize the pellet with lifetime and impact effect prefab
    public void Initialize(float life, GameObject effectPrefab)
    {
        lifetime = life;
        impactEffectPrefab = effectPrefab;
        StartCoroutine(LifetimeCoroutine());
    }

    // Auto-destroy after lifetime
    private IEnumerator LifetimeCoroutine()
    {
        yield return new WaitForSeconds(lifetime);
        TriggerImpactEffect();
    }

    // Called when pellet collides with something
    private void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return; // prevent double triggers
        hasHit = true;

        // Ignore the player
        if (!collision.gameObject.CompareTag("Player"))
        {
            Vector3 impactPoint = collision.contacts[0].point;
            TriggerImpactEffect(impactPoint);
        }
    }

    // Spawns the impact effect and destroys the pellet
    private void TriggerImpactEffect(Vector3? position = null)
    {
        Vector3 spawnPos = position ?? transform.position;

        if (impactEffectPrefab != null)
        {
            GameObject impactEffect = Instantiate(
                impactEffectPrefab,
                spawnPos,
                Quaternion.Euler(90f, 0f, 0f)
            );

            impactEffect.transform.localScale *= explosionSize;
            Destroy(impactEffect, 0.1f);
        }

        Destroy(gameObject);
    }
}
