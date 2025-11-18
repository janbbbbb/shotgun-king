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

        List<int> pelletIndices = new List<int>();
        for (int i = 0; i < pelletsPerShot; i++)
        {
            pelletIndices.Add(i);
        }

        for (int i = 0; i < pelletIndices.Count; i++)
        {
            int temp = pelletIndices[i];
            int randomIndex = Random.Range(i, pelletIndices.Count);
            pelletIndices[i] = pelletIndices[randomIndex];
            pelletIndices[randomIndex] = temp;
        }


        for (int i = 0; i < pelletsPerShot; i++)
        {
            Vector3 randomDir =
                Quaternion.Euler(
                    Random.Range(-spreadAngle, spreadAngle),
                    Random.Range(-spreadAngle, spreadAngle),
                    0
                ) * forwardDir;

            Quaternion rot = Quaternion.LookRotation(randomDir) * Quaternion.Euler(90, 0, 0);

            GameObject pellet = Instantiate(pelletPrefab, spawnPos, rot);

            pellet.GetComponent<Rigidbody>().velocity = randomDir * pelletSpeed;

            SpriteRenderer sr = pellet.GetComponent<SpriteRenderer>();
            sr.sprite = pelletSprites[Random.Range(0, pelletSprites.Length)];

            pellet.transform.localScale *= 0.7f;

            float destroyDistance;
            int pelletIndex = pelletIndices[i];

            if (pelletIndex < pelletsPerShot / 3)
            {
                destroyDistance = Random.Range(range * 0.25f, range * 0.4f);
            }
            else if (pelletIndex < pelletsPerShot * 2 / 3)
            {
                destroyDistance = Random.Range(range * 0.55f, range * 0.75f);
            }
            else
            {
                destroyDistance = Random.Range(range * 0.9f, range);
            }

            float pelletLifetime = destroyDistance / pelletSpeed;

            PelletManager controller = pellet.GetComponent<PelletManager>();
            if (controller != null)
            {
                controller.Initialize(pelletLifetime, pelletExplodePrefab);
            }
        }
    }
}