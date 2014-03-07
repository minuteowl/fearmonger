using UnityEngine;
using System.Collections;

// Cursor logic
public class Selector : MonoBehaviour {

	bool CursorMode=true;

	Transform targetTransform;
	[HideInInspector] public enum FocusType {None, Movable, Person, Solid, Door};
	PlayerLevel level;
	Ability currentAbility;

	// Use this for initialization
	void Start () {
	}

	// An ability is selected and you click on some point in the room
	void ClickOnEmpty() {
	}

	// Clicked on a person
	void ClickOnPerson() {
	}

	// Based on what the mouse is hovering over, it changes appearance
	// we'll have preloaded sprites and then switch between them
	void UpdateAppearance(){

	}
	
	void Update () {
		if (CursorMode) {
			if (GameInput.IsInViewport(GameInput.Mouse2D(Vector3.zero)))
				transform.position = GameInput.Mouse2D(transform.position);
		}
	}


}
