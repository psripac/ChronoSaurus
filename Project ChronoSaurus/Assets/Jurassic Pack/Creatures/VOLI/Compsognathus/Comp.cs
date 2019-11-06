using UnityEngine;

public class Comp : Creature
{
  [Space (10)] [Header("COMPSOGNATHUS SOUNDS")]
	public AudioClip Waterflush;
  public AudioClip Hit_jaw;
  public AudioClip Hit_head;
  public AudioClip Hit_tail;
  public AudioClip Smallstep;
  public AudioClip Smallsplash;
  public AudioClip Bite;
  public AudioClip Comp1;
  public AudioClip Comp2;
  public AudioClip Comp3;
  public AudioClip Comp4;
  public AudioClip Comp5;
	Transform Spine0, Spine1, Spine2, Spine3, Spine4, Spine5, Neck0, Neck1, Neck2, Neck3, Head, 
	Tail0, Tail1, Tail2, Tail3, Tail4, Tail5, Tail6, Tail7, Tail8, Arm1, Arm2, 
	Left_Hips, Right_Hips, Left_Leg, Right_Leg, Left_Foot0, Right_Foot0;
  Vector3 dir=Vector3.zero;

	//*************************************************************************************************************************************************
	//Get bone transforms
	void Start()
	{
		Right_Hips = transform.Find ("Comp/root/pelvis/right leg0");
		Right_Leg = transform.Find ("Comp/root/pelvis/right leg0/right leg1");
		Right_Foot0 = transform.Find ("Comp/root/pelvis/right leg0/right leg1/right foot0");

		Left_Hips = transform.Find ("Comp/root/pelvis/left leg0");
		Left_Leg = transform.Find ("Comp/root/pelvis/left leg0/left leg1");
		Left_Foot0 = transform.Find ("Comp/root/pelvis/left leg0/left leg1/left foot0");
	
		Tail0 = transform.Find ("Comp/root/pelvis/tail0");
		Tail1 = transform.Find ("Comp/root/pelvis/tail0/tail1");
		Tail2 = transform.Find ("Comp/root/pelvis/tail0/tail1/tail2");
		Tail3 = transform.Find ("Comp/root/pelvis/tail0/tail1/tail2/tail3");
		Tail4 = transform.Find ("Comp/root/pelvis/tail0/tail1/tail2/tail3/tail4");
		Tail5 = transform.Find ("Comp/root/pelvis/tail0/tail1/tail2/tail3/tail4/tail5");
		Tail6 = transform.Find ("Comp/root/pelvis/tail0/tail1/tail2/tail3/tail4/tail5/tail6");
		Tail7 = transform.Find ("Comp/root/pelvis/tail0/tail1/tail2/tail3/tail4/tail5/tail6/tail7");
		Tail8 = transform.Find ("Comp/root/pelvis/tail0/tail1/tail2/tail3/tail4/tail5/tail6/tail7/tail8");
		Spine0 = transform.Find ("Comp/root/spine0");
		Spine1 = transform.Find ("Comp/root/spine0/spine1");
		Spine2 = transform.Find ("Comp/root/spine0/spine1/spine2");
		Spine3 = transform.Find ("Comp/root/spine0/spine1/spine2/spine3");
		Spine4 = transform.Find ("Comp/root/spine0/spine1/spine2/spine3/spine4");
		Spine5 = transform.Find ("Comp/root/spine0/spine1/spine2/spine3/spine4/spine5");
		Arm1 = transform.Find ("Comp/root/spine0/spine1/spine2/spine3/spine4/spine5/left arm0");
		Arm2 = transform.Find ("Comp/root/spine0/spine1/spine2/spine3/spine4/spine5/right arm0");
		Neck0 = transform.Find ("Comp/root/spine0/spine1/spine2/spine3/spine4/spine5/neck0");
		Neck1 = transform.Find ("Comp/root/spine0/spine1/spine2/spine3/spine4/spine5/neck0/neck1");
		Neck2 = transform.Find ("Comp/root/spine0/spine1/spine2/spine3/spine4/spine5/neck0/neck1/neck2");
		Neck3 = transform.Find ("Comp/root/spine0/spine1/spine2/spine3/spine4/spine5/neck0/neck1/neck2/neck3");
		Head  = transform.Find ("Comp/root/spine0/spine1/spine2/spine3/spine4/spine5/neck0/neck1/neck2/neck3/head");

    crouch_max=0.75f;
		ang_t=0.1f;
		yaw_max=16f;
		pitch_max=9f;
	}

