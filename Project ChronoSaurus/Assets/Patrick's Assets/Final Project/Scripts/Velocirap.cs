using UnityEngine;

public class Velocirap : MonoBehaviour
{
    public float health = 100f;
    public float Speed = 6f;
    public float AttackDamage = 10f;
    public float AttackSpeed = 2f;

    public float HealthMultiplier = 1.0f;
    public float DamageMultiplier = 1.0f;

    Animator animate;

    public Transform target;
    public float disToTarget;

    private float NextAttackRate = 0f;

    void Start()
    {
        animate = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        disToTarget = Mathf.Sqrt(Mathf.Pow((transform.position.z - target.position.z), 2) + Mathf.Pow((transform.position.x - target.position.x), 2));
    }

    void Update()
    {
        if (health <= 0)
        {
            Death();
            Invoke("StopAttacking", 1.0f);

        }
        else if (disToTarget >= 2.0f && disToTarget <= 6.0f)
        {
            FollowPlayer();
            Invoke("StopAttacking", 1.0f);
        }
        else if (disToTarget < 2.0f)
        {
            AttackPlayer();
        }
        else if (disToTarget > 6.0f && disToTarget <= 12.0f)
        {
            RunToPlayer();
            Invoke("StopAttacking", 1.0f);
        }
        else
        {
            animate.SetInteger("Move", 0);
        }

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
    void Death()
    {
        animate.SetInteger("Idle", -1);
        animate.SetInteger("Move", 0);
        Invoke("KillDino", 3.0f);
    }
    void AttackPlayer()
    {
        transform.LookAt(target, Vector3.up);
        animate.SetBool("Attack", true);
        animate.SetInteger("Move", 0);
        if(Time.time >= NextAttackRate)
        {
            NextAttackRate = Time.time + 1f/AttackSpeed;
            Damage();
        }
        
    }
    void StopAttacking()
    {
        animate.SetBool("Attack", false);
    }
    void KillDino()
    {
        Destroy(gameObject);
    }
    void Damage()
    {
        target.GetComponent<PlayerController>().takeDamage(AttackDamage);
    }
    public void takeDamage(float damage)
    {
        health = health - damage;
        Debug.Log(transform.name + " health: " + health);
    }
}