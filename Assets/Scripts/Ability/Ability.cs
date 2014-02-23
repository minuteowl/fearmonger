using UnityEngine;
using System.Collections;
using System;

public abstract class Ability {

	public string Name;
	public string Description;
	public int Cost;
	public bool Locked=true;
	public int Level;
	public float Duration;

	// Animation info

	public void Unlock()
	{
		Locked = false;
		Debug.Log("LEVEL UP TO "+Level+"! New ability: "+this.Name);
	}

	public abstract void UseAbility(Leveling lvl, MonoBehaviour[] args);

}
