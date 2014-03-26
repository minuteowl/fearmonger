using UnityEngine;
using System.Collections;
using System;

public abstract class Ability {
	
	public string Name, Description;
	public bool Locked=true;
	public int MinLevel; // we might not use this
	public int BuyCost, EnergyCost;
	public float Duration; // in seconds
	public RoomObject CurrentRoom;
	
	// Animation and sound?
	
	public void Unlock()
	{
		Locked = false;
		Debug.Log("Unlocked ability: "+this.Name);
	}

	// room = the current room (based on camera)
	// args = depends on ability
	public virtual void UseAbility(RoomObject room, MonoBehaviour[] args){
		room.ActiveAbilityEffects.Add (this);
	}

	private void EndAbility(){
		CurrentRoom.ActiveAbilityEffects.Remove (this);
	}
	
}
