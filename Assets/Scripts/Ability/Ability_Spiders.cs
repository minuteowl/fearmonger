using UnityEngine;
using System.Collections;

public class Ability_Spiders : Ability {
	
	// Use this for initialization
	public Ability_Spiders () {
		Name="Spawn Spiders";
		Description = "Spiders come out of the ground at a specific point.";
		MinLevel=1;
		Duration = 2.5f;
		EnergyCost = 2;
		BuyCost = 1;
	}
	
	public override void UseAbility (PlayerLevel level, MonoBehaviour[] args) {
		Debug.Log("spider spell");
		// create a tentacles object - to be implemented later
	}
}
