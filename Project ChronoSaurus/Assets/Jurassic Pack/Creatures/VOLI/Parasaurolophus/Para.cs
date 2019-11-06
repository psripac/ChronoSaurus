using UnityEngine;

public class Para : Creature
{
  [Space (10)] [Header("PARASAUROLOPHUS SOUNDS")]
	public AudioClip Waterflush;
  public AudioClip Hit_jaw;
  public AudioClip Hit_head;
  public AudioClip Hit_tail;
  public AudioClip Medstep;
  public AudioClip Medsplash;
  public AudioClip Sniff2;
  public AudioClip Chew;
  public AudioClip Largestep;
  public AudioClip Largesplash;
  public AudioClip Idleherb;
  public AudioClip Para1;
  public AudioClip Para2;
  public AudioClip Para3;
  public AudioClip Para4;
	Transform Spine0, Spine1, Spine2, Spine3, Spine4, Neck0, Neck1, Neck2, Neck3, Head, Tail0, Tail1, Tail2, Tail3, Tail4, Tail5, Tail6, Tail7, Tail8, 
	Left_Arm0, Right_Arm0, Left_Arm1, Right_Arm1, Left_Hand, Right_Hand, 
	Left_Hips, Right_Hips, Left_Leg, Right_Leg, Left_Foot, Right_Foot;

	//*************************************************************************************************************************************************
	//Get bone transforms
	void Start()
	{
		Left_Hips = transform.Find ("Para/root/pelvis/left hips");
		Right_Hips = transform.Find ("Para/root/pelvis/right hips");
		Left_Leg  = transform.Find ("Para/root/pelvis/left hips/left leg");
		Right_Leg = transform.Find ("Para/root/pelvis/right hips/right leg");
		
		Left_Foot = transform.Find ("Para/root/pelvis/left hips/left leg/left foot");
		Right_Foot = transform.Find ("Para/root/pelvis/right hips/right leg/right foot");
		
		Left_Arm0 = transform.Find ("Para/root/spine0/spine1/spine2/spine3/spine4/left arm0");
		Right_Arm0 = transform.Find ("Para/root/spine0/spine1/spine2/spine3/spine4/right arm0");
		Left_Arm1 = transform.Find ("Para/root/spine0/spine1/spine2/spine3/spine4/left arm0/left arm1");
		Right_Arm1 = transform.Find ("Para/root/spine0/spine1/spine2/spine3/spine4/right arm0/right arm1");
		
		Left_Hand = transform.Find ("Para/root/spine0/spine1/spine2/spine3/spine4/left arm0/left arm1/left hand");
		Right_Hand = transform.Find ("Para/root/spine0/spine1/spine2/spine3/spine4/right arm0/right arm1/right hand");
		
		Tail0 = transform.Find ("Para/root/pelvis/tail0");
		Tail1 = transform.Find ("Para/root/pelvis/tail0/tail1");
		Tail2 = transform.Find ("Para/root/pelvis/tail0/tail1/tail2");
		Tail3 = transform.Find ("Para/root/pelvis/tail0/tail1/tail2/tail3");
		Tail4 = transform.Find ("Para/root/pelvis/tail0/tail1/tail2/tail3/tail4");
		Tail5 = transform.Find ("Para/root/pelvis/tail0/tail1/tail2/tail3/tail4/tail5");
		Tail6 = transform.Find ("Para/root/pelvis/tail0/tail1/tail2/tail3/tail4/tail5/tail6");
		Tail7 = transform.Find ("Para/root/pelvis/tail0/tail1/tail2/tail3/tail4/tail5/tail6/tail7");
		Tail8 = transform.Find ("Para/root/pelvis/tail0/tail1/tail2/tail3/tail4/tail5/tail6/tail7/tail8");
    Spine0 = transform.Find ("Para/root/spine0");
		Spine1 = transform.Find ("Para/root/spine0/spine1");
		Spine2 = transform.Find ("Para/root/spine0/spine1/spine2");
		Spine3 = transform.Find ("Para/root/spine0/spine1/spine2/spine3");
    Spine4 = transform.Find ("Para/root/spine0/spine1/spine2/spine3/spine4");
		Neck0 = transform.Find ("Para/root/spine0/spine1/spine2/spine3/spine4/neck0");
		Neck1 = transform.Find ("Para/root/spine0/spine1/spine2/spine3/spine4/neck0/neck1");
    Neck2 = transform.Find ("Para/root/spine0/spine1/spine2/spine3/spine4/neck0/neck1/neck2");
    Neck3 = transform.Find ("Para/root/spine0/spine1/spine2/spine3/spine4/neck0/neck1/neck2/neck3");
		Head = transform.Find ("Para/root/spine0/spine1/spine2/spine3/spine4/neck0/neck1/neck2/neck3/head");

    crouch_max=4f;
		ang_t=0.03f;
		yaw_max=20f;
		pitch_max=15f;
	}

	//*************************************************************************************************************************************************
	//Play sound
	void OnCollisionStay(Collision col)
	{
		int rndPainsnd=Random.Range(0, 4); AudioClip painSnd=null;
		switch (rndPainsnd) { case 0: painSnd=Para1; break; case 1: painSnd=Para2; break; case 2: painSnd=Para3; break; case 3: painSnd=Para4; break; }
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
				else if(IsOnWater) source[1].PlayOneShot(Medsplash, Random.Range(0.25f, 0.5f));
				else if(IsOnGround) source[1].PlayOneShot(Medstep, Random.Range(0.25f, 0.5f));
				lastframe=currframe; break;
			case "Hit": source[1].pitch=Random.Range(1.0f, 1.25f); source[1].PlayOneShot(IsOnWater|IsInWater?Largesplash:Largestep, 1.0f);
				lastframe=currframe; break;
			case "Die": source[1].pitch=Random.Range(1.0f, 1.25f); source[1].PlayOneShot(IsOnWater|IsInWater?Largesplash:Largestep, 1.0f);
				lastframe=currframe; IsDead=true; break;
			case "Sniff": source[0].pitch=Random.Range(1.0f, 1.25f); source[0].PlayOneShot(Sniff2, 0.5f);
				lastframe=currframe; break;
			case "Chew": source[0].pitch=Random.Range(1.0f, 1.25f); source[0].PlayOneShot(Chew, 0.5f);
				lastframe=currframe; break;
			case "Repose": source[0].pitch=Random.Range(1.0f, 1.25f); source[0].PlayOneShot(Idleherb, 0.25f);
				lastframe=currframe; break;
			case "Growl": int rnd1 = Random.Range (0, 2); source[0].pitch=Random.Range(1.0f, 1.25f);
				if(rnd1==0)source[0].PlayOneShot(Para1, 1.0f);
				else source[0].PlayOneShot(Para2, 1.0f);
				lastframe=currframe; break;
			case "Call": int rnd2 = Random.Range (0, 2); source[0].pitch=Random.Range(1.0f, 1.25f);
				if(rnd2==0)source[0].PlayOneShot(Para3, 1.0f);
				else source[0].PlayOneShot(Para4, 1.0f);
				lastframe=currframe; break;
			}
		}
	}
	
	//*************************************************************************************************************************************************
	// Add forces to the Rigidbody
	void FixedUpdate ()
	{
		StatusUpdate(); if(!IsActive | AnimSpeed==0.0f) { body.Sleep(); return; }
		OnReset=false; IsConstrained= false;

    //Set mass
		if(IsInWater) { if(Health>0) anm.SetInteger ("Move", 2); body.mass=2; body.drag=4; body.angularDrag=4; }
		else { body.mass=1; body.drag=4; body.angularDrag=4; }
    //Set Y position
    if(IsOnGround | IsInWater | IsOnWater)
    body.AddForce((Vector3.up*Size)*(posY-transform.position.y), ForceMode.VelocityChange);
    else body.AddForce((Vector3.up*Size)*-256, ForceMode.Acceleration);
		
		//Stopped
		if(NextAnm.IsName("Para|Idle1A") | NextAnm.IsName("Para|Idle2A") | CurrAnm.IsName("Para|Idle1A") | CurrAnm.IsName("Para|Idle2A") |
			CurrAnm.IsName("Para|Die1") | CurrAnm.IsName("Para|Die2"))
		{
			if(CurrAnm.IsName("Para|Die1")) { OnReset=true; if(!IsDead) { PlaySound("Growl", 2); PlaySound("Die", 12); } }
			else if(CurrAnm.IsName("Para|Die2")) { OnReset=true; if(!IsDead) { PlaySound("Growl", 2); PlaySound("Die", 10); } }
		}
		
		//Forward
		else if(CurrAnm.IsName("Para|Walk") | CurrAnm.IsName("Para|WalkGrowl") | CurrAnm.IsName("Para|Step1") | CurrAnm.IsName("Para|Step2") |
		   CurrAnm.IsName("Para|ToEatA") | CurrAnm.IsName("Para|ToEatC") | CurrAnm.IsName("Para|ToIdle1D"))
		{
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
			body.AddForce(transform.forward*15*Size*anm.speed);
			if(CurrAnm.IsName("Para|WalkGrowl")) { PlaySound("Growl", 1); PlaySound("Step", 6); PlaySound("Step", 13); }
			else if(CurrAnm.IsName("Para|Walk")) { PlaySound("Step", 6); PlaySound("Step", 13); }
			else PlaySound("Step", 9);
		}

		//Running
		else if(NextAnm.IsName("Para|Run") | CurrAnm.IsName("Para|Run") | CurrAnm.IsName("Para|RunGrowl"))
		{
      roll=Mathf.Clamp(Mathf.Lerp(roll, spineX*10.0f, 0.1f), -20f, 20f);
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
			body.AddForce(transform.forward*80*Size*anm.speed);
			if(CurrAnm.IsName("Para|RunGrowl")) { PlaySound("Growl", 2); PlaySound("Step", 5); PlaySound("Step", 12); }
			else { PlaySound("Step", 5); PlaySound("Step", 12);}
		}
		
		//Backward
		else if(CurrAnm.IsName("Para|Step1-") | CurrAnm.IsName("Para|Step2-") | CurrAnm.IsName("Para|ToSit1"))
		{
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
			body.AddForce(transform.forward*-15*Size*anm.speed);
			PlaySound("Step", 9);
		}
		
		//Strafe/Turn right
		else if(CurrAnm.IsName("Para|Strafe1+") | CurrAnm.IsName("Para|Strafe2+"))
		{
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
			body.AddForce(transform.right*8*Size*anm.speed);
			PlaySound("Step", 5); PlaySound("Step", 12);
		}
		
		//Strafe/Turn left
		else if(CurrAnm.IsName("Para|Strafe1-") |CurrAnm.IsName("Para|Strafe2-"))
		{
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
			body.AddForce(transform.right*-8*Size*anm.speed);
			PlaySound("Step", 5); PlaySound("Step", 12);
		}

		//Various
		else if(CurrAnm.IsName("Para|EatA")) PlaySound("Chew", 10);
		else if(CurrAnm.IsName("Para|EatB")) { PlaySound("Chew", 1); PlaySound("Chew", 4); PlaySound("Chew", 8); PlaySound("Chew", 12); }
		else if(CurrAnm.IsName("Para|EatC")) OnReset=true;
		else if(CurrAnm.IsName("Para|ToSit")) IsConstrained=true;
		else if(CurrAnm.IsName("Para|SitIdle")) IsConstrained=true;
		else if(CurrAnm.IsName("Para|Sleep")) { OnReset=true; IsConstrained=true; PlaySound("Repose", 2); }
		else if(CurrAnm.IsName("Para|SitGrowl")) { IsConstrained=true; PlaySound("Growl", 2); }
		else if(CurrAnm.IsName("Para|Idle1B")) PlaySound("Growl", 2);
		else if(CurrAnm.IsName("Para|Idle1C")) PlaySound("Call", 1);
		else if(CurrAnm.IsName("Para|Idle1D")) { OnReset=true; PlaySound("Sniff", 1); }
		else if(CurrAnm.IsName("Para|Idle2B")) PlaySound("Growl", 2);
		else if(CurrAnm.IsName("Para|Idle2C")) PlaySound("Call", 2);
		else if(CurrAnm.IsName("Para|ToRise1") | CurrAnm.IsName("Para|ToRise2")) { OnReset=true; PlaySound("Sniff", 3); PlaySound("Growl", 1); }
		else if(CurrAnm.IsName("Para|ToRise1-") | CurrAnm.IsName("Para|ToRise2-")) { OnReset=true; PlaySound("Hit", 7); }
		else if(CurrAnm.IsName("Para|Rise1Growl")) { OnReset=true; PlaySound("Call", 1); }
		else if(CurrAnm.IsName("Para|Rise2Growl")) { OnReset=true; PlaySound("Growl", 1); }
    else if(CurrAnm.IsName("Para|Rise1Idle") | CurrAnm.IsName("Para|Rise2Idle")) OnReset=true;
		else if(CurrAnm.IsName("Para|Die1-") | CurrAnm.IsName("Para|Die2-")) { PlaySound("Growl", 3); IsDead=false; }
	}
	
	void LateUpdate()
	{
		//*************************************************************************************************************************************************
		// Bone rotation
		if(!IsActive) return;

		TakeDamageAnim(Quaternion.Euler(-lastHit, 0, 0));

    //Spine rotation
    RotateBone(50f);
    Spine0.rotation*= Quaternion.Euler(0, 0, spineX);
		Spine1.rotation*= Quaternion.Euler(0, 0, spineX);
		Spine2.rotation*= Quaternion.Euler(0, 0, spineX);
		Spine3.rotation*= Quaternion.Euler(0, 0, spineX);
    Spine4.rotation*= Quaternion.Euler(0, 0, spineX);
    Neck0.rotation*= Quaternion.Euler(0, headX, headX);
		Neck1.rotation*= Quaternion.Euler(0, headX, headX);
		Neck2.rotation*= Quaternion.Euler(headY, headX, headX);
		Neck3.rotation*= Quaternion.Euler(headY*1.5f, headX, headX);
		Head.rotation*= Quaternion.Euler(headY*2.0f, headX, headX);
  	//Tail rotation
		Tail0.rotation*= Quaternion.Euler(0, 0, -spineX);
		Tail1.rotation*= Quaternion.Euler(0, 0, -spineX);
		Tail2.rotation*= Quaternion.Euler(0, 0, -spineX);
		Tail3.rotation*= Quaternion.Euler(0, 0, -spineX);
		Tail4.rotation*= Quaternion.Euler(0, 0, -spineX);
		Tail5.rotation*= Quaternion.Euler(0, 0, -spineX);
		Tail6.rotation*= Quaternion.Euler(0, 0, -spineX);
		Tail7.rotation*= Quaternion.Euler(0, 0, -spineX);
		Tail8.rotation*= Quaternion.Euler(0, 0, -spineX);
    //Legs rotation
		roll=Mathf.Lerp(roll, 0.0f, ang_t);
		Right_Hips.rotation*= Quaternion.Euler(-roll, 0, 0);
		Left_Hips.rotation*= Quaternion.Euler(-roll, 0, 0);
		Right_Arm0.rotation*= Quaternion.Euler(-roll, 0, 0);
		Left_Arm1.rotation*= Quaternion.Euler(0, roll, 0);

    HeadPos=Head;

		//Check for ground layer
		GetGroundPos(IkType.Quad, Right_Hips, Right_Leg, Right_Foot, Left_Hips, Left_Leg, Left_Foot, Right_Arm0, Right_Arm1, Right_Hand, Left_Arm0, Left_Arm1, Left_Hand, -0.5f*Size);

		//*************************************************************************************************************************************************
		// CPU
		if(UseAI && Health!=0) { AICore(1, 2, 3, 4, 5, 6, 7); }
		//*************************************************************************************************************************************************
		// Human
		else if(Health!=0) { GetUserInputs(1, 2, 3, 4, 5, 6, 7, 4); }
		//*************************************************************************************************************************************************
		//Dead
		else { anm.SetInteger ("Move", 0); anm.SetInteger ("Idle", -1); }
	}
}
