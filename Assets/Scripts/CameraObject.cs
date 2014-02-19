using UnityEngine;
using System.Collections;

public class CameraObject : MonoBehaviour {

	public Transform MapPositionTransform;
	public float sizeSmall, sizeLarge;
	Vector3 mapPosition;
	Camera cam;
	GameManager game = GameManager.Instance;
	public CameraObject reference;

	// Use this for initialization
	void Start () {
		mapPosition = MapPositionTransform.position;
		cam = (Camera)transform.GetComponent(typeof(Camera));
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
		cam.orthographicSize = sizeSmall;
	}

	public void ZoomIn(RoomObject room) {
		Debug.Log("Zoom in");
		cam.transform.position = room.transform.FindChild("CameraPosition").position;
		room.IsInFocus = true;
		game.currentRoom = room;
		SetSize(sizeSmall);
	}

	public void ZoomOut() {
		Debug.Log("Zoom out.");
		cam.transform.position = mapPosition;
		game.currentRoom.IsInFocus = false;
		SetSize(sizeLarge);
	}
}
