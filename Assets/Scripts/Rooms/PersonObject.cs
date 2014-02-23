using UnityEngine;
using System.Collections;
/* 
 * DESCRIPTION:
 * This class describes an invidual person in a room.
 */
public class PersonObject : MonoBehaviour {

	/*======== VARIABLES ========*/
	// Refs
	RoomObject myRoom;
	GameManager game;
	Transform playerTransform;
	PlayerActivity player;
	LampObject lamp1, lamp2;
	// Behavior
	float sightRadius;
	public bool isDead = false;
	public bool isFleeing = false;
	public int currentSanity;
	public int MaxSanity = 10;
	float lamp_epsilon=2f;
	// Movement
	Vector3 destination;
	Vector3 velocity;
	bool seesPlayer=false;
	float walkCountdownBase, walkCountdown;
	float sanityRegenCountdown, sanityRegenCountdownMax;
	float StepDistance;

	/*======== FUNCTIONS ========*/

	public void AssignRoom(RoomObject r)
	{
		this.myRoom = r;
	}

	// Use this for initialization
	void Start () {
		sanityRegenCountdownMax = 20f;
		sanityRegenCountdown = 0f;
		walkCountdownBase = 0.7f;
		walkCountdown=0;
		currentSanity = MaxSanity;
		lamp1=myRoom.lamp1.GetComponent<LampObject>();
		lamp2=myRoom.lamp2.GetComponent<LampObject>();
		playerTransform = GameObject.Find("Player").transform;
		player = playerTransform.GetComponent<PlayerActivity>();
	}

	void OnGUI(){
		GUI.Label(new Rect(transform.position.x-0.2f,transform.position.y+1,2,2),currentSanity+"/"+MaxSanity);

	}

	// Update is called once per frame
	void Update () {
		if (!isDead) {
			UpdateAlive();
		}
		else {
			Leave();
		}
		if (myRoom.lampsOn==2) {
			sightRadius = 8f;
		}
		else if (myRoom.lampsOn==1) {
			sightRadius = 4f;
		}
		else {
			sightRadius = 0.5f;
		}
		seesPlayer=false;
		if (myRoom==myRoom.game.currentRoom) {
			if ((transform.position - playerTransform.position).magnitude < sightRadius) {
				if (!player.IsInvisible || player.grab.isHolding) {
					seesPlayer=true;
				}
			}
		}
	}

	void UpdateAlive() {
		StepDistance = 1.2f*MaxSanity/(currentSanity+1);
		if (!isFleeing && currentSanity<MaxSanity) {
			StepDistance *= 1.05f;
			// Regen sanity
			if (sanityRegenCountdown>0) {
				sanityRegenCountdown -= Statics.Tick*Time.deltaTime;
			}
			else {
				sanityRegenCountdown = sanityRegenCountdownMax;
				currentSanity++;
			}
		}
		// AI if still sane
		if (!isFleeing && myRoom.StayDuration>0 && currentSanity>0) {
			if (!lamp1.LightOn){
				StepDistance *= 1.1f;
				Debug.Log("a light is off");
				destination = lamp1.transform.position;
				if ((lamp1.transform.position-transform.position).magnitude<lamp_epsilon){
					lamp1.TurnOn();
				}
			}
			else if (!lamp2.LightOn) {
				StepDistance *= 1.1f;
				destination = lamp2.transform.position;
				if ((lamp2.transform.position-transform.position).magnitude<lamp_epsilon){
					lamp2.TurnOn();
				}
			}
			else if (walkCountdown>0) {
				walkCountdown -= Statics.Tick*Time.deltaTime;
			}
			else if (currentSanity>0) {
				walkCountdown = walkCountdownBase + Random.Range(0,currentSanity-1);
				destination = transform.position + GetVelocity();
			}
		}
		else if (!isFleeing) // zero sanity -> flee, runs to door
		{
			StepDistance *= 1.5f;
			isFleeing = true;
			currentSanity=0;
			myRoom.StayDuration=0;
			destination = myRoom.transform.FindChild("Exit").position;
			destination = new Vector3(destination.x, destination.y, transform.position.z);
			transform.rigidbody2D.Sleep();
			transform.collider2D.enabled=false;
		}

		if ((destination-transform.position).magnitude>0.005f) // Decide when to stop at a destination
		{
			transform.position += (destination-transform.position).normalized*StepDistance*Time.deltaTime;
		}
		if (isFleeing && (destination-transform.position).magnitude<1.5f) // fleeing and reached destination
		{
			Leave ();
		}
	}

	Vector3 GetVelocity()
	{
		Vector3 direction = Vector3.zero;
		if (seesPlayer && currentSanity>0) {
			direction = (transform.position - playerTransform.position).normalized*(MaxSanity)/(currentSanity+7);
		}
		direction += (destination-transform.position).normalized;
		velocity= new Vector3( Random.Range(-3f,3f)+direction.x, Random.Range(-3f,3f)+direction.y,0);
		return velocity;
	}

	// 0=up, 1=right, 2=down, 3=left
	int FacingDir()
	{
		if (Mathf.Abs(velocity.x)>Mathf.Abs(velocity.y)){
			if (velocity.x>0) return 1;
			else return 3;
		}
		else {
			if (velocity.y>0) return 0;
			else return 2;
		}
	}

	public void Leave(){
		myRoom.CheckOut();
		Destroy(gameObject);
		Destroy(this);
	}

	public float PercentSanity()
	{
		return ((float)currentSanity)/MaxSanity;
	}

	public int DecreaseSanity(int delta)
	{
		currentSanity -= delta;
		if (currentSanity < 0){
			delta += currentSanity;
		}
		return delta;
	}
	
}
