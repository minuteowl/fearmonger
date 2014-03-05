using UnityEngine;
using System.Collections;

// Cursor logic
public class Selector : MonoBehaviour {

	Transform targetTransform;
	[HideInInspector] public enum FocusType {None, Movable, Person, Solid, Door};
	PlayerLevel level;
	Ability currentAbility;

	// Use this for initialization
	void Start () {
	}

	void ClickOnEmpty() {
	}

	void ClickOnPerson() {
	}

	void UpdateAppearance(){

	}

	void Update () {

	}


}
