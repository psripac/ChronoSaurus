using UnityEngine;

public class GunSemiAuto : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public ParticleSystem MuzzleFlash;
    public ParticleSystem cartridgeEjection;

    public GameObject bulletPrefab;
    public GameObject bulletSpawn;
    GameObject pc;
    WaitForSecondsRealtime wait;

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
        MuzzleFlash.Play();
        cartridgeEjection.Play();
    }


}