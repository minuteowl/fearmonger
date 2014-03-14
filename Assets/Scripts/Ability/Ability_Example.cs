using UnityEngine;
using System.Collections;

public class Ability_Example : Ability {
	
	// Use this for initialization
	public Ability_Example () {
		Name="Example Ability";
		Description = "Describe what this ability does.";
		MinLevel=1;
		Duration = 2.0f;
		EnergyCost = 2;
		BuyCost = 1;
	}
	
	public override void UseAbility (PlayerLevel level, MonoBehaviour[] args) {
		Debug.Log("Used this ability");
		// create a tentacles object - to be implemented later
	}
}
