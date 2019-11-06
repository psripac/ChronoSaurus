using UnityEngine;

public class Ptera : Creature
{
  [Space (10)] [Header("PTERANOSAURUS SOUNDS")]
	public AudioClip Waterflush;
  public AudioClip Wind;
  public AudioClip Hit_jaw;
  public AudioClip Hit_head;
  public AudioClip Hit_tail;
  public AudioClip Smallstep;
  public AudioClip Smallsplash;
  public AudioClip Swallow;
  public AudioClip Idlecarn;
  public AudioClip Bite;
  public AudioClip Sniff2;
  public AudioClip Bigstep;
  public AudioClip Largesplash;
  public AudioClip Ptera1;
  public AudioClip Ptera2;
  public AudioClip Ptera3;
	Transform Root, Neck0, Neck1, Neck2, Neck3, Neck4, Neck5, Neck6, Head, 
	Right_Wing0, Left_Wing0, Right_Wing1, Left_Wing1, Right_Hand, Left_Hand, 
	Left_Hips, Right_Hips, Left_Leg, Right_Leg, Left_Foot, Right_Foot;

	//*************************************************************************************************************************************************
	//Get bone transforms
	void Start()
	{
		Root = transform.Find ("Ptera/root");

		Left_Hips = transform.Find ("Ptera/root/low spine0/low spine1/pelvis/left hips");
		Right_Hips = transform.Find ("Ptera/root/low spine0/low spine1/pelvis/right hips");
		Left_Leg  = transform.Find ("Ptera/root/low spine0/low spine1/pelvis/left hips/left leg");
		Right_Leg = transform.Find ("Ptera/root/low spine0/low spine1/pelvis/right hips/right leg");
		Left_Foot = transform.Find ("Ptera/root/low spine0/low spine1/pelvis/left hips/left leg/left foot");
		Right_Foot = transform.Find ("Ptera/root/low spine0/low spine1/pelvis/right hips/right leg/right foot");


		Right_Wing0 = transform.Find ("Ptera/root/spine0/spine1/spine2/right wing0");
		Left_Wing0 = transform.Find ("Ptera/root/spine0/spine1/spine2/left wing0");
		Right_Wing1 = transform.Find ("Ptera/root/spine0/spine1/spine2/right wing0/right wing1/right wing2");
		Left_Wing1 = transform.Find ("Ptera/root/spine0/spine1/spine2/left wing0/left wing1/left wing2");
		Right_Hand = transform.Find ("Ptera/root/spine0/spine1/spine2/right wing0/right wing1/right wing2/right wing3");
		Left_Hand = transform.Find ("Ptera/root/spine0/spine1/spine2/left wing0/left wing1/left wing2/left wing3");

		Neck0 = transform.Find ("Ptera/root/spine0/spine1/spine2/neck0");
		Neck1 = transform.Find ("Ptera/root/spine0/spine1/spine2/neck0/neck1");
		Neck2 = transform.Find ("Ptera/root/spine0/spine1/spine2/neck0/neck1/neck2");
		Neck3 = transform.Find ("Ptera/root/spine0/spine1/spine2/neck0/neck1/neck2/neck3");
		Neck4 = transform.Find ("Ptera/root/spine0/spine1/spine2/neck0/neck1/neck2/neck3/neck4");
		Neck5 = transform.Find ("Ptera/root/spine0/spine1/spine2/neck0/neck1/neck2/neck3/neck4/neck5");
		Neck6 = transform.Find ("Ptera/root/spine0/spine1/spine2/neck0/neck1/neck2/neck3/neck4/neck5/neck6");
		Head  = transform.Find ("Ptera/root/spine0/spine1/spine2/neck0/neck1/neck2/neck3/neck4/neck5/neck6/head");

    crouch_max=2.5f;
		ang_t=0.05f;
		yaw_max=12f;
		pitch_max=10f;
	}

