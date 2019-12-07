using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigidBody;
    private Animator animator;
    public float health = 100f;
    public float speed = 4f;

    public GameObject weapon;

    Vector3 lookPos;
    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            lookPos = hit.point;
        }

        Vector3 lookDir = lookPos - transform.position;
        lookDir.y = 0;

        transform.LookAt(transform.position + lookDir, Vector3.up);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movement = new Vector3(horizontal, 0, vertical);

        if (health <= 0)
        {
            lookPos = Vector3.zero;
        }

        animCharacter(movement, lookDir);
    }

    void Die()
    {
        animator.SetInteger("Idle", -1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveCharacter(movement);

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    void moveCharacter(Vector3 direction)
    {
        rigidBody.velocity = direction*speed;

        if (health <= 0)
        {
            Die();
            rigidBody.velocity = Vector3.zero;
            FindObjectOfType<GameManager>().EndGame();
            Destroy(weapon);
        }
    }

    void animCharacter(Vector3 moveTo, Vector3 lookTo)
    {
        Quaternion lookQuaternion = (Quaternion.LookRotation(lookTo, Vector3.up));
        float lookInQuaternions = lookQuaternion.y;

        if (lookInQuaternions >= 0.9) //looking down
        {
            if (moveTo.z < 0) //moving down
            {
                animator.SetFloat("walkForward", (-moveTo.z));
                animator.SetFloat("walkBackward", 0);
                animator.SetFloat("strafeLeft", 0);
                animator.SetFloat("strafeRight", 0);
            }
            if (moveTo.z > 0) //moving up
            {
                animator.SetFloat("walkBackward", moveTo.z);
                animator.SetFloat("walkForward", 0);
                animator.SetFloat("strafeLeft", 0);
                animator.SetFloat("strafeRight", 0);
            }
            if (moveTo.x < 0) //moving left
            {
                animator.SetFloat("strafeRight", (-moveTo.x));
                animator.SetFloat("walkForward", 0);
                animator.SetFloat("walkBackward", 0);
                animator.SetFloat("strafeLeft", 0);
            }
            if (moveTo.x > 0) //moving right
            {
                animator.SetFloat("strafeLeft", moveTo.x);
                animator.SetFloat("walkForward", 0);
                animator.SetFloat("walkBackward", 0);
                animator.SetFloat("strafeRight", 0);
            }
        }
        else if (lookInQuaternions < (-0.3) && lookInQuaternions >= (-0.9)) //looking left
        {
            if (moveTo.x > 0) //moving right
            {
                animator.SetFloat("walkBackward", moveTo.x);
                animator.SetFloat("walkForward", 0);
                animator.SetFloat("strafeLeft", 0);
                animator.SetFloat("strafeRight", 0);
            }
            if (moveTo.x < 0) //moving left
            {
                animator.SetFloat("walkForward", (-moveTo.x));
                animator.SetFloat("walkBackward", 0);
                animator.SetFloat("strafeLeft", 0);
                animator.SetFloat("strafeRight", 0);
            }
            if (moveTo.z > 0) //moving up
            {
                animator.SetFloat("strafeRight", moveTo.z);
                animator.SetFloat("walkForward", 0);
                animator.SetFloat("walkBackward", 0);
                animator.SetFloat("strafeLeft", 0);
            }
            if (moveTo.z < 0) //moving down
            {
                animator.SetFloat("strafeLeft", (-moveTo.z));
                animator.SetFloat("walkForward", 0);
                animator.SetFloat("walkBackward", 0);
                animator.SetFloat("strafeRight", 0);
            }
        }
        else if (lookInQuaternions >= (-0.3) && lookInQuaternions <= 0.3) //looking up
        {
            if (moveTo.x > 0) //moving right
            {
                animator.SetFloat("strafeRight", moveTo.x);
                animator.SetFloat("walkForward", 0);
                animator.SetFloat("walkBackward", 0);
                animator.SetFloat("strafeLeft", 0);
            }
            if (moveTo.x < 0) //moving left
            {
                animator.SetFloat("strafeLeft", (-moveTo.x));
                animator.SetFloat("walkForward", 0);
                animator.SetFloat("walkBackward", 0);
                animator.SetFloat("strafeRight", 0);
            }
            if (moveTo.z > 0) //moving up
            {
                animator.SetFloat("walkForward", moveTo.z);
                animator.SetFloat("walkBackward", 0);
                animator.SetFloat("strafeLeft", 0);
                animator.SetFloat("strafeRight", 0);
            }
            if (moveTo.z < 0) //moving down
            {
                animator.SetFloat("walkBackward", (-moveTo.z));
                animator.SetFloat("walkForward", 0);
                animator.SetFloat("strafeLeft", 0);
                animator.SetFloat("strafeRight", 0);
            }
        }
        else if (lookInQuaternions > 0.3 && lookInQuaternions < 0.9) //looking right
        {
            if (moveTo.x > 0) //moving right
            {
                animator.SetFloat("walkForward", moveTo.x);
                animator.SetFloat("walkBackward", 0);
                animator.SetFloat("strafeLeft", 0);
                animator.SetFloat("strafeRight", 0);
            }
            if (moveTo.x < 0) //moving left
            {
                animator.SetFloat("walkBackward", (-moveTo.x));
                animator.SetFloat("walkForward", 0);
                animator.SetFloat("strafeLeft", 0);
                animator.SetFloat("strafeRight", 0);
            }
            if (moveTo.z > 0) //moving up
            {
                animator.SetFloat("strafeLeft", moveTo.z);
                animator.SetFloat("walkForward", 0);
                animator.SetFloat("walkBackward", 0);
                animator.SetFloat("strafeRight", 0);
            }
            if (moveTo.z < 0) //moving down
            {
                animator.SetFloat("strafeRight", (-moveTo.z));
                animator.SetFloat("walkForward", 0);
                animator.SetFloat("walkBackward", 0);
                animator.SetFloat("strafeLeft", 0);
            }
        }
        else
        {
            animator.SetFloat("walkForward", 0);
            animator.SetFloat("walkBackward", 0);
            animator.SetFloat("strafeLeft", 0);
            animator.SetFloat("strafeRight", 0);
        }

    }
}
