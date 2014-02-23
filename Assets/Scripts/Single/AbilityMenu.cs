using UnityEngine;
using System;
using System.Collections;

public class AbilityMenu: MonoBehaviour
{
	public bool paused;
	public int selected = 0;

	void start()
	{
		paused = false;
	}

	void Update()
	{
		if (paused == false) 
			Time.timeScale = 1f;
		else
			Time.timeScale = 0f;
	}

	void OnGUI()
	{
		if (paused)
		{
			GUI.Box (new Rect(50, 50, 100, 20), "Ability Menu");
			GUI.Box (new Rect(50, 100, 70, 20), "Tentacle");
			GUI.Box (new Rect(50, 150, 70, 20), "Flash");
			if(GUI.Button (new Rect(120, 100, 50, 20), "Equip")){
			   selected = 0;
				Debug.Log ("Tentacle Equipped");
			}
			if(GUI.Button (new Rect(120, 150, 50, 20), "Equip")){
				selected = 1;
				Debug.Log ("Flash, Equipped");
			}
			                                                   
		}

	}

	public int getSelected()
	{
		return selected;
	}
	public void togglePause()
	{
		if (paused == true)
			paused = false;
		else
			paused = true;
	}



}