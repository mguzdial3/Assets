using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	public GameObject[] enemyPrefabs;
	public Transform top, bottom;
	
	
	public float timerMax = 4.0f;
	private float timer;
	public int maxNumEnemies = 4;
	private RageHandler rageHandler;
	
	void Start()
	{
		rageHandler = GameObject.Find("Admin").GetComponent<RageHandler>();
	}
	
	void Update()
	{
		if(timer<timerMax)
		{
			timer+=Time.deltaTime*Random.Range(0.7f,0.9f);
		}
		else
		{
			timer=0;
			
			//Instantiate
			int enemyWaveType = Random.Range(0,enemyPrefabs.Length);
			int numEnemies = Random.Range(1,maxNumEnemies+1);
			
			Vector3 difference = bottom.position-top.position;
			
			//Normal enemy
			if(enemyWaveType==0)
			{

			}
			else if(enemyWaveType==1) //Police Enemy
			{
				if(rageHandler.getRatio()<0.2f)
				{
					numEnemies=1;	
				}
				else if(rageHandler.getRatio()>0.9f)
				{
					numEnemies=maxNumEnemies+2;
				}
			}
			
			else if(enemyWaveType==2) //Powerup Enemy
			{
				numEnemies =1;	
			}
			else if(enemyWaveType==3) //Speed Enemy
			{
				if(rageHandler.getRatio()<0.2f)
				{
					numEnemies=1;	
				}
				else if(rageHandler.getRatio()>0.9f)
				{
					numEnemies=maxNumEnemies+2;
				}
			}
			else if(enemyWaveType==4) //Tough Enemy
			{
				if(rageHandler.getRatio()<0.2f)
				{
					enemyWaveType=0;	
				}	
				else if(rageHandler.getRatio()<0.9f)
				{
					numEnemies=Random.Range(1,3);
				}
			}
			
			
			
			for(int i =0; i<numEnemies; i++)
			{
				float amnt = ((float)i/(float)numEnemies)+Random.Range(0,0.5f);
				
				Vector3 pos = top.position+difference*amnt;
				
				Instantiate(enemyPrefabs[enemyWaveType],pos,transform.rotation);
			}
			
			
		}
	}
}
