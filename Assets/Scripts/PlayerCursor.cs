using UnityEngine;
using System.Collections;

// This code manages what the cursor looks like. NOT INPUT.
// Clicking ingame for abilities is in AbilityManager
public class PlayerCursor : MonoBehaviour {

	Transform targetTransform;
	//[HideInInspector] public enum FocusType {None, Movable, Person, Solid, Door};
	public Texture2D[] cursorTextures;
	//float zDepth= -15f;
	Vector2 clickPosition;
	// sound effect
	AudioClip mouseClickSound;

	// Use this for initialization
	void Start () {
		cursorTextures = Resources.LoadAll<Texture2D>("TEMP-cursorhand");
	}

	// An ability is selected and you click on some point in the room
	/*
	void ClickOnEmpty() {
		if (mouseClickSound)
			AudioSource.PlayClipAtPoint (mouseClickSound, transform.position); // play the mouseClickSound when clicked
	}

	// Clicked on a person
	void ClickOnPerson() {
		if (mouseClickSound)
			AudioSource.PlayClipAtPoint (mouseClickSound, transform.position); // play the mouseClickSound when clicked
	}


	// Based on what the mouse is hovering over, it changes appearance
	// we'll have preloaded sprites and then switch between them
	void UpdateAppearance(){
	}
	*/

	private void Update () {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
		Vector2 mouse2d = (Vector2)(ray.origin + ray.direction);
		transform.position = new Vector3(mouse2d.x, mouse2d.y, GameVars.DepthCursor);
	}
}
