﻿using UnityEngine;
using System.Collections;

public class RageHandler : MonoBehaviour {
	protected CuttlefishMovement cuttlefish;
	
	public float currRage = 0;
	public float maxRage = 100;
	public float currGoal = 0;
	
	public GUITexture rageMeter, rageMeterBack, coolDownMarker;
	public int markerWidth = 10;
	public float divisorForRageMeter = 6;
	public float rageMeterWidthPercent = 0.8f;
	public float speed=3.0f;
	
	void Start()
	{
		cuttlefish = GameObject.FindGameObjectWithTag("Player").GetComponent<CuttlefishMovement>();

		float height = Screen.height/divisorForRageMeter;
		rageMeterBack.pixelInset = new Rect(0,Screen.height-height,Screen.width,height);
		rageMeterBack.color = Color.black;
		rageMeter.pixelInset = new Rect(Screen.width*(1f-rageMeterWidthPercent)/2f,Screen.height-height+(height*(1f-rageMeterWidthPercent)/2f),getCurrentRageMeterWidth(),height*rageMeterWidthPercent);
		rageMeter.color = Color.white;
		// place the cool down mark at the 80% mark of the rage meter
		coolDownMarker.pixelInset = new Rect((float)(Screen.width-(0.2*Screen.width)), Screen.height-height+(height*(1f-rageMeterWidthPercent)/2f), markerWidth, height);
		coolDownMarker.transform.position = new Vector3(0,0,5);
		coolDownMarker.active = false;
		currGoal = currRage;
		
	}
	
	public float getRatio()
	{
		return currRage/maxRage;
	}
	
	void Update()
	{
		if(currRage!=currGoal)
		{
			float diff = Mathf.Abs(currRage-currGoal);

			/**
			if(diff>0.1f)
			{
				currRage+= (diff)*Time.deltaTime*speed;
			}
			else
			{
				currRage = currGoal;
			}
			*/
			if(diff>1f)
			{
			
				if(currRage>currGoal)
				{
					currRage=currGoal;
				}
				else if(currRage<currGoal)
				{
					currRage+=Time.deltaTime*speed*2;
				}
			}
			else
			{
				currRage = currGoal;
			}
			
			
			if(currRage>0 && currRage<maxRage)
			{
				resetRageMeterVisual();
			}
		}
		
	}
	
	//Method to call to determine (returns current rage)
	public float alterRage(float rageAlteration)
	{
		if (rageAlteration < 0) cuttlefish.damageFlash();

		currGoal+=rageAlteration;
		
		if(currGoal<0)
		{
			currGoal=0;
			
			
		}
		
		if(currGoal>maxRage)
		{
			currGoal = maxRage;
		}
		
		if(currRage>0 && currRage<maxRage)
		{
			resetRageMeterVisual();
		}
		
		return currRage;
	}
	
	//Called to reset rage meter's appearance after a change 
	private void resetRageMeterVisual()
	{
		float height = Screen.height/divisorForRageMeter;
		rageMeter.pixelInset = new Rect(Screen.width*(1f-rageMeterWidthPercent)/2f
			,Screen.height-height+(height*(1f-rageMeterWidthPercent))
			,getCurrentRageMeterWidth(),
			height*rageMeterWidthPercent);
		rageMeter.color = getCurrRageMeterColor();
	}
	
	
	
	private float getCurrentRageMeterWidth()
	{
		return ((1f-((maxRage-currRage)/maxRage))*rageMeterWidthPercent*Screen.width)+5f;
	}
	
	
	private Color getCurrRageMeterColor()
	{
		float val = (1f-((maxRage-currRage)/maxRage));
		//Debug.Log("color value" + val);
		if (val > 0.8f) {
			Debug.Log("switching the coolDownMarker on");
			coolDownMarker.active = true;
		} else {
			coolDownMarker.active = false;
		}
		return Color.Lerp(new Color(1.0f,0.3f,0.3f,1.0f),Color.red,val);
	}
	
	
}