	//*************************************************************************************************************************************************
	//Play sound
	void OnCollisionStay(Collision col)
	{
		int rndPainsnd=Random.Range(0, 4); AudioClip painSnd=null;
		switch (rndPainsnd) { case 0: painSnd=Comp1; break; case 1: painSnd=Comp2; break; case 2: painSnd=Comp3; break; case 3: painSnd=Comp4; break; }
		ManageCollision(col, pitch_max, crouch_max, source, painSnd, Hit_jaw, Hit_head, Hit_tail);
	}
	void PlaySound(string name, int time)
	{
		if(time==currframe && lastframe!=currframe)
		{
			switch (name)
			{
			case "Step": source[1].pitch=Random.Range(0.75f, 1.25f);  
				if(IsInWater) source[1].PlayOneShot(Waterflush, Random.Range(0.25f, 0.5f));
				else if(IsOnWater) source[1].PlayOneShot(Smallsplash, Random.Range(0.25f, 0.5f));
				else if(IsOnGround) source[1].PlayOneShot(Smallstep, Random.Range(0.25f, 0.5f));
				lastframe=currframe; break;
			case "Bite": source[1].pitch=Random.Range(1.0f, 1.25f); source[1].PlayOneShot(Bite, 0.5f);
				lastframe=currframe; break;
			case "Die": source[1].pitch=Random.Range(0.8f, 1.0f); source[1].PlayOneShot(IsOnWater|IsInWater?Smallsplash:Smallstep, 1.0f);
				lastframe=currframe; IsDead=true; break;
			case "Call": source[0].pitch=Random.Range(1.0f, 1.25f); source[0].PlayOneShot(Comp4, 1.0f);
				lastframe=currframe; break;
			case "Atk": int rnd1 = Random.Range (0, 3); source[0].pitch=Random.Range(1.0f, 1.75f);
				if(rnd1==0)source[0].PlayOneShot(Comp2, 1.0f);
				else if(rnd1==1)source[0].PlayOneShot(Comp3, 1.0f);
				else if(rnd1==2) source[0].PlayOneShot(Comp5, 1.0f);
				lastframe=currframe; break;
			case "Growl": int rnd2 = Random.Range (0, 5); source[0].pitch=Random.Range(1.0f, 1.75f);
				if(rnd2==0)source[0].PlayOneShot(Comp1, 1.0f);
				else if(rnd2==1)source[0].PlayOneShot(Comp2, 1.0f);
				else if(rnd2==2)source[0].PlayOneShot(Comp3, 1.0f);
				else if(rnd2==3)source[0].PlayOneShot(Comp4, 1.0f);
				else if(rnd2==4)source[0].PlayOneShot(Comp5, 1.0f);
				lastframe=currframe; break;
			}
		}
	}

