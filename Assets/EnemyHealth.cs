using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;
    public GameObject hitAnimPrefab;

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Called by pellet on hit
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        Vector3 spawnPos = transform.position + Vector3.up * 0.5f; // raise it a bit above the enemy
        Quaternion spawnRot = Quaternion.Euler(90f, 0f, 0f);       // rotate so sprite faces camera

        Instantiate(hitAnimPrefab, spawnPos, spawnRot);


        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Remove from board logic
        BoardManager.Instance.RemovePiece(gameObject);

        // Disable enemy logic
        Collider col = GetComponent<Collider>();
        if (col) col.enabled = false;

        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (var s in scripts)
            if (s != this) s.enabled = false;

        // Start fade
        StartCoroutine(FadeAndDestroy());
    }

    private IEnumerator FadeAndDestroy()
    {
        float duration = 0.5f;   // fade time
        float t = 0;

        // Collect all renderers (MeshRenderer / SpriteRenderer)
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        // Save original colors
        List<Material> mats = new List<Material>();
        foreach (Renderer r in renderers)
            mats.AddRange(r.materials); // handles multiple meshes

        // Fade loop
        while (t < duration)
        {
            t += Time.deltaTime;
            float alpha = 1f - (t / duration);

            foreach (Material m in mats)
            {
                if (m.HasProperty("_Color"))
                {
                    Color c = m.color;
                    c.a = alpha;
                    m.color = c;
                }
            }

            yield return null;
        }

        Destroy(gameObject);
    }


}
