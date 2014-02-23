using UnityEngine;
using System.Collections;

public class Ability_Tentacle : Ability {
	
	// Use this for initialization
	public Ability_Tentacle () {
		Name="Tentacle";
		Description = "Extend long tentacles from your body to reach out.";
		Cost = 2;
		Level=3;
		Duration = 2f;
	}
	
	// Update is called once per frame
	public override void UseAbility (Leveling level, MonoBehaviour[] args) {
		Debug.Log("used tentacle");
		// to be implemented later
	}
}
