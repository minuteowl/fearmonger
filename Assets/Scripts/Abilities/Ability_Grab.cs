using UnityEngine;
using System.Collections;

public class Ability_Grab : Ability {

	public Ability_Grab () {
		Name="Reaching Hand";
		Description = "A hand appears and reaches out, pulling the closest victim toward its origin.";
		MinLevel=6;
		Duration = 0f; // instant
		EnergyCost = 6;
		BuyCost = 3;
		HazardTransform=Resources.Load<GameObject>("Prefabs/Hazards/Arm").transform;
	}
	
	public override void UseAbility (RoomObject room, MonoBehaviour[] args) {
		Debug.Log("grab spell");
		base.UseAbility(room,args);
	}
}
