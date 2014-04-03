using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Person_Priest : Person {

	Animator anim;
	bool facingRight = false;
	bool facingLeft = false;
	bool facingUp = false;
	bool facingDown = true;

	public GUITexture healthBar;
	public float maxHealth = 100.00f;
	public float currentHealth = 100.00f;

	private Game viewChecker;

	List<Ability> activeSpells;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();

		viewChecker = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<Game>();

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


	// Update is called once per frame
	protected override void Update () {
		if (activeSpells.Count>0){
			Ability targetSpell = activeSpells[0];
			Dispel (targetSpell);
		}
		base.Update ();

		if (viewChecker.isAtMap()) {
			healthBar.enabled = false;
		} else {
			healthBar.enabled = true;
		}

		if (currentHealth > 0) {
			float healthRemPercent = currentHealth/maxHealth;

			//divide width by for because pixelinset width is set to 25
			float healthBarLen = (healthRemPercent * 100.00f)/4;
			healthBar.guiTexture.pixelInset = new Rect(healthBar.guiTexture.pixelInset.x,healthBar.guiTexture.pixelInset.y, healthBarLen,healthBar.guiTexture.pixelInset.height);
		}

		if (Mathf.Abs (rigidbody2D.velocity.y) > Mathf.Abs (rigidbody2D.velocity.x)) {
			if (rigidbody2D.velocity.y > 0) { 
				facingUp = true;
				facingDown = false;
				facingRight = false;
				facingLeft = false;
				
				anim.SetBool ("walkUp", facingUp);
				anim.SetBool ("walkDown", facingDown);
				anim.SetBool ("walkRight", facingRight);
				anim.SetBool ("walkLeft", facingLeft);
			} else { 
				facingUp = false;
				facingDown = true;
				facingRight = false;
				facingLeft = false;
				
				anim.SetBool ("walkUp", facingUp);
				anim.SetBool ("walkDown", facingDown);
				anim.SetBool ("walkRight", facingRight);
				anim.SetBool ("walkLeft", facingLeft);
			}
		} else if (Mathf.Abs (rigidbody2D.velocity.y) < Mathf.Abs (rigidbody2D.velocity.x)) {
			if (rigidbody2D.velocity.x > 0) {
				facingUp = false;
				facingDown = false;
				facingRight = true;
				facingLeft = false;
				
				anim.SetBool ("walkUp", facingUp);
				anim.SetBool ("walkDown", facingDown);
				anim.SetBool ("walkRight", facingRight);
				anim.SetBool ("walkLeft", facingLeft);
			} else {
				facingUp = false;
				facingDown = false;
				facingRight = false;
				facingLeft = true;
				
				anim.SetBool ("walkUp", facingUp);
				anim.SetBool ("walkDown", facingDown);
				anim.SetBool ("walkRight", facingRight);
				anim.SetBool ("walkLeft", facingLeft);
			}
		} else {
			facingUp = false;
			facingDown = false;
			facingRight = false;
			facingLeft = false;
			
			anim.SetBool ("walkUp", facingUp);
			anim.SetBool ("walkDown", facingDown);
			anim.SetBool ("walkRight", facingRight);
			anim.SetBool ("walkLeft", facingLeft);
		}

	}
}
