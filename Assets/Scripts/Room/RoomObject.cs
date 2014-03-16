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
	[HideInInspector] public Vector3
		bed1StartPos, bed2StartPos, bed3StartPos,
		DoorLocation, lamp1StartPos, lamp2StartPos;
	// AI stuff

	public bool isOccupied=false, isUnlocked=false;
	public float stayTimer=0, stayTimerMax; // stay duration, max is reset randomly according to # of occupants
	public float vacantTimer=0, vacantTimerMax; // time in between vacant rooms, max is reset randomly
	
	[HideInInspector] public int lampsOn=2; // accessible to occupants
	public int numberOccupants=0;
	public Transform[] peoplePrefabTypes;
	Vector3[] spawnPositions;
	public Transform prefab_child_m, prefab_child_f,
		prefab_adult_m, prefab_adult_f, prefab_candle_m,
		prefab_candle_f, prefab_priest;

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
		spawnPositions = new Vector3[]{
			transform.FindChild("Spawn 1").position,
			transform.FindChild("Spawn 2").position,
			transform.FindChild("Spawn 3").position
		};
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
		peoplePrefabTypes = new Transform[]{prefab_child_m,prefab_child_f,prefab_adult_m,prefab_adult_f,prefab_candle_m,prefab_candle_f,prefab_priest};
	}

	Transform[] getNewCombo() {
		int i = UnityEngine.Random.Range(0,100)%(PersonLists.Combinations.Count);
		print ("Picked "+i+" of "+PersonLists.Combinations.Count+" combinations.");
		int[] array = PersonLists.Combinations[i];
		Transform[] t = new Transform[array.Length];
		for(int j=0; j<array.Length; j++){
			if (array[j]>-1) {
				t[j] = peoplePrefabTypes[array[j]];
			}
			else t[j]=null;
		}
		return t;
	}

	public void Unlock(float fInitial) {
		isUnlocked = true;
		vacantTimerMax = fInitial;
	}

	public void CheckIn(){
		// Prepare the room
		ResetFurniture ();
		isOccupied = true;
		stayTimer = 0; vacantTimer = 0;
		stayTimerMax = 20f;//UnityEngine.Random.Range(30f+numberOccupants*4,40f+numberOccupants*4);
		Transform[] combo = getNewCombo();
		numberOccupants = combo.Length;
		Transform temp;
		occupants = new Person[numberOccupants];
		for (int i=0; i<combo.Length; i++) {
			temp = (Transform)Instantiate(combo[i],spawnPositions[i],Quaternion.identity);
			occupants[i] = temp.GetComponent<Person>();
			occupants[i].SetRoom(this);
		}
		game.NumOccupiedRooms++;
		Debug.Log("Checking in "+numberOccupants+" people into room "+RoomName);
	}

	public float GetDuration()
	{
		return stayTimerMax - stayTimer;
	}

	public void CheckOut(){
		// Each person will call this when they leave,
		// so make sure that it is only called once.
		isOccupied=false;
		vacantTimer=0; stayTimer = 0;
		vacantTimerMax = 2f;//UnityEngine.Random.Range(7f,12f); // delay to next check-in
		for (int i=numberOccupants-1; i>=0; i--) {
			if (occupants[i]!=null) {
				occupants[i].Leave ();
			}
		}
		numberOccupants=0;
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
