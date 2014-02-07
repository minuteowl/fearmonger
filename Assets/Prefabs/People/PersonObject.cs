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
	RoomObject currentRoom;
	int MaxSanity = 10;
	Vector2 target;

	bool isDead = false;
	bool isLeaving = false;

	float StepDistance = 3f; // how far player walks in each update

	private int currentSanity; // This is how you make a variable ead-only
	public int Sanity { get {return currentSanity;} }

	/*======== FUNCTIONS ========*/

	// Constructor is not the initializer; see Start() method
	public PersonObject(RoomObject r, Vector2 pos2d, int plevel)
	{
		this.currentRoom = r;
		this.transform.position = pos2d;
		this.playerlevel = plevel;
	}

	// Use this for initialization
	void Start () {
		currentSanity = MaxSanity;
		// We will have a conditional statement here that takes the player's
		// level and translates it into this object's MaxSanity
	}

	// Update is called once per frame
	void Update () {
		// Later we will have the person move around the room
		if (isLeaving)
		{
			Vector2 toTarget = target - (Vector2)transform.position;
			transform.position += (Vector3)toTarget.normalized*StepDistance*Time.deltaTime;
		}
	}

	public void Leave(){
		if (isDead){
			// Play animation for dead character
		}
		else {
			// Play animation for leaving character
			isLeaving = true;
			target = currentRoom.DoorLocation;
		}
		Destroy(this);
	}

	public float PercentSanity()
	{
		return ((float)currentSanity)/MaxSanity;
	}

	public int DecreaseSanity(int delta)
	{
		if (currentSanity < delta){
			TempInt = currentSanity;
			currentSanity = 0;
			return TempInt;
		}
		else {
			currentSanity -= delta;
			return currentSanity;
		}
	}

	// This is a special condition, not sure if we will actually use this
	public void Kill()
	{
		isDead = true;
		currentSanity = 0;
	}

}
