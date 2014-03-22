using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// Manages energy usage and player level.
public class PlayerLevel : MonoBehaviour {

	/*======== VARIABLES ========*/
	
	private AbilityMenu abilityMenu;
	private List<Ability> listAbilities;

	private int level=1,
		buyPoints=2,
		energyMax=10, // energy to next level
		energyMin=2;
	private int energyCurrent;
	// when energy is below minimum energy, it regenerate
	// By convention, timers start at zero and increment to max, then resets back to zero
	private float energyRegenTimer=0f, // timer, in seconds
		  energyRegenTimerMax=1f; // time, in seconds, to regen 1 energy

	// "Read-only" variables
	public int Level {
		get {return level;}
	}
	public int BuyPoints {
		get {return buyPoints;}
	}
	public int EnergyCurrent {
		get {return energyCurrent;}
	}
		  
	/*======== FUNCTIONS ========*/

	private void Start () {
		energyCurrent=energyMax;
		abilityMenu = transform.GetComponent<AbilityMenu>();
		listAbilities = abilityMenu.listAbilities;
	}

	private void LevelUp(){
		level++;
		// these next values are arbitrary & we can change them later:
		energyMax = 10*level;
		energyMin ++;
		buyPoints +=2;
		Debug.Log ("LEVELED UP TO "+level);
	}

	public void AddEnergy(int e){
		energyCurrent += e;
		if (energyCurrent>energyMax){
			energyCurrent -= energyMax; // to level up, energyCurrent >= energyMax,
			//UseEnergy (0); // but to be safe, make sure energyCurrent >= 0.
			LevelUp ();
		}
	}

	// call UseEnergy(0) to make sure energyCurrent >= 0
	// generally this is called from using an ability
	public void UseEnergy(int e){
		energyCurrent -= e;
		if (energyCurrent<0)
			energyCurrent=0;
	}

	public void BuyAbility(Ability ability) {
		ability.Unlock();
		buyPoints -= ability.BuyCost;
	}

	public bool CanUseAbility(Ability ability) {
		if (energyCurrent<ability.EnergyCost) {
			Debug.Log("Not enough energy to use "+ability.Name+", which requires "+ability.EnergyCost+" energy.");
			return false;
		}
		else return true;
	}

	private void Update() {
		// energy regeneration to bring it up to energyMin
		if (energyCurrent < energyMin) {
			if (energyRegenTimer<energyRegenTimerMax) {
				energyRegenTimer += Time.deltaTime;
			} else {
				energyCurrent++;
				energyRegenTimer=0f;
			}
		}
	}

	/* void OnGUI()
	{
		GUI.Box (new Rect (150, 38, 100, 25), "Player Level: " + Level);
		GUI.Box (new Rect (450, 38, 100, 25), "Energy: " + energyCurrent + "/" + energyMax);
	}*/
}
