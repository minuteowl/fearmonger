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
		HazardTransform=Resources.Load<GameObject>("Sprites/Abilities/SpiderParticles").transform;
	}
	
	public override void UseAbility(RoomObject room, Vector2 clickLocation, MonoBehaviour[] args){
		base.UseAbility(room, clickLocation, args);
	}
}
