using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class RoomObject : MonoBehaviour {

	/*======== VARIABLES ========*/
	public string RoomName;
	Person[] occupants;
	[HideInInspector] public GameManager game; // accessible to occupants
	PlayerLevel playerLevel;

	// Physical
	[HideInInspector] public Vector3 CameraPosition, entryLoc; // Persons need to read this
	[HideInInspector] public Transform bed1, bed2, bed3, lamp1, lamp2;
	[HideInInspector] public Vector3 spawn1Pos, spawn2Pos, spawn3Pos,
		bed1StartPos, bed2StartPos, bed3StartPos,
		DoorLocation, lamp1StartPos, lamp2StartPos;
	// AI stuff

	public bool isOccupied=false, isUnlocked=false;
	public float stayTimer=0, stayTimerMax; // stay duration, max is reset randomly according to # of occupants
	public float vacantTimer=0, vacantTimerMax; // time in between vacant rooms, max is reset randomly
	
	[HideInInspector] public int lampsOn=2; // accessible to occupants
	public int numberOccupants=0;

	/*======== ROOM MANAGEMENT ========*/

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
		occupants = new Person[3];
		//memberObjects = new GameObject[3];
		//memberTypes= new GameObject[3];
		//memberTypes[0] = Resources.Load<GameObject>("Generated/People/Person1");
	}

	public void Unlock(float fInitial) {
		isUnlocked = true;
		vacantTimerMax = fInitial;
	}

	//Person[] GeneratePeople() {
	//}

	public void CheckIn(){
		// Prepare the room
		Debug.Log("Checked into room "+RoomName);
		ResetFurniture ();
		isOccupied = true;
		stayTimer = 0; vacantTimer = 0;
		stayTimerMax = UnityEngine.Random.Range(30f+numberOccupants*4,40f+numberOccupants*4);
		/*
		game.NumOccupiedRooms++;
		// Add people
		memberObjects[0] = Instantiate(memberTypes[0],spawn1Pos,Quaternion.identity) as GameObject;
		members[0] = memberObjects[0].GetComponent<Person>();
		members[0].SetRoom(this);
		numberOccupants=1;
		if (UnityEngine.Random.Range(0,playerLevel.level)>2) { // succeds at 3
			numberOccupants++;
			memberObjects[1] = Instantiate(memberTypes[0],spawn2Pos,Quaternion.identity) as GameObject;
			members[1] = memberObjects[1].GetComponent<Person>();
			members[1].SetRoom(this);
		}
		if (UnityEngine.Random.Range(0,playerLevel.level)>5) { // succeeds at 6
			numberOccupants++;
			memberObjects[2] = Instantiate(memberTypes[0],spawn3Pos,Quaternion.identity) as GameObject;
			members[2] = memberObjects[2].GetComponent<Person>();
			members[2].SetRoom(this);
		}
		Debug.Log("Checking in "+numberOccupants+" people into room "+RoomName);
		*/
	}

	public float GetDuration()
	{
		return stayTimerMax - stayTimer;
	}

	public void CheckOut(){
		// Each person will call this when they leave,
		// so make sure that it is only called once.
		numberOccupants=0;
		isOccupied=false;
		vacantTimer=0; stayTimer = 0;
		vacantTimerMax = UnityEngine.Random.Range(7f,12f); // delay to next check-in
		/*
		for (int i=members.Length-1; i>=0; i--){
			members[i]=null;
			memberObjects[i]=null;
		}
		*/
		game.NumOccupiedRooms--;
		Debug.Log("Checked out from room "+RoomName);
	}
	
	// Update is called once per frame
	public void Update () {
		// don't check out the room while the player is still looking at it
		if (isUnlocked) {
			if (isOccupied) {
				if (stayTimer<stayTimerMax) {
					stayTimer += GameVars.Tick*Time.deltaTime;
				}
				else {// if (game.currentRoom!=this){
					CheckOut();
				}
			}
			else { // vacant
				if (vacantTimer<vacantTimerMax) {
					vacantTimer += GameVars.Tick*Time.deltaTime;
				}
				else {
					CheckIn ();
				}
			}
		}
	}
}
