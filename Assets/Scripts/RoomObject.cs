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

	private ArrayList members = new ArrayList();
	private Vector2 spawnPoint1;

	[HideInInspector]
	public Vector3 CameraPosition;
	[HideInInspector]
	public Vector2 DoorLocation;
	// We will do the other 2 spawnpoints later

	static float Tick  = 5f; // tick = countdown rate
	static float CameraDistance = 10f;
	private float MaxStayDuration = 20f; // later on we can make this vary
	private float StayDuration;

	// Pretend that these are read-only
	public int RoomNumber;
	public bool IsVacant;

	/*======== FUNCTIONS ========*/

	public RoomObject(int roomNumber){
		this.RoomNumber = roomNumber;
	}

	// Use this for initialization
	void Start () {
	//	plevel = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLevel>();
		IsVacant = true;
		DoorLocation = (Vector2)transform.GetChild(0).position;
		spawnPoint1 = (Vector2)this.transform.position;
		CameraPosition = this.transform.position - new Vector3(0,0,CameraDistance);
	}

	void CheckIn(){
		// person object constructor gets 1. pointer to this, 2. a spawn location,
		// and 3. the value of the player's level.
		PersonObject p = new PersonObject(this, spawnPoint1);
		members.Add (p);
		IsVacant = false;
		StayDuration = MaxStayDuration;
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
	}
	
	// Update is called once per frame
	public void Update () {
		Debug.Log ("Updating room number "+RoomNumber);
		if (IsVacant) {
			;; // do something here when room is vacant
		}
		else if (StayDuration>0){
			// count down
			StayDuration -= Tick*Time.deltaTime;
			foreach (PersonObject m in members){
				if (m.Sanity<1){
					CheckOut();
				}
			}
		}
		else {
			// Stay duration has ended
			CheckOut();
		}
	}

}
