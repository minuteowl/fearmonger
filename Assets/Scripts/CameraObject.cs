using UnityEngine;
using System.Collections;

public class CameraObject : MonoBehaviour {

	public Transform MapPositionTransform;
	public float sizeSmall, sizeLarge;
	Vector3 mapPosition;
	Camera cam;
	GameManager game;
	public CameraObject reference;

	// Use this for initialization
	void Start () {
		game = GameObject.Find("GameManager").GetComponent<GameManager>();
		mapPosition = MapPositionTransform.position;
		cam = (Camera)transform.GetComponent("Camera");
		SetSize(sizeSmall);
		Debug.Log("Camera set up.");
	}

	// Update is called once per frame
	void Update () {
		reference = game.cameraObject;
	}

	public void Foo()
	{
	}

	void SetSize(float newsize) {
		cam.orthographicSize = newsize;
	}

	public void ZoomIn(RoomObject room) {
		Debug.Log("Zoom in"+room.transform.position);
		transform.position = room.transform.position+(new Vector3(0,0,-6));//room.transform.FindChild("CameraPosition").position;
		//room.IsInFocus = true;
		//game.currentRoom = room;
		SetSize(sizeSmall);
	}

	public void ZoomOut() {
		Debug.Log("Zoom out.");
		transform.position = mapPosition;
		game.currentRoom.IsInFocus = false;
		SetSize(sizeLarge);
	}
}
