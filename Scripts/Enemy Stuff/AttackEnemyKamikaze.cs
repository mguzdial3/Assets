using UnityEngine;
using System.Collections;

//Basic type of attacking enemy
public class AttackEnemyKamikaze : AttackEnemy {
	public override void Start ()
	{
		base.Start (); //We need to have this to set up the basic info from Basic Enemy 
		transform.position = transform.position - ( new Vector3((cuttlefish.transform.position.x-transform.position.x)+90f, 0f, 0f));
		currDirection = cuttlefish.transform.position -transform.position;
	}
	protected override void NormalGameplay ()
	{
		//look at the Cuttlefish Code
		var faceCuttlefishRotation = Quaternion.LookRotation(cuttlefish.transform.position - transform.position, Vector3.up);
   		faceCuttlefishRotation.x = 0.0f;
		faceCuttlefishRotation.y = 0.0f;
		faceCuttlefishRotation.z = faceCuttlefishRotation.z;
		transform.rotation = Quaternion.Slerp(transform.rotation, faceCuttlefishRotation, Time.deltaTime * 8);
		
		//Determine if we should change the currDirection yet
		if(timer<timerMax)
		{
			timer+=Time.deltaTime;
		}
		else
		{
			timer=0;
			currDirection = cuttlefish.transform.position -transform.position;
		}
		
		
		
		transform.position+=currDirection.normalized*speedSeenPlayer*Time.deltaTime;
		
		
	}
	
	//What it should do when it hits something
	void OnTriggerEnter(Collider other)
	{
		//When it hits the player
		if(other.collider.tag == "Player")
		{
			//Do damage to cuttlefish
			cuttlefish.rageHandler.alterRage(-2*damageAmount*Time.deltaTime);
			//Push Cuttlefish away
			cuttlefish.transform.position+=rigidbody.velocity;
			Destroy (gameObject);
		}
	}
	
}
