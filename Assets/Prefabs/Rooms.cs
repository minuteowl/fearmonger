using UnityEngine;
using System.Collections;

public class Rooms : MonoBehaviour {


	PlayerLevel plevel;
	int level;
	float countdown;
	float Tick = 2f;
	int[] RoomNumbers = new int[16];

	// Use this for initialization
	void Start () {
		plevel = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLevel>();
		//plevel = transform.GetComponent<PlayerLevel>(); // If this class is attached to the player
		countdown = 3f; // Initial wait before first family
		for (int i=0; i<16; i++){
			RoomNumbers[i]=0;
		}

	}

	int GetVacantRoom(){
		// 1 = occupied, 0=vacant
		for (int i=0; i<16; i++){
			if (RoomNumbers[i]==0){
				return i;
			}
		}
		return -1;
	}


	public void FreeRoom(int n){
		Debug.Log("The ramily at room "+n+" has left.");
		RoomNumbers[n] =0;
	}

	// Update is called once per frame
	void Update () {
		level = plevel.Level;
		if (countdown < 0)
		{
			int i = GetVacantRoom();
			if (i != -1){
				// add new family
				Debug.Log ("A new family has arrived in room "+i);
				FamilyObject f = new FamilyObject();
				f.SetRoomNumber(i);
				f.SetRoomsObject(this);
				RoomNumbers[i] = 1; // set occupied
				countdown = 20f + 20f/level;
			}
			else {
				countdown = 4f + 20f/level;
			}
		}
		else {
			countdown -= Tick*Time.deltaTime;
		}


	}
}
