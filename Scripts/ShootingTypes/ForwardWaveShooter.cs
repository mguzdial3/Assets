using UnityEngine;
using System.Collections;

public class ForwardWaveShooter : CuttlefishShooter {
	public int maxAmtBullets = 10;
	public int minAmtBullets = 1;
	
	
	void Start()
	{
		shooterType=3;
	}
	
	public override void shoot (GameObject beam, RageHandler rageHandler, Vector3 moveDirection, bool raging)
	{
		
		if(!audio.isPlaying)
		{
			audio.Play();
		}
		
		Vector3 difference = Vector3.right*Random.Range(0f,0.15f)+Vector3.up*Random.Range(-0.5f,0.5f);
		float bulletSizeMax = 8.0f;

		Bullet b = (Instantiate(beam,gameObject.GetComponent<CuttlefishMovement>().beamPosition.transform.position+difference,transform.rotation) as GameObject).GetComponent<Bullet>();	


		if(raging)
		{
			b.transform.localScale*=(10f);
			
			b.particleSystem.startSize*= (3f);
		}
		else
		{
			b.transform.localScale*=(1+rageHandler.getRatio()*bulletSizeMax);
			b.particleSystem.startSize*= (1+rageHandler.getRatio());
			b.transform.parent = transform.parent;
		}

		b.origPosition = gameObject.GetComponent<CuttlefishMovement>().beamPosition.transform.position;
		float xVal = Random.Range(0f, 1f);
		float yVal = Random.Range(-0.5f, 0.5f);
		
		b.mvmntVector = new Vector3( xVal, yVal, 0);
		

		
		
		
	}
	
	
}
