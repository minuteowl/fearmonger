using UnityEngine;
using System.Collections;
/* 
 * DESCRIPTION:
 * This class describes an invidual person in a room.
 */
public class PersonObject : MonoBehaviour {

	/*======== VARIABLES ========*/

	private int TempInt;

	int playerlevel;  // this is a pointer to the level object
	RoomObject myRoom;
	public int MaxSanity = 10;
	public Vector3 destination;

	LampObject lamp1, lamp2;
	float lamp_epsilon=1.6f;

	private float walkCountdownBase, walkCountdown;
	float sanityRegenCountdown, sanityRegenCountdownMax;
	Random rand;
	static float Tick;

	public bool isDead = false;
	bool isFleeing = false;

	float StepDistance; // how far player walks in each update

	public int currentSanity; // This is how you make a variable ead-only

	/*======== FUNCTIONS ========*/

	// Constructor is not the initializer; see Start() method
	public void AssignRoom(RoomObject r)
	{
		this.myRoom = r;
	}

	// Use this for initialization
	void Start () {
		sanityRegenCountdownMax = 30f;
		sanityRegenCountdown = sanityRegenCountdownMax;
		walkCountdownBase = 0.7f;
		walkCountdown=0;
		currentSanity = MaxSanity;
		rand = new Random();
		GameManager game = GameObject.Find("GameManager").GetComponent<GameManager>();
		Tick = game.Tick;
		lamp1=myRoom.lamp1.GetComponent<LampObject>();
		lamp2=myRoom.lamp2.GetComponent<LampObject>();
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
	}

	void UpdateAlive() {
		StepDistance = 1.2f*MaxSanity/(currentSanity+1);
		// Regen sanity
		if (!isFleeing && currentSanity<MaxSanity) {
			if (sanityRegenCountdown>0) {
				sanityRegenCountdown -= Tick*Time.deltaTime;
			}
			else {
				sanityRegenCountdown = sanityRegenCountdownMax;
				currentSanity++;
			}
		}
		// AI if still sane
		if (!isFleeing && myRoom.StayDuration>0 && currentSanity>0) {
			if (!lamp1.LightOn){
				destination = lamp1.transform.position;
				if ((lamp1.transform.position-transform.position).magnitude<lamp_epsilon){
					lamp1.TurnOn();
				}
			}
			else if (!lamp2.LightOn) {
				destination = lamp2.transform.position;
				if ((lamp2.transform.position-transform.position).magnitude<lamp_epsilon){
					lamp2.TurnOn();
				}
			}
			else if (walkCountdown>0) {
				walkCountdown -= Tick*Time.deltaTime;
			}
			else if (currentSanity>0) {
				walkCountdown = walkCountdownBase + Random.Range(0,currentSanity-1);
				destination = transform.position + Rand ();
			}
		}
		else if (!isFleeing) // zero sanity -> flee, exit through wall
		{
			isFleeing = true;
			currentSanity=0;
			myRoom.StayDuration=0;
			destination = myRoom.transform.FindChild("Exit").position;
			transform.rigidbody2D.Sleep();
			transform.collider2D.enabled=false;
		}

		if ((destination-transform.position).magnitude>0.005f) // Decide when to stop at a destination
		{
			transform.position += (destination-transform.position).normalized*StepDistance*Time.deltaTime;
		}
		else if (isFleeing && (destination-transform.position).magnitude<0.1f) // fleeing and reached destination
		{
			Leave ();
		}
	}

	Vector3 Rand()
	{
		return new Vector3(
			Random.Range(-3f,3f),
			Random.Range(-3f,3f));
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
			currentSanity = 0;
		}
		return delta;

	}

}
