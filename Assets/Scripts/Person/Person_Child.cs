using UnityEngine;
using System.Collections;

public class Person_Child : Person
{
	// Use this for initialization
	protected override void Start () {
		isAdult=false;
		defenseBase=0;
		defenseSupport=1;
		sanityMax=8;
		sanityCurrent=sanityMax;
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
	}
}
