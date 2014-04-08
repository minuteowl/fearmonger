using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class RoomObject : MonoBehaviour {

	/*======== VARIABLES ========*/
	private string roomName;
	public string RoomName { get {return ("Room "+roomName);}}
	public Person[] occupants;
	public List<Ability> ActiveAbilityEffects = new List<Ability>();
	[HideInInspector] public Game game; // accessible to occupants
	protected PlayerLevel playerLevel;
	
	// Physical
	[HideInInspector] public Vector3 CameraPosition, ExitLocation;//, EntryLocation; 
	private Vector3 bed1StartPos, bed2StartPos, bed3StartPos, lamp1StartPos, lamp2StartPos;
	private Transform bed1, bed2, bed3, lamp1, lamp2;
	public LampObject Lamp1, Lamp2;

	// AI stuff
	public AudioClip doorOpenSound, doorCloseSound;
	public bool isOccupied=false, isUnlocked=false;
	private float stayTimer=0, stayTimerMax; // stay duration, max is reset randomly according to # of occupants
	private float vacantTimer=0, vacantTimerMax; // time in between vacant rooms, max is reset randomly
	
	public int LightsOn, LampsOn=2; // accessible to lamps and occupants
	private int LightsOnMax;
	public int numberOccupants=0;
	private GameObject[] peoplePrefabTypes=new GameObject[7];
	private Vector3[] spawnPositions;

	public string MapName(){
		if (!isUnlocked){
			return "Locked";
		} else if (!isOccupied) {
			return "Vacant";
		} else {
			return roomName;
		}
	}

	/*======== ROOM MANAGEMENT ========*/

	private void ResetFurniture()
	{
		Debug.Log("Reset furniture in "+RoomName);
		bed1.position = bed1StartPos;
		bed2.position = bed2StartPos;
		bed3.position = bed3StartPos;
		lamp1.position = lamp1StartPos;
		lamp2.position = lamp2StartPos;
		LampsOn = 2;
	}
	
	// Use this for initialization
	private void Start () {
		game=GameObject.Find("GameManager").GetComponent<Game>();
		ExitLocation = transform.FindChild ("Door").position;
		spawnPositions = new Vector3[]{
			transform.FindChild("Spawn 1").position,
			transform.FindChild("Spawn 2").position,
			transform.FindChild("Spawn 3").position
		};
		// Normalize to proper Z-depth
		spawnPositions[0] = new Vector3(spawnPositions[0].x, spawnPositions[0].y, GameVars.DepthPeopleHazards);
		spawnPositions[1] = new Vector3(spawnPositions[1].x, spawnPositions[1].y, GameVars.DepthPeopleHazards);
		spawnPositions[2] = new Vector3(spawnPositions[2].x, spawnPositions[2].y, GameVars.DepthPeopleHazards);
		bed1 = transform.FindChild("Bed 1");
		bed2 = transform.FindChild("Bed 2");
		bed3 = transform.FindChild("Bed 3");
		lamp1 = transform.FindChild("Lamp 1");
		lamp2 = transform.FindChild("Lamp 2");
		Lamp1 = lamp1.GetComponent<LampObject>();
		Lamp2 = lamp2.GetComponent<LampObject>();
		bed1StartPos = bed1.position;
		bed2StartPos = bed2.position;
		bed3StartPos = bed3.position;
		//EntryLocation = transform.FindChild("Entry").position;
		lamp1StartPos = lamp1.position;
		lamp2StartPos = lamp2.position;
		CameraPosition = this.transform.FindChild("CameraPosition").position;
		roomName = transform.name.Split(' ')[1];
		//peoplePrefabTypes = Resources.LoadAll<GameObject>("Prefabs/Person");
		//print (peoplePrefabTypes.Length);
		peoplePrefabTypes[0]=Resources.Load<GameObject>("Prefabs/Person/bot1_ChildMale");
		peoplePrefabTypes[1]=Resources.Load<GameObject>("Prefabs/Person/bot2_ChildFemale");
		peoplePrefabTypes[2]=Resources.Load<GameObject>("Prefabs/Person/bot3_AdultMale");
		peoplePrefabTypes[3]=Resources.Load<GameObject>("Prefabs/Person/bot4_AdultFemale");
		peoplePrefabTypes[4]=Resources.Load<GameObject>("Prefabs/Person/bot5_CandleMale");
		peoplePrefabTypes[5]=Resources.Load<GameObject>("Prefabs/Person/bot6_CandleFemale");
		peoplePrefabTypes[6]=Resources.Load<GameObject>("Prefabs/Person/bot7_Priest");
	}

	private Transform[] getNewCombo() {
		int i = UnityEngine.Random.Range(0,100)%(PersonLists.Combinations.Count);
		int[] array = PersonLists.Combinations[i];
		Transform[] t = new Transform[array.Length];
		for(int j=0; j<array.Length; j++){
			if (array[j]>-1) {
				t[j] = peoplePrefabTypes[array[j]].transform;
			}
			else t[j]=null;
		}
		return t;
	}

	public void Unlock(float fInitial) {
		isUnlocked = true;
		vacantTimerMax = fInitial;
	}

	public void CheckIn(){
		// Prepare the room
		ResetFurniture ();
		isOccupied = true;
		stayTimer = 0; vacantTimer = 0;
		stayTimerMax = UnityEngine.Random.Range(30f+numberOccupants*4,40f+numberOccupants*4);
		Transform[] combo = getNewCombo();
		numberOccupants = combo.Length;
		Transform temp;
		occupants = new Person[numberOccupants];
		if (doorOpenSound!=null)
			AudioSource.PlayClipAtPoint (doorOpenSound, transform.position);
		LightsOnMax=2;
		for (int i=0; i<combo.Length; i++) {
			temp = (Transform)Instantiate(combo[i],spawnPositions[i],Quaternion.identity);
			occupants[i] = temp.GetComponent<Person>();
			if (occupants[i] is Person_Candle){
				LightsOnMax++;
			}
		}
		// this is called separately so occupants can set their roommates
		foreach (Person p in occupants){
			p.SetRoom(this);
		}
		game.NumOccupiedRooms++;
		//Debug.Log("Checking in "+numberOccupants+" people into room "+roomName);
	}

	public float GetDuration()
	{
		return stayTimerMax - stayTimer;
	}

	public void CheckOut(){
		isOccupied=false;
		vacantTimer=0; stayTimer = 0;
		vacantTimerMax = UnityEngine.Random.Range(6f,10f); // delay to next check-in
		for (int i=numberOccupants-1; i>=0; i--) {
			if (occupants[i]!=null) {
				occupants[i].Leave ();
			}
		}
		numberOccupants=0;
		game.NumOccupiedRooms--;
		Debug.Log("Checked out from room "+roomName);
		//if (doorCloseSound!=null)
		//	AudioSource.PlayClipAtPoint (doorCloseSound, transform.position);
	}

	public void TurnLightOn(){
		print ("Turning light on");
		if (LampsOn<LightsOnMax){
			LampsOn++;
			LightsOn++;
		}
	}

	public void TurnLightOff(){
		print ("Turning light off.");
		if (LampsOn>0){
			LightsOn--;
			LampsOn--;
		}
	}
	
	// Update is called once per frame
	private void Update () {
		// don't check out the room while the player is still looking at it
		if (isUnlocked) {
			if (isOccupied) {
				if (stayTimer<stayTimerMax) {
					stayTimer += GameVars.Tick*Time.deltaTime;
				}
				else if (numberOccupants>0){
					foreach(Person p in occupants){
						p.GoToDoor();
					}
				}
				else if (numberOccupants<1){// if (game.currentRoom!=this){
					CheckOut();
				}
			}
			else { // vacant
				if (vacantTimer<vacantTimerMax) {
					vacantTimer += GameVars.Tick*Time.deltaTime;
				}
				else {
					CheckIn ();
				}
			}
		}
	}
}
