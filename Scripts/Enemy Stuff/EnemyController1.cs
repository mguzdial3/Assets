using UnityEngine;
using System.Collections;

public class EnemyController1 : MonoBehaviour {
	public GameObject[] enemyPrefabs;
	public Transform top, bottom;
	
	private int test = 0;
	public float timerMax = 4.0f;
	private float staticGeneration = 25.0f;
	private float timer;
	public int maxNumEnemies = 4;
	private RageHandler rageHandler;
	
	void Start()
	{
		rageHandler = GameObject.Find("Admin").GetComponent<RageHandler>();
	}
	
	void Update()
	{
				if(timer<staticGeneration && test < 6){
			
			timer+=Time.deltaTime*Random.Range(0.7f,0.9f);
			Vector3 difference = bottom.position-top.position;
			
			if(timer > 1.5 && timer <2.0f && test <1 ){
				for(int i =0; i<3; i++)
				{
					float amnt = ((float)i/(float)3)+Random.Range(0,0.5f);
					Vector3 pos = top.position+difference*amnt;
					Instantiate(enemyPrefabs[0],pos,transform.rotation);
				}
				test = 1;
			}
			else if(timer > 4.5f && timer <5.0f && test < 2){
				for(int i =0; i<1; i++)
				{
					float amnt = ((float)i/(float)1)+Random.Range(0,0.5f);
					Vector3 pos = top.position+difference*amnt;
					Instantiate(enemyPrefabs[0],pos,transform.rotation);
				}
				test = 2;
			}
			else if(timer > 8.5f && timer <9.0f && test < 3){
				for(int i =0; i<2; i++)
				{
					float amnt = ((float)i/(float)2)+Random.Range(0,0.5f);
					Vector3 pos = top.position+difference*amnt;
					Instantiate(enemyPrefabs[2],pos,transform.rotation);
				}
				test = 3;
			}
			else if(timer >13.5f && timer <14.0f && test < 4){
				for(int i =0; i<3; i++)
				{
					float amnt = ((float)i/(float)3)+Random.Range(0,0.5f);
					Vector3 pos = top.position+difference*amnt;
					Instantiate(enemyPrefabs[3],pos,transform.rotation);
				}
				test = 4;
			}
			else if(timer > 18.5f && timer < 19.0f && test < 5){
				for(int i =0; i<1; i++)
				{
					float amnt = ((float)i/(float)1)+Random.Range(0,0.5f);
					Vector3 pos = top.position+difference*amnt;
					Instantiate(enemyPrefabs[4],pos,transform.rotation);
				}
				test = 5;
			}
			else if(timer > 24.5f && timer <25.0f && test < 6){
				for(int i =0; i<2; i++)
				{
					float amnt = ((float)i/(float)2)+Random.Range(0,0.5f);
					Vector3 pos = top.position+difference*amnt;
					Instantiate(enemyPrefabs[5],pos,transform.rotation);
				}
				test = 6;
			}
		}
		else{
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
						numEnemies=maxNumEnemies+2; //changed from maxNumEnemies+2
					}
				}
				
				else if(enemyWaveType==2) //Powerup Enemy
				{
					numEnemies =1;	
				}
				else if(enemyWaveType==3) //Speed Enemy
				{
					//if(rageHandler.getRatio()<0.5f)
					//{
						numEnemies=1;	
					//}
					/**
					else if(rageHandler.getRatio()>0.9f)
					{
						numEnemies=maxNumEnemies;  //maxNumEnemies+2
					}
					*/
				}
				else if(enemyWaveType==4) //Tough Enemy
				{
					if(rageHandler.getRatio()<0.7f)
					{
						enemyWaveType=0;	
					}	
					else if(rageHandler.getRatio()<0.9f)
					{
						numEnemies=Random.Range(1,3);
					}
				}


				if(enemyWaveType==3)
				{
					numEnemies=1;
				}

				Debug.Log("Enemy Wave Type: "+enemyWaveType);
				
				for(int i =0; i<numEnemies; i++)
				{
					float amnt = ((float)i/(float)numEnemies)+Random.Range(0,0.5f);
					
					Vector3 pos = top.position+difference*amnt;
					
					Instantiate(enemyPrefabs[enemyWaveType],pos,transform.rotation);
				}
				
				
			}
		}
	}

}
