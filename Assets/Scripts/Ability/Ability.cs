using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class Ability {
	
	public string Name, Description;
	public bool Locked=true;
	public int MinLevel; // we might not use this
	public int BuyCost, EnergyCost, FearDamage;
	protected float Duration; // in seconds
	protected GameObject hazard;
	protected Game game;
	public AudioClip effectSound;
	
	// Animation and sound?
	
	public void Unlock()
	{
		Locked = false;
		Debug.Log("Unlocked ability: " + this.Name);
	}

	// room = the current room (based on GameManager)
	// args = depends on ability
	public virtual void UseAbility(Game game, Vector2 clickLocation){
		//game.currentRoom.ActiveAbilityEffects.Add (this);
		game.playerLevel.UseEnergy(EnergyCost);
		// normalize to proper Z-depth
		Debug.Log("Used ability "+ this.Name);
		Vector3 clickLocation3d = new Vector3(clickLocation.x, clickLocation.y, GameVars.DepthPeopleHazards);
		GameObject.Instantiate(hazard,clickLocation3d,Quaternion.identity);
		if(effectSound!=null)
			AudioSource.PlayClipAtPoint (effectSound, hazard.transform.position);
	}

	private void EndAbility(){
		//game.currentRoom.ActiveAbilityEffects.Remove (this);
	}
	
}