	//*************************************************************************************************************************************************
	// Add forces to the Rigidbody
	void FixedUpdate()
	{
		StatusUpdate(); if(!IsActive | AnimSpeed==0.0f) { body.Sleep(); return; }
		OnReset=false; OnAttack=false; IsConstrained=false;

    //Set mass
		if(IsInWater) { if(Health>0) anm.SetInteger ("Move", 2); body.mass=2; body.drag=4; body.angularDrag=4; }
    else { body.mass=1; body.drag=4; body.angularDrag=4; }
   
    //Set Y position
    if(IsOnGround | IsInWater | IsOnWater)
    { 
      if(IsOnGround | IsInWater) anm.SetBool("OnGround", true);
      dir=new Vector3(transform.forward.x, 0, transform.forward.z);
      body.AddForce((Vector3.up*Size)*(posY-transform.position.y), ForceMode.VelocityChange);
    } 
    else
    {
      body.AddForce((Vector3.up*Size)*-256, ForceMode.Acceleration);
      anm.SetBool("OnGround", false);
    }

		//Stopped
		if(NextAnm.IsName("Comp|IdleA") | CurrAnm.IsName("Comp|IdleA") | CurrAnm.IsName("Comp|Die"))
		{
			if(CurrAnm.IsName("Comp|Die")) { OnReset=true; if(!IsDead) { PlaySound("AtkB", 2); PlaySound("Die", 12); } }
		}

		//Jump
		else if(CurrAnm.IsName("Comp|IdleJumpStart") | CurrAnm.IsName("Comp|RunJumpStart") | CurrAnm.IsName("Comp|JumpIdle") |
			CurrAnm.IsName("Comp|IdleJumpEnd") | CurrAnm.IsName("Comp|RunJumpEnd") | CurrAnm.IsName("Comp|JumpAtk"))
		{
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
			if(CurrAnm.IsName("Comp|IdleJumpStart") | CurrAnm.IsName("Comp|RunJumpStart"))
			{
				if(CurrAnm.normalizedTime > 0.4) { body.AddForce((Vector3.up*4)*Size, ForceMode.VelocityChange); } else OnJump=true;
        if(!anm.GetInteger("Move").Equals(0)&&CurrAnm.IsName("Comp|RunJumpStart")) body.AddForce(dir*80*Size*anm.speed);
        PlaySound("Step", 1); PlaySound("Step", 2);
			}
			else if(CurrAnm.IsName("Comp|IdleJumpEnd") | CurrAnm.IsName("Comp|RunJumpEnd"))
			{ 
        if(CurrAnm.IsName("Comp|RunJumpEnd")) body.AddForce(dir*80*Size*anm.speed);
        body.velocity = new Vector3(body.velocity.x, 0.0f, body.velocity.z); OnJump=false;
				PlaySound("Step", 3); PlaySound("Step", 4); 
			}
      else if(CurrAnm.IsName("Comp|JumpAtk"))
      {
        if(anm.GetInteger("Move").Equals(1)|anm.GetInteger("Move").Equals(2)) body.AddForce(dir*80*Size*anm.speed, ForceMode.Acceleration);
        OnAttack=true; PlaySound("AtkB", 1); PlaySound("Bite", 9);
        body.velocity = new Vector3(body.velocity.x, body.velocity.y>0?body.velocity.y:0, body.velocity.z);
      } else if(anm.GetInteger("Move").Equals(1)|anm.GetInteger("Move").Equals(2)) body.AddForce(dir*80*Size*anm.speed, ForceMode.Acceleration);
		}

		//Forward
		else if(NextAnm.IsName("Comp|Walk") | CurrAnm.IsName("Comp|Walk"))
		{
      roll=Mathf.Clamp(Mathf.Lerp(roll, spineX*15.0f, 0.1f), -30f, 30f);
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
			body.AddForce(transform.forward*20*Size*anm.speed);
			PlaySound("Step", 8); PlaySound("Step", 9);
		}

		//Running
		else if(NextAnm.IsName("Comp|Run") | CurrAnm.IsName("Comp|Run") |
		   CurrAnm.IsName("Comp|RunGrowl") | CurrAnm.IsName("Comp|RunAtk1") |
		   (CurrAnm.IsName("Comp|RunAtk2") && CurrAnm.normalizedTime < 0.9) |
		   (CurrAnm.IsName("Comp|IdleAtk3") && CurrAnm.normalizedTime > 0.5 && CurrAnm.normalizedTime < 0.9))
		{
      roll=Mathf.Clamp(Mathf.Lerp(roll, spineX*15.0f, 0.1f), -30f, 30f);
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
			body.AddForce(transform.forward*80*Size*anm.speed);
			if(CurrAnm.IsName("Comp|Run")){ PlaySound("Step", 4); PlaySound("Step", 12); }
			else if(CurrAnm.IsName("Comp|RunGrowl")) { PlaySound("Atk", 2); PlaySound("Step", 4); PlaySound("Step", 12); }
			else if( CurrAnm.IsName("Comp|RunAtk1")) { OnAttack=true; PlaySound("Atk", 2); PlaySound("Step", 4); PlaySound("Step", 12); }
			else if( CurrAnm.IsName("Comp|RunAtk2")| CurrAnm.IsName("Comp|IdleAtk3"))
			{ OnAttack=true; PlaySound("Atk", 2); PlaySound("Step", 4); PlaySound("Bite", 9); PlaySound("Step", 12); }
		}
		
		//Backward
		else if(NextAnm.IsName("Comp|Walk-") | CurrAnm.IsName("Comp|Walk-"))
		{
			body.AddForce(transform.forward*-16*Size*anm.speed);
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
			PlaySound("Step", 8); PlaySound("Step", 9);
		}

		//Strafe/Turn right
		else if(NextAnm.IsName("Comp|Strafe-") | CurrAnm.IsName("Comp|Strafe-"))
		{
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
			body.AddForce(transform.right*16*Size*anm.speed);
			PlaySound("Step", 8); PlaySound("Step", 9);
		}
		
		//Strafe/Turn left
		else if(NextAnm.IsName("Comp|Strafe+") | CurrAnm.IsName("Comp|Strafe+"))
		{
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
			body.AddForce(transform.right*-16*Size*anm.speed);
			PlaySound("Step", 8); PlaySound("Step", 9);
		}

		//Various
		else if(CurrAnm.IsName("Comp|IdleAtk3")) { OnAttack=true; transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t); PlaySound("Atk", 1); }
		else if(CurrAnm.IsName("Comp|GroundAtk")) { OnAttack=true; PlaySound("Atk", 2); PlaySound("Bite", 4); }
		else if(CurrAnm.IsName("Comp|IdleAtk1") | CurrAnm.IsName("Comp|IdleAtk2"))
		{ OnAttack=true; transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t); PlaySound("Atk", 2); PlaySound("Bite", 9); }
		else if(CurrAnm.IsName("Comp|ToSleep")) { OnReset=true; IsConstrained=true; }
		else if(CurrAnm.IsName("Comp|Sleep")) { OnReset=true; IsConstrained=true; PlaySound("Repose", 1); }
		else if(CurrAnm.IsName("Comp|EatA")) { OnReset=true; IsConstrained=true; }
		else if(CurrAnm.IsName("Comp|EatB")) { OnReset=true; IsConstrained=true; PlaySound("Bite", 3); }
		else if(CurrAnm.IsName("Comp|EatC")) OnReset=true;
		else if(CurrAnm.IsName("Comp|IdleB")) { OnReset=true; PlaySound("Atk", 1); }
		else if(CurrAnm.IsName("Comp|IdleC")) { OnReset=true; IsConstrained=true; PlaySound("Step", 2); }
		else if(CurrAnm.IsName("Comp|IdleD")) PlaySound("Growl", 1);
		else if(CurrAnm.IsName("Comp|IdleE")) { PlaySound("Call", 1); PlaySound("Call", 4); PlaySound("Call", 8); }
		else if(CurrAnm.IsName("Comp|Die-")) { OnReset=true; PlaySound("Atk", 1);  IsDead=false; }
	}

	void LateUpdate()
	{
		//*************************************************************************************************************************************************
		// Bone rotation
		if(!IsActive) return;

    TakeDamageAnim(Quaternion.Euler(lastHit, 0, 0));

		//Spine rotation
    RotateBone(60f);
		float headZ =-headY*headX/yaw_max;
		Spine0.rotation*=Quaternion.Euler(-headY, headZ, headX);
		Spine1.rotation*=Quaternion.Euler(-headY, headZ, headX);
		Spine2.rotation*=Quaternion.Euler(-headY, headZ, headX);
		Spine3.rotation*=Quaternion.Euler(-headY, headZ, headX);
		Spine4.rotation*=Quaternion.Euler(-headY, headZ, headX);
		Spine5.rotation*=Quaternion.Euler(-headY, headZ, headX);
		//Arms rotation
		Arm1.rotation*=Quaternion.Euler(headY*8, 0, 0);
		Arm2.rotation*=Quaternion.Euler(0, headY*8, 0);
		Neck0.rotation*=Quaternion.Euler(-headY, headZ, headX);
		Neck1.rotation*=Quaternion.Euler(-headY, headZ, headX);
		Neck2.rotation*=Quaternion.Euler(-headY, headZ, headX);
		Neck3.rotation*=Quaternion.Euler(-headY, headZ, headX);
		Head.rotation*=Quaternion.Euler(-headY, headZ, headX);
		//Tail rotation
		Tail0.rotation*=Quaternion.Euler(0, 0, -spineX);
		Tail1.rotation*=Quaternion.Euler(0, 0, -spineX);
		Tail2.rotation*=Quaternion.Euler(0, 0, -spineX);
		Tail3.rotation*=Quaternion.Euler(0, 0, -spineX);
		Tail4.rotation*=Quaternion.Euler(0, 0, -spineX);
		Tail5.rotation*=Quaternion.Euler(0, 0, -spineX);
		Tail6.rotation*=Quaternion.Euler(0, 0, -spineX);
		Tail7.rotation*=Quaternion.Euler(0, 0, -spineX);
		Tail8.rotation*=Quaternion.Euler(0, 0, -spineX);
    //Legs rotation
    roll=Mathf.Lerp(roll, 0.0f, ang_t);
		Right_Hips.rotation*= Quaternion.Euler(-roll, 0, 0);
		Left_Hips.rotation*= Quaternion.Euler(0, roll, 0);
    HeadPos=Head;

		//Check for ground layer
		GetGroundPos(IkType.SmBiped, Right_Hips, Right_Leg, Right_Foot0, Left_Hips, Left_Leg, Left_Foot0); 

		//*************************************************************************************************************************************************
		// CPU
		if(UseAI && Health!=0) { AICore(1, 2, 3, 4, 5, 6, 7); }
		//*************************************************************************************************************************************************
		// Human
		else if(Health!=0) { GetUserInputs(1, 2, 3, 4, 5, 6, 7); }
		//*************************************************************************************************************************************************
		//Dead
		else { anm.SetBool("Attack", false); anm.SetInteger ("Move", 0); anm.SetInteger ("Idle", -1); }
	}
}
