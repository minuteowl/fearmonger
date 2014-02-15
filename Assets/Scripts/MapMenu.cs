using UnityEngine;
using System.Collections;
using System;

public class MapMenu: MonoBehaviour {
	Camera cam = Camera.main;
	RoomObject selectedRoom;
	Vector3 targetPosition;
	Vector3 MapPosition = new Vector3(45, 45, 0);
	int size = 60;
	int roomNum = 0;

	public void OpenMenu()
	{
		cam.transform.position = MapPosition;
		cam.orthographicSize = size;
	}

	void onGUI()
	{
				if (GUI.Button (new Rect (0, 0, 0, 20), "Room 101")) {
						roomNum = 101;
				}
				if (GUI.Button (new Rect (0, 0, 0, 20), "Room 102")) {
						roomNum = 102;
				}
				if (GUI.Button (new Rect (0, 0, 0, 20), "Room 103")) {
						roomNum = 103;
				}
				if (GUI.Button (new Rect (0, 0, 0, 20), "Room 104")) {
						roomNum = 104;
				}
				if (GUI.Button (new Rect (0, 0, 0, 20), "Room 201")) {
						roomNum = 201;
				}
				if (GUI.Button (new Rect (0, 0, 0, 20), "Room 202")) {
						roomNum = 202;
				}
				if (GUI.Button (new Rect (0, 0, 0, 20), "Room 203")) {
						roomNum = 203;
				}
				if (GUI.Button (new Rect (0, 0, 0, 20), "Room 204")) {
						roomNum = 204;
				}
				if (GUI.Button (new Rect (0, 0, 0, 20), "Room 301")) {
						roomNum = 301;
				}
				if (GUI.Button (new Rect (0, 0, 0, 20), "Room 302")) {
						roomNum = 302;
				}
				if (GUI.Button (new Rect (0, 0, 0, 20), "Room 303")) {
						roomNum = 303;
				}
				if (GUI.Button (new Rect (0, 0, 0, 20), "Room 304")) {
						roomNum = 304;
				}
				if (GUI.Button (new Rect (0, 0, 0, 20), "Room 401")) {
						roomNum = 401;
				}
				if (GUI.Button (new Rect (0, 0, 0, 20), "Room 402")) {
						roomNum = 402;
				}
				if (GUI.Button (new Rect (0, 0, 0, 20), "Room 403")) {
						roomNum = 403;
				}
				if (GUI.Button (new Rect (0, 0, 0, 20), "Room 404")) {
						roomNum = 404;
				}
		}
	/*public void FocusOnRoom(RoomObject r)
	{
		selectedRoom = r;
		targetPosition = selectedRoom.FindChild ("CameraPosition").transform.position;
		cam.transform.position = targetPosition;
	}*/
}

