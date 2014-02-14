using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
public class GrabFocus : MonoBehaviour {

	public Transform focusArrow; // never change this
	public Vector3 facingDirection;
	PlayerMovement pmovement;

	// What the player is looking at, not what player is holding
	private const int NONE = 0;
	private const int MOVABLE = 1;
	private const int PERSON = 2;
	private const int SOLID = 3;
	public int focusInt;
	public Transform focusTransform;
	float focusDistance;
	float minDistance;

	public Transform heldTransform;
	MovableObject heldObject;
	public bool holding = false;
	


	// Use this for initialization
	void Start () {
		pmovement = transform.GetComponent<PlayerMovement>();
		focusInt = NONE;
		focusArrow = transform.FindChild("Focus");
		focusDistance = (focusArrow.position-transform.position).magnitude;
		BoxCollider2D bodyCollider;
		bodyCollider = (BoxCollider2D)this.transform.GetComponent("BoxCollider2D");
		minDistance = bodyCollider.size.x/2;
	}


	void Drop() {
		if (!heldObject.Intersect) {
			heldObject.IsBeingHeld = false;
			heldTransform = null;
			holding = false;
		}
	}

	void Pickup(Transform t) {
		heldTransform = t;
		heldObject = heldTransform.GetComponent<MovableObject>();
		if (heldObject.Weight>5) {
			heldTransform = null;
			holding = false;
		}
		else {
			holding = true;
			heldObject.IsBeingHeld = true;
		}
	}

	void UpdateAppearance(){
		switch (focusInt){
		case (MOVABLE):
			focusArrow.GetComponent<SpriteRenderer>().color = Color.red;
			break;
		case (PERSON):
			focusArrow.GetComponent<SpriteRenderer>().color = Color.green;
			break;
		default:
			focusArrow.GetComponent<SpriteRenderer>().color = Color.white;
			break;
		}
	}

	// Update is called once per frame
	void Update () {
		facingDirection = pmovement.FacingDirection;
		focusArrow.position = transform.position + focusDistance*facingDirection;
		focusInt = 0;
		focusTransform = GetFocusTransform();
		if (holding)
		{
			if (facingDirection.x==1) {
				heldTransform.position = transform.position + new Vector3(minDistance+heldObject.sizex,0);
			}
			else if (facingDirection.x==-1) {
				heldTransform.position = transform.position - new Vector3(minDistance+heldObject.sizex,0);
			}
			else if (facingDirection.y==1) {
				heldTransform.position = transform.position + new Vector3(0,minDistance+heldObject.sizey);
			}
			else {
				heldTransform.position = transform.position - new Vector3(0,minDistance+heldObject.sizey);
			}
			focusArrow.position = heldTransform.position;
			if (PlayerInput.InputGrab()){
				Drop();
			}
		}
		else if(focusInt==MOVABLE) {
			focusArrow.position = focusTransform.position;
			if (PlayerInput.InputGrab()){
				Pickup(focusTransform);
			}
		}
		else if(focusInt==PERSON) {
			focusArrow.position = focusTransform.position;
		}
		UpdateAppearance();
	}

	// Returns the movable object or person that the player is currently focusing on
	Transform GetFocusTransform(){
		RaycastHit2D hit = Physics2D.Linecast(transform.position, focusArrow.position, 1 << LayerMask.NameToLayer("Person"));
		if (hit)
		{
			focusInt = PERSON; 
			return hit.transform;
		}
		hit = Physics2D.Linecast(transform.position, focusArrow.position, 1 << LayerMask.NameToLayer("Solid"));
		if (hit)
		{
			focusInt = SOLID;
			return hit.transform;
		}
		if (!holding) {
			hit = Physics2D.Linecast(transform.position, focusArrow.position, 1 << LayerMask.NameToLayer("Movable"));
			if (hit)
			{
				focusInt = MOVABLE; 
				return hit.transform;
			}
			return null;
		}
		else {
			focusInt = NONE; 
			return (Transform)null;
		}
	}
}
