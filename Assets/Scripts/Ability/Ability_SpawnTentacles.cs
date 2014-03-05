using UnityEngine;
using System.Collections;

public class Ability_SpawnTentacles : Ability {
	
	// Use this for initialization
	public Ability_SpawnTentacles () {
		Name="Spawn Tentacles";
		Description = "Tentacles come out of the ground at a specific point.";
		MinLevel=1;
		Duration = 2.5f;
		EnergyCost = 2;
		BuyCost = 1;
	}
	
	public override void UseAbility (PlayerLevel level, MonoBehaviour[] args) {
		Debug.Log("used tentacle");
		// create a tentacles object - to be implemented later
	}
}
