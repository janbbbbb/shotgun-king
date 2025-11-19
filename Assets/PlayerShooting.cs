using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Camera cam;

    public GameObject pelletPrefab;

    public Sprite[] pelletSprites;

    public int pelletsPerShot = 1;
    public float spreadAngle = 12f;
    public float pelletSpeed = 15f;

    void Start()
    {
        if (cam == null)
            cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

    void Shoot()
    {
        Vector3 spawnPos = transform.position;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        plane.Raycast(ray, out float distance);

        Vector3 targetPoint = ray.GetPoint(distance);
        Vector3 forwardDir = (targetPoint - spawnPos).normalized;

        for (int i = 0; i < pelletsPerShot; i++)
        {
            // Apply random shotgun spread
            Vector3 randomDir =
                Quaternion.Euler(
                    Random.Range(-spreadAngle, spreadAngle),
                    Random.Range(-spreadAngle, spreadAngle),
                    0
                ) * forwardDir;

            // Correct for 2D sprites in 3D (facing top-down)
            Quaternion rot = Quaternion.LookRotation(randomDir) * Quaternion.Euler(90, 0, 0);

            // Spawn pellet
            GameObject pellet = Instantiate(pelletPrefab, spawnPos, rot);

            // Give movement
            pellet.GetComponent<Rigidbody>().velocity = randomDir * pelletSpeed;

            // Random sprite
            SpriteRenderer sr = pellet.GetComponent<SpriteRenderer>();
            sr.sprite = pelletSprites[Random.Range(0, pelletSprites.Length)];

            pellet.transform.localScale *= 0.7f;

            // Destroy after a short time
            Destroy(pellet, 1.5f);
        }
    }
}