	//*************************************************************************************************************************************************
	//Play sound
	void OnCollisionStay(Collision col)
	{
		int rndPainsnd=Random.Range(0, 3); AudioClip painSnd=null;
		switch (rndPainsnd) { case 0: painSnd=Ptera1; break; case 1: painSnd=Ptera2; break; case 2: painSnd=Ptera3; break; }
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
			case "Bite": source[1].pitch=Random.Range(1.5f, 1.75f); source[1].PlayOneShot(Bite, 0.5f);
				lastframe=currframe; break;
			case "Sniff": source[1].pitch=Random.Range(1.5f, 1.75f);
				if(IsInWater) source[1].PlayOneShot(Waterflush, Random.Range(0.25f, 0.5f));
				else source[1].PlayOneShot(Sniff2, Random.Range(0.1f, 0.2f));
				lastframe=currframe; break;
			case "Die": source[1].pitch=Random.Range(0.8f, 1.0f); source[1].PlayOneShot(IsOnWater|IsInWater?Largesplash:Bigstep, 1.0f);
				lastframe=currframe; IsDead=true; break;
			case "Food": source[0].pitch=Random.Range(3.0f, 3.25f); source[0].PlayOneShot(Swallow, 0.1f);
				lastframe=currframe; break;
			case "Repose": source[0].pitch=Random.Range(3.0f, 3.25f); source[0].PlayOneShot(Idlecarn, 0.25f);
				lastframe=currframe; break;
			case "Atk": int rnd1 = Random.Range(0, 4); source[0].pitch=Random.Range(1.5f, 1.75f);
				if(rnd1==0) source[0].PlayOneShot(Ptera1, 1.0f);
				else if(rnd1==1) source[0].PlayOneShot(Ptera3, 1.0f);
				lastframe=currframe; break;
			case "Growl": source[0].pitch=Random.Range(1.0f, 1.25f); source[0].PlayOneShot(Ptera2, 1.0f);
				lastframe=currframe; break;
			}
		}
	}

	//*************************************************************************************************************************************************
	// Add forces to the Rigidbody
	void FixedUpdate ()
	{
		StatusUpdate(); if(!IsActive | AnimSpeed==0.0f) { body.Sleep(); return; }
    Vector3 dir=-Root.right; anm.SetBool("OnGround", IsOnGround);
		OnReset=false; OnAttack=false; IsOnLevitation=false; IsConstrained= false;

    //Set mass
    if(IsOnGround | Health==0) //on ground mass
    { 
      roll = Mathf.Lerp(roll, 0.0f, 0.1f); pitch = Mathf.Lerp(pitch, 0.0f, 0.1f);
      body.mass=1; body.drag=4; body.angularDrag=4;
    }
    else { body.mass=1; body.drag=1; body.angularDrag=1; } //in air mass
    //Set Y position
    if(IsInWater)
    {
      if(Health==0) body.AddForce((Vector3.up*Size)*(posY-transform.position.y), ForceMode.VelocityChange);
      else body.AddForce((Vector3.up*Size)*128, ForceMode.Acceleration); anm.SetInteger ("Move", 0);
    }
    else if(IsOnGround) body.AddForce((Vector3.up*Size)*(posY-transform.position.y), ForceMode.VelocityChange);
    else if(Health==0) body.AddForce((Vector3.up*Size)*-256, ForceMode.Acceleration);

		//Stopped
		if(NextAnm.IsName("Ptera|IdleA") | CurrAnm.IsName("Ptera|IdleA") |
			CurrAnm.IsName("Ptera|Die1") | CurrAnm.IsName("Ptera|Die2") | CurrAnm.IsName("Ptera|Fall"))
		{
			if(CurrAnm.IsName("Ptera|Die1")) { OnReset=true; if(!IsDead) { PlaySound("Growl", 1); PlaySound("Die", 11); } }
			else if(CurrAnm.IsName("Ptera|Die2"))
			{
				OnReset=true; body.velocity = new Vector3(0, 0, 0); 
				if(!IsDead) PlaySound("Die", 0);
			}
			else if(CurrAnm.IsName("Ptera|Fall"))
			{
				OnReset=true; IsOnLevitation=true;
				if(IsInWater) anm.SetBool("OnGround", true);
				if(CurrAnm.normalizedTime<0.1f) source[0].PlayOneShot(Ptera2, 1.0f);
			} 
		}
		
		//Forward
		else if(NextAnm.IsName("Ptera|Walk") | CurrAnm.IsName("Ptera|Walk"))
		{
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
			body.AddForce(transform.forward*10*Size*anm.speed);
			PlaySound("Step", 5); PlaySound("Step", 12);
		}

		//Running
		else if(NextAnm.IsName("Ptera|Run") | CurrAnm.IsName("Ptera|Run"))
		{
			IsOnLevitation=true;
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
			body.AddForce(transform.forward*100*Size*anm.speed);
			PlaySound("Step", 5); PlaySound("Step", 6); PlaySound("Sniff", 7); PlaySound("Sniff", 8);
		}
		
		//Backward
		else if(NextAnm.IsName("Ptera|Walk-") | CurrAnm.IsName("Ptera|Walk-"))
		{
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
			body.AddForce(transform.forward*-5*Size*anm.speed);
			PlaySound("Step", 5); PlaySound("Step", 12);
		}
		
		//Strafe/Turn right
		else if(NextAnm.IsName("Ptera|Strafe+") | CurrAnm.IsName("Ptera|Strafe+"))
		{
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
			body.AddForce(transform.right*8*Size*anm.speed);
			PlaySound("Step", 5); PlaySound("Step", 12);
		}
		
		//Strafe/Turn left
		else if(NextAnm.IsName("Ptera|Strafe-") | CurrAnm.IsName("Ptera|Strafe-"))
		{
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
			body.AddForce(transform.right*-8*Size*anm.speed);
			PlaySound("Step", 5); PlaySound("Step", 12);
		}

		//Takeoff
		else if(CurrAnm.IsName("Ptera|Takeoff"))
		{
			IsOnLevitation=true;
			if(CurrAnm.normalizedTime > 0.5) body.AddForce(Vector3.up*50*Size);
			PlaySound("Sniff", 7); PlaySound("Sniff", 8);
		}

		//Run to Fly
		else if(NextAnm.IsName("Ptera|RunToFlight") | CurrAnm.IsName("Ptera|RunToFlight"))
		{
			IsOnLevitation=true;
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
			body.AddForce((transform.forward*100*Size*anm.speed) + (Vector3.up*50*Size));
			PlaySound("Step", 5); PlaySound("Step", 6); PlaySound("Sniff", 7); PlaySound("Sniff", 8);
		}
		
		//Fly to Run
		else if(NextAnm.IsName("Ptera|FlightToRun") | CurrAnm.IsName("Ptera|FlightToRun"))
		{
			IsOnLevitation=true;
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
			body.AddForce(transform.forward*100*Size*anm.speed);
			PlaySound("Step", 5); PlaySound("Step", 6); PlaySound("Sniff", 7); PlaySound("Sniff", 8);
		}

		//Fly
		else if(NextAnm.IsName("Ptera|Flight") | CurrAnm.IsName("Ptera|Flight") | NextAnm.IsName("Ptera|FlightGrowl") | CurrAnm.IsName("Ptera|FlightGrowl") |
		   NextAnm.IsName("Ptera|Glide") | CurrAnm.IsName("Ptera|Glide") | NextAnm.IsName("Ptera|GlideGrowl") | CurrAnm.IsName("Ptera|GlideGrowl"))
		{
			IsOnLevitation=true;
			roll = -spineX*6.0f;
      pitch = Mathf.Lerp(pitch, Mathf.Clamp(anm.GetFloat("Pitch"),-0.75f, 1.0f)*90f, ang_t);
			transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
			body.AddForce(-Root.right*(50+Mathf.Abs(anm.GetFloat("Pitch")*30f))*Size);
			if(body.velocity.magnitude!=0) body.AddForce(-Vector3.up*Mathf.Lerp(dir.y, (64/body.velocity.magnitude)*Size, 1.0f));

			if(CurrAnm.IsName("Ptera|Flight")) { PlaySound("Sniff", 5); PlaySound("Sniff", 6); }
			else if(CurrAnm.IsName("Ptera|FlightGrowl")) { PlaySound("Atk", 3); PlaySound("Sniff", 5); }
			else if(CurrAnm.IsName("Ptera|GlideGrowl")) PlaySound("Growl", 2);
		}
		
		//Fly - Stationary
		else if(CurrAnm.IsName("Ptera|Statio") | CurrAnm.IsName("Ptera|StatioGrowl") | CurrAnm.IsName("Ptera|IdleD") | CurrAnm.IsName("Ptera|FlyAtk"))
		{
			IsOnLevitation=true;
			roll = Mathf.Lerp(roll, 0.0f, ang_t);
      pitch = Mathf.Lerp(pitch, 0.0f, ang_t);
      transform.rotation=Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, anm.GetFloat("Turn"), transform.eulerAngles.z), ang_t);
      body.AddForce(Vector3.up*40*Size*-anm.GetFloat("Pitch")); //fly up/down
			if(IsOnGround&&CurrAnm.IsName("Ptera|FlyAtk")) body.AddForce(Vector3.up*50*Size); //takeoff
			if(anm.GetInteger("Move")==1 | anm.GetInteger("Move")==2 | anm.GetInteger("Move")==3) body.AddForce(transform.forward*60*Size); //fly forward
			else if(anm.GetInteger("Move")== -1) body.AddForce(transform.forward*-40*Size); //fly backward
			else if(anm.GetInteger("Move")== -10) body.AddForce(transform.right*40*Size); //fly right
			else if(anm.GetInteger("Move") == 10) body.AddForce(transform.right*-40*Size); //fly left

			if(CurrAnm.IsName("Ptera|StatioGrowl")) PlaySound("Atk", 2);
			else if(CurrAnm.IsName("Ptera|IdleD")) { PlaySound("Atk", 2); PlaySound("Step", 10); }
			else if(CurrAnm.IsName("Ptera|FlyAtk")) { OnAttack=true; PlaySound("Atk", 3); PlaySound("Bite", 8); }
			else { PlaySound("Sniff", 5); PlaySound("Sniff", 6); }
		}
	
		//Various
		else if(CurrAnm.IsName("Ptera|Landing")) { IsOnLevitation=true; PlaySound("Step", 2); PlaySound("Step", 3); }
		else if(CurrAnm.IsName("Ptera|IdleB")) PlaySound("Atk", 2);
		else if(CurrAnm.IsName("Ptera|IdleC")) { OnReset=true; IsConstrained=true; }
		else if(CurrAnm.IsName("Ptera|EatA")) { OnReset=true; IsConstrained=true; PlaySound("Food", 1); }
		else if(CurrAnm.IsName("Ptera|EatB")) { OnReset=true; IsConstrained=true; PlaySound("Bite", 0); }
		else if(CurrAnm.IsName("Ptera|EatC")) OnReset=true;
		else if(CurrAnm.IsName("Ptera|ToSleep")){ OnReset=true; IsConstrained=true; }
		else if(CurrAnm.IsName("Ptera|Sleep")) { OnReset=true; IsConstrained=true; PlaySound("Repose", 1); }
		else if(CurrAnm.IsName("Ptera|Die-")) { IsConstrained=true; PlaySound("Atk", 2);  IsDead=false; }

		//Play wind sound based on speed
		if(IsOnLevitation)
		{
			if(!source[2].isPlaying) source[2].PlayOneShot(Wind);
			source[2].volume=body.velocity.magnitude/(80*Size);
			source[2].pitch=body.velocity.magnitude/(40*Size);
		}
		else if(source[2].isPlaying) source[2].Pause();
	}

	void LateUpdate()
	{
		//*************************************************************************************************************************************************
		// Bone rotation
		if(!IsActive) return;
      
		TakeDamageAnim(Quaternion.Euler(0, lastHit, 0));

		//Root rotation
    RotateBone(32f);
		Root.rotation*= Quaternion.Euler(roll, pitch, 0);
		//Wings rotation
		Right_Wing0.rotation*= Quaternion.Euler(roll/2, Mathf.Clamp(roll, -35, 0), Mathf.Clamp(-pitch, -35, 0));
		Left_Wing0.rotation*= Quaternion.Euler(roll/2, Mathf.Clamp(-roll, -35, 0), Mathf.Clamp(pitch, 0, 35));
		Right_Wing0.GetChild(0).rotation*= Quaternion.Euler(0, 0, Mathf.Clamp(pitch, 0, 90)+Mathf.Abs(roll)/2);
		Left_Wing0.GetChild(0).rotation*= Quaternion.Euler(0, 0, Mathf.Clamp(-pitch, -90, 0)-Mathf.Abs(roll)/2);
		Right_Hand.rotation*= Quaternion.Euler(0, 0, Mathf.Clamp(-pitch, -90, 0)-Mathf.Abs(roll));
		Left_Hand.rotation*= Quaternion.Euler(0, 0, Mathf.Clamp(pitch, 0, 90)+Mathf.Abs(roll));
		//Head rotation
		float headZ =headY*headX/yaw_max;
		Neck0.rotation*= Quaternion.Euler(-headZ, -headY, headX);
		Neck1.rotation*= Quaternion.Euler(-headZ, -headY, headX);
		Neck2.rotation*= Quaternion.Euler(-headZ, -headY, headX);
		Neck3.rotation*= Quaternion.Euler(-headZ, -headY, headX);
		Neck4.rotation*= Quaternion.Euler(-headZ, -headY, headX);
		Neck5.rotation*= Quaternion.Euler(-headZ, -headY, headX);
		Neck6.rotation*= Quaternion.Euler(-headZ, -headY, headX);
		Head.rotation*= Quaternion.Euler(-headZ, -headY, headX);

    HeadPos=Head;

    //Check for ground layer
		GetGroundPos(IkType.Flying, Right_Hips, Right_Leg, Right_Foot, Left_Hips, Left_Leg, Left_Foot, Right_Wing0, Right_Wing1, Right_Hand, Left_Wing0, Left_Wing1, Left_Hand);
    anm.SetBool("OnGround", IsOnGround);

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










