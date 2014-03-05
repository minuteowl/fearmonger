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

	[HideInInspector] public List<Ability> listAbilities;
	
	AbilityMenu()
	{
		listAbilities = new List<Ability>();
		listAbilities.Add(new Ability_SpawnTentacles());
		// listAbilities.Add(new Ability_...()); for all abilities
	}

	void Start()
	{
	}

	void Update() {
	}

}