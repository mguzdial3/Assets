using UnityEngine;
using System.Collections;

public class CuttlefishMovement : MonoBehaviour {
	
	// movement property references
    public float speed = 6.0F;
	private float origSpeedVal;
	public float turnSpeed = 6.0f;
	public float moveBackAmnt = 0.6f;
	
	//Beam to Instantiate
	public GameObject beam;
	//Place to instantiate beam from
	public Transform beamPosition;
	
	//Facing Left To Begin With
	private bool facingRight = false;
	
	private Quaternion goalRotation;
	private float minShrinkVal = 0.9f;
	
	public float angerBarHeight; 
	
	public RageHandler rageHandler;
	public CuttlefishShooter shooter;
	
	public GUIText powerupText;
	
	private float shootPeriod = 0.25f;
	private float shootPCounter = 0.0f;

	public Component[] playerPieces;
	public int damageFlashNumTimes = 2;
	private bool damaged = false;
	private int flashTimes = 0;
	private int damageCounter = 0;

	public GUIText scoreText;
	public int score = 0;
	
	// Use this for initialization
	void Start () {
		playerPieces = GetComponentsInChildren<Renderer>();

		goalRotation = Quaternion.Euler(new Vector3(0,180,0));
		facingRight = true;
		origSpeedVal = speed;
		shootPeriod += rageHandler.getRatio();
		
		angerBarHeight = Screen.height/rageHandler.divisorForRageMeter;
		
	}
	
	// Update is called once per frame
	void Update () {
		
		if((powerupText.text)!=(""+shooter.getShooter()))
		{
			powerupText.text=""+	shooter.getShooter();
		}

		if((scoreText.text)!=(""+score)) 
		{
			Debug.Log("changin score to " + score);
			scoreText.text=""+score;
		}
		
		shootPeriod = 0.25f+(rageHandler.getRatio());
		
		Vector3 moveDirection = new Vector3(-1*Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
		float maxSpeedBoost = 5.0f;
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= (speed+maxSpeedBoost*rageHandler.getRatio());
		
		
		
		
		if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)
			|| Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.UpArrow))
		{
			if(speed<origSpeedVal*1.5f)
			{
				speed+=Time.deltaTime*4;
			}
		}
		else
		{
			speed=origSpeedVal;
		}
		
		
		
		
		if(transform.rotation!=goalRotation)
		{
			 transform.rotation = Quaternion.Lerp(transform.rotation, goalRotation, (turnSpeed*4* Time.deltaTime));
		}
		
		if(Input.GetKey(KeyCode.Space))
		{
			
			shootPCounter += Time.deltaTime;
			if(shootPCounter < shootPeriod){
				shooter.shoot(beam,rageHandler, moveDirection, facingRight);
			}
			else if (shootPCounter < shootPeriod + 0.2f){
			}
			else if (shootPCounter > shootPeriod + 0.2f){
				shootPCounter = 0f;
			}
			if(transform.localScale.y>minShrinkVal)
			{
				transform.localScale-=Vector3.up*Time.deltaTime*5;
			}
		}
		else
		{
			if(transform.localScale.y<1)
			{
				transform.localScale+=Vector3.up*Time.deltaTime*2;
			}
		}
		
		Vector3 newPos = transform.position+moveDirection * Time.deltaTime;
		
		Rect screenRect = new Rect(0,0,Screen.width,Screen.height-angerBarHeight);
		Vector3 screenPos = Camera.main.WorldToScreenPoint(newPos);
		
		if(-1*Input.GetAxis("Horizontal")>0)
		{
			screenPos.x-=20;
		}
		else
		{
			screenPos.x+=20;
		}
		

		
		if(screenRect.Contains(screenPos))
		{
			transform.position = newPos;
		}

		// flash character if hit
		if (damaged) {
			damageCounter ++;
			if (damageCounter == 4) {
				foreach(Renderer piece in playerPieces) {
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

	RageHandler getRageHandler(){
		return rageHandler;
	}

	public void damageFlash(){
		foreach(Renderer piece in playerPieces) {
			piece.enabled = false;
		}
		damaged = true;
		flashTimes = 0;
		damageCounter = 0;
	}
}
