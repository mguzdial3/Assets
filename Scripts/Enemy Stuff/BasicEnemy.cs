using UnityEngine;
using System.Collections;

public class BasicEnemy : MonoBehaviour {
	//Link to cuttlefish
	protected CuttlefishMovement cuttlefish; //Reference to the player
	public float hitPoints=10f; //Max hitpoints of this fish
	protected float origHitPoints;
	public float pointsWorth = 1.0f; //The amount of rage points killing this fish gives you/takes away if you miss it
	public float speed, speedSeenPlayer; //Two different speeds
	
	//High level gameState stuff
	protected int gameState;
	protected const int NORMAL_STATE=0; //What the fish does normally
	protected const int ATTACK_STATE = 3; //If the fish has a different behavior when attacking
	protected const int PAUSED_STATE = 2; //If the fish should be paused

	public int damageFlashNumTimes = 2;
	private int damageCounter = 0;
	private int flashTimes = 0;
	private bool damaged = false;

	//for talking to children of this object
	public Component[] enemyPieces;

	protected float redTimer = 0;
	protected float redTimerMax = 0.2f;
	protected Color[] origColors;

	// Use this for initialization
	public virtual void Start () {
		cuttlefish = GameObject.FindGameObjectWithTag("Player").GetComponent<CuttlefishMovement>();
		enemyPieces = GetComponentsInChildren<Renderer>();
		origColors = new Color[enemyPieces.Length];

		for(int i =0; i<enemyPieces.Length; i++)
		{
			if(enemyPieces[i].renderer!=null)
			{
				origColors[i] = enemyPieces[i].renderer.material.color;
			}
		}

		origHitPoints = hitPoints;
	}
	
	// Update is called once per frame
	void Update () 
	{
			
		if(gameState==NORMAL_STATE)
		{
			NormalGameplay();
			
			if(checkIfOff())
			{
				Escape();
			}
			
			gameState = transferStateFromNormal();
			
		}
		else if(gameState==ATTACK_STATE)
		{
			AttackGameplay();
		}

		//flash enemy if hit
		if (damaged) {
			damageCounter ++;
			if (damageCounter == 4) {
				foreach(Renderer piece in enemyPieces) {
					if (flashTimes == damageFlashNumTimes) {
						damaged = false;
						piece.enabled = true;
					} else {
						piece.enabled = !piece.enabled;
					}
				}
				flashTimes ++;
				damageCounter = 0;
			}
		}
		
		
		
	}
	
	//Determine if you should move into a different state from the normal
	protected virtual int transferStateFromNormal()
	{
		return 0;	
	}
	
	//Called if fish has different attack behavior
	protected virtual void AttackGameplay()
	{}
	
	//Normal fish behavior
	protected virtual void NormalGameplay()
	{
		float maxSpeed = 5.0f;
		
		transform.position+=Vector3.left*Time.deltaTime*(speed+maxSpeed*cuttlefish.rageHandler.getRatio());

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
	
	
	
	//Checks if we're off the screen
	protected virtual bool checkIfOff()
	{
		Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
		
		//If we've made it off the screen
		if(screenPos.x<0)
		{
			return true;
		}
		
		return false;
		
	}
	
	
	//Called when the enemy goes off the screen to the left
	protected virtual void Escape()
	{
		cuttlefish.rageHandler.alterRage(-1*pointsWorth*Time.deltaTime);
		
		Destroy(gameObject);
	}
	
	//Called to do damage to this fish, calls OnDeath() if hitpoints too low
	public virtual void doDamage(float damage)
	{
		Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position+Vector3.right*2);
		Rect screenRect  = new Rect(0,0,Screen.width,Screen.height);
		
		if(screenRect.Contains(screenPos))
		{
			for(int i =0; i<enemyPieces.Length; i++)
			{
				if(enemyPieces[i].renderer!=null)
				{
					enemyPieces[i].renderer.material.color = Color.Lerp(origColors[i],Color.red,	0.3f+0.5f*((origHitPoints - hitPoints)/origHitPoints));	
				}
			}
			redTimer = redTimerMax;
			damaged = true;
			flashTimes = 0;
			damageCounter = 0;
			hitPoints-=damage;
			
			if(hitPoints<=0)
			{
				cuttlefish.score += (int)pointsWorth;
				OnDeath();
			}
		}
	}
	
	//What to do when this fish dies
	protected virtual void OnDeath()
	{
		cuttlefish.rageHandler.alterRage(pointsWorth);

		Destroy(gameObject);
	}
	
	
}
