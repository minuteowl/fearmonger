using UnityEngine;
using System.Collections;

public class Ability_Monster : Ability {

	public Ability_Monster () {
		FearDamage=GameVars.damage_monster;
		Name="Summon Monster";
		Description = "A monster that chases people around. Explodes into fear damage on impact.";
		MinLevel=5;
		Duration = GameVars.duration_monster;
		EnergyCost = 4;
		BuyCost = 4;
		hazard=Resources.Load<GameObject>("Prefabs/Hazards/Monster");
		effectSound=Resources.Load<AudioClip>("Sounds/ghost_giggle_3");
	}
	
	public override void UseAbility(Game game, Vector2 clickLocation){
		base.UseAbility(game, clickLocation);
	}
}
