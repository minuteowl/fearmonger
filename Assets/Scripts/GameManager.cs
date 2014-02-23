using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	// GLOBAL VARIABLES GO HERE:
	public enum View { Start, Game, Map, Stats}
	public View currentView;
	PlayerActivity player;
	public int NumOccupiedRooms=0;
	public bool IsPaused=false;

	// CAMERA LOGIC
	[HideInInspector] public CameraObject cameraObject;
	Transform cameraMapPositionTransform;

	// ROOMS
	public RoomObject currentRoom, lastRoom;
	Transform[,] squares;
	RoomObject[,] roomObjects;
	float selectedRoomMarkerZ;
	int[,] PeoplePerRoom;
	int i,j;
	[HideInInspector] public int row=0, col=0;
	float upBound, leftBound, rightBound, downBound;

	public bool locked;

	float countdown, countdownMax=5f;

	public void Pause(){
		Time.timeScale=0f;
		IsPaused=true;
	}
	public void Unpause(){
		Time.timeScale=1f;
		IsPaused=false;
	}

	void Start ()
	{
		countdown = countdownMax; // Initial countdown is shorter than others
		cameraObject = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<CameraObject>();
		player  = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActivity>();
		squares = new Transform[4,4];
		roomObjects = new RoomObject[4,4];
		PeoplePerRoom = new int[4,4];
		for (i=0; i<4; i++)  for (j=0; j<4; j++) {
			squares[i,j] = GameObject.Find("Room "+(i+1)+"0"+(j+1)).transform;
			roomObjects[i,j] = (RoomObject)squares[i,j].GetComponent<RoomObject>();
		}
		currentRoom = roomObjects[0,0];
		//GoToRoom(currentRoom);
	}
	
	public void GoToRoom(RoomObject room)
	{
		Unpause();
		if (currentView==View.Map) {
			Debug.Log("Go to room "+room.RoomName);
			player.FaceUp();
			player.MoveTo(room.entryLoc);
			//currentView = View.Game;
			cameraObject.ZoomIn(room);
		}
	}

	public void GoToMap() {
		Pause ();
		if (currentView==View.Game) {
			player.FaceUp();
			lastRoom = currentRoom;
			cameraObject = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<CameraObject>();
			currentView = View.Map;
			cameraObject.ZoomOut();
		}
	}

	void MoveMarker()
	{
		Statics.LockInput = true;
		currentRoom = roomObjects[row,col];
		player.transform.position = new Vector3(currentRoom.entryLoc.x,
		                                          currentRoom.entryLoc.y,
		                                          player.transform.position.z);
	}

	void UpdateInput() {

	}

	void Update() {
		if (!IsPaused && NumOccupiedRooms<4) {
			if (countdown > 0f) {
				countdown -= Statics.Tick*Time.deltaTime;
			}
			else {
				for (i=0;i<4;i++) for (j=0;j<4;j++) {
					if (roomObjects[i,j]!=currentRoom && roomObjects[i,j].numberOccupants==0 && roomObjects[i,j].Ready)
					{
						roomObjects[i,j].CheckIn();
						i=5; j=5;
						countdown = countdownMax;
					}
				}
			}
		}
		if (currentView==View.Map) {
			for (i=0; i<4; i++)  for (j=0; j<4; j++) {
				PeoplePerRoom[i,j] = roomObjects[i,j].numberOccupants;
			}
			if (!currentRoom) {
				currentRoom = roomObjects[row,col];
			}
			if (!Statics.LockInput) {
				if (PlayerInput.InputLeftOnce() && col>0) {
					col--; MoveMarker(); Statics.LockInput=true;
				}
				else if (PlayerInput.InputRightOnce() && col<3) {
					col++; MoveMarker(); Statics.LockInput=true;
				}
				else if (PlayerInput.InputUpOnce() && row<3) {
					row++; MoveMarker(); Statics.LockInput=true;
				}
				else if (PlayerInput.InputDownOnce() && row>0){
					row--; MoveMarker(); Statics.LockInput=true;
				}
				else if (PlayerInput.InputAction())
				{
					Debug.Log("Going to room "+currentRoom.RoomName);
					Statics.LockInput=false;
					GoToRoom(currentRoom);
					lastRoom = currentRoom;
					currentView = View.Game;
				}
			}
		}
		if (Statics.LockInput) {
			Statics.LockInput = false;
		}
		locked=Statics.LockInput;

	}

}