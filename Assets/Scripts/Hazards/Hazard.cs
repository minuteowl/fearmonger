using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour {

	protected float duration, timer=0f; // in seconds
	protected float radius;
	protected CircleCollider2D ccollider;
	protected RoomObject currentRoom;
	protected int damage;

	public void SetRoom(RoomObject r) {
		currentRoom = r;
	}

	// Use this for initialization
	protected virtual void Start () {
		ccollider = transform.GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if (timer<duration) {
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
