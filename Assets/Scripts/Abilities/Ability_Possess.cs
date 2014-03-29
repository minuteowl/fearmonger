using UnityEngine;
using System.Collections;

public class Ability_Possess : Ability {

	public Ability_Possess () {
		Name="Possession";
		Description = "Selected person becomes possessed, scaring the other people in the room.";
		MinLevel=12;
		Duration = 15f;
		EnergyCost = 8;
		BuyCost = 6;
		HazardTransform=Resources.Load<GameObject>("Prefabs/Hazards/Possession").transform;
	}
	
	public override void UseAbility (RoomObject room, MonoBehaviour[] args) {
		Debug.Log("possession spell");
		base.UseAbility(room,args);
	}
}
