using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;
    public GameObject hitAnimPrefab;

    public event System.Action<EnemyHealth> OnDeath;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        Vector3 spawnPos = transform.position + Vector3.up * 0.5f;
        Quaternion spawnRot = Quaternion.Euler(90f, 0f, 0f);

        Instantiate(hitAnimPrefab, spawnPos, spawnRot);

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        // Notify LevelManager
        OnDeath?.Invoke(this);

        // Remove from board logic
        BoardManager.Instance.RemovePiece(gameObject);

        // Disable enemy logic
        Collider col = GetComponent<Collider>();
        if (col) col.enabled = false;

        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (var s in scripts)
            if (s != this) s.enabled = false;

        // Start fade-out
        StartCoroutine(FadeAndDestroy());
    }


    private IEnumerator FadeAndDestroy()
    {
        float duration = 0.5f;
        float t = 0;

        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        List<Material> mats = new List<Material>();
        foreach (Renderer r in renderers)
            mats.AddRange(r.materials);

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
