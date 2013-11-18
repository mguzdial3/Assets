using UnityEngine;
using System.Collections;

public class RandomShooter : CuttlefishShooter {

	public int maxAmtBullets = 10;
	public int minAmtBullets = 1;
	private int numOfBulletsInCircle = 0;
	private int divisibleBullets = 0;
	
	void Start()
	{
		shooterType=2;
	}
	
	//shoots a single radial burst of bullets
	public override void shoot (GameObject beam, RageHandler rageHandler, Vector3 moveDirection, bool raging)
	{
		
		if(!audio.isPlaying)
		{
			audio.Play();
		}
		
		maxAmtBullets = (5)+(int)(5*(rageHandler.getRatio()));
		
		numOfBulletsInCircle = Random.Range(minAmtBullets, (int)(maxAmtBullets));
		Vector3 difference = Vector3.right*Random.Range(-0.15f,0.15f)+Vector3.up*Random.Range(-0.15f,0.15f);
		float bulletSizeMax = 5.0f;

		//CalculateMovementVector();
		divisibleBullets = numOfBulletsInCircle;
		while (divisibleBullets % 4 != 0) {
			divisibleBullets ++;
		}

		int quad1 = divisibleBullets/4;
		int quad2 = quad1*2;
		int quad3 = quad1*3;
		int quad4 = quad1*4;

		for (int x = 0; x < numOfBulletsInCircle; x++ ) {
			Bullet b = (Instantiate(beam,gameObject.GetComponent<CuttlefishMovement>().beamPosition.transform.position+difference,transform.rotation) as GameObject).GetComponent<Bullet>();	
			//b.transform.localScale*=(1+rageHandler.getRatio()*bulletSizeMax);
			//if(rageHandler.getRatio()==1)
			//{
			//	b.transform.localScale*= bulletSizeMax;
			//}
			
			
			b.transform.parent = transform.parent;
			b.origPosition = gameObject.GetComponent<CuttlefishMovement>().beamPosition.transform.position;


			float xVal;
			float yVal;

			if (x < quad1 || x == quad1) {
				xVal = (float)(quad1-x)/quad1;
				yVal = (float)x/quad1;
			} else if (x < quad2 || x == quad2) {
				xVal = (float)(quad1-x)/quad1;
				yVal = (float)(quad2-x)/quad1;
			} else if (x < quad3 || x == quad3) {
				xVal = (float)(x-quad3)/quad1;
				yVal = (float)(quad2-x)/quad1;
			} else {
				xVal = (float)(x-quad3)/quad1;
				yVal = (float)(x-quad4)/quad1;
			}

			xVal *= Random.Range(-4f, 4f);
			yVal *= Random.Range(-4f, 4f);

			if(raging)
			{
				b.transform.localScale*=(10f);
				
				b.particleSystem.startSize*= (3f);
			}

			b.mvmntVector = new Vector3( xVal, yVal, 0);
			b.mvmntVector.Normalize();

		}


		
	}
	
	
}
