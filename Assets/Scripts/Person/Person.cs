﻿using UnityEngine;
using System.Collections;

// Base class for all Person AI
public abstract class Person : MonoBehaviour {

	public bool IS_FACING_UP=false, IS_FACING_DOWN=false, IS_FACING_RIGHT=false, IS_FACING_LEFT=false;

	/*======== VARIABLES ========*/
	protected RoomObject myRoom;
	//public string roomName;
	protected Game game;
	protected PlayerLevel leveling;
	[HideInInspector] public bool isAdult; // adults have different responsibilities
	[HideInInspector] public float attentionRadius;
	const float RADIUS_SMALL = 2.0f, RADIUS_MED = 4.0f, RADIUS_LARGE = 6.0f;

	// Behavior
	protected Animator anim;
	protected float sightRadius;
	protected GUITexture healthBar;
	protected GUIText text;
	public int sanityCurrent, sanityMax;
	public int Sanity {get{return sanityCurrent;}}
	protected int defenseBase, defenseCurrent, defenseSupport;
	public Person[] roommates;
	private bool canMove=true;
	public bool CanMove {get{return canMove;} set{canMove=value;}}

	//protected Ability abilityWeak, abilityResist;
	// turning on or off lamps
	protected LampObject targetLamp=null;
	public bool hasTargetLamp=false;

	// people move by choosing a destination and then walking toward it
	// By convention, timers start at zero and increment to max, then reset to zero
	public bool isHurt=false, isLeaving=false, isText=false;
	private float motionTimer=0f, motionTimerMax; // how long to walk or wait
	private float hurtTimer, hurtTimerMax=1.5f;// temporary invincibility when hurt
	private float textTimer, textTimerMax=0.8f;
	//public Vector3 destination3d;
	public Vector3 destination;
	private float walkSpeed;
	private Vector2 walkDirection;

	// sounds
	public AudioClip screamSound;

	/*======== FUNCTIONS ========*/

	// HEY ANIMATION PEOPLE!!! USE THIS!!!
	public void Animate(){
		SetFacingDirection (); // -> Sets IS_FACING_UP, etc, for you -> don't recalculate them.
		if (anim!=null){
			anim.SetBool ("walkUp", IS_FACING_UP);
			anim.SetBool ("walkDown", IS_FACING_DOWN);
			anim.SetBool ("walkRight", IS_FACING_RIGHT);
			anim.SetBool ("walkLeft", IS_FACING_LEFT);
		}
	}

	public void SetRoom(RoomObject r)
	{
		this.myRoom = r;
		//roomName = myRoom.RoomName;
		transform.parent = myRoom.transform;
		leveling = myRoom.game.playerLevel;
		roommates = new Person[myRoom.numberOccupants-1];
		int i=0;
		foreach(Person p in myRoom.occupants){
			if (p!=this) {
				roommates[i]=p;
				i++;
			}
		}
		game = myRoom.game;
		RestartWalk();
	}

	protected virtual void Start(){
		anim = transform.GetComponent<Animator> ();
		healthBar=transform.GetChild (0).GetComponent<GUITexture>();
		text=transform.GetChild (0).GetComponent<GUIText>();
		text.text="";
		//targetLamp=null;
	}

	void OnGUI(){
		// show health bar above head
	}

	// Update is called once per frame
	protected virtual void Update () {
		walkSpeed = 3.2f*(1.75f - sanityCurrent/sanityMax); // proportional to sanity% + 25%
		if (game.currentView==Game.View.Room && isText){
			if (textTimer<textTimerMax){
				textTimer += GameVars.Tick*Time.deltaTime;
			} else {
				text.text="";
				textTimer=0f;
				isText=false;
			}
		} else {
			text.text="";
			textTimer=0f;
			isText=false;
		}
		if (!isLeaving && sanityCurrent>0) {
			// reduce # of times to update complex AI
			UpdateStaying();
		}
		else {
			UpdateLeaving();
		}
		// Health bar:
		if (game.currentView==Game.View.Room && sanityCurrent>0){
			healthBar.enabled=true;
			float healthRemPercent = (1.0f*sanityCurrent)/sanityMax;
			//divide width by for because pixelinset width is set to 25
			float healthBarLen = (healthRemPercent * 100.00f)/4;
			healthBar.guiTexture.pixelInset = new Rect(healthBar.guiTexture.pixelInset.x,healthBar.guiTexture.pixelInset.y, healthBarLen,healthBar.guiTexture.pixelInset.height);
		}
		else {
			healthBar.enabled=false;
		}
		Animate ();
	}

	private void UpdateLeaving(){
		Debug.Log("I am "+((Vector2)destination-(Vector2)transform.position).magnitude+" distance from the door.");
		// run toward door, assume that desination is the exit position
		if (((Vector2)destination-(Vector2)transform.position).magnitude<0.5f) // fleeing and reached destination
		{
			Leave ();
		}
		else {
			rigidbody2D.velocity = ((Vector2)destination-(Vector2)transform.position).normalized*walkSpeed;
		}
	}

	// Logic for comforting or being comforted
	private void DefendOther(Person other){
		other.AddDefense(defenseSupport);
	}

	// Other roommates are sources of external defense
	public void AddDefense(int defSupport){
		defenseCurrent+=defSupport;
	}

	// if the lamp is off, the room assigns me to turn the lamp on
	public void AssignLamp(LampObject lamp) {
		targetLamp = lamp;
		hasTargetLamp=true;
		destination = new Vector3(targetLamp.transform.position.x,
		                          targetLamp.transform.position.y,
		                          GameVars.DepthPeopleHazards);
	}

	private void RestartWalk(){
		motionTimer=0f;
		motionTimerMax = Random.Range(0.3f, 0.9f);
		destination= new Vector3(
			3.85f*UnityEngine.Random.Range(-attentionRadius,attentionRadius)+myRoom.transform.position.x,
			3.75f*UnityEngine.Random.Range(-attentionRadius,attentionRadius)+myRoom.transform.position.y,
			GameVars.DepthPeopleHazards);
		walkDirection = ((Vector2)destination-(Vector2)transform.position).normalized;
		rigidbody2D.velocity = Vector2.zero;
	}

	// Prevent them from sticking to one another
	private void OnCollisionEnter2D(Collision2D other){
		if (!isLeaving && other.transform.CompareTag("Person")) {
			/// bumping into another person
			walkDirection = ((Vector2)transform.position-(Vector2)other.transform.position).normalized;
			motionTimerMax*=1.5f;
		}
		else if (other.transform.CompareTag ("Hazard")){
			///bump into (or get too close to) a hazard
			walkDirection = ((Vector2)transform.position-(Vector2)other.transform.position).normalized;
			motionTimerMax*=2.2f;
		}
	}

	public void GoToDoor(){
		destination = myRoom.ExitLocation;
		//sanityCurrent=0;
		//isHurt=true;
		isLeaving = true;
	}

	// update when sanity>0
	private void UpdateStaying() {
		// the # of lights in the room determine the attention radius
		if (myRoom.LightsOn>=2) { attentionRadius=RADIUS_LARGE; }
		else if (myRoom.LightsOn==1){ attentionRadius=RADIUS_MED;}
		else {attentionRadius = RADIUS_SMALL;}

		// recalculate defense
		defenseCurrent=defenseBase;
		foreach(Person roomie in roommates){
			if (roomie!=null){ // always check to see that a person still exists.
				if (((Vector2)roomie.transform.position-(Vector2)transform.position).magnitude<attentionRadius){
					DefendOther(roomie);
				}
			}
		}
		// recovering from an attack
		if (isHurt) {
			if (hurtTimer<hurtTimerMax) {
				hurtTimer += GameVars.Tick*Time.deltaTime;

			} else {
				hurtTimer = 0f;
				isHurt=false;
			}
		}
		if(canMove){
			// has been assigned to turn on a lamp
			if (targetLamp!=null){
				Debug.Log ("I am "+((Vector2)targetLamp.transform.position-(Vector2)transform.position).magnitude+" away from the lamp.");
				if (targetLamp.IsOn) {
					// lamp is already on --> don't have to turn it on anymore
					targetLamp=null;
					hasTargetLamp=false;
					RestartWalk();
				}
				//private const float LAMP_EPSILON=2f; // minimum distance to activate lamp
				else if (((Vector2)targetLamp.transform.position-(Vector2)transform.position).magnitude<1f){
					// close enough to the lamp --> turn on the lamp
					targetLamp.TurnOn();
					targetLamp=null;
					hasTargetLamp=false;
					RestartWalk();
				}
				else { // lamp is still off --> walk toward lamp
					walkDirection = ((Vector2)destination-(Vector2)transform.position).normalized;
				}
			}
			else if (((Vector2)transform.position-(Vector2)destination).magnitude<0.8f || motionTimer>=motionTimerMax){
				RestartWalk();
			}
			else {
				motionTimer += GameVars.Tick*Time.deltaTime;
			}
			rigidbody2D.velocity=walkDirection*walkSpeed;

		}// only do the above if canMove
	}
	
	// use this to determine which sprite(s) to use
	private void SetFacingDirection()
	{
		if (Mathf.Abs(rigidbody2D.velocity.y)>Mathf.Abs(rigidbody2D.velocity.x)){
			if (rigidbody2D.velocity.y>0) {
				IS_FACING_UP=true; IS_FACING_DOWN=false;
				IS_FACING_LEFT=false; IS_FACING_RIGHT=false;
			}
			else {
				IS_FACING_UP=false; IS_FACING_DOWN=true;
				IS_FACING_LEFT=false; IS_FACING_RIGHT=false;
			}
		}
		else {
			if (rigidbody2D.velocity.x>0){
				IS_FACING_UP=false; IS_FACING_DOWN=false;
				IS_FACING_LEFT=false; IS_FACING_RIGHT=true;
			}
			else {
				IS_FACING_UP=false; IS_FACING_DOWN=false;
				IS_FACING_LEFT=true; IS_FACING_RIGHT=false;
			}
		}
	}

	// 
	public void Threaten(Hazard haz){
		if (!hasTargetLamp && !isLeaving && sanityCurrent>0) {
			Vector3 v = (transform.position - haz.transform.position).normalized;
			Vector2 noise = new Vector2(UnityEngine.Random.Range(-.45f,.45f),UnityEngine.Random.Range(-.4f,.4f));
			walkDirection = new Vector3(v.x+noise.x, v.y+noise.x);
		}
	}

	// forces the person to leave, delete object
	public void Leave(){
		myRoom.numberOccupants--;
		Destroy(gameObject);
		Destroy(this);
	}

	// returns the amount of sanity damage dealt, after defense calculation
	// also, if sanity <=0, set insane behavior
	public void Damage(int delta)
	{
		delta -= defenseCurrent;
		if (!isHurt && delta > 0) {
			isHurt=true;
			if (UnityEngine.Random.Range (0,10)<5){
				text.text=ShockPhrases.Phrase (delta);
			} else {
				text.text="!";
			}
			isText=true;
			if (screamSound!=null)
				AudioSource.PlayClipAtPoint (screamSound, transform.position);
			if (delta>=sanityCurrent){
				delta=sanityCurrent;
				GoToDoor();
			}
			sanityCurrent -= delta;
			
			leveling.AddExperience(delta);
		}
	}
}