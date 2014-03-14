using UnityEngine;
using System.Collections;

public static class MouseInput {
	
	// Functions with input
	public static Vector3 To2D(Vector3 v, float z) {
		Camera cam = Camera.main;
		Vector3 u = cam.ScreenToWorldPoint(Input.mousePosition);
		return new Vector3(u.x,u.y,z);
	}

}
