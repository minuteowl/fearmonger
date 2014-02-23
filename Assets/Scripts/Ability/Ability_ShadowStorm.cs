using UnityEngine;
using System.Collections;

public class Ability_ShadowStorm : Ability {

	// Use this for initialization
	public Ability_ShadowStorm () {
		Name="Shadow Storm";
		Description = "Shadows in the room come to life and cause chaos.";
		Cost = 5;
		Level=5;
		Duration = 4f;
	}
	
	// Update is called once per frame
	public override void UseAbility (Leveling level, MonoBehaviour[] args) {
		Debug.Log("used shadow storm");
		// to be implemented later
	}
}
