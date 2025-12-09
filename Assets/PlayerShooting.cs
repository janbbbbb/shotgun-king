using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Camera cam;

    public GameObject pelletPrefab;

    public GameObject pelletExplodePrefab;

    public Sprite[] pelletSprites;

    public int pelletsPerShot = 10;
    public float spreadAngle = 12f;
    public float pelletSpeed = 15f;
    public float range = 15f;

    public int maxAmmo = 6;       // one full shotgun load
    public int currentAmmo = 6;   // start full
    public bool isReloading = false;

    void Start()
    {
        if (cam == null)
            cam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Shoot();

        if (Input.GetKeyDown(KeyCode.R))
            Reload();
    }

    void Shoot()
    {

        if (currentAmmo <= 0)
        {
            Debug.Log("Out of ammo! Must reload.");
            return;
        }

        GameManager.Instance.NextTurn();

        currentAmmo--;
        Debug.Log("Shot fired. Ammo left: " + currentAmmo);

        Vector3 spawnPos = transform.position;

        // Ray from camera to mouse
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        plane.Raycast(ray, out float distance);
        Vector3 targetPoint = ray.GetPoint(distance);

        Vector3 forwardDir = (targetPoint - spawnPos).normalized;

        // Shuffle pellets for lifetime randomness
        List<int> pelletIndices = new List<int>();
        for (int i = 0; i < pelletsPerShot; i++) pelletIndices.Add(i);
        for (int i = 0; i < pelletIndices.Count; i++)
        {
            int temp = pelletIndices[i];
            int randomIndex = Random.Range(i, pelletIndices.Count);
            pelletIndices[i] = pelletIndices[randomIndex];
            pelletIndices[randomIndex] = temp;
        }

        for (int i = 0; i < pelletsPerShot; i++)
        {
            // Horizontal spread only, tiny vertical offset for variation
            float randomYaw = Random.Range(-spreadAngle, spreadAngle);
            float randomPitch = Random.Range(-spreadAngle * 0.05f, spreadAngle * 0.05f);

            Vector3 direction = Quaternion.Euler(randomPitch, randomYaw, 0) * forwardDir;

            // Rotation for top-down visibility
            Quaternion rot = Quaternion.LookRotation(direction) * Quaternion.Euler(90, 0, 0);

            GameObject pellet = Instantiate(pelletPrefab, spawnPos, rot);

            // Use Rigidbody for movement
            Rigidbody rb = pellet.GetComponent<Rigidbody>();
            if (rb != null && !rb.isKinematic)
                rb.velocity = direction * pelletSpeed;

            // Random sprite
            SpriteRenderer sr = pellet.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.sprite = pelletSprites[Random.Range(0, pelletSprites.Length)];

            pellet.transform.localScale *= 0.7f;

            // Determine lifetime based on distance
            float destroyDistance;
            int pelletIndex = pelletIndices[i];
            if (pelletIndex < pelletsPerShot / 3)
                destroyDistance = Random.Range(range * 0.25f, range * 0.4f);
            else if (pelletIndex < pelletsPerShot * 2 / 3)
                destroyDistance = Random.Range(range * 0.55f, range * 0.75f);
            else
                destroyDistance = Random.Range(range * 0.9f, range);

            float pelletLifetime = destroyDistance / pelletSpeed;

            // Initialize PelletManager
            PelletManager controller = pellet.GetComponent<PelletManager>();
            if (controller != null)
                controller.Initialize(pelletLifetime, pelletExplodePrefab);
        }
    }


    public void Reload()
    {
        if (currentAmmo == maxAmmo) {
            return;
        }

        GameManager.Instance.NextTurn();

        if (isReloading) return;

        isReloading = true;
        Debug.Log("Reloading...");

        // full reload after delay (turn-based, so instant is also fine)
        currentAmmo = maxAmmo;
        isReloading = false;

        Debug.Log("Reload complete. Ammo: " + currentAmmo);
    }
}