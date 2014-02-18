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

	public string RoomName;

	private ArrayList members = new ArrayList();
	[HideInInspector]
	public Vector3 spawnLoc1, spawnLoc2, spawnLoc3, bedLoc1, bedLoc2, bedLoc3,
		DoorLocation, lampLoc1, lampLoc2, entryLoc;
	public Transform bed1, bed2, bed3, lamp1, lamp2;

	static float Tick  = 5f; // tick = countdown rate
	private float MaxStayDuration = 20f; // later on we can make this vary
	private float StayDuration;

	// Pretend that these are read-only
	//public int RoomNumber;
	public bool IsVacant;
	public Vector3 CameraPosition;

	/*======== FUNCTIONS ========*/

	//public RoomObject(int roomNumber){
	//	this.RoomNumber = roomNumber;
	//}
	void ResetFurniture()
	{
		bed1.position = bedLoc1;
		bed2.position = bedLoc2;
		bed3.position = bedLoc3;
		lamp1.position = lampLoc1;
		lamp2.position = lampLoc2;
	}


	// Use this for initialization
	void Start () {
	//	plevel = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLevel>();
		IsVacant = true;
		DoorLocation = transform.FindChild ("Entry").position;
		spawnLoc1 = new Vector3(0,0,0);
		spawnLoc2 = new Vector3(2,3,0);
		spawnLoc3 = new Vector3(2,-3,0);
		spawnLoc1 = transform.FindChild("Spawn 1").position;
		spawnLoc2 = transform.FindChild("Spawn 2").position;
		spawnLoc3 = transform.FindChild("Spawn 3").position;
		bedLoc1 = bed1.position;
		bedLoc2 = bed2.position;
		bedLoc3 = bed3.position;
		entryLoc = transform.FindChild("Entry").position;
		//lampLoc1 = lamp1.position;
		//lampLoc2 = lamp2.position;
		CameraPosition = this.transform.FindChild("CameraPosition").position;
	}

	void CheckIn(){
		// person object constructor gets 1. pointer to this, 2. a spawn location,
		// and 3. the value of the player's level.
		PersonObject p = new PersonObject(this, spawnLoc1);
		members.Add (p);
		IsVacant = false;
		StayDuration = MaxStayDuration;
		numberOccupants = 1;
	}

	public int GetDuration()
	{
		return (int)Mathf.RoundToInt(StayDuration);
	}

	void CheckOut(){
		foreach (PersonObject m in members){
			members.Remove(m);
			m.Leave();
		}
		IsVacant = true;
		numberOccupants = 0;
		if (!IsInFocus) {
			ResetFurniture ();
		}
	}
	
	// Update is called once per frame
	public void Update () {
		//Debug.Log ("Updating room number "+RoomNumber);
		if (IsVacant) {
			;; // do something here when room is vacant
		}
		else if (StayDuration>0){
			// count down
			StayDuration -= Tick*Time.deltaTime;
			foreach (PersonObject m in members){
				if (!IsInFocus && m.Sanity<1){
					CheckOut();
				}
			}
		}
		else if (!IsInFocus){
			// Stay duration has ended
			CheckOut();
		}
	}


}
