using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Selector))]
public class PlayerActivity : MonoBehaviour {

	// Attached scripts
	private Selector grab;
	private Transform grabTransform;
	private float grabDistance;

	GameManager game = GameManager.Instance;


	// Motion
	private Vector3 facingDirection;
	public BoxCollider2D bodyCollider;
	private Vector3 xAxis = new Vector3(1,0,0);
	private Vector3 yAxis = new Vector3(0,1,0);
	public float Speed = 10f;
	public Vector3 FacingDirection {
		get { return facingDirection;}
	}

	public bool IsInvisible = true;


	// Use this for initialization
	void Start () {
		bodyCollider = (BoxCollider2D)transform.GetComponent("BoxCollider2D");
		facingDirection = yAxis;
		grabTransform = transform.FindChild("Selector");
		grab = grabTransform.GetComponent<Selector>();
		game.currentView = GameManager.View.Game;
		Debug.Log("Begin.");
		grabDistance = bodyCollider.size.x/2 + grab.box.size.x/2;
	}

	void EnterRoom()
	{
		IsInvisible = true;
		facingDirection = yAxis;
	}

	void ToggleInvisible()
	{
		IsInvisible = !IsInvisible;
	}

	void Move()
	{
		this.transform.position += facingDirection*Speed*Time.deltaTime;
	}

	void GetMovementInput() {
		if (PlayerInput.InputRight())
		{
			facingDirection = xAxis;
			Move ();
		}
		else if (PlayerInput.InputLeft())
		{
			facingDirection = -xAxis;
			Move ();
		}
		if (PlayerInput.InputUp())
		{
			facingDirection = yAxis;
			Move ();	
		}
		else if (PlayerInput.InputDown())
		{
			facingDirection = -yAxis;
			Move ();
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (game.currentView==GameManager.View.Game){
			grabTransform.position = transform.position + (grabDistance*facingDirection);
			if (PlayerInput.InputAction()) {
				if (grab.isHolding) {
					if (grab.currentFocus==Selector.FocusType.None) {
						grab.Drop();
					}
					else {
						Debug.Log("Can't drop that here.");
					}
				}
				else { // Is not holding anything
					if (grab.currentFocus==Selector.FocusType.Door) {
						Debug.Log("Exit room, go to map.");
						game.GoToMap();
					}
					else if (grab.currentFocus==Selector.FocusType.Movable) {
						Debug.Log ("Pick up object");
						grab.Pickup();
					}
					else if (grab.currentFocus==Selector.FocusType.Person) {
						Debug.Log("Activate Person");
					}
				}
			}
			else if (PlayerInput.InputInvisible()) {
				Debug.Log("toggle invisible");
				ToggleInvisible();
			}
			else if (PlayerInput.InputStatMenu()){
				Debug.Log("Open stats menu");
				//currentView=View.StatMenu;
			}
			else
			{
				GetMovementInput();
			}
		}
	}
	
}

