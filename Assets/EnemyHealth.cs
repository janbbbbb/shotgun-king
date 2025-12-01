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
        BoardManager.Instance.RemovePiece(gameObject);

        Destroy(gameObject);
    }
}
