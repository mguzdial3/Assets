using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Basic type of attacking enemy
public class AttackEnemy : BasicEnemy {
	public float damageAmount; //The amount of damage this enemy does to the player
	private float timer; //Timer for movement
	private float timerMax = 1.0f; //Max amount of time for movement
	private Vector3 currDirection; //The current direction we're moving in
	
	public Animation myAnimation;
	
	protected List<AttackEnemy> otherAttackers;

	private bool beenOnScreen;

	public override void Start ()
	{
		base.Start (); //We need to have this to set up the basic info from Basic Enemy 
		currDirection = cuttlefish.transform.position -transform.position;

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		otherAttackers = new List<AttackEnemy>();

		for(int i =0; i<enemies.Length; i++)
		{
			if(enemies[i]!=gameObject)
			{
				AttackEnemy a = enemies[i].GetComponent<AttackEnemy>();
				if(a!=null)
				{
					otherAttackers.Add(a);
				}


			}
		}
	}
	
	protected override void NormalGameplay ()
	{
		
		//Determine if we should change the currDirection yet
		if(timer<timerMax)
		{
			timer+=Time.deltaTime;
		}
		else
		{
			timer=0;
			currDirection = cuttlefish.transform.position -transform.position;

			if(otherAttackers!=null)
			{
				for(int i =0; i<otherAttackers.Count; i++)
				{
					if(otherAttackers[i]!=null)
					{
						Vector3 diff = transform.position-otherAttackers[i].transform.position;

						if(diff.sqrMagnitude<5f)
						{
							currDirection+=diff.normalized;
						}

					}
				}
			}
		
		}
		
		if(currDirection.x>0)
		{
			myAnimation.Play("SwimLeft");
		}
		else
		{
			myAnimation.Play("SwimRight");
		}
		
		//Move this enemy

		Vector3 newPos = 
		transform.position+currDirection.normalized*speedSeenPlayer*Time.deltaTime;

		Rect screenRect = new Rect(0,0,Screen.width,Screen.height);
		Vector3 screenPos = Camera.main.WorldToScreenPoint(newPos);
		if(beenOnScreen)
		{
			if(screenRect.Contains(screenPos))
			{
				transform.position = newPos;
			}
			else
			{
				currDirection.x*=-1;
				currDirection.y*=-1;
			}
		}
		else
		{
			if(screenRect.Contains(screenPos))
			{
				beenOnScreen = true;

			}

			transform.position = newPos;
		}


		if(redTimer>0)
		{
			redTimer-=Time.deltaTime;
			
			if(redTimer<=0)
			{
				for(int i =0; i<enemyPieces.Length; i++)
				{
					if(enemyPieces[i].renderer!=null)
					{
						enemyPieces[i].renderer.material.color = origColors[i];	
					}
				}
			}
		}
	}
	
	//What it should do when it hits something
	void OnTriggerEnter(Collider other)
	{
		//When it hits the player
		if(other.collider.tag == "Player")
		{
			//Do damage to cuttlefish
			cuttlefish.rageHandler.alterRage(-1*damageAmount*Time.deltaTime);
			//Push Cuttlefish away
			cuttlefish.transform.position+=rigidbody.velocity;
		}
	}
	
	//Change the BasicEnemy Escape, as attack enemies don't want to escape
	protected override void Escape ()
	{}
}
