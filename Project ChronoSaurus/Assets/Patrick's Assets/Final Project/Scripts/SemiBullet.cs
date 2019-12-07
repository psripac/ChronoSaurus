using UnityEngine;
using System.Collections.Generic;

public class SemiBullet : MonoBehaviour
{
    public Rigidbody projectile;

    public Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);

    float damage;

    // Use this for initialization
    void Start()
    {
        projectile = GetComponent<Rigidbody>();
        damage = transform.GetComponentInParent<GunSemiAuto>().damage;
    }

    // Update is called once per frame
    void Update()
    {
        TargetHit();
    }

    void TargetHit()
    {
        Vector3 currentPos = transform.position;
        Vector3 newPos = currentPos + velocity * Time.deltaTime;

        transform.position = newPos;

        RaycastHit hit;
        if (Physics.Raycast(currentPos, newPos, out hit, 10))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.name.Contains("Velociraptor"))
            {
                Velocirap target = hit.transform.GetComponent<Velocirap>();
                target.takeDamage(damage);
            }

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 3.0f);
        }
    }
}