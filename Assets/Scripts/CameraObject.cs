using UnityEngine;
using System.Collections;

public class CameraObject : MonoBehaviour {

	private float sizeSmall, sizeLarge;
	private Vector3 mapPosition;
	//private GameManager game;

	// Use this for initialization
	private void Start () {
		sizeSmall=9.6f; // for some reason I have to call this twice
		sizeLarge=90f;
		mapPosition = GameObject.Find("GameManager").transform.position;
		SetCameraSize(sizeSmall);
	}

	// Update is called once per frame
	private void Update () {
	}

	private void SetCameraSize(float newsize) {
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
