using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// Manages energy usage and player level.
public class PlayerLevel : MonoBehaviour {
	
	/*======== VARIABLES ========*/
	private int level=1,
	buyPoints=2,
	// when energy is below minimum energy, it regenerates
	energyMax=10,
	energyMin=2;
	private int energyCurrent;
	private int expCurrent;
	private int expToNextLevel;
	private Game game;
	
	// when energy is below minimum energy, it regenerate
	// By convention, timers start at zero and increment to max, then resets back to zero
	public float energyRegenTimer=0f; // timer, in seconds
	private float energyRegenTimerMax=1f; // time, in seconds, to regen 1 energy
	
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

	void CHEAT_DEBUG(){
		if (Input.GetKeyDown ("space")){
			LevelUp ();
		}
	}

	private void Start () {
		energyCurrent=energyMax;
		expToNextLevel=20;
		game=GameObject.Find("GameManager").GetComponent<Game>();
	}
	
	private void LevelUp(){
		PersonLists.GetNewCombos (level);
		level++;
		// these next values are arbitrary & we can change them later:
		energyMax = 8+2*level; //level 1: 10, level 11: 30, etc.
		energyMin ++;
		buyPoints +=2;
		expCurrent -= expToNextLevel;// resets to zero or overflow
		if (expCurrent<0) expCurrent=0;
		expToNextLevel = 20*level;
		if (level == 3 || level == 6 || level == 9) 
		{
			game.unlockFloor ();
		}
		Debug.Log ("LEVELED UP TO "+level); // also some message text is displayed
	}
	
	
	public void AddExperience(int e){
		expCurrent += e;
		if (energyCurrent+e>energyMax){
			energyCurrent=energyMax;
		} else {
			energyCurrent += e;
		}
		if (expCurrent+e>=expToNextLevel){
			LevelUp ();
		}
	}
	
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
		if (!ability.Locked && energyCurrent<ability.EnergyCost) {
			Debug.Log("Not enough energy to use "+ability.Name+", which requires "+ability.EnergyCost+" energy.");
			return false;
		}
		else return true;
	}
	
	private void Update() {
		CHEAT_DEBUG();
		// energy regeneration to bring it up to energyMin
		if (energyCurrent < energyMin) {
			if (energyRegenTimer<energyRegenTimerMax) {
				energyRegenTimer += Time.deltaTime*GameVars.Tick;
			} else {
				energyCurrent++;
				energyRegenTimer=0f;
			}
		} else if (energyCurrent < energyMax){
			// or, if it's less than max, it refills slowly
			if (energyRegenTimer<energyRegenTimerMax) {
				energyRegenTimer += 0.25f*Time.deltaTime*GameVars.Tick;
			} else {
				energyCurrent++;
				energyRegenTimer=0f;
			}
		}
	}
	
	private void OnGUI()
	{
		GUI.Box (new Rect (1, 1, 110, 60), "Level " + level +
		         ", XP: "+expCurrent+"/"+expToNextLevel + 
		         "\nEnergy: " + energyCurrent + "/" + energyMax +
		         "\nAbility Points: " + buyPoints);
	}
}
