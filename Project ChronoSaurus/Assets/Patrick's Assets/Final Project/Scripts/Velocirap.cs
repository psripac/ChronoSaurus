using UnityEngine;

public class Velocirap : MonoBehaviour
{
    public float Health = 100f;
    public float Speed = 6f;
    public float AttackDamage = 10f;

    public float HealthMultiplier = 1.0f;
    public float DamageMultiplier = 1.0f;

    Animator animate;

    public Transform target;

    void Start()
    {
        animate = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
     
    }

    void LateUpdate()
    {
        if (Health <= 0)
        {
            animate.SetInteger("Idle", -1);
        }
        else
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        transform.LookAt(target, Vector3.up);
        transform.position += transform.forward * Speed * Time.deltaTime;
    }
}