using UnityEngine;
using System.Collections;

// a hand comes out of the groud, pulls the nearest person toward it

public class Hazard_Grab : Hazard {

	Vector3 origin, direction;
	float speed=20f; // how fast it stretches
	bool hasGrabbed=false;
	Person targetPerson;
	float minSeparation=0.5f; // when to grab or die

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		// Find closest NPC
		origin = transform.position;
		float minDist=999f;
		foreach (Person p in currentRoom.occupants){
			direction = (transform.position - p.transform.position);
			if (direction.magnitude<minDist){
				minDist = direction.magnitude;
				targetPerson = p;
			}
		}
	}
	
	// No duration
	protected override void Update () {
		if (!hasGrabbed) {
			transform.position += direction*speed*GameVars.Tick*Time.deltaTime;
			if ((transform.position-origin).magnitude<minSeparation){
				targetPerson.canMove=false;
				hasGrabbed=true;
				targetPerson.Damage(damage);
			}
		}
		else {
			if ((transform.position-origin).magnitude>minSeparation){
				transform.position -= direction*speed*GameVars.Tick*Time.deltaTime;
				targetPerson.transform.position -= direction*speed*GameVars.Tick*Time.deltaTime;
			}
			else {
				targetPerson.canMove=true;
				Finish ();
			}
		}
	}
}
