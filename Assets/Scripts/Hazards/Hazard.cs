using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour {

	public float duration, timer; // in seconds
	protected float maxDistance=20f;
	protected float radius;
	protected CircleCollider2D ccollider;
	protected RoomObject currentRoom;
	protected int damage;

	// Use this for initialization
	protected virtual void Start () {
		ccollider = transform.GetComponent<CircleCollider2D>();
		// This stays as the room in which the hazard was created
		currentRoom = GameObject.Find ("GameManager").GetComponent<Game>().currentRoom;
		transform.parent = currentRoom.transform;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if (((Vector2)transform.position-(Vector2)currentRoom.transform.position).magnitude>maxDistance){
			Finish (); // prevents it from flying out of the room!
		}
		else if (timer<duration) {
			timer += GameVars.Tick*Time.deltaTime;
		}
		else {
			Finish();
		}
	}

	// Run when duration is up
	protected virtual void Finish() {
		// play some particle effect/animation
		Destroy(gameObject);
		Destroy(this);
	}
}
