using UnityEngine;
using System.Collections;

public class RainbowSkin : MonoBehaviour {
	private Color[] colors  = {Color.white,Color.red,Color.blue,Color.green,Color.cyan,new Color(1.0f,0.0f,1.0f,1.0f)};
	private int colorIndex = 0;
	private float timer;
	private float timerMax = 1.0f;
	
	// Update is called once per frame
	void Update () {
		if(timer<timerMax)
		{
			timer+=Time.deltaTime;
		}
		else
		{
			colorIndex++;
			
			if(colorIndex>=colors.Length)
			{
				colorIndex = 0;
			}
			timer=0;
		}
		
		int nextColor = colorIndex+1;
		
		
		if(nextColor>=colors.Length)
		{
			nextColor = 0;
		}
		
		
		renderer.material.color = Color.Lerp(colors[colorIndex],colors[nextColor],(timerMax-timer)/timerMax);
	}
}
