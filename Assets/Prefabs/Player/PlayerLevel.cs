using UnityEngine;
using System.Collections;
/* 
 * DESCRIPTION:
 * This class handles the player's leveling and experience.
 */
public class PlayerLevel : MonoBehaviour {

	/*======== VARIABLES ========*/

	// Treat these variables as read-only
	public int Level;
	public int ExpCurrent;
	public int ExpToNextLevel;

	/*======== FUNCTIONS ========*/

	// Use this for initialization
	void Start () {
		Level = 1;
		ExpCurrent = 0;
		ExpToNextLevel = 10*(Level+1);
	}

	void LevelUp(){
		Level++;
		ExpToNextLevel = 10*(Level+1);
		ExpCurrent = ExpCurrent - ExpToNextLevel;  // Bring to zero or leftover experience
	}

	public void AddExperience(int e){
		ExpCurrent += e;
	}

	// Update is called once per frame
	void Update () {
		if (ExpCurrent >= ExpToNextLevel){
			LevelUp();
		}
	}
}
