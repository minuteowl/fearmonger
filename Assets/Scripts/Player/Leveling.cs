using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/* 
 * DESCRIPTION:
 * This class handles the player's leveling and experience.
 */
public class Leveling : MonoBehaviour {

	/*======== VARIABLES ========*/

	public int Level, ExpCurrent, ExpToNextLevel;

	AbilityMenu abilities;
	List<Ability> listAbilities;

	public int energyCurrent;
	int energyMax=5;
	public float energyCountdown=0f;
	float energyCountdownMax=2f;
	public float energyRegenRate = 1f;

	/*======== FUNCTIONS ========*/

	void Start () {
		energyCurrent=energyMax;
		Level=1;
		ExpCurrent=0;
		ExpToNextLevel=10;
		abilities = transform.GetComponent<AbilityMenu>();
		listAbilities = abilities.listAbilities;
	}

	void LevelUp(){
		Level++;
		ExpToNextLevel += 10*Level;  // Bring to zero or leftover experience
		energyMax ++;
		energyCurrent = energyMax;
		Debug.Log ("LEVELED UP TO "+Level);
		foreach (Ability a in listAbilities) {
			if (a.Locked && a.Level<=this.Level) {
				a.Unlock();
			}
		}
	}

	public void AddExperience(int e){
		ExpCurrent += e;
	}

	public bool CanUse(Ability ability) {
		if (energyCurrent<ability.Cost) {
			Debug.Log("Not enough energy to use "+ability.Name+", which requires "+ability.Cost+" energy.");
			return false;
		}
		else return true;
	}

	public void UseAbility(Ability ability) {
		energyCurrent -= ability.Cost;
		energyCountdown = energyCountdownMax;
	}

	void Update() {
		if (ExpCurrent >= ExpToNextLevel){
			LevelUp();
		}
		if (energyCurrent<energyMax)
		{
			if (energyCountdown <= 0) {
				energyCurrent++;
				energyCountdown = energyCountdownMax;
			}
			else {
				energyCountdown -= energyRegenRate*Statics.Tick*Time.deltaTime;
			}
		}
		else if (energyCurrent>energyMax)
		{
			energyCurrent=energyMax;
		}
	}

	void OnGUI()
	{
		GUI.Box (new Rect (150, 38, 100, 25), "Player Level: " + Level);
		GUI.Box (new Rect (300, 38, 100, 25), "EXP: " + ExpCurrent + "/" + ExpToNextLevel);
		GUI.Box (new Rect (450, 38, 100, 25), "Energy: " + energyCurrent + "/" + energyMax);
	}
}
