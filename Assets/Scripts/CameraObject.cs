using UnityEngine;
using System.Collections;

public class CameraObject : MonoBehaviour {

	public Transform MapPositionTransform;
	public float sizeSmall, sizeLarge;
	Vector3 mapPosition;
	Camera cam;
	public RoomObject currentRoom;

	// Use this for initialization
	void Start () {
		mapPosition = MapPositionTransform.position;
		cam = (Camera)transform.GetComponent("Camera");
		currentRoom = GameObject.Find ("Room 101").GetComponent<RoomObject>();
		SetSize(sizeSmall);
	}

	// Update is called once per frame
	void Update () {
	
	}

	void SetSize(float newsize) {
		cam.orthographicSize = sizeSmall;
	}

	public void ZoomIn(RoomObject room) {
		Debug.Log("Zoom in");
		transform.position = room.transform.FindChild("CameraPosition").position;
		room.IsInFocus = true;
		currentRoom = room;
		SetSize(sizeSmall);
	}

	public void ZoomOut() {
		if (currentRoom) {
			Debug.Log("Zoom out.");
			transform.position = mapPosition;
			currentRoom.IsInFocus = false;
			currentRoom = null;
			SetSize(sizeLarge);
		}
	}
}
