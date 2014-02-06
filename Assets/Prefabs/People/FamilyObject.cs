using UnityEngine;
using System.Collections;
using System;

public class FamilyObject : MonoBehaviour {

	ArrayList members = new ArrayList();
	public float StayDuration;
	PlayerLevel plevel;
	public float Tick;
	public int RoomNumber = 0;
	Rooms rooms;

	public void SetRoomsObject(Rooms r){
		rooms = r;
	}

	// Use this for initialization
	void Start () {
		plevel = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLevel>();
		PersonObject p = new PersonObject();
		System.Random r = new System.Random();
		members.Add(p); // A family must have at least one member
		int n = r.Next(1,10);
		if ((plevel.Level>5 && n>3) || (plevel.Level>3 && n>5) || (plevel.Level>1 && n>7)){
			p = new PersonObject();
			members.Add(p); // Chance to have at least 2 members
		}
		n = r.Next(1,10);
		if ((plevel.Level>7 && n>3) || (plevel.Level>5 && n>5) || (plevel.Level>3 && n>7)){
			p = new PersonObject();
			members.Add(p); // Chance to have 3 members
		} 
		Tick = 5f;
		StayDuration = 20f;
	}

	public void SetRoomNumber(int i){
		RoomNumber = i;
		foreach (PersonObject m in members){
			m.RoomNumber = i;
		}
	}
	
	// Update is called once per frame
	void Update () {
		foreach (PersonObject m in members){
			if (m.Sanity<1){
				Leave ();
			}
		}
		if (StayDuration>0){
			StayDuration -= Tick*Time.deltaTime;
		}
		else {
			Leave ();
		}
	}

	void Leave()
	{
		foreach (PersonObject m in members){
			m.Leave();
		}
		rooms.FreeRoom(RoomNumber);
		Destroy(this);
	}
}
