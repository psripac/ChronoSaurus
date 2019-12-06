using UnityEngine;

public class GunSemiAuto : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public ParticleSystem MuzzleFlash;
    public ParticleSystem cartridgeEjection;

    public GameObject bulletPrefab;
    public GameObject bulletSpawn;
    GameObject pc;

    public float BULLET_BASE_SPEED = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            Debug.Log("Gun Fired...");
        }

    }

    void Shoot()
    {
        MuzzleFlash.Play();
        cartridgeEjection.Play();
    }


}