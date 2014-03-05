using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	// GLOBAL VARIABLES GO HERE:
	public enum View { Start, Game, Map, Menu}
	public View currentView;
	public int NumOccupiedRooms=0;

	// CAMERA LOGIC
	CameraObject cameraObject;
	Transform cameraMapPositionTransform;

	// ROOMS
	public RoomObject currentRoom, lastRoom;
	int floorsUnlocked=1;
	Transform[,] squares;
	RoomObject[,] roomObjects;
	int[,] PeoplePerRoom;
	[HideInInspector] public int row=0, col=0;
	float upBound, leftBound, rightBound, downBound;

	// By convention, timers start at zero and increment to max
	// this counts the time, in seconds, between check-ins.
	// the timer max will change randomly
	float checkInTimer=0, checkInTimerMax=5f;

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
		currentRoom = roomObjects[0,0];
	}
	
	public void GoToRoom(RoomObject room)
	{
		Unpause();
		if (currentView==View.Map) {
			Debug.Log("Go to room "+room.RoomName);
			cameraObject.ZoomIn(room);
			currentView = View.Game;
		}
	}

	public void GoToMap() {
		Pause ();
		if (currentView==View.Game) {
			lastRoom = currentRoom;
			currentView = View.Map;
			cameraObject.ZoomOut();
		}
	}

	void MoveMarker()
	{
		GameVars.InputLock = true;
		currentRoom = roomObjects[row,col];
	}

	void UpdateInput() {

	}

	void Update() {
		if (GameVars.IsPaused && NumOccupiedRooms<floorsUnlocked*4) {
			if (checkInTimer < checkInTimerMax) {
				checkInTimer += GameVars.Tick*Time.deltaTime;
			}
			else {
				// check into a new vacant room, if able room
				for (int i=0; i<floorsUnlocked; i++){
					for (int j=0; j<4; j++){
						if (roomObjects[i,j]!=currentRoom && !roomObjects[i,j].isOccupied)
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
			if (GameVars.InputLock) {
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
				}
			}
		}
		if (GameVars.InputLock) {
			GameVars.InputLock = false;
		}

	}

}