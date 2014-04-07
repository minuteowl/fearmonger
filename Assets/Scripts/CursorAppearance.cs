using UnityEngine;
using System.Collections;

// This code manages what the cursor looks like. NOT INPUT.
// Clicking ingame for abilities is in AbilityManager
public class CursorAppearance : MonoBehaviour {

	//Transform targetTransform;
	private Sprite[] cursorSprites;
	SpriteRenderer spriteRenderer;
	RaycastHit2D hit;
	Ray ray;
	Vector2 mouse2d;
	// sound effect
	public AudioClip mouseClickSound;

	// Use this for initialization
	void Start () {
		cursorSprites = Resources.LoadAll<Sprite>("Sprites/Cursors");
		spriteRenderer = transform.GetComponent<SpriteRenderer>();
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
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
		mouse2d = (Vector2)(ray.origin + ray.direction);
		hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		transform.position = new Vector3(mouse2d.x, mouse2d.y, GameVars.DepthCursor);
		if (hit && hit.collider.gameObject.CompareTag("Door")){
			spriteRenderer.sprite = cursorSprites[1];
		}
		else {
			spriteRenderer.sprite = cursorSprites[0];
		}

		if ((Input.GetMouseButtonDown(0) && hit.collider.gameObject.CompareTag ("Door")) ||
		     (Input.GetMouseButtonDown(0) && hit.collider.gameObject.CompareTag ("Lamp"))) {
			AudioSource.PlayClipAtPoint(mouseClickSound, transform.position);
		}
	}
}
