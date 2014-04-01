using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Person_Priest : Person {

	/*
	Animator anim;
	bool facingRight = false;
	bool facingLeft = false;
	bool facingUp = false;
	bool facingDown = true;
	*/

	List<Ability> activeSpells;

	// Use this for initialization
	void Start () {
		//anim = GetComponent<Animator> ();

		isAdult=true;
		defenseBase=4;
		defenseSupport=2;
		sanityMax=12;
		sanityCurrent=sanityMax;
		activeSpells = myRoom.ActiveAbilityEffects;
	}

	void Dispel(Ability targetSpell){
		// destroy ability
		// play dispel animation and sound
	}


	/*
	// Update is called once per frame
	void FixedUpdate () {
		
		float moveHor = Input.GetAxis ("Horizontal");
		float moveVer = Input.GetAxis ("Horizontal");
		
		if (moveVer > 0 && rigidbody2D.velocity.y > 0.0f) { 
			facingUp = true;
			facingDown = false;
			facingRight = false;
			facingLeft = false;
		}else if (moveVer < 0 && rigidbody2D.velocity.y < 0.0f){
			facingUp = false;
			facingDown = true;
			facingRight = false;
			facingLeft = false;
		}else if (moveHor > 0 && rigidbody2D.velocity.x > 0.0f){ 
			facingUp = false;
			facingDown = false;
			facingRight = true;
			facingLeft = false;
		}else if (moveHor < 0 && rigidbody2D.velocity.x < 0.0f){
			facingUp = false;
			facingDown = false;
			facingRight = false;
			facingLeft = true;
		}
	}
	*/

	// Update is called once per frame
	protected override void Update () {
		if (activeSpells.Count>0){
			Ability targetSpell = activeSpells[0];
			Dispel (targetSpell);
		}
		base.Update ();
	}
}
