using UnityEngine;
using System.Collections;

public class CameraObject : MonoBehaviour {

	float sizeSmall, sizeLarge;
	Vector3 mapPosition;
	//GameManager game;

	// Use this for initialization
	void Start () {
		sizeSmall=9.6f; // for some reason I have to call this twice
		sizeLarge=90f;
		mapPosition = GameObject.Find("GameManager").transform.position;
		SetCameraSize(sizeSmall);
	}

	// Update is called once per frame
	void Update () {
	}

	void SetCameraSize(float newsize) {
		Debug.Log("set camera size.");
		Camera.main.orthographicSize = newsize;
	}

	public void ZoomIn(RoomObject room) {
		Debug.Log("zooming in to "+room.CameraPosition+" at room "+room.RoomName);
		transform.position = room.CameraPosition;
		SetCameraSize(sizeSmall);
	}

	public void ZoomOut() {
		transform.position = mapPosition;
		SetCameraSize(sizeLarge);
	}
}
