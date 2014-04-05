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
		hazard=Resources.Load<GameObject>("Prefabs/Hazards/Possession");
		effectSound=Resources.Load<AudioClip>("Sounds/PLACEHOLDER-darksound");
	}
	
	public override void UseAbility(RoomObject room, Vector2 clickLocation){
		base.UseAbility(room, clickLocation);
	}
}
