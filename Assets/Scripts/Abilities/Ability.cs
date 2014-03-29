using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Ability {
	
	public string Name, Description;
	public bool Locked=true;
	public int MinLevel; // we might not use this
	public int BuyCost, EnergyCost;
	protected float Duration; // in seconds
	public RoomObject CurrentRoom;
	protected Transform HazardTransform;
	protected Vector3 clickLocation;
	
	// Animation and sound?
	
	public void Unlock()
	{
		Locked = false;
		Debug.Log("Unlocked ability: " + this.Name);
	}

	// room = the current room (based on camera)
	// args = depends on ability
	public virtual void UseAbility(RoomObject room, MonoBehaviour[] args){
		room.ActiveAbilityEffects.Add (this);
		Vector2 vmouse = GameInput.GetMouse2D();
		// normalize to proper Z-depth
		clickLocation = new Vector3(vmouse.x, vmouse.y, GameVars.DepthPeopleHazards);
		GameObject.Instantiate(HazardTransform,clickLocation,Quaternion.identity);
	}

	private void EndAbility(){
		CurrentRoom.ActiveAbilityEffects.Remove (this);
	}
	
}
