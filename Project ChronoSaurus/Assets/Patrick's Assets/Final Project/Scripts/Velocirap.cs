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
    public float disToTarget;

    void Start()
    {
        animate = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        disToTarget = Mathf.Sqrt(Mathf.Pow((transform.position.z - target.position.z), 2) + Mathf.Pow((transform.position.x - target.position.x), 2));
        Debug.Log(disToTarget);

        if (Health <= 0)
        {
            animate.SetInteger("Idle", -1);
        }
        else if (disToTarget >= 2.0f && disToTarget <= 6.0f)
        {
            FollowPlayer();
        }
        else if (disToTarget < 2.0f)
        {
            AttackPlayer();
        }
        else if (disToTarget > 6.0f && disToTarget <= 12.0f)
        {
            RunToPlayer();
        }
        else
        {
            animate.SetInteger("Move", 0);
        }

    }

    void LateUpdate()
    {
        
    }

    void FollowPlayer()
    {
        transform.LookAt(target, Vector3.up);
        transform.position += transform.forward * Speed * Time.deltaTime;
        animate.SetInteger("Move", 1);
    }
    void RunToPlayer()
    {
        transform.LookAt(target, Vector3.up);
        transform.position += transform.forward * (Speed*2.0f) * Time.deltaTime;
        animate.SetInteger("Move", 2);
    }
    void AttackPlayer()
    {
        transform.LookAt(target, Vector3.up);
        animate.SetBool("Attack", true);
        animate.SetInteger("Move", 0);
        Invoke("StopAttacking", 1.0f);
    }
    void StopAttacking()
    {
        animate.SetBool("Attack", false);
    }
}