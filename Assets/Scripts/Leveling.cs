using UnityEngine;
using System.Collections;
/* 
 * DESCRIPTION:
 * This class handles the player's leveling and experience.
 */
public class Leveling : MonoBehaviour {

	/*======== VARIABLES ========*/

	public int Level;
	public int ExpCurrent;
	public int ExpToNextLevel;

	GameManager game;

	public int energyCurrent;
	int energyMax=5;
	public float energyCountdown=0f;
	float energyCountdownMax=2f;
	public float energyRegenRate = 1f;

	/*======== FUNCTIONS ========*/

	// Use this for initialization
	void Start () {
		game = transform.GetComponent<GameManager>();
		Level = 1;
		ExpCurrent = 0;
		ExpToNextLevel = 10*(Level+1);
		energyCurrent=energyMax;
	}

	void LevelUp(){
		Level++;
		ExpToNextLevel += 10*(Level+1);  // Bring to zero or leftover experience
		energyMax ++;
		energyCurrent = energyMax;
	}

	public void AddExperience(int e){
		ExpCurrent += e;
	}

	public void UseAbility(Ability ability, int xp) {
		if (energyCurrent>ability.Cost) {
			energyCurrent -= ability.Cost;
			AddExperience(xp);
			Debug.Log("Used "+ability.Name+" ("+ability.Description+") and gained "+xp+" experience and cost "+ability.Cost);
		}
		else
		{
			Debug.Log("Not enough energy to use "+ability.Name+", which requires "+ability.Cost+" energy.");
		}
	}

	// This is called from the PlayerActivity Update()
	public void Update() {
		if (ExpCurrent >= ExpToNextLevel){
			LevelUp();
			Debug.Log("LEVEL UP TO LEVEL "+Level+"!");
		}
		if (energyCurrent<energyMax)
		{
			if (energyCountdown <= 0) {
				energyCurrent++;
				energyCountdown = energyCountdownMax;
			}
			else {
				energyCountdown -= game.Tick*energyRegenRate*Time.deltaTime;
			}
		}
		else if (energyCurrent>energyMax)
		{
			energyCurrent=energyMax;
		}
	}
}
