using UnityEngine;
using System.Collections;

// Base class for all Person AI
public abstract class Person : MonoBehaviour {

	/*======== VARIABLES ========*/
	private int aicheck=0,aicheckmax=10;//reduce the # of times to check AI
	protected RoomObject myRoom;
	protected GameManager game;
	protected PlayerLevel leveling;
	[HideInInspector] public bool isAdult; // adults have different responsibilities
	public float attentionRadius;
	const float RADIUS_SMALL = 1.0f, RADIUS_MED = 1.75f, RADIUS_LARGE = 2.5f;

	// Behavior
	protected float sightRadius;
	protected int sanityCurrent, sanityMax=1;
	public int defenseBase, defenseCurrent, defenseSupport;
	public Person[] roommates;

	//protected Ability abilityWeak, abilityResist;
	// turning on or off lamps
	public LampObject targetLamp=null;
	private const float LAMP_EPSILON=2f; // minimum distance to activate lamp

	// people move by choosing a destination and then walking toward it
	// By convention, timers start at zero and increment to max, then reset to zero
	private bool isWalking=false, isHurt=false, isLeaving=false;
	private float motionTimer=0f, motionTimerMax; // how long to walk or wait
	private float hurtTimer, hurtTimerMax=0.25f;//
	private Vector3 destination;
	private float walkSpeed;
	private Vector2 walkDirection;

	/*======== FUNCTIONS ========*/

	public void SetRoom(RoomObject r)
	{
		this.myRoom = r;
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
		StopWalking();
	}

	void OnGUI(){
		// show health bar above head
	}

	// Update is called once per frame
	protected virtual void Update () {
		walkSpeed = 90f*(1.75f - sanityCurrent/sanityMax); // proportional to sanity% + 25%
		if (!isLeaving && sanityCurrent>0) {
			// reduce # of times to update complex AI
			aicheck = (aicheck+1)%aicheckmax;
			if (aicheck==0){
				UpdateStaying();
			}
		}
		else {
			UpdateLeaving();
		}
	}

	void UpdateLeaving(){
		// run toward door, assume that desination is the exit position
		print (flatvector(destination,transform.position).magnitude);
		if (flatvector(destination,transform.position).magnitude<0.05f) // fleeing and reached destination
		{
			Leave ();
		}
		else {
			rigidbody2D.velocity = flatvector(destination,transform.position).normalized*walkSpeed*Time.deltaTime;
		}
	}

	// Logic for comforting or being comforted
	private void DefendOther(Person other){
		other.SetDefense(defenseSupport);
	}

	// Other roommates are sources of external defense
	public void SetDefense(int defSupport){
		defenseCurrent+=defSupport;
	}

	// if the lamp is off, the room assigns me to turn the lamp on
	public void AssignLamp(LampObject lamp) {
		targetLamp = lamp;
		destination = targetLamp.transform.position;
	}

	void StopWalking(){
		isWalking=false;
		motionTimer=0f;
		motionTimerMax = Random.Range(0.05f, 0.5f);
		rigidbody2D.velocity=Vector2.zero;
		walkDirection=Vector2.zero;
	}

	void StartWalking(){
		isWalking=true;
		motionTimer=0f;
		motionTimerMax = Random.Range(0.1f, 0.8f);
		destination= new Vector3(
			2*UnityEngine.Random.Range(-attentionRadius,attentionRadius),
			2*UnityEngine.Random.Range(-attentionRadius,attentionRadius), 0f);
		walkDirection = (Vector2)(destination-transform.position).normalized;
	}

	public void GoToDoor(){
		destination = myRoom.ExitLocation;
		sanityCurrent=0;
		isHurt=true;
		isLeaving = true;
	}

	Vector2 flatvector(Vector3 v1, Vector3 v2){
		return new Vector2(v1.x-v2.x,v1.y-v2.y);
	}

	// update when sanity>0
	void UpdateStaying() {
		motionTimer += GameVars.Tick*Time.deltaTime;
		// the # of lights in the room determine the attention radius
		if (myRoom.lampsOn==2) { attentionRadius=RADIUS_LARGE; }
		else if (myRoom.lampsOn==1){ attentionRadius=RADIUS_MED;}
		else {attentionRadius = RADIUS_SMALL;}
		// recalculate defense
		defenseCurrent=defenseBase;
		foreach(Person roomie in roommates){
			if (flatvector(roomie.transform.position,transform.position).magnitude<attentionRadius){
				DefendOther(roomie);
			}
		}
		// recovering from an attack
		if (isHurt) {
			if (hurtTimer<hurtTimerMax) { hurtTimer += GameVars.Tick; }
			else { hurtTimer = 0f; isHurt=false; }
		}
		// has been assigned to turn on a lamp
		if (targetLamp!=null){
			if (targetLamp.IsOn) {
				// lamp is already on --> don't have to turn it on anymore
				targetLamp=null;
				StopWalking();
			}
			else if (flatvector(targetLamp.transform.position,transform.position).magnitude<LAMP_EPSILON){
				// close enough to the lamp --> turn on the lamp
				targetLamp.TurnOn();
				targetLamp=null;
				StopWalking();
			}
			else { // lamp is still off --> walk toward lamp
				rigidbody2D.velocity = flatvector(destination,transform.position).normalized*walkSpeed*Time.deltaTime;
			}
		}
		// otherwise it doesn't really matter where you end up
		else if (isWalking){ // walking --> wait?
			if (motionTimer<motionTimerMax)
				rigidbody2D.velocity=walkDirection*walkSpeed*Time.deltaTime;
			else
				StopWalking();
		}
		else // waiting --> walk?
			if (motionTimer>motionTimerMax){
				StartWalking();
			}
	}


	// 0=up, 1=down, 2=right, 3=left
	// use this to determine which sprite(s) to use
	public int GetFacingDirection()
	{
		if (Mathf.Abs(rigidbody2D.velocity.y)>Mathf.Abs(rigidbody2D.velocity.x)){
			if (rigidbody2D.velocity.y>0) return 0;
			else return 1;
		}
		else {
			if (rigidbody2D.velocity.x>0) return 2;
			else return 3;
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
			if (delta>=sanityCurrent){
				delta=sanityCurrent;
				GoToDoor();
			}
			sanityCurrent -= delta;
			leveling.AddExperience(delta);
		}
	}
}