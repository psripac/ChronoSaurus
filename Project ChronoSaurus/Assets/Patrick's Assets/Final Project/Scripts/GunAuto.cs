﻿using UnityEngine;

public class GunAuto : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public Vector3 rotation = Vector3.zero;

    public ParticleSystem MuzzleFlash;
    public ParticleSystem cartridgeEjection;

    Vector3 lookPos;

    public GameObject bulletPrefab;
    public GameObject bulletSpawn;
    public GameObject aimAt;
    GameObject bullet;

    AudioSource gunshot;

    public float BULLET_BASE_SPEED = 1.0f;

    private float nexTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nexTimeToFire)
        {
            nexTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            Debug.Log("Gun Firing...");
        }
    }

    void Shoot()
    {
        gunshot = GetComponent<AudioSource>();
        gunshot.Play();
        MuzzleFlash.Play();
        cartridgeEjection.Play();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit0;

        if (Physics.Raycast(ray, out hit0, 100))
        {
            lookPos = hit0.point;
        }

        Vector3 lookDir = lookPos - transform.position;
        lookDir.y = 0;

        bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, Quaternion.Euler(rotation * Time.deltaTime)) as GameObject;
        bullet.transform.parent = transform;
        bullet.GetComponent<AutoBullet>().velocity = aimAt.transform.forward * (BULLET_BASE_SPEED);
    }
}