using UnityEngine;
using System.Collections;

public class PowerUpEnemy : BasicEnemy {

	private GameObject player;
	public Renderer myBody;
	public string[] scriptNames;
	public Texture[] imagesForPowerups;

	private int[] indexesToUse;

	private int currIndex;

	private float timer, timerMax = 1f;

	public override void Start() {
		base.Start();

		player = GameObject.FindGameObjectWithTag("Player");


		indexesToUse = new int[3];

		for(int i =0; i<3; i++)
		{
			int indexToUse = Random.Range(0, scriptNames.Length);

			indexesToUse[i] = indexToUse;
		}

		currIndex = 0;

		myBody.material.mainTexture = imagesForPowerups[currIndex];
	}

	void Update()
	{
		if(timer<timerMax)
		{
			timer+=Time.deltaTime;
		}
		else
		{
			if(currIndex<2)
			{
				currIndex++;
			}
			else
			{
				currIndex = 0;
			}
			myBody.material.mainTexture = imagesForPowerups[indexesToUse[currIndex]];
			timer=0;
		}

		transform.position-=Vector3.right*Time.deltaTime;
	}


	protected override void OnDeath() {
		Destroy(player.GetComponent<CuttlefishShooter>());
		Destroy(player.GetComponent<RandomShooter>());
		Destroy(player.GetComponent<ForwardWaveShooter>());
		Destroy(player.GetComponent<GeyserShooter>());

		Component r = player.AddComponent(scriptNames[indexesToUse[currIndex]]);
		player.GetComponent<CuttlefishMovement>().shooter = (CuttlefishShooter)r;
		
		Destroy(gameObject);
	}
}
