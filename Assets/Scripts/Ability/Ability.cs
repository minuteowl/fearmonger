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
	protected Transform HazardTransform;
	protected Game game;
	public AudioClip spiderSound;
	
	// Animation and sound?
	
	public void Unlock()
	{
		Locked = false;
		Debug.Log("Unlocked ability: " + this.Name);
	}

	// room = the current room (based on GameManager)
	// args = depends on ability
	public virtual void UseAbility(RoomObject room, Vector2 clickLocation, MonoBehaviour[] args){
		room.ActiveAbilityEffects.Add (this);
		// normalize to proper Z-depth
		Debug.Log("Used ability "+ this.Name);
		Vector3 clickLocation3d = new Vector3(clickLocation.x, clickLocation.y, GameVars.DepthPeopleHazards);
		GameObject.Instantiate(HazardTransform,clickLocation3d,Quaternion.identity);
		AudioSource.PlayClipAtPoint (spiderSound, HazardTransform.position);
	}

	private void EndAbility(){
		game.currentRoom.ActiveAbilityEffects.Remove (this);
	}
	
}
