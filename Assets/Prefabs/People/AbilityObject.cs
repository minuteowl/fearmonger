using UnityEngine;
using System.Collections;

public class AbilityObject : MonoBehaviour {

	PlayerLevel plevel; // this is a pointer to the level object

	public int RequiredLevel;
	public int Damage;
	int ExpMultiplier;
	float countdown;

	public int RecoveryTime {
		get { return (int)countdown; }
	}

	public AbilityObject(int requiredlevel, int damage, int xpmultiplier, float recoverytime)
	{
		RequiredLevel = requiredlevel;
		Damage = damage;
		ExpMultiplier = xpmultiplier;
		countdown = recoverytime;
	}

	// Use this for initialization
	void Start () {
		plevel = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLevel>();
	}

	int UseAbility(PersonObject p){
		int dmg = p.DecreaseSanity(Damage);
		return dmg*ExpMultiplier;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
