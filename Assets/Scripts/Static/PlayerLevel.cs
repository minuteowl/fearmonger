using UnityEngine;
using System.Collections;
/* 
 * DESCRIPTION:
 * This class handles the player's leveling and experience.
 */
public static class PlayerLevel  {

	/*======== VARIABLES ========*/

	// Treat these variables as read-only
	static int Level;
	static int ExpCurrent;
	static int ExpToNextLevel;

	/*======== FUNCTIONS ========*/

	// Use this for initialization
	static void Start () {
		Level = 1;
		ExpCurrent = 0;
		ExpToNextLevel = 10*(Level+1);
	}

	static void LevelUp(){
		Level++;
		ExpToNextLevel = 10*(Level+1);
		ExpCurrent = ExpCurrent - ExpToNextLevel;  // Bring to zero or leftover experience
	}

	public static void AddExperience(int e){
		ExpCurrent += e;
	}

	// Update is called once per frame
	static void Update () {
		if (ExpCurrent >= ExpToNextLevel){
			LevelUp();
		}
	}
}
