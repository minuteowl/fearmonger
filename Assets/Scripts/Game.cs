using UnityEngine;
using System.Collections.Generic;

public class Game : MonoBehaviour {
	
	// GLOBAL VARIABLES GO HERE:
	public enum View { Start, Room, Map, Menu}
	public View currentView;
	public int NumOccupiedRooms=0;
	
	// ABILITY AND INPUT
	public Ability[] listAbilities;
	public Ability currentAbility;
	private Vector2 clickLocation2D;
	private RaycastHit2D hit;
	private Ray2D ray;
	private int selectedIndex;
	
	// INTERFACE LOGIC
	private CameraObject cameraObject;
	private Transform cameraMapPositionTransform;
	
	// we also need to keep track of this
	[HideInInspector] public PlayerLevel playerLevel;
	
	// ROOMS
	public RoomObject currentRoom, lastRoom;
	private int floorsUnlocked=1;
	private Transform[,] squares;
	private RoomObject[,] rooms;
	private int[,] PeoplePerRoom;
	private int row=0, col=0;
	private float upBound, leftBound, rightBound, downBound;
	
	// By convention, timers start at zero and increment to max
	// this counts the time, in seconds, between check-ins.
	// the timer max will change randomly
	private float checkInTimer=0, checkInTimerMax=5f;
	
	public void Pause(){
		Time.timeScale=0f;
		GameVars.IsPaused=true;
	}
	public void Unpause(){
		Time.timeScale=1f;
		GameVars.IsPaused=false;
	}
	
	public void unlockFloor()
	{
		if (floorsUnlocked < 4)
			floorsUnlocked++;
	}
	
	private void Start ()
	{
		listAbilities = new Ability[5];
		listAbilities[0] = new Ability_Spiders(); 
		listAbilities[1] = new Ability_Darkness();
		listAbilities[2] = new Ability_Claw();
		listAbilities[3] = new Ability_Monster();
		listAbilities[4] = new Ability_Possess();
		
		selectedIndex = 10;
		
		cameraObject = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<CameraObject>();
		playerLevel = transform.GetComponent<PlayerLevel>();
		if (playerLevel==null){
			throw(new MissingReferenceException());
		}
		squares = new Transform[4,4];
		rooms = new RoomObject[4,4];
		PeoplePerRoom = new int[4,4];
		for (int i=0; i<4; i++) {
			for (int j=0; j<4; j++) { // Get all of the rooms
				squares[i,j] = GameObject.Find("Room "+(i+1)+"0"+(j+1)).transform;
				rooms[i,j] = (RoomObject)squares[i,j].GetComponent<RoomObject>();
			}
		}
		currentRoom = rooms[0,0]; 		// Initialize room 101?
		rooms[0,0].Unlock(2f);
		rooms[0,1].Unlock(8f);
		rooms[0,2].Unlock(14f);
		rooms[0,3].Unlock(20f);
		currentView = View.Room;
	}
	public bool isAtMap(){
		if (currentView == View.Map) {
			return true;
		} else {
			return false;
		}
	}
	
	public void GoToRoom(RoomObject room)
	{
		if (room.isUnlocked) {
			Unpause();
			if (currentView==View.Map) {
				Debug.Log("Go to room "+room.RoomName);
				cameraObject.ZoomIn(room);
				currentView = View.Room;
			}
		}
		else {
			Debug.Log("Room "+room.RoomName+" is locked!");
		}
	}
	
	public void GoToRoom(int row, int column){
		currentRoom = rooms[row,column];
		GoToRoom(currentRoom);
	}
	
	public void GoToMap() {
		Debug.Log("Going to map");
		Pause ();
		if (currentView==View.Room) {
			lastRoom = currentRoom;
			cameraObject = GameObject.FindGameObjectWithTag("MainCamera").transform.GetComponent<CameraObject>();
			currentView = View.Map;
			cameraObject.ZoomOut();
		}
	}
	
	
	private void SelectAbility(int index){
		Debug.Log("Selected ability: "+listAbilities[index].Name);
		currentAbility = listAbilities[index];
		// other stuff
	}
	
	
	private void OnGUI(){
		// GAME MODE = LIST OF ABILITIES
		if (currentView == View.Room){
			if (GUI.Button (new Rect (1, 61, 115, 30), listAbilities [0].Name)) {
				if(listAbilities[0].Locked && listAbilities[0].BuyCost <= playerLevel.BuyPoints)
				{
					playerLevel.BuyAbility(listAbilities[0]);
					SelectAbility(0);
					selectedIndex = 0;
				}
				else if(listAbilities[0].Locked && listAbilities[0].BuyCost > playerLevel.BuyPoints)
				{
					Debug.Log ("Not Enough Points.");
				}
				else{
					SelectAbility(0);
					selectedIndex = 0;
				}
				
			}
			else if (GUI.Button (new Rect (1, 91, 115, 30), listAbilities [1].Name)) {
				if(listAbilities[1].Locked && listAbilities[1].BuyCost <= playerLevel.BuyPoints)
				{
					playerLevel.BuyAbility(listAbilities[1]);
					SelectAbility(1);
					selectedIndex = 1;
				}
				else if(listAbilities[1].Locked && listAbilities[1].BuyCost > playerLevel.BuyPoints)
				{
					Debug.Log ("Not Enough Points.");
				}
				else{
					SelectAbility(1);
					selectedIndex = 1;
				}
			}
			else if (GUI.Button (new Rect (1, 121, 115, 30), listAbilities [2].Name)) {
				if(listAbilities[2].Locked && listAbilities[2].BuyCost <= playerLevel.BuyPoints)
				{
					playerLevel.BuyAbility(listAbilities[2]);
					SelectAbility(2);
					selectedIndex = 2;
				}
				else if(listAbilities[2].Locked && listAbilities[2].BuyCost > playerLevel.BuyPoints)
				{
					Debug.Log ("Not Enough Points.");
				}
				else{
					SelectAbility(2);
					selectedIndex = 2;
				}
			}
			else if (GUI.Button (new Rect (1, 151, 115, 30), listAbilities [3].Name)) {
				if(listAbilities[3].Locked && listAbilities[3].BuyCost <= playerLevel.BuyPoints)
				{
					playerLevel.BuyAbility(listAbilities[3]);
					SelectAbility(3);
					selectedIndex = 3;
				}
				else if(listAbilities[3].Locked && listAbilities[3].BuyCost > playerLevel.BuyPoints)
				{
					Debug.Log ("Not Enough Points.");
				}
				else{
					SelectAbility(3);
					selectedIndex = 3;
				}
			}
			if (GUI.Button (new Rect (1, 181, 115, 30), listAbilities [4].Name)) {
				if(listAbilities[4].Locked && listAbilities[4].BuyCost <= playerLevel.BuyPoints)
				{
					playerLevel.BuyAbility(listAbilities[4]);
					SelectAbility(4);
					selectedIndex = 4;
				}
				else if(listAbilities[4].Locked && listAbilities[4].BuyCost > playerLevel.BuyPoints)
				{
					Debug.Log ("Not Enough Points.");
				}
				else{
					SelectAbility(4);
					selectedIndex = 4;
				}
			}
			for (int i=0; i<5 ; i++) {
				if (i==selectedIndex && selectedIndex != 10 && !listAbilities[i].Locked) {
					GUI.color = Color.cyan;
					GUI.Box(new Rect(115, (61 + (i * 30)), 20, 30),"E");
				}
				else if (listAbilities[i].Locked) {
					GUI.color = Color.red;
					GUI.Box(new Rect(115, (61 + (i * 30)), 20, 30), listAbilities[i].BuyCost.ToString());
				}
			}
		}
		// MAP = ROOM SELECTION
		else if (currentView == View.Map){
			//Floor 1
			if (GUI.Button (new Rect (Screen.width * .325f, Screen.height * .735f, Screen.width * .07f, Screen.height * .05f), rooms[0,0].MapName ())) {
				GoToRoom(0,0);
			}	
			if (GUI.Button (new Rect (Screen.width * .42f, Screen.height * .735f, Screen.width * .07f, Screen.height * .05f), rooms[0,1].MapName ())) {
				GoToRoom(0,1);
			}
			if (GUI.Button (new Rect (Screen.width * .513f, Screen.height * .735f, Screen.width * .07f, Screen.height * .05f), rooms[0,2].MapName ())) {
				GoToRoom(0,2);
			}
			if (GUI.Button (new Rect (Screen.width * .608f, Screen.height * .735f, Screen.width * .07f, Screen.height * .05f),  rooms[0,3].MapName ())) {
				GoToRoom(0,3);
			}
			//Floor 2
			if (GUI.Button (new Rect (Screen.width * .325f, Screen.height * .57f, Screen.width * .07f, Screen.height * .05f), rooms[1,0].MapName ())) {
				if(floorsUnlocked >= 2) GoToRoom(1,0);
			}	
			if (GUI.Button (new Rect (Screen.width * .42f, Screen.height * .57f, Screen.width * .07f, Screen.height * .05f), rooms[1,1].MapName ())) {
				if(floorsUnlocked >= 2) GoToRoom(1,1);
			}
			if (GUI.Button (new Rect (Screen.width * .513f, Screen.height * .57f, Screen.width * .07f, Screen.height * .05f), rooms[1,2].MapName ())) {
				if(floorsUnlocked >= 2) GoToRoom(1,2);
			}
			if (GUI.Button (new Rect (Screen.width * .608f, Screen.height * .57f, Screen.width * .07f, Screen.height * .05f), rooms[1,3].MapName ())) {
				if(floorsUnlocked >= 2) GoToRoom(1,3);
			}
			//Floor 3
			if (GUI.Button (new Rect (Screen.width * .325f, Screen.height * .402f, Screen.width * .07f, Screen.height * .05f), rooms[2,0].MapName ())) {
				if(floorsUnlocked >= 3) GoToRoom(2,0);
			}	
			if (GUI.Button (new Rect (Screen.width * .42f, Screen.height * .402f, Screen.width * .07f, Screen.height * .05f), rooms[2,1].MapName ())) {
				if(floorsUnlocked >= 3) GoToRoom(2,1);
			}
			if (GUI.Button (new Rect (Screen.width * .513f, Screen.height * .402f, Screen.width * .07f, Screen.height * .05f), rooms[2,2].MapName ())) {
				if(floorsUnlocked >= 3) GoToRoom(2,2);
			}
			if (GUI.Button (new Rect (Screen.width * .608f, Screen.height * .402f, Screen.width * .07f, Screen.height * .05f), rooms[2,3].MapName ())) {
				if(floorsUnlocked >= 3) GoToRoom(2,3);
			}
			//Floor 4
			if (GUI.Button (new Rect (Screen.width * .325f, Screen.height * .235f, Screen.width * .07f, Screen.height * .05f), rooms[3,0].MapName ())) {
				if(floorsUnlocked >=4) GoToRoom(3,0);
			}	
			if (GUI.Button (new Rect (Screen.width * .42f, Screen.height * .235f, Screen.width * .07f, Screen.height * .05f), rooms[3,1].MapName ())) {
				if(floorsUnlocked >=4) GoToRoom(3,1);
			}
			if (GUI.Button (new Rect (Screen.width * .513f, Screen.height * .235f, Screen.width * .07f, Screen.height * .05f), rooms[3,2].MapName ())) {
				if(floorsUnlocked >=4) GoToRoom(3,2);
			}
			if (GUI.Button (new Rect (Screen.width * .608f, Screen.height * .235f, Screen.width * .07f, Screen.height * .05f), rooms[3,3].MapName ())) {
				if(floorsUnlocked >=4) GoToRoom(3,3);
			}
		}
		
	}
	
	private void Update() {
		// UPDATE THE ROOMS AND PEOPLE
		for (int i=0; i<4; i++) for (int j=0; j<4; j++) {
			PeoplePerRoom[i,j] = rooms[i,j].numberOccupants;
		}
		if (!currentRoom) {
			currentRoom = rooms[row,col];
		}
		// CHECKING PEOPLE IN AND OUT
		if (NumOccupiedRooms<floorsUnlocked*4) {
			if (checkInTimer < checkInTimerMax) {
				checkInTimer += GameVars.Tick*Time.deltaTime;
			}
			else {
				// check into a new vacant room, if able room
				for (int i=0; i<floorsUnlocked; i++){
					for (int j=0; j<4; j++){
						if (!rooms[i,j].isOccupied)
						{
							rooms[i,j].CheckIn();
							i=5; j=5;
							checkInTimer =0;
							checkInTimerMax = Random.Range(20f,40f);
						}
					}
				}
			}
		}
		// REGISTER INPUT
		hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		Ray ray3d = Camera.main.ScreenPointToRay(Input.mousePosition);    
		clickLocation2D = (Vector2)(ray3d.origin + ray3d.direction);
		if (Input.GetMouseButtonDown (0)) {
			if (currentView == View.Room) {
				if (hit && hit.collider.gameObject.CompareTag("Door")){
					// clicked on the door -> go to the map
					GoToMap ();
				}
				else if (currentAbility!=null && hit){
					if (playerLevel.EnergyCurrent>=currentAbility.EnergyCost){
						if( !hit.collider.gameObject.CompareTag("Solid") && !hit.collider.gameObject.CompareTag("Background"))
							currentAbility.UseAbility(this, clickLocation2D);
					}
					else {
						Debug.Log ("Not enough energy!");
					}
				}
				else {
					Debug.Log ("No ability selected.");
				}
			}
		}
	}
}