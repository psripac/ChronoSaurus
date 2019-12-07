using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    public float damage;
    public Vector3 velocity = new Vector3(0.0f, 0.0f, 0.0f);

    public Rigidbody rbody;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = new Vector3(transform.position.x, 0.0f, transform.position.z);
        Vector3 newPos = currentPos + velocity * Time.deltaTime;

        transform.position = newPos;

        RaycastHit hit;
        if (Physics.Raycast(currentPos, newPos, out hit, 10))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
