using UnityEngine;
using System.Collections;

public class RageSplosion : MonoBehaviour {
	public GameObject bullet;
	public int numBullets = 1000;
	// Use this for initialization
	void Start () {
		Debug.Log("RAGE SPLOSION");
		for(int i =0; i<numBullets; i++)
		{
			Vector3 difference = new Vector3(Random.Range(-8f, 8f),Random.Range(-8f, 8f),0);

			Bullet b = (Instantiate(bullet,transform.position+difference,transform.rotation) as GameObject).GetComponent<Bullet>();	
			
			b.origPosition = transform.position;

			b.transform.localScale*=(5f);
			
			b.particleSystem.startSize*= (2f);


			float xVal = Random.Range(-1f, 1f);
			float yVal = Random.Range(-1f, 1f);
			
			b.mvmntVector = new Vector3( xVal, yVal, 0);
		}

		Destroy(gameObject);
	}
}
