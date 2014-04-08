﻿using UnityEngine;
using System.Collections;

public class Ability_Possess : Ability {

	public Ability_Possess () {
		Name="Possession";
		Description = "Selected person becomes possessed, scaring the other people in the room.";
		MinLevel=12;
		Duration = GameVars.duration_possession;
		EnergyCost = 8;
		BuyCost = 6;
		FearDamage=GameVars.damage_possession;
		hazard=Resources.Load<GameObject>("Prefabs/Hazards/Possession");
		effectSound=Resources.Load<AudioClip>("Sounds/PLACEHOLDER-darksound");
	}
	
	public override void UseAbility(Game game, Vector2 clickLocation){
		base.UseAbility(game, clickLocation);
	}
}
