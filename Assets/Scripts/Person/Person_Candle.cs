using UnityEngine;
using System.Collections;

public class Person_Candle : Person {

	public GUITexture healthBar;
	public float maxHealth = 100.00f;
	public float currentHealth = 100.00f;
	
	private Game viewChecker;

	// Use this for initialization
	void Start () {
		viewChecker = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<Game>();

		isAdult=true;
		defenseBase=1;
		defenseSupport=3;
		sanityMax=12;
		sanityCurrent=sanityMax;
	}
	
	// Update is called once per frame
	protected override void Update () {
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
	}
}
