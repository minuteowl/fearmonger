﻿using UnityEngine;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {

	bool CursorMode=true;
	
	Transform targetTransform;

	// GLOBAL VARIABLES GO HERE:
	public enum View { Start, Game, Map, Menu, Stats}
	public View currentView;
	public int NumOccupiedRooms=0;
	public bool IsPaused=false;

	// INTERFACE LOGIC
	CameraObject cameraObject;
	Transform cameraMapPositionTransform;
	//public Cursor gameCursor;

	// ROOMS
	public RoomObject currentRoom, lastRoom;
	public int floorsUnlocked=1;
	Transform[,] squares;
	RoomObject[,] roomObjects;
	int[,] PeoplePerRoom;
	[HideInInspector] public int row=0, col=0;
	float upBound, leftBound, rightBound, downBound;

	public bool locked;
	
	float countdown, countdownMax=5f;

	// By convention, timers start at zero and increment to max
	// this counts the time, in seconds, between check-ins.
	// the timer max will change randomly
	public float checkInTimer=0, checkInTimerMax=5f;

	public void Pause(){
		Time.timeScale=0f;
		GameVars.IsPaused=true;
	}
	public void Unpause(){
		Time.timeScale=1f;
		GameVars.IsPaused=false;
	}

	void Start ()
	{
		
		countdown = countdownMax; // Initial countdown is shorter than others

		cameraObject = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<CameraObject>();
		squares = new Transform[4,4];
		roomObjects = new RoomObject[4,4];
		PeoplePerRoom = new int[4,4];
		for (int i=0; i<4; i++) {
			for (int j=0; j<4; j++) { // Get all of the rooms
				squares[i,j] = GameObject.Find("Room "+(i+1)+"0"+(j+1)).transform;
				roomObjects[i,j] = (RoomObject)squares[i,j].GetComponent<RoomObject>();
			}
		}
		// Initialize room 101
		currentRoom = roomObjects[0,0];
		roomObjects[0,0].Unlock(5f);
		roomObjects[0,1].Unlock(15f);
		roomObjects[0,2].Unlock(30f);
		roomObjects[0,3].Unlock(45f);
	}
	
	public void GoToRoom(RoomObject room)
	{
		if (room.isUnlocked) {
			Unpause();
			if (currentView==View.Map) {
				Debug.Log("Go to room "+room.RoomName);
				cameraObject.ZoomIn(room);
				//currentView = View.Game;
			}
		}
	}

	public void GoToMap() {
		Pause ();
		if (currentView==View.Game) {
			lastRoom = currentRoom;
			cameraObject = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<CameraObject>();
			currentView = View.Map;
			cameraObject.ZoomOut();
		}
	}

	void OnGUI(){
		if (currentView == View.Map) {
			//Floor 1
			if (GUI.Button (new Rect (Screen.width * .225f, Screen.height * .635f, Screen.width * .07f, Screen.height * .1f), "R101")) {
				buttonInteract(0,0);
			}	
			if (GUI.Button (new Rect (Screen.width * .32f, Screen.height * .635f, Screen.width * .07f, Screen.height * .1f), "R102")) {
				buttonInteract(0,1);
			}
			if (GUI.Button (new Rect (Screen.width * .413f, Screen.height * .635f, Screen.width * .07f, Screen.height * .1f), "R103")) {
				buttonInteract(0,2);
			}
			if (GUI.Button (new Rect (Screen.width * .508f, Screen.height * .635f, Screen.width * .07f, Screen.height * .1f), "R104")) {
				buttonInteract(0,3);
			}
			
			//Floor 2
			if (GUI.Button (new Rect (Screen.width * .225f, Screen.height * .47f, Screen.width * .07f, Screen.height * .1f), "R201")) {
				buttonInteract(1,0);
			}	
			if (GUI.Button (new Rect (Screen.width * .32f, Screen.height * .47f, Screen.width * .07f, Screen.height * .1f), "R202")) {
				buttonInteract(1,1);
			}
			if (GUI.Button (new Rect (Screen.width * .413f, Screen.height * .47f, Screen.width * .07f, Screen.height * .1f), "R203")) {
				buttonInteract(1,2);
			}
			if (GUI.Button (new Rect (Screen.width * .508f, Screen.height * .47f, Screen.width * .07f, Screen.height * .1f), "R204")) {
				buttonInteract(1,3);
			}
			
			//Floor 3
			if (GUI.Button (new Rect (Screen.width * .225f, Screen.height * .302f, Screen.width * .07f, Screen.height * .1f), "R301")) {
				buttonInteract(2,0);
			}	
			if (GUI.Button (new Rect (Screen.width * .32f, Screen.height * .302f, Screen.width * .07f, Screen.height * .1f), "R302")) {
				buttonInteract(2,1);
			}
			if (GUI.Button (new Rect (Screen.width * .413f, Screen.height * .302f, Screen.width * .07f, Screen.height * .1f), "R303")) {
				buttonInteract(2,2);
			}
			if (GUI.Button (new Rect (Screen.width * .508f, Screen.height * .302f, Screen.width * .07f, Screen.height * .1f), "R304")) {
				buttonInteract(2,3);
			}
			
			//Floor 4
			if (GUI.Button (new Rect (Screen.width * .225f, Screen.height * .135f, Screen.width * .07f, Screen.height * .1f), "R401")) {
				buttonInteract(3,0);
			}	
			if (GUI.Button (new Rect (Screen.width * .32f, Screen.height * .135f, Screen.width * .07f, Screen.height * .1f), "R402")) {
				buttonInteract(3,1);
			}
			if (GUI.Button (new Rect (Screen.width * .413f, Screen.height * .135f, Screen.width * .07f, Screen.height * .1f), "R403")) {
				buttonInteract(3,2);
			}
			if (GUI.Button (new Rect (Screen.width * .508f, Screen.height * .135f, Screen.width * .07f, Screen.height * .1f), "R404")) {
				buttonInteract(3,3);
			}
		}
	}

	void buttonInteract(int row, int column){
		//Statics.LockInput = true;
		currentRoom = roomObjects[row,column];
		Debug.Log("Going to room "+currentRoom.RoomName);
		//Statics.LockInput=false;
		GoToRoom(currentRoom);
		lastRoom = currentRoom;
		currentView = View.Game;
	}

	/*
	void MoveMarker()
	{
		GameVars.InputLock = true;
		currentRoom = roomObjects[row,col];
	}
	*/

	void UpdateInput() {

	}

	void Update() {
		if (NumOccupiedRooms<floorsUnlocked*4) {
			if (checkInTimer < checkInTimerMax) {
				checkInTimer += GameVars.Tick*Time.deltaTime;
			}
			else {
				// check into a new vacant room, if able room
				for (int i=0; i<floorsUnlocked; i++){
					for (int j=0; j<4; j++){
						if (!roomObjects[i,j].isOccupied)
						{
							roomObjects[i,j].CheckIn();
							i=5; j=5;
							checkInTimer =0;
							checkInTimerMax = Random.Range(20f,40f);
						}
					}
				}
			}
		}
		if (currentView==View.Map) {
			for (int i=0; i<4; i++)  for (int j=0; j<4; j++) {
				PeoplePerRoom[i,j] = roomObjects[i,j].numberOccupants;
			}
			if (!currentRoom) {
				currentRoom = roomObjects[row,col];
			}
			if (GameVars.InputLock) {/*
				if (GameInput.LeftOnce() && col>0) {
					col--; MoveMarker(); GameVars.InputLock=true;
				}
				else if (GameInput.RightOnce() && col<3) {
					col++; MoveMarker(); GameVars.InputLock=true;
				}
				else if (GameInput.UpOnce() && row<floorsUnlocked-1) {//row 0 to 3, floors 1 to 4
					row++; MoveMarker(); GameVars.InputLock=true;
				}
				else if (GameInput.DownOnce() && row>0){
					row--; MoveMarker(); GameVars.InputLock=true;
				}
				else if (GameInput.Action())
				{
					Debug.Log("Going to room "+currentRoom.RoomName);
					GoToRoom(currentRoom);
					lastRoom = currentRoom;
					GameVars.InputLock=false;
				}*/
			}
		}
		if (GameVars.InputLock) {
			GameVars.InputLock = false;
		}

	}

}