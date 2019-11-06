using UnityEngine;

public class Dime : Creature
{
  [Space (10)] [Header("DIMETRODON SOUNDS")]
	public AudioClip Waterflush;
  public AudioClip Hit_jaw;
  public AudioClip Hit_head;
  public AudioClip Hit_tail;
  public AudioClip Medstep;
  public AudioClip Medsplash;
  public AudioClip Sniff2;
  public AudioClip Bite;
  public AudioClip Swallow;
  public AudioClip Largestep;
  public AudioClip Largesplash;
  public AudioClip Idlecarn;
  public AudioClip Dime1;
  public AudioClip Dime2;
  public AudioClip Dime3;
  public AudioClip Dime4;
	Transform Spine0, Spine1, Spine2, Spine3, Neck0, Neck1, Neck2, Head, Tail0, Tail1, Tail2, Tail3, Tail4, Tail5, Tail6, Tail7, Tail8, 
	Left_Arm0, Right_Arm0, Left_Arm1, Right_Arm1, Left_Hand, Right_Hand, 
	Left_Hips, Right_Hips, Left_Leg, Right_Leg, Left_Foot, Right_Foot;

	//*************************************************************************************************************************************************
	//Get bone transforms
	void Start()
	{
		Left_Hips = transform.Find ("Dime/root/pelvis/left hips");
		Right_Hips = transform.Find ("Dime/root/pelvis/right hips");
		Left_Leg  = transform.Find ("Dime/root/pelvis/left hips/left leg");
		Right_Leg = transform.Find ("Dime/root/pelvis/right hips/right leg");
		
		Left_Foot = transform.Find ("Dime/root/pelvis/left hips/left leg/left foot0");
		Right_Foot = transform.Find ("Dime/root/pelvis/right hips/right leg/right foot0");
		
		Left_Arm0 = transform.Find ("Dime/root/spine0/spine1/spine2/spine3/spine4/left arm0");
		Right_Arm0 = transform.Find ("Dime/root/spine0/spine1/spine2/spine3/spine4/right arm0");
		Left_Arm1 = transform.Find ("Dime/root/spine0/spine1/spine2/spine3/spine4/left arm0/left arm1");
		Right_Arm1 = transform.Find ("Dime/root/spine0/spine1/spine2/spine3/spine4/right arm0/right arm1");
		
		Left_Hand = transform.Find ("Dime/root/spine0/spine1/spine2/spine3/spine4/left arm0/left arm1/left hand0");
		Right_Hand = transform.Find ("Dime/root/spine0/spine1/spine2/spine3/spine4/right arm0/right arm1/right hand0");
	
		Tail0 = transform.Find ("Dime/root/pelvis/tail0");
		Tail1 = transform.Find ("Dime/root/pelvis/tail0/tail1");
		Tail2 = transform.Find ("Dime/root/pelvis/tail0/tail1/tail2");
		Tail3 = transform.Find ("Dime/root/pelvis/tail0/tail1/tail2/tail3");
		Tail4 = transform.Find ("Dime/root/pelvis/tail0/tail1/tail2/tail3/tail4");
		Tail5 = transform.Find ("Dime/root/pelvis/tail0/tail1/tail2/tail3/tail4/tail5");
		Tail6 = transform.Find ("Dime/root/pelvis/tail0/tail1/tail2/tail3/tail4/tail5/tail6");
		Tail7 = transform.Find ("Dime/root/pelvis/tail0/tail1/tail2/tail3/tail4/tail5/tail6/tail7");
		Tail8 = transform.Find ("Dime/root/pelvis/tail0/tail1/tail2/tail3/tail4/tail5/tail6/tail7/tail8");

    Spine0 = transform.Find ("Dime/root/spine0");
		Spine1 = transform.Find ("Dime/root/spine0/spine1");
		Spine2 = transform.Find ("Dime/root/spine0/spine1/spine2");
		Spine3 = transform.Find ("Dime/root/spine0/spine1/spine2/spine3");
		Neck0 = transform.Find ("Dime/root/spine0/spine1/spine2/spine3/spine4/neck0");
		Neck1 = transform.Find ("Dime/root/spine0/spine1/spine2/spine3/spine4/neck0/neck1");
		Neck2 = transform.Find ("Dime/root/spine0/spine1/spine2/spine3/spine4/neck0/neck1/neck2");
		Head = transform.Find ("Dime/root/spine0/spine1/spine2/spine3/spine4/neck0/neck1/neck2/head");

    crouch_max=2f;
		ang_t=0.04f;
		yaw_max=30f;
		pitch_max=15f;
	}

	//*************************************************************************************************************************************************
	//Play sound
	void OnCollisionStay(Collision col)
	{
		int rndPainsnd=Random.Range(0, 4); AudioClip painSnd=null;
		switch (rndPainsnd) { case 0: painSnd=Dime1; break; case 1: painSnd=Dime2; break; case 2: painSnd=Dime3; break; case 3: painSnd=Dime4; break; }
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
			case "Bite": source[1].pitch=Random.Range(0.75f, 1.0f); source[1].PlayOneShot(Bite, 0.5f);
				lastframe=currframe; break;
			case "Die": source[1].pitch=Random.Range(1.0f, 1.25f); source[1].PlayOneShot(IsOnWater|IsInWater?Largesplash:Largestep, 1.0f);
				lastframe=currframe; IsDead=true; break;
			case "Food": source[0].pitch=Random.Range(1.0f, 1.25f); source[0].PlayOneShot(Swallow, 0.75f);
				lastframe=currframe; break;
			case "Sniff": source[0].pitch=Random.Range(1.0f, 1.25f); source[0].PlayOneShot(Sniff2, 0.5f);
				lastframe=currframe; break;
			case "Repose": source[0].pitch=Random.Range(1.0f, 1.25f); source[0].PlayOneShot(Idlecarn, 0.25f);
				lastframe=currframe; break;
			case "Atk": source[0].pitch=Random.Range(1.0f, 1.25f); source[0].PlayOneShot(Dime2, 1.0f);
				lastframe=currframe; break;
			case "Growl": int rnd2 = Random.Range (0, 3); source[0].pitch=Random.Range(1.0f, 1.25f);
				if(rnd2==0)source[0].PlayOneShot(Dime1, 1.0f);
				if(rnd2==1)source[0].PlayOneShot(Dime3, 1.0f);
				else source[0].PlayOneShot(Dime4, 1.0f);
				lastframe=currframe; break;
			}
		}
	}

	//*************************************************************************************************************************************************
	// Add forces to the Rigidbody
	void FixedUpdate ()
	{
		StatusUpdate(); if(!IsActive | AnimSpeed==0.0f) { body.Sleep(); return; }
		OnReset=false; OnAttack=false; IsConstrained= false;

    //Set mass
		if(IsInWater) { if(Health>0) anm.SetInteger ("Move", 2); body.mass=2; body.drag=4; body.angularDrag=4; }
		else { body.mass=1; body.drag=4; body.angularDrag=4; }
    //Set Y position
    if(IsOnGround | IsInWater | IsOnWater)
    body.AddForce((Vector3.up*Size)*(posY-transform.position.y), ForceMode.VelocityChange);
    else body.AddForce((Vector3.up*Size)*-256, ForceMode.Acceleration);

		//Stopped
		if(NextAnm.IsName("Dime|Idle1A") | NextAnm.IsName("Dime|Idle2A") | CurrAnm.IsName("Dime|Die1") | CurrAnm.IsName("Dime|Die2"))
		{
			if(CurrAnm.IsName("Dime|Die1")) { OnReset=true; if(!IsDead) { PlaySound("Atk", 1); PlaySound("Die", 12); } }
			else if(CurrAnm.IsName("Dime|Die2")) { OnReset=true; if(!IsDead) { PlaySound("Atk", 1); PlaySound("Die", 12); } }
		}

		//Forward
		else if(CurrAnm.IsName("Dime|Walk") | CurrAnm.IsName("Dime|WalkGrowl") |
		       (CurrAnm.IsName("Dime|Step1") && CurrAnm.normalizedTime < 0.7) | (CurrAnm.IsName("Dime|Step2") && CurrAnm.normalizedTime < 0.7) |
           (CurrAnm.IsName("Dime|StepAtk1") && CurrAnm.normalizedTime < 0.7) | (CurrAnm.IsName("Dime|StepAtk2") && CurrAnm.normalizedTime < 0.7) |
           (CurrAnm.IsName("Dime|ToIdle1C") && CurrAnm.normalizedTime < 0.7) |
            NextAnm.IsName("Dime|Walk") |  NextAnm.IsName("Dime|ToIdle1C") |
            NextAnm.IsName("Dime|Step2") | NextAnm.IsName("Dime|Step1") |
            NextAnm.IsName("Dime|StepAtk1") | NextAnm.IsName("Dime|StepAtk2"))
		{
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
			body.AddForce(transform.forward*18*Size*anm.speed);
			if(CurrAnm.IsName("Dime|WalkGrowl")) { PlaySound("Growl", 2); PlaySound("Step", 6); PlaySound("Step", 13); }
			else if(CurrAnm.IsName("Dime|Walk")) { PlaySound("Step", 6); PlaySound("Step", 13); }
			else if(NextAnm.IsName("Dime|StepAtk1") | CurrAnm.IsName("Dime|StepAtk1") | NextAnm.IsName("Dime|StepAtk2") | CurrAnm.IsName("Dime|StepAtk2"))
			{ OnAttack=true; PlaySound("Atk", 2); PlaySound("Bite", 4); } else PlaySound("Step", 8);
		}

		//Running
		else if(NextAnm.IsName("Dime|Run") | CurrAnm.IsName("Dime|Run") | CurrAnm.IsName("Dime|RunGrowl") |
		  	 NextAnm.IsName("Dime|WalkAtk") | CurrAnm.IsName("Dime|WalkAtk"))
		{
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
			body.AddForce(transform.forward*60*Size*anm.speed);
			if(CurrAnm.IsName("Dime|WalkAtk")) { OnAttack=true; PlaySound("Atk", 2); PlaySound("Bite", 4); }
			else if(CurrAnm.IsName("Dime|RunGrowl")) { PlaySound("Growl", 2); PlaySound("Step", 6); PlaySound("Step", 13); }
			else if(CurrAnm.IsName("Dime|Run")) { PlaySound("Step", 6); PlaySound("Step", 13); }
			else PlaySound("Step", 8);
		}
		
		//Backward
		else if(NextAnm.IsName("Dime|Step1-") | CurrAnm.IsName("Dime|Step1-") | NextAnm.IsName("Dime|Step2-") | CurrAnm.IsName("Dime|Step2-") |
		   NextAnm.IsName("Dime|ToSleep2") | CurrAnm.IsName("Dime|ToSleep2") | NextAnm.IsName("Dime|ToIdle2C") | CurrAnm.IsName("Dime|ToIdle2C") |
		   NextAnm.IsName("Dime|ToEatA") | CurrAnm.IsName("Dime|ToEatA") | NextAnm.IsName("Dime|ToEatC") | CurrAnm.IsName("Dime|ToEatC"))
		{
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
			body.AddForce(transform.forward*-15*Size*anm.speed);
			PlaySound("Step", 8);
		}

		//Strafe/Turn right
		else if(CurrAnm.IsName("Dime|Strafe1-") | CurrAnm.IsName("Dime|Strafe2+"))
		{
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
			body.AddForce(transform.right*8*Size*anm.speed);
			PlaySound("Step", 6); PlaySound("Step", 13);
		}

		//Strafe/Turn left
		else if(CurrAnm.IsName("Dime|Strafe1+") | CurrAnm.IsName("Dime|Strafe2-"))
		{
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
			body.AddForce(transform.right*-8*Size*anm.speed);
			PlaySound("Step", 6); PlaySound("Step", 13);
		}

		//Various
		else if(CurrAnm.IsName("Dime|EatA")) { OnReset=true; IsConstrained=true; PlaySound("Food", 4); }
		else if(CurrAnm.IsName("Dime|EatB") | CurrAnm.IsName("Dime|EatC")) { OnReset=true; IsConstrained=true; }
		else if(CurrAnm.IsName("Dime|Sleep")) { OnReset=true; IsConstrained=true; PlaySound("Repose", 2); }
		else if(CurrAnm.IsName("Dime|ToSleep1") | CurrAnm.IsName("Dime|ToSleep2")) OnReset=true;
		else if(CurrAnm.IsName("Dime|ToSleep-")) { IsConstrained=true; PlaySound("Sniff", 2); }
		else if(CurrAnm.IsName("Dime|Idle1B")) PlaySound("Growl", 1);
		else if(CurrAnm.IsName("Dime|Idle1C")) { PlaySound("Sniff", 4); PlaySound("Sniff", 7); PlaySound("Sniff", 10);}
		else if(CurrAnm.IsName("Dime|Idle2B")) PlaySound("Growl", 1);
		else if(CurrAnm.IsName("Dime|Idle2C")) { OnReset=true; PlaySound("Sniff", 1); }
		else if(CurrAnm.IsName("Dime|Die1-")) { IsConstrained=true; PlaySound("Growl", 3);  IsDead=false;}
		else if(CurrAnm.IsName("Dime|Die2-")) { IsConstrained=true; PlaySound("Growl", 3);  IsDead=false; }
	}

	void LateUpdate()
	{
		//*************************************************************************************************************************************************
		// Bone rotation
		if(!IsActive) return;

		TakeDamageAnim(Quaternion.Euler(0, lastHit, 0));

    //Spine rotation
    RotateBone(48f);
		Neck0.rotation*= Quaternion.Euler(0, -headY, -headX);
		Neck1.rotation*= Quaternion.Euler(0, -headY, -headX);
		Neck2.rotation*= Quaternion.Euler(0, -headY, -headX);
		Head.rotation*= Quaternion.Euler(0, -headY, -headX);

    Spine0.rotation*= Quaternion.Euler(0, 0, -spineX);
		Spine1.rotation*= Quaternion.Euler(0, 0, -spineX);
		Spine2.rotation*= Quaternion.Euler(0, 0, -spineX);
		Spine3.rotation*= Quaternion.Euler(0, 0, -spineX);

		//Tail rotation
		Tail0.rotation*= Quaternion.Euler(0, 0, spineX);
		Tail1.rotation*= Quaternion.Euler(0, 0, spineX);
		Tail2.rotation*= Quaternion.Euler(0, 0, spineX);
		Tail3.rotation*= Quaternion.Euler(0, 0, spineX);
		Tail4.rotation*= Quaternion.Euler(0, 0, spineX);
		Tail5.rotation*= Quaternion.Euler(0, 0, spineX);
		Tail6.rotation*= Quaternion.Euler(0, 0, spineX);
		Tail7.rotation*= Quaternion.Euler(0, 0, spineX);
		Tail8.rotation*= Quaternion.Euler(0, 0, spineX);

    HeadPos=Head;

		//Check for ground layer
		GetGroundPos(IkType.Convex, Right_Hips, Right_Leg, Right_Foot, Left_Hips, Left_Leg, Left_Foot, Right_Arm0, Right_Arm1, Right_Hand, Left_Arm0, Left_Arm1, Left_Hand);
	
		//*************************************************************************************************************************************************
		// CPU
		if(UseAI && Health!=0) { AICore(1, 2, 3, 0, 4, 5, 6); }
		//*************************************************************************************************************************************************
		// Human
		else if(Health!=0) { GetUserInputs(1, 2, 3, 0, 4, 5, 6); }
		//*************************************************************************************************************************************************
		//Dead
		else { anm.SetBool("Attack", false); anm.SetInteger ("Move", 0); anm.SetInteger ("Idle", -1); }
	}
}



