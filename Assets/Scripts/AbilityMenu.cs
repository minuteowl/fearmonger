using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AbilityMenu: MonoBehaviour
{
	GameManager game;
	Rect menuBackground;
	int[] dimensions = new int[]{80, 100, 520, 410}; // x, y, width, height
	Color menuBackgroundColor;
	Ability selectedAbility;
	int i;

	[HideInInspector] public List<Ability> listAbilities;
	
	AbilityMenu()
	{
	}

	void Start()
	{
		listAbilities = new List<Ability>();
		listAbilities.Add(new Ability_Spiders()); 
		for (i = 0; i < 7; i++)
			listAbilities.Add (new Ability_Example ());
	}

	void OnGUI(){
		if (GUI.Button (new Rect (1, 61, 125, 30), listAbilities [0].Name)) {
			selectedAbility = listAbilities [0];
			Debug.Log (listAbilities [0].Name);
		}
		if (GUI.Button (new Rect (1, 91, 125, 30), listAbilities [1].Name + " 2")) {
			selectedAbility = listAbilities [1];
			Debug.Log (listAbilities [1].Name);
		}
		if (GUI.Button (new Rect (1, 121, 125, 30), listAbilities [1].Name + " 3")) {
			selectedAbility = listAbilities [2];
			Debug.Log (listAbilities [2].Name);
		}
		if (GUI.Button (new Rect (1, 151, 125, 30), listAbilities [1].Name + " 4")) {
			selectedAbility = listAbilities [3];
			Debug.Log (listAbilities [3].Name);
		}
		if (GUI.Button (new Rect (1, 181, 125, 30), listAbilities [1].Name + " 5")) {
			selectedAbility = listAbilities [4];
			Debug.Log (listAbilities [4].Name);
		}
		if (GUI.Button (new Rect (1, 211, 125, 30), listAbilities [1].Name + " 6")) {
			selectedAbility = listAbilities [5];
			Debug.Log (listAbilities [5].Name);
		}
		if (GUI.Button (new Rect (1, 241, 125, 30), listAbilities [1].Name + " 7")) {
			selectedAbility = listAbilities [6];
			Debug.Log (listAbilities [6].Name);
		}
		if (GUI.Button (new Rect (1, 271, 125, 30), listAbilities [1].Name + " 8")) {
			selectedAbility = listAbilities [7];
			Debug.Log (listAbilities [7].Name);
		}
	}

	void Update() {
	}

}