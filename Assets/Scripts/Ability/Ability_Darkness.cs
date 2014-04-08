using UnityEngine;
using System.Collections;

public class Ability_Darkness : Ability {
	
	public Ability_Darkness () {
		FearDamage=GameVars.damage_darkness;
		Name="Dark Orb";
		Description = "The air turns dark and cold, and nearby lights go out.";
		MinLevel=2;
		Duration = GameVars.duration_darkness;
		EnergyCost = 3;
		BuyCost = 1;
		hazard=Resources.Load<GameObject>("Prefabs/Hazards/Darkness");
		effectSound=Resources.Load<AudioClip>("Sounds/PLACEHOLDER-darksound");
	}
	
	public override void UseAbility(Game game, Vector2 clickLocation){
		base.UseAbility(game, clickLocation);
	}
}
