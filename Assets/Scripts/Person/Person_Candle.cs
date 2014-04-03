using UnityEngine;
using System.Collections;

public class Person_Candle : Person {

	public GUITexture healthBar;
	public float maxHealth = 100.00f;
	public float currentHealth = 100.00f;


	// Use this for initialization
	void Start () {
		isAdult=true;
		defenseBase=1;
		defenseSupport=3;
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
	}
}
