using UnityEngine;
using System.Collections;

public class Person_Adult : Person {

	Animator anim;
	bool facingRight = false;
	bool facingLeft = false;
	bool facingUp = false;
	bool facingDown = true;

	GUITexture healthBar;
	float maxHealth = 100.00f;
	float currentHealth = 100.00f;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();		

		isAdult=true;
		defenseBase=1;
		defenseSupport=1;
		sanityMax=12;
		sanityCurrent=sanityMax;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();

		/*
		if (currentHealth > 0) {
			float healthRemPercent = currentHealth/maxHealth;
			float healthBarLen = healthRemPercent * 100.00f;

			//healthBar.guiTexture.pixelInset.width = healthBarLen;
		}
		*/

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
