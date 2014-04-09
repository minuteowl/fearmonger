using UnityEngine;
using System.Collections;

public class Hazard_Claw_Projectile : MonoBehaviour {

	private Hazard_Claw parentHazard;
	private Vector2 origin, destination;
	private float speed=20f; // how fast it stretches
	private bool isExtending=true;
	private Person targetPerson;
	private const float minSeparation=0.95f; // when to grab or die
	
	// Find closest person and target them
	private void Start () {
		parentHazard = transform.parent.GetComponent<Hazard_Claw>();
		RoomObject currentRoom = parentHazard.room;
		origin = (Vector2)parentHazard.transform.position;
		float minDist=999f;
		float dist;
		foreach (Person p in currentRoom.occupants){
			if (p!=null){
				dist = ((Vector2)p.transform.position - (Vector2)transform.position).magnitude;
				if (dist<minDist){
					minDist = dist;
					targetPerson=p;
					destination=(Vector2)targetPerson.transform.position;
				}
			}
		}
	}
	
	private void OnTriggerEnter2D(Collider2D other){
		if (other.transform.CompareTag ("Person")){
			targetPerson = other.transform.GetComponent<Person>();
			targetPerson.CanMove=false;
			isExtending=false;
			targetPerson.Damage(parentHazard.dam);
		}
	}

	private void Finish(){
		Destroy(gameObject);
		Destroy(this);
	}
	
	// No duration
	private void Update () {
		if(parentHazard==null || targetPerson==null){
			Finish ();
		}
		else if (isExtending) { // extending
			destination = (Vector2)targetPerson.transform.position;
			transform.position += (Vector3)(destination-origin).normalized*speed*Time.deltaTime;
			if (((Vector2)transform.position-destination).magnitude<minSeparation){
				isExtending=false;
				speed *= 0.33f;
			}
		}
		else { // retracting
			transform.position += (Vector3)(origin-destination).normalized*speed*Time.deltaTime;
			if (((Vector2)transform.position-origin).magnitude>minSeparation){
				if (targetPerson != null) {
					targetPerson.CanMove=false;
					targetPerson.rigidbody2D.velocity = (origin-destination).normalized*speed;
				}
			}
			else {
				if (targetPerson != null){
					targetPerson.CanMove=true;
				}
				Finish ();
			}
		}
	}
}
