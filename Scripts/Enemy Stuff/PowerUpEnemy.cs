using UnityEngine;
using System.Collections;

public class PowerUpEnemy : BasicEnemy {

	private GameObject player;
	public string[] scriptNames;
	public override void Start() {
		base.Start();

		player = GameObject.FindGameObjectWithTag("Player");
	}

	protected override void OnDeath() {
		Destroy(player.GetComponent<CuttlefishShooter>());
		Destroy(player.GetComponent<RandomShooter>());
		Destroy(player.GetComponent<ForwardWaveShooter>());
		
		Component r = player.AddComponent(scriptNames[Random.Range(0,scriptNames.Length)]);
		player.GetComponent<CuttlefishMovement>().shooter = (CuttlefishShooter)r;
		
		Destroy(gameObject);
	}
}
