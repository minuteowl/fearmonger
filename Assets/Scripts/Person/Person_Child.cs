using UnityEngine;
using System.Collections;

public class Person_Child : Person
{

	// sound effect	
	public AudioClip doorOpenSound;

	// Use this for initialization
	void Start () {
		AudioSource.PlayClipAtPoint (doorOpenSound, transform.position);
		isAdult=false;
		defenseBase=0;
		defenseSupport=1;
		sanityMax=8;
		sanityCurrent=sanityMax;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();
	}
}
