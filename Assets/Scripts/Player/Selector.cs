using UnityEngine;
using System.Collections;

public class Selector : MonoBehaviour {

	[HideInInspector] public CircleCollider2D circle;
	[HideInInspector] public Transform heldTransform, focusTransform;
	[HideInInspector] public MovableObject heldObject;
	[HideInInspector] public bool isHolding = false;
	float grabDistance;
	[HideInInspector] public enum FocusType {None, Movable, Person, Solid, Door};
	public FocusType currentFocus;
	[HideInInspector] public Vector3 zOffset;
	PlayerActivity player;
	float waitCountdown=0f;

	// Use this for initialization
	void Start () {
		circle = transform.GetComponent<CircleCollider2D>();// = (BoxCollider2D)transform.GetComponent("BoxCollider2D");
		player = transform.parent.GetComponent<PlayerActivity>();
		currentFocus = FocusType.None;
		heldObject = null;
		zOffset = new Vector3(0,0,transform.position.z+1); // counting up from negative
	}

	public void SetWait(float duration){
		waitCountdown = duration;
		transform.GetComponent<SpriteRenderer>().enabled=false;
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
			LampObject lamp = heldObject.transform.GetComponent<LampObject>();
			if (lamp){
				lamp.Switch();
			}
		}
	}

	void UpdateAppearance(){
		//transform.rotation = Quaternion.LookRotation(Vector3.back, player.FacingDirection);
		if (isHolding) {
			if (currentFocus==FocusType.None) {
				transform.GetComponent<SpriteRenderer>().color = Color.yellow;
			}
			else {
				transform.GetComponent<SpriteRenderer>().color = Color.red;
			}
		}
		else {
			if (currentFocus==FocusType.None) {
				transform.GetComponent<SpriteRenderer>().color = Color.white;
			}
			else if (currentFocus==FocusType.Solid) {
				transform.GetComponent<SpriteRenderer>().color = Color.grey;
			}
			else {
				transform.GetComponent<SpriteRenderer>().color = Color.green;
			}
		}
	}
		
		// Update is called once per frame
	void Update () {
		grabDistance = (transform.GetComponent<SpriteRenderer>().bounds.size.x+ player.bodyCollider.size.x*1.5f)/2;
		transform.position = player.transform.position + (grabDistance*player.FacingDirection) + zOffset;
		transform.rotation = Quaternion.LookRotation(Vector3.forward,player.FacingDirection);
		if (waitCountdown>0)
		{
			waitCountdown -= Statics.Tick*Time.deltaTime;
		}
		else if (!transform.GetComponent<SpriteRenderer>().enabled) {
			transform.GetComponent<SpriteRenderer>().enabled=true;
		}
		else 
		{
			UpdateAppearance();
			GetDetected();
			if (isHolding) {
				//heldTransform.rotation = transform.rotation;
				heldTransform.position = new Vector3 (transform.position.x,
				                                      transform.position.y, heldTransform.position.z);
			}
		}
	}

	/*
	void OnTriggerExit2D(Collider2D other) {
		Debug.Log("lost trigger");
		focusTransform = null;
		currentFocus = FocusType.None;
	}
	void OnTriggerStay2D(Collider2D other) {
		if (other.CompareTag("Person")) {
			Debug.Log("detect trigger: person");
			focusTransform = other.transform;
			currentFocus = FocusType.Person;
		}
		else if (other.CompareTag("Movable"))
		{
			Debug.Log("detect trigger: movable");
			focusTransform = other.transform;
			currentFocus = FocusType.Movable;
		}
		else if (other.CompareTag("Solid"))
		{
			Debug.Log("detect trigger: solid");
			focusTransform = other.transform;
			currentFocus = FocusType.Solid;
		}
		else if (other.CompareTag("Door"))
		{
			Debug.Log("detect trigger: door");
			focusTransform = other.transform;
			currentFocus = FocusType.Door;
		}
		else 
		{
			Debug.Log("lost trigger");
			focusTransform = null;
			currentFocus = FocusType.None;
		}
	}*/

	void GetDetected() {
		Collider2D temp;
		if (temp = Physics2D.OverlapCircle(transform.position,circle.radius,1 << LayerMask.NameToLayer("Person"))){
			focusTransform = temp.transform;
			currentFocus = FocusType.Person;
			return;
		}
		else if (temp = Physics2D.OverlapCircle(transform.position,circle.radius,1 << LayerMask.NameToLayer("Door"))){
			focusTransform = temp.transform;
			currentFocus = FocusType.Door;
			return;
		}
		else if (temp = Physics2D.OverlapCircle(transform.position,circle.radius,1 << LayerMask.NameToLayer("Solid"))){
			focusTransform = temp.transform;
			currentFocus = FocusType.Solid;
			return;
		}
		else if (isHolding) {
			focusTransform = null;
			currentFocus = FocusType.None;
		}
		else if (temp = Physics2D.OverlapCircle(transform.position,circle.radius,1 << LayerMask.NameToLayer("Movable"))){
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
