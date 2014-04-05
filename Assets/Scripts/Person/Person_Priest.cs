using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Person_Priest : Person {

	List<Ability> activeSpells;

	// Use this for initialization
	protected override void Start () {
		isAdult=true;
		defenseBase=4;
		defenseSupport=2;
		sanityMax=12;
		sanityCurrent=sanityMax;
		activeSpells = myRoom.ActiveAbilityEffects;
		base.Start ();
	}

	void Dispel(Ability targetSpell){
		// destroy ability
		// play dispel animation and sound
	}


	// Update is called once per frame
	protected override void Update () {
		if (activeSpells.Count>0){
			Ability targetSpell = activeSpells[0];
			Dispel (targetSpell);
		}
		base.Update ();

	}
}
