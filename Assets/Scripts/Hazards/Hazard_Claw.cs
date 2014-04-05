using UnityEngine;
using System.Collections;

// a hand comes out of the groud, pulls the nearest person toward it

public class Hazard_Claw : Hazard {

	// So this is the thing that is dropped on the ground
	private Transform clawObject;

	// Use this for initialization
	protected override void Start () {
		duration=0f;
		base.Start ();
		clawObject=transform.GetChild(0);
	}

	public RoomObject room { get { return currentRoom;}}
	public int dam { get { return damage;}}

	// No duration
	protected override void Update () {
		if(clawObject==null){
			Finish ();
		}
	}

	private void OnCollision2D(Collision2D other){
		if (other.transform.CompareTag ("Person")){
			Person p = other.transform.GetComponent<Person>();
			p.Damage (damage);
		}
	}
}
