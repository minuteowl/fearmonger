using UnityEngine;
using System.Collections;
using System;

public abstract class Ability {

	public string Name, Description;
	public bool Locked=true;
	public int MinLevel; // we might not use this
	public int BuyCost, EnergyCost;
	public float Duration; // in seconds
	public int DarkBias=0; // 0=none, 1=dark, 2=light

	// Animation and sound?

	public void Unlock()
	{
		Locked = false;
		Debug.Log("Unlocked ability: "+this.Name);
	}

	public abstract void UseAbility(PlayerLevel lvl, MonoBehaviour[] args);

}
