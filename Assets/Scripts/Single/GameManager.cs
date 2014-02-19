﻿using UnityEngine;

// Source: http://wiki.unity3d.com/index.php/Singleton
// Content Available under http://creativecommons.org/licenses/by-sa/3.0/
public class GameManager : MonoBehaviour {

	// GLOBAL VARIABLES GO HERE:
	public enum View { Start, Game, Map, Stats}
	public View currentView;
	//Transform playerTransform;
	PlayerActivity player;

	public CameraObject cameraObject;
	Camera mainCam;
	Transform cameraMapPositionTransform;

	// ROOMS
	public RoomObject currentRoom, lastRoom;
	static Transform[,] squares;
	static RoomObject[,] roomObjects;
	static Transform selectedRoomMarker;
	static float selectedRoomMarkerZ;
	static int[,] PeoplePerRoom;
	int i,j;
	public int row, col;
	float upBound, leftBound, rightBound, downBound;
	
	void Awake ()
	{
		mainCam = Camera.main.GetComponent<Camera>();//GameObject.Find("MainCamera").GetComponent<Camera>();
		//mainCam = Camera.main;
		selectedRoomMarker = GameObject.Find("CurrentRoomMarker").transform;
		selectedRoomMarkerZ = selectedRoomMarker.position.z;
		cameraObject = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<CameraObject>();
			//7mainCam.transform.GetComponent<CameraObject>();
		player  = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActivity>();
		squares = new Transform[4,4];
		roomObjects = new RoomObject[4,4];
		PeoplePerRoom = new int[4,4];
		squares[0,0] = GameObject.Find("Room 101").transform;
		squares[0,1] = GameObject.Find("Room 102").transform;
		squares[0,2] = GameObject.Find("Room 103").transform;
		squares[0,3] = GameObject.Find("Room 104").transform;
		squares[1,0] = GameObject.Find("Room 201").transform;
		squares[1,1] = GameObject.Find("Room 202").transform;
		squares[1,2] = GameObject.Find("Room 203").transform;
		squares[1,3] = GameObject.Find("Room 204").transform;
		squares[2,0] = GameObject.Find("Room 301").transform;
		squares[2,1] = GameObject.Find("Room 302").transform;
		squares[2,2] = GameObject.Find("Room 303").transform;
		squares[2,3] = GameObject.Find("Room 304").transform;
		squares[3,0] = GameObject.Find("Room 401").transform;
		squares[3,1] = GameObject.Find("Room 402").transform;
		squares[3,2] = GameObject.Find("Room 403").transform;
		squares[3,3] = GameObject.Find("Room 404").transform;
		row = 0;
		col = 0;

		for (i=0; i<4; i++) {
			for (j=0; j<4; j++) {
				roomObjects[i,j] = (RoomObject)squares[i,j].GetComponent<RoomObject>();
			}
		}
		currentRoom = roomObjects[0,0];
		if (!currentRoom) Debug.Log("current room missing");
	}
	
	public void GoToRoom(RoomObject room)
	{
		player.FaceUp();
		Debug.Log("going to room");
		player.transform.position = room.entryLoc;
		currentView = View.Game;
		cameraObject.ZoomIn(room);
	}

	public void GoToMap() {
		Debug.Log("Go to map");
		lastRoom = currentRoom;
		cameraObject = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<CameraObject>();
		cameraObject.Foo();
		//cameraObject.Update();
		currentView = View.Map;
		cameraObject.ZoomOut();
	}

	void MoveMarker()
	{
		currentRoom = roomObjects[row,col];
		selectedRoomMarker.position = new Vector3(currentRoom.transform.position.x,
		                                          currentRoom.transform.position.y,
		                                          selectedRoomMarkerZ);
	}

	void Update() {

		if (currentView==View.Map) {
			for (i=0; i<4; i++) {
				for (j=0; j<4; j++) {
					PeoplePerRoom[i,j] = roomObjects[i,j].numberOccupants;
				}
			}
			if (!currentRoom) {
				currentRoom = roomObjects[row,col];
			}

			if (PlayerInput.InputLeftOnce() && col>0) {
				col--; MoveMarker();
			}
			else if (PlayerInput.InputRightOnce() && col<3) {
				col++; MoveMarker();
			}
			else if (PlayerInput.InputUpOnce() && row<3) {
				row++; MoveMarker();
			}
			else if (PlayerInput.InputDownOnce() && row>0){
				row--; MoveMarker();
			}
			else if (PlayerInput.InputAction())
			{
				Debug.Log("input player action");
				GoToRoom(currentRoom);
				//cameraObject.ZoomIn(currentRoom);
				lastRoom = currentRoom;
				currentView = View.Game;
			}
		}
		
	}

}