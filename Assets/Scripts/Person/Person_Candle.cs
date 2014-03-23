﻿using UnityEngine;
using System.Collections;

public class Person_Candle : Person {

	// sound effect	
	public AudioClip doorOpenSound;

	// Use this for initialization
	void Start () {
		AudioSource.PlayClipAtPoint (doorOpenSound, transform.position);
		isAdult=true;
		defenseBase=1;
		defenseSupport=3;
		sanityMax=12;
		sanityCurrent=sanityMax;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}
}
