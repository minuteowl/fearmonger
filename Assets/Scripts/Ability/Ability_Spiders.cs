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
		//HazardTransform=Resources.Load<GameObject>("Sprites/Abilities/SpiderParticles").transform;
		hazard=Resources.Load<GameObject>("Prefabs/Hazards/Spiders");
		effectSound=Resources.Load<AudioClip>("Sounds/PLACEHOLDER-spidersound");
	}
	
	public override void UseAbility(RoomObject room, Vector2 clickLocation){
		Transform particles = Resources.Load<GameObject>("Sprites/Abilities/SpiderParticles").transform;
		Vector3 clickLocation3d = new Vector3(clickLocation.x, clickLocation.y, GameVars.DepthPeopleHazards);
		GameObject.Instantiate(particles,clickLocation3d,Quaternion.identity);
		base.UseAbility(room, clickLocation);
	}
}
