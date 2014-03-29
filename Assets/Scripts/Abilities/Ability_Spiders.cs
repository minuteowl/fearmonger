using UnityEngine;
using System.Collections;

public class Ability_Spiders : Ability {

	public Ability_Spiders () {
		Name="Spiders";
		Description = "Spiders come out of the ground and walk around the room.";
		MinLevel=1;
		Duration = 2.5f;
		EnergyCost = 2;
		BuyCost = 1;
		HazardTransform=Resources.Load<GameObject>("Prefabs/Hazards/Spiders").transform;
	}
	
	public override void UseAbility (RoomObject room, MonoBehaviour[] args) {
		Debug.Log("spider spell");
		base.UseAbility(room,args);
	}
}
