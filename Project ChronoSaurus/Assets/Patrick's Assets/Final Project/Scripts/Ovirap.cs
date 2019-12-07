using System;
using UnityEngine;

public class Ovirap : MonoBehaviour
{
    public float maxHealth = 100f;
    public float Speed = 6f;
    public float AttackDamage = 10f;
    public float AttackSpeed = 2f;

    public float minRange = 0f;
    public float maxRange = 0f;
    public float runRange = 0f;

    private float health;

    Animator animate;
    public AudioSource deathsound;

    public Transform target;
    float disToTarget;

    private float NextAttackRate = 0f;
    bool isDead = false;
    bool isRunning = false;

    void Start()
    {
        animate = GetComponentInChildren<Animator>();
        health = maxHealth;
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
        else if (disToTarget >= minRange && disToTarget <= runRange)
        {
            FollowPlayer();
            Invoke("StopAttacking", 1.0f);
        }
        else if (disToTarget < minRange)
        {
            AttackPlayer();
        }
        else if (disToTarget > runRange && disToTarget <= maxRange)
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
        transform.position += transform.forward * (Speed * 2.0f) * Time.deltaTime;
        animate.SetInteger("Move", 2);
        if (isRunning == false)
        {
            deathsound.Play();
            isRunning = true;
        }
    }
    void Death()
    {
        animate.SetInteger("Idle", -1);
        animate.SetInteger("Move", 0);
        Invoke("KillDino", 5.0f);
    }
    void AttackPlayer()
    {
        transform.LookAt(target, Vector3.up);
        animate.SetBool("Attack", true);
        animate.SetInteger("Move", 0);
        if (Time.time >= NextAttackRate)
        {
            NextAttackRate = Time.time + 1f / AttackSpeed;
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
        health -= damage;
        Debug.Log(transform.name + " health: " + health);
        float currentHealthPct = (float)health / (float)maxHealth;
        GetComponentInChildren<HealthBar>().ChangeHealthBar(currentHealthPct);
        if (health <= 0 && isDead == false)
        {
            deathsound.Play();
            isDead = true;
        }
    }
}