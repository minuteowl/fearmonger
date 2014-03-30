using UnityEngine;
using System.Collections;

public class Ability_Monster : Ability {

	public Ability_Monster () {
		Name="Summon Monster";
		Description = "A monster that chases people around. Explodes into fear damage on impact.";
		MinLevel=5;
		Duration = 12f;
		EnergyCost = 4;
		BuyCost = 4;
		HazardTransform=Resources.Load<GameObject>("Prefabs/Hazards/Monster").transform;
	}
	
	public override void UseAbility(RoomObject room, Vector2 clickLocation, MonoBehaviour[] args){
		base.UseAbility(room, clickLocation, args);
	}
}
