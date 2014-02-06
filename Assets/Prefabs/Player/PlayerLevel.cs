using UnityEngine;
using System.Collections;

public class PlayerLevel : MonoBehaviour {

	public int Level = 1;
	public int ExpCurrent = 0;
	public int ExpToNextLevel = 20;


	// Use this for initialization
	void Start () {
		Level = 1;
	}

	void LevelUp(){
		Level++;
		ExpToNextLevel = 10*(Level+1);
	}

	public void AddExperience(int e){
		ExpCurrent += e;
	}

	// Update is called once per frame
	void Update () {
		if (ExpCurrent >= ExpToNextLevel){
			LevelUp();
			ExpCurrent -= ExpToNextLevel; // Bring to zero or leftover experience
		}
	}
}
