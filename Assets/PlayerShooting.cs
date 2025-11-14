using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Camera cam;
    public GameObject bulletPrefab;
    public float bulletSpeed = 20f;

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
        Vector3 dir = (targetPoint - spawnPos).normalized;

        Quaternion look = Quaternion.LookRotation(dir);
        Quaternion finalRotation = look * Quaternion.Euler(90f, 0f, 0f);

        GameObject bullet = Instantiate(bulletPrefab, spawnPos, finalRotation);

        bullet.transform.localScale = new Vector3(0.4f, 0.4f);

        bullet.GetComponent<Rigidbody>().velocity = dir * bulletSpeed;
    }
}


