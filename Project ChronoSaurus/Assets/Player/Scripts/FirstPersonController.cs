using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
	public Transform feet;
	public LayerMask ground;
	private Vector3 direction;
	private Rigidbody rbody;
	private float jumpHeight;
	private int doubleJump = 0;
    private float speed = 2f;

    private float rotationSpeed = 1f;
    private float rotationX = 0;
    private float rotationY = 10f;

    //private AudioSource audio1;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        jumpHeight = 6.0f;
        //audio1 = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = Vector3.zero;
        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");
        direction = direction.normalized;
        if(direction.x != 0)
            rbody.MovePosition(rbody.position+transform.right*direction.x*speed*Time.deltaTime);
        if(direction.z != 0)
            rbody.MovePosition(rbody.position+transform.forward*direction.z*speed*Time.deltaTime);

        rotationX = transform.localEulerAngles.y+Input.GetAxis("Mouse X")*rotationSpeed;
        rotationY += Input.GetAxis("Mouse Y")*rotationSpeed;
        transform.localEulerAngles = new Vector3(-rotationY,rotationX,0);

        bool isGrounded() {
        	if(Physics.CheckSphere(feet.position, 0.1f, ground, QueryTriggerInteraction.Ignore)) {
        		doubleJump = 0;
        		return true;
        	} else {
        		return false;
        	}
		} 
       	Debug.Log(isGrounded());
        if(Input.GetButtonDown("Jump") && (isGrounded() || doubleJump < 2)) {
        	//audio1.Play();
            doubleJump += 1;
        	rbody.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
        }
        if(Input.GetKey ("escape"))
        {

            Application.LoadLevel(0);
        }
    }
}





