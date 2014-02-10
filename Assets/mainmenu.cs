using UnityEngine;
using System.Collections;
using System;

public class GUITest: MonoBehavior 
{
	void OnGUI()
	{
		GUI.Box(new Rect(10,10,100,90), "Main Menu");

		if(GUI.Button(new Rect(20,40,80,20), "New Game")) {

		}
		
		// Make the second button.
		if(GUI.Button(new Rect(20,70,80,20), "Help")) {
		}
	}
}