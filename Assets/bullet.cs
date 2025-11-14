using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 direction;

    public void Initialize(Vector3 dir)
    {
        direction = dir.normalized;
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        // Destroy when leaving screen bounds
        if (Mathf.Abs(transform.position.x) > 20f ||
            Mathf.Abs(transform.position.z) > 20f)
        {
            Destroy(gameObject);
        }
    }
}

