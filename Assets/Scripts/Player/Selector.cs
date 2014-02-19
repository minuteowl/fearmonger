using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour {

	public BoxCollider2D box;

	public Transform heldTransform, focusTransform;
	public MovableObject heldObject;
	public bool isHolding = false;
	float radius;

	public enum FocusType {None, Movable, Person, Solid, Door};
	public FocusType currentFocus;

	// Use this for initialization
	void Start () {
		box = (BoxCollider2D)transform.GetComponent("BoxCollider2D");
		currentFocus = FocusType.None;
		heldObject = null;
		radius = box.size.x/2;
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
		GetDetected();
		UpdateAppearance();
		if (isHolding) {
			heldTransform.position = transform.position;
		}
		else 
		{


		}
	}

	void GetDetected() {
		Collider2D temp;
		if (temp = Physics2D.OverlapCircle(transform.position,radius,1 << LayerMask.NameToLayer("Person"))){
			focusTransform = temp.transform;
			currentFocus = FocusType.Person;
			return;
		}
		else if (temp = Physics2D.OverlapCircle(transform.position,radius,1 << LayerMask.NameToLayer("Door"))){
			focusTransform = temp.transform;
			currentFocus = FocusType.Door;
			return;
		}
		else if (temp = Physics2D.OverlapCircle(transform.position,radius,1 << LayerMask.NameToLayer("Solid"))){
			focusTransform = temp.transform;
			currentFocus = FocusType.Solid;
			return;
		}
		else if (isHolding) {
			focusTransform = null;
			currentFocus = FocusType.None;
		}
		else if (temp = Physics2D.OverlapCircle(transform.position,radius,1 << LayerMask.NameToLayer("Movable"))){
			focusTransform = temp.transform;
			currentFocus = FocusType.Movable;
			return;
		}
		else {
			focusTransform = null;
			currentFocus = FocusType.None;
		}


	}


}
