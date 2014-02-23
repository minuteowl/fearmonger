using UnityEngine;
using System.Collections;

public class Ability_Tentacle : Ability {
	
	public Ability_Tentacle () {
		this.Name = "Tentacle";
		this.Description = "Extend a tentacle";
		this.Damage = 1;
		this.Multiplier = 1;
		this.Cost = 1;
	}

	public override void Use (Leveling lvl, MonoBehaviour[] args) {
		PersonObject p = (PersonObject)args[0];
		int xp = p.DecreaseSanity(Damage)*Multiplier;
		lvl.UseAbility(this, xp);
	}
}
