using UnityEngine;
using System.Collections;

// Cursor logic
public class PlayerCursor : MonoBehaviour {

	Transform targetTransform;
	[HideInInspector] public enum FocusType {None, Movable, Person, Solid, Door};
	public Texture2D[] cursorTextures;
	float zDepth= -15f;

	PlayerLevel level;
	Ability currentAbility;

	// sound effect
	AudioClip mouseClickSound;

	// Use this for initialization
	void Start () {
		Texture2D[] t = Resources.LoadAll<Texture2D>("TEMP-cursorhand");
	}

	// An ability is selected and you click on some point in the room
	void ClickOnEmpty() {
		AudioSource.PlayClipAtPoint (mouseClickSound, transform.position); // play the mouseClickSound when clicked
	}

	// Clicked on a person
	void ClickOnPerson() {
		AudioSource.PlayClipAtPoint (mouseClickSound, transform.position); // play the mouseClickSound when clicked
	}

	// Based on what the mouse is hovering over, it changes appearance
	// we'll have preloaded sprites and then switch between them
	void UpdateAppearance(){

	}

	void GetClicked(){
		Ray ray = Camera.main.ScreenPointToRay(transform.position);
		RaycastHit hit;
		if (Physics.Raycast(ray,out hit)) {
			Debug.Log( hit.transform.gameObject.name );
		}
	}
	
	void Update () {
		transform.position = MouseInput.To2D(transform.position, zDepth);
		if (Input.GetMouseButtonDown(0)) {
			GetClicked();
		}
	}
/*
	void OnMouseEnter () {
		Cursor.SetCursor(cursorTextures[0], Vector2.zero, CursorMode.Auto);
	}

	void OnMouseExit () {
		Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
	}
*/
}
