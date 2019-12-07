using UnityEngine;

public class GunAuto : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;

    public ParticleSystem MuzzleFlash;
    public ParticleSystem cartridgeEjection;

    public GameObject bulletPrefab;
    public GameObject bulletSpawn;
    GameObject pc;
    AudioSource gunshot;

    public float BULLET_BASE_SPEED = 1.0f;

    private float nexTimeToFire = 0f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nexTimeToFire)
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
    }
}