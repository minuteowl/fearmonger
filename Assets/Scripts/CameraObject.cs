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
	}

	// Update is called once per frame
	void Update () {
		reference = game.cameraObject;
	}

	void SetSize(float newsize) {
		cam.orthographicSize = newsize;
	}

	public void ZoomIn(RoomObject room) {
		transform.position = room.CameraPosition;//transform.position+(new Vector3(0,0,-6));//room.transform.FindChild("CameraPosition").position;
		SetSize(sizeSmall);
	}

	public void ZoomOut() {
		transform.position = mapPosition;
		SetSize(sizeLarge);
	}
}
