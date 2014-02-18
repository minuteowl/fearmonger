using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour {

	public BoxCollider2D box;

	public Transform heldTransform, focusTransform;
	public MovableObject heldObject;
	public bool isHolding = false;

	public enum FocusType {None, Movable, Person, Solid, Door};
	public FocusType currentFocus;

	// Use this for initialization
	void Start () {
		box = (BoxCollider2D)transform.GetComponent("BoxCollider2D");
		currentFocus = FocusType.None;
		heldObject = null;
	}

	// This gets called externally when a menu opens up
	public void Drop() {
		if (heldTransform!=null){//heldObject!=null) {
			if (!heldObject.Intersect) {
				heldObject.IsBeingHeld = false;
				heldTransform = null;
				isHolding = false;
				currentFocus = FocusType.None;
			}

		}
	}
	
	public void Pickup() {
		heldTransform = focusTransform;
		heldObject = heldTransform.GetComponent<MovableObject>();
		if (heldObject.Weight>5) {
			heldTransform = null;
			isHolding = false;
		}
		else {
			isHolding = true;
			heldObject.IsBeingHeld = true;
		}
	}

	void UpdateAppearance(){
		if (currentFocus==FocusType.Movable) {
			transform.GetComponent<SpriteRenderer>().color = Color.red;
		}
		else if (currentFocus==FocusType.Person) {
			transform.GetComponent<SpriteRenderer>().color = Color.green;
		}
		else if (currentFocus==FocusType.Door) {
			transform.GetComponent<SpriteRenderer>().color = Color.cyan;
		}
		else {
			transform.GetComponent<SpriteRenderer>().color = Color.white;
		}
	}

	// Update is called once per frame
	void Update () {
		UpdateAppearance();
		if (isHolding) {
			heldTransform.position = transform.position;
		}
		else 
		{

		}
	}

	void OnTriggerExit2D(Collider2D other) {
		Debug.Log("None.");
		currentFocus = FocusType.None;
		focusTransform = null;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (!isHolding) {
			if (other.CompareTag("Door")) {
				if (currentFocus!=FocusType.Door ) {
					Debug.Log("door.");
					currentFocus = FocusType.Door;
				}
			}
			else if (other.CompareTag("Person")){
				if (currentFocus!=FocusType.Person) {
					Debug.Log("person.");
					currentFocus = FocusType.Person;
					focusTransform = other.transform;
				}
			}
			else if (other.CompareTag("Movable")) {
				if (currentFocus!=FocusType.Movable) {
					Debug.Log("Movable.");
					currentFocus = FocusType.Movable;
					focusTransform = other.transform;
				}
			}
			else if (other.CompareTag("Solid")) {
				if (currentFocus!=FocusType.Solid) {
					Debug.Log("Solid.");
					currentFocus = FocusType.Solid;
				}
			}
			else if (other==null) {
				Debug.Log("None.");
				currentFocus = FocusType.None;
				focusTransform = null;
			}
		}
	}


}
