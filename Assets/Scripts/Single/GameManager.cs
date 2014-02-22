using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	// GLOBAL VARIABLES GO HERE:
	public enum View { Start, Game, Map, Stats}
	public View currentView;
	PlayerActivity player;
	public int NumOccupiedRooms=0;

	// CAMERA LOGIC
	public CameraObject cameraObject;
	Camera mainCam;
	Transform cameraMapPositionTransform;
	bool JustPressed;

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

	public float countdown, countdownMax;
	public float Tick = 1f;

	// USING ABILITIES
	public List<Ability> listAbilities;
	//Ability currentAbility;

	public void SetUpAbilities()
	{
		if (listAbilities==null) {
			listAbilities = new List<Ability>();
			listAbilities.Add (new Ability("Tentacle","Extend a tentacle",1,1));
			listAbilities.Add (new Ability("Flash", "The lights flash", 1, 1));
			//currentAbility = listAbilities[0];
		}
	}

	void Start ()
	{
		countdownMax = 5f;
		countdown = countdownMax; // Initial countdown is shorter than others
		JustPressed = false;
		mainCam = Camera.main.GetComponent<Camera>();
		selectedRoomMarker = GameObject.Find("CurrentRoomMarker").transform;
		selectedRoomMarkerZ = selectedRoomMarker.position.z;
		cameraObject = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<CameraObject>();
		player  = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerActivity>();
		squares = new Transform[4,4];
		roomObjects = new RoomObject[4,4];
		PeoplePerRoom = new int[4,4];
		row = 0;
		col = 0;
		for (i=0; i<4; i++)  for (j=0; j<4; j++) {
			squares[i,j] = GameObject.Find("Room "+(i+1)+"0"+(j+1)).transform;
			roomObjects[i,j] = (RoomObject)squares[i,j].GetComponent<RoomObject>();
		}
		currentRoom = roomObjects[0,0];
		SetUpAbilities();
	}
	
	public void GoToRoom(RoomObject room)
	{
		if (currentView==View.Map) {
			player.FaceUp();
			player.transform.position = room.entryLoc;
			currentView = View.Game;
			cameraObject.ZoomIn(room);
		}
	}

	public void GoToMap() {
		if (currentView==View.Game) {
			Debug.Log("Go to map");
			player.FaceUp();
			lastRoom = currentRoom;
			cameraObject = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<CameraObject>();
			currentView = View.Map;
			cameraObject.ZoomOut();
			JustPressed = true;
		}
	}

	void MoveMarker()
	{
		currentRoom = roomObjects[row,col];
		selectedRoomMarker.position = new Vector3(currentRoom.transform.position.x,
		                                          currentRoom.transform.position.y,
		                                          selectedRoomMarkerZ);
	}

	void Update() {
		if (NumOccupiedRooms<4) {
			if (countdown > 0f) {
				countdown -= Tick*Time.deltaTime;
			}
			else {
				for (i=0;i<4;i++) for (j=0;j<4;j++) {
					if (roomObjects[i,j]!=currentRoom && roomObjects[i,j].numberOccupants==0 && roomObjects[i,j].Ready)
					{
						roomObjects[i,j].CheckIn();
						i=4; j=4;
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
			if (JustPressed) {
				JustPressed = false;
			}
			else if (!JustPressed && PlayerInput.InputAction())
			{
				GoToRoom(currentRoom);
				lastRoom = currentRoom;
				currentView = View.Game;
			}
		}
	}
}