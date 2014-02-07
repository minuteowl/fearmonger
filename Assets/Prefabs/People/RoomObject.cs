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
	private Vector2 spawnPoint1 = Vector2.zero;
	public Vector2 DoorLocation = Vector2.zero;
	// We will do the other 2 spawnpoints later

	PlayerLevel plevel; // this is a pointer to the level object

	private const float Tick  = 5f; // tick = countdown rate
	private const float MaxStayDuration = 20f; // later on we can make this vary
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
		plevel = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLevel>();
		IsVacant = true;
	}

	int GetLevel()
	{
		return plevel.Level;
	}

	void CheckIn(){
		// person object constructor gets 1. pointer to this, 2. a spawn location,
		// and 3. the value of the player's level.
		PersonObject p = new PersonObject(this, spawnPoint1, plevel.Level);
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
	void Update () {
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
