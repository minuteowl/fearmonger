using UnityEngine;
using System.Collections;

public class Ability_Darkness : Ability {
	
	public Ability_Darkness () {
		Name="Dark Orb";
		Description = "The air turns dark and cold, and nearby lights go out.";
		MinLevel=2;
		Duration = 1.75f;
		EnergyCost = 3;
		BuyCost = 1;
		HazardTransform=Resources.Load<GameObject>("Prefabs/Hazards/Darkness").transform;
	}
	
	public override void UseAbility(RoomObject room, Vector2 clickLocation, MonoBehaviour[] args){
		base.UseAbility(room, clickLocation, args);
	}
}
