using UnityEngine;
using System.Collections;

public class CuttlefishShooter : MonoBehaviour {
	public float moveBackAmnt = 0.6f;
	protected int shooterType = 1;
	
	//Called when the player wants to shoot
	public virtual void shoot(GameObject beam, RageHandler rageHandler, Vector3 moveDirection,  bool facingRight)
	{
		
		if(!audio.isPlaying)
		{
			audio.Play();
		}
		
		Vector3 difference = Vector3.right*Random.Range(-0.15f,0.15f)+Vector3.up*Random.Range(-0.15f,0.15f);
			
		float bulletSizeMax = 5.0f;
		Bullet b = (Instantiate(beam,gameObject.GetComponent<CuttlefishMovement>().beamPosition.transform.position+difference,transform.rotation) as GameObject).GetComponent<Bullet>();
			
		//b.transform.localScale*=(1+rageHandler.getRatio()*bulletSizeMax);
		
		//b.transform.parent = transform.parent;
		
		
		
		b.origPosition = gameObject.GetComponent<CuttlefishMovement>().beamPosition.transform.position;
		
	}
	
	public int getShooter()
	{
		return shooterType;
	}
	
	
}
