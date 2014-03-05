using UnityEngine;
using System.Collections;

// This is probably going to be one of the most complex classes
public abstract class Person : MonoBehaviour {

	/*======== VARIABLES ========*/
	RoomObject myRoom;
	GameManager game;

	// Behavior
	protected float sightRadius;
	protected int sanityCurrent, sanityMax;

	protected Ability abilityWeak, abilityResist;

	LampObject targetLamp=null;
	float lamp_epsilon=2f; // minimum distance to activate lamp

	// people move by choosing a destination and then walking toward it
	// By convention, timers start at zero and increment to max, then reset to zero
	bool isWalking=false;
	float waitTimer=0.1f, waitTimerMax; // initially the person is waiting, so quickly have them start moving
	Vector3 destination;
	float walkSpeed;

	/*======== FUNCTIONS ========*/

	public void SetRoom(RoomObject r)
	{
		this.myRoom = r;
	}

	public void SetAbilityWeak(Ability a){
		this.abilityWeak = a;
	}

	public void SetAbilityResist(Ability a){
		this.abilityResist = a;
	}

	protected void OnGUI(){
		// show health bar above head
	}

	// Update is called once per frame
	public virtual void Update () {
		if (sanityCurrent>0) {
			if (myRoom.lampsOn==2) {
				sightRadius = 8f;
			}
			else if (myRoom.lampsOn==1) {
				sightRadius = 4f;
			}
			else {
				sightRadius = 0.5f;
			}
			UpdateSane();
		}
		else {
			UpdateInsane();
		}
	}

	void UpdateInsane(){
		// run toward door, assume that desination is the exit position
		if ((destination-transform.position).magnitude<1.5f) // fleeing and reached destination
		{
			Leave ();
		}
		else {
			transform.position += (destination-transform.position).normalized*walkSpeed*Time.deltaTime;
		}
	}

	// if the lamp is off, the room assigns me to turn the lamp on
	public void AssignLamp(LampObject lamp) {
		targetLamp = lamp;
		destination = targetLamp.transform.position;
	}

	// update when sanity>0
	void UpdateSane() {
		walkSpeed = 1.2f*sanityMax/(sanityCurrent+1); // less sane = faster
		if (myRoom.isOccupied) {
			if (targetLamp!=null){ // I have been assigned to turn on a lamp
				if (targetLamp.IsOn) {
					targetLamp=null; // the lamp is on, so I don't need to turn it on
				}
				else {
					if ((destination-transform.position).magnitude<lamp_epsilon){
						targetLamp.TurnOn();
						// set random destination
					}
					else {
						transform.position += (destination-transform.position).normalized*walkSpeed*Time.deltaTime;
					}
				}
			}
			else if (isWalking) {
				if ((destination-transform.position).magnitude>0.005f) // Decide when to stop at a destination
				{
					transform.position += (destination-transform.position).normalized*walkSpeed*Time.deltaTime;
				}
				else {
					waitTimer = 0;
					isWalking = false;
					waitTimerMax = Random.Range(0.3f, 1f);
				}
			}
			else if (waitTimer < waitTimerMax){ // should keep waiting
				waitTimer += GameVars.Tick*Time.deltaTime;
			}
			else { // done waiting
				isWalking = true;
				// set random destination
			}
		}
		else {
			// check-out logic
		}
	}


	// 0=up, 1=down, 2=right, 3=left
	// use this to determine which sprite(s) to use
	public int GetFacingDirection()
	{
		Vector3 velocity = destination-transform.position;
		if (Mathf.Abs(velocity.y)>Mathf.Abs(velocity.x)){
			if (velocity.y>0) return 0;
			else return 1;
		}
		else {
			if (velocity.x>0) return 2;
			else return 3;
		}
	}

	public void Leave(){
		//myRoom.CheckOut();
		Destroy(gameObject);
		Destroy(this);
	}

	// returns the amount of sanity damage dealt
	// also, if sanity <=0, set insane behavior
	public int Damage(int delta)
	{
		sanityCurrent -= delta;
		if (sanityCurrent <= 0){
			delta += sanityCurrent;
			destination = myRoom.DoorLocation;
			transform.rigidbody2D.Sleep();
			transform.collider2D.enabled=false;
		}
		return delta;
	}
}