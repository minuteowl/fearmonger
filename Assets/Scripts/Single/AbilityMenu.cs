using UnityEngine;
using System;
using System.Collections;

public class AbilityMenu
{
	public bool paused = false;
	public int selected = 0;

	void OnGUI()
	{
		if (paused)
		{
			GUI.Box (new Rect(50, 50, 100, 100), "Ability Menu");
			if(GUI.Button (new Rect(40, 40, 100, 100), "Tentacle"))
			   selected = 0;
			else if(GUI.Button (new Rect(30, 30, 100, 100), "Flash"))
				selected = 1;
			Debug.Log ("Selected Ability: " + selected);
		}

	}
	public void togglePause()
	{
		if(Time.timeScale == 0f)
		{
			Time.timeScale = 1f;
			paused = false;
		}
		else
		{
			Time.timeScale = 0f;
			paused = true;  
		}
	}



}