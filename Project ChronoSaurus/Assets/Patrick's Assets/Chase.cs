using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public Transform target;
    public float speed;

    public GameObject explostion;
    public GameObject hitmark;

    // Use this for initialization
    void Start()
    {
        speed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target, Vector3.up);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

}
