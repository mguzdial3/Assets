using UnityEngine;
using System.Collections;

public class MovingTextureCheck : MonoBehaviour {
	public float scrollSpeed = 1.0f;
	private float origSpeed; 
	public RageHandler rageHandler;
	public float maxExtraSpeed = 0.2f;
	private float offset;

	void Start()
	{
		origSpeed = scrollSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("Rage Ratio: "+rageHandler.getRatio());
		scrollSpeed = origSpeed +maxExtraSpeed*rageHandler.getRatio();
		//Debug.Log("Scroll Speed: "+scrollSpeed);
		//Change not to add on and to just eaual ratio for acceleration
		offset += scrollSpeed*Time.deltaTime;//((scrollSpeed));


		renderer.material.SetTextureOffset ("_MainTex", new Vector2(offset,0));
	}
}
