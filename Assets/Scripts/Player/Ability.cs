using UnityEngine;
using System.Collections;

public class Ability {

	public string Name;
	public string Description;
	public int Damage;
	public int Multiplier;

	// Animation info


	public Ability(string n, string des, int dam, int mul) {
		Name = n;
		Description = des;
		Damage = dam;
		Multiplier = mul;
	}

}
