using UnityEngine;
using System.Collections;

public abstract class Ability {

	public string Name;
	public string Description;
	public int Damage;
	public int Multiplier;
	public int Cost;

	// Animation info

	public abstract void Use(Leveling lvl, MonoBehaviour[] args);

}
