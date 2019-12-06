using UnityEngine;

public class GunAuto : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    public ParticleSystem MuzzleFlash;
    public ParticleSystem cartridgeEjection;

    public GameObject bulletPrefab;
    public GameObject bulletSpawn;
    GameObject pc;
    WaitForSecondsRealtime wait;

    public float BULLET_BASE_SPEED = 1.0f;

    // Update is called once per frame
    void Update()
    {
        while (Input.GetButtonDown("Fire1"))
        {
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