using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	//RoomObject[] rooms;

	// Use this for initialization
	void Start () {
		//rooms = (RoomObject[])GameObject.FindObjectsOfType(typeof( RoomObject));
	}

	public void SwitchToRoom(RoomObject room) {
		this.transform.position = room.CameraPosition;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
