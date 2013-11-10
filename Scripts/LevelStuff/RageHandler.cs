using UnityEngine;
using System.Collections;

public class RageHandler : MonoBehaviour {
	protected CuttlefishMovement cuttlefish;
	
	public float currRage = 0;
	public float maxRange = 100;
	public float currGoal = 0;
	
	public GUITexture rageMeter, rageMeterBack;
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
		
		currGoal = currRage;
		
	}
	
	public float getRatio()
	{
		return currRage/maxRange;
	}
	
	void Update()
	{
		if(currRage!=currGoal)
		{
			float diff = Mathf.Abs(currRage-currGoal);
			
			
			if(diff>0.1f)
			{
				currRage+= (diff)*Time.deltaTime*speed;
			}
			else
			{
				currRage = currGoal;
			}
			
			if(currRage>currGoal)
			{
				currRage = currGoal;
			}
			
			
			if(currRage>0 && currRage<maxRange)
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
		
		if(currGoal>maxRange)
		{
			currGoal = maxRange;
		}
		
		if(currRage>0 && currRage<maxRange)
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
		return ((1f-((maxRange-currRage)/maxRange))*rageMeterWidthPercent*Screen.width)+5f;
	}
	
	
	private Color getCurrRageMeterColor()
	{
		float val = (1f-((maxRange-currRage)/maxRange));
		
		return Color.Lerp(new Color(1.0f,0.3f,0.3f,1.0f),Color.red,val);
	}
	
	
}
