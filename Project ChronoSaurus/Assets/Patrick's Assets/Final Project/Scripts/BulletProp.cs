﻿using UnityEngine;
using System.Collections;

public class BulletProp : MonoBehaviour
{
    public Rigidbody projectile;
    public Transform Spawnpoint;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Pressed primary button.");


            Rigidbody clone;
            clone = (Rigidbody)Instantiate(projectile, Spawnpoint.position, projectile.rotation);

            clone.velocity = Spawnpoint.TransformDirection(Vector3.forward * 20);
        }
    }
}