using UnityEngine;
using System.Collections;
using System;
/*
 * This class is both the "physical" room in the hotel and
 * also the "family" of the 1-3 people. In general, we will
 * only access people PersonObjects through this class.
 */
public class RoomObject : MonoBehaviour {

	/*======== VARIABLES ========*/

	public bool IsInFocus = false;
	public int numberOccupants;

	public int lampsOn;

	public Transform personPrefab1;

	public string RoomName;

	public PersonObject[] members;
	public GameManager game;

	public Vector3 spawnLoc1, spawnLoc2, spawnLoc3, bedLoc1, bedLoc2, bedLoc3,
		DoorLocation, lampLoc1, lampLoc2, entryLoc;
	public Transform bed1, bed2, bed3, lamp1, lamp2;

	static float Tick; // tick = countdown rate
	private float MaxStayDuration = 20f; // later on we can make this vary
	public float StayDuration;

	private float MaxDelayTime = 8f; // time in between empty rooms
	public float DelayTime;
	public bool Ready=true;
	public bool HasCheckedOut=false;

	public Vector3 CameraPosition;

	/*======== FUNCTIONS ========*/

	void ResetFurniture()
	{
		bed1.position = bedLoc1;
		bed2.position = bedLoc2;
		bed3.position = bedLoc3;
		lamp1.position = lampLoc1;
		lamp2.position = lampLoc2;
		lampsOn = 2;
	}
	
	// Use this for initialization
	void Start () {
		game=GameObject.Find("GameManager").GetComponent<GameManager>();
		StayDuration=0;
		DoorLocation = transform.FindChild ("Entry").position;
		spawnLoc1 = transform.FindChild("Spawn 1").position;
		spawnLoc2 = transform.FindChild("Spawn 2").position;
		spawnLoc3 = transform.FindChild("Spawn 3").position;
		bedLoc1 = bed1.position;
		bedLoc2 = bed2.position;
		bedLoc3 = bed3.position;
		entryLoc = transform.FindChild("Entry").position;
		lampLoc1 = lamp1.position;
		lampLoc2 = lamp2.position;
		CameraPosition = this.transform.FindChild("CameraPosition").position;
		RoomName = transform.name;
		lampsOn = 2;
		members = new PersonObject[3];
		if (RoomName.Equals("Room 101")){ // start game with a person in room 101
			CheckIn();
		}
	}

	public void CheckIn(){
		// Prepare the room
		Debug.Log("Checking in to room "+RoomName);
		ResetFurniture ();
		StayDuration = MaxStayDuration;
		game.NumOccupiedRooms++;

		// Add people
		numberOccupants = 1;
		GameObject t = Instantiate(personPrefab1,spawnLoc1,Quaternion.identity) as GameObject;
		PersonObject p = t.GetComponent<PersonObject>();
		p.AssignRoom(this);
		members[0]=p;

	}

	public int GetDuration()
	{
		return (int)Mathf.RoundToInt(StayDuration);
	}

	public void CheckOut(){
		// Each person will call this when they leave,
		// so make sure that it is only called once.
		if (numberOccupants>0) {
			numberOccupants=0;
			StayDuration = 0;
			DelayTime = MaxDelayTime;
			for (int i=members.Length-1; i>=0; i--){
				if (members[i]!=null) {
					members[i]=null;
				}
			}
			game.NumOccupiedRooms--;
			HasCheckedOut=true;
			Ready = false;
			Debug.Log("Checked out.");
		}
	}
	
	// Update is called once per frame
	public void Update () {
		if (game.currentRoom!=this && HasCheckedOut && numberOccupants==0) {
			if (DelayTime>0) {
				DelayTime -= game.Tick*Time.deltaTime;
			}
			else
			{
				Debug.Log("room "+RoomName+" is ready.");
				Ready=true;
				HasCheckedOut=false;
			}
		}

	}


}
