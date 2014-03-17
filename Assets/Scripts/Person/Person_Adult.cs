using UnityEngine;
using System.Collections;

public class Person_Adult : Person {

	// Use this for initialization
	void Start () {
		isAdult=true;
		defenseBase=1;
		defenseSupport=1;
		sanityMax=12;
		sanityCurrent=sanityMax;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}
}
