using UnityEngine;

public class Pistol : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;

    
    public ParticleSystem MuzzleFlash;
    public ParticleSystem cartridgeEjection;

    Vector3 lookPos;

    public GameObject bulletPrefab;
    public GameObject aimAt;
    public GameObject bulletSpawn;
    AudioSource gunshot;

    public float BULLET_BASE_SPEED = 1.0f;

    // Update is called once per frame
    void Update()
    {
        gunshot = GetComponent<AudioSource>();
   
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
            gunshot.Play();
        }

    }


    void Shoot()
    {
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

        transform.LookAt(transform.position + lookDir, Vector3.up);

        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.transform.position, Quaternion.identity);
        bullet.GetComponent<BulletImpact>().velocity = aimAt.transform.forward * (BULLET_BASE_SPEED);
        bullet.transform.Rotate(0, 0, Mathf.Atan2(lookDir.z, lookDir.x) * Mathf.Rad2Deg);
        Destroy(bullet, 1f);
    }


}