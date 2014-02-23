using UnityEngine;
using System.Collections;
using System;

public class RoomObject : MonoBehaviour {

	/*======== VARIABLES ========*/
	public string RoomName;
	[HideInInspector] public PersonObject[] members;
	private GameObject[] memberObjects;
	GameObject[] memberTypes;
	[HideInInspector] public GameManager game;
	Leveling level;
	//public Transform personPrefab1;
	[HideInInspector] public Transform bed1, bed2, bed3, lamp1, lamp2;
	// Physical
	public Vector3 CameraPosition, entryLoc; // PersonObjects need to read this
	[HideInInspector] public Vector3 spawn1Pos, spawn2Pos, spawn3Pos,
		bed1StartPos, bed2StartPos, bed3StartPos,
		DoorLocation, lamp1StartPos, lamp2StartPos;
	// AI stuff
	float MaxStayDuration = 20f; // later on we can make this vary
	[HideInInspector] public float StayDuration=0;
	float MaxDelayTime = 8f; // time in between empty rooms
	float DelayTime=0;
	[HideInInspector] public bool Ready=true;
	bool HasCheckedOut=false;
	[HideInInspector] public int lampsOn=2;
	public int numberOccupants=0;

	/*======== FUNCTIONS ========*/

	void ResetFurniture()
	{
		bed1.position = bed1StartPos;
		bed2.position = bed2StartPos;
		bed3.position = bed3StartPos;
		lamp1.position = lamp1StartPos;
		lamp2.position = lamp2StartPos;
		lampsOn = 2;
	}
	
	// Use this for initialization
	void Start () {
		game=GameObject.Find("GameManager").GetComponent<GameManager>();
		level=GameObject.Find("Player").GetComponent<Leveling>();
		DoorLocation = transform.FindChild ("Entry").position;
		spawn1Pos = transform.FindChild("Spawn 1").position;
		spawn2Pos = transform.FindChild("Spawn 2").position;
		spawn3Pos = transform.FindChild("Spawn 3").position;
		bed1 = transform.FindChild("Bed 1");
		bed2 = transform.FindChild("Bed 2");
		bed3 = transform.FindChild("Bed 3");
		lamp1 = transform.FindChild("Lamp 1");
		lamp2 = transform.FindChild("Lamp 2");
		bed1StartPos = bed1.position;
		bed2StartPos = bed2.position;
		bed3StartPos = bed3.position;
		entryLoc = transform.FindChild("Entry").position;
		lamp1StartPos = lamp1.position;
		lamp2StartPos = lamp2.position;
		CameraPosition = this.transform.FindChild("CameraPosition").position;
		RoomName = transform.name;
		members = new PersonObject[3];
		memberObjects = new GameObject[3];
		memberTypes= new GameObject[3];
		memberTypes[0] = Resources.Load<GameObject>("Generated/People/Person1");
		if (RoomName.Equals("Room 101")){ // start game with a person in room 101
			CheckIn();
		}

	}

	public void CheckIn(){
		// Prepare the room
		ResetFurniture ();
		StayDuration = MaxStayDuration;
		game.NumOccupiedRooms++;
		// Add people
		memberObjects[0] = Instantiate(memberTypes[0],spawn1Pos,Quaternion.identity) as GameObject;
		members[0] = memberObjects[0].GetComponent<PersonObject>();
		members[0].AssignRoom(this);
		numberOccupants=1;
		if (UnityEngine.Random.Range(0,level.Level)>2) { // succeds at 3
			numberOccupants++;
			memberObjects[1] = Instantiate(memberTypes[0],spawn2Pos,Quaternion.identity) as GameObject;
			members[1] = memberObjects[1].GetComponent<PersonObject>();
			members[1].AssignRoom(this);
		}
		if (UnityEngine.Random.Range(0,level.Level)>5) { // succeeds at 6
			numberOccupants++;
			memberObjects[2] = Instantiate(memberTypes[0],spawn3Pos,Quaternion.identity) as GameObject;
			members[2] = memberObjects[2].GetComponent<PersonObject>();
			members[2].AssignRoom(this);
		}
		Debug.Log("Checking in "+numberOccupants+" people into room "+RoomName);

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
				members[i]=null;
				memberObjects[i]=null;
			}
			game.NumOccupiedRooms--;
			HasCheckedOut=true;
			Ready = false;
			Debug.Log("Checked out from room "+RoomName);
		}
	}
	
	// Update is called once per frame
	public void Update () {
		if (game.currentRoom!=this && HasCheckedOut && numberOccupants==0) {
			if (DelayTime>0) {
				DelayTime -= Statics.Tick*Time.deltaTime;
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
