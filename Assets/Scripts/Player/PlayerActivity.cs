using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Selector))]
public class PlayerActivity : MonoBehaviour {

	// Attached scripts
	private Selector grab;
	private Transform grabTransform;
	private float grabDistance;
	public Vector3 zDistanceVector = Vector3.zero;//new Vector3(0,0,-2);

	GameManager game;
	PlayerLevel leveling;
	Ability currentAbility;
	public bool UsingAbility=false;


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


	public void FaceUp()
	{
		facingDirection = new Vector3(0,1,0);
	}

	// Use this for initialization
	void Start () {
		bodyCollider = (BoxCollider2D)transform.GetComponent("BoxCollider2D");
		facingDirection = yAxis;
		grabTransform = transform.FindChild("Selector");
		grab = grabTransform.GetComponent<Selector>();
		leveling = GameObject.Find("GameManager").GetComponent<PlayerLevel>();
		game = GameObject.Find("GameManager").GetComponent<GameManager>();
		game.currentView = GameManager.View.Game;
		game.SetUpAbilities();
		grabDistance = bodyCollider.size.x/2 + grab.box.size.x/2;
		currentAbility = game.listAbilities[0];
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
			grabTransform.position = transform.position + (grabDistance*facingDirection) + zDistanceVector;
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
						game.GoToMap();
					}
					else if (grab.currentFocus==Selector.FocusType.Movable) {
						grab.Pickup();
					}
					else if (!UsingAbility && grab.currentFocus==Selector.FocusType.Person) {
						UsingAbility=true; // make sure that the ability is only called once at a time
						PersonObject p = grab.focusTransform.GetComponent<PersonObject>();
						leveling.UseAbility(p,currentAbility);
					}
					else if (UsingAbility)
					{
						UsingAbility = false;
					}
				}
			}
			else if (PlayerInput.InputInvisible()) {
				Debug.Log("toggle invisible");
				ToggleInvisible();
			}
			else if (PlayerInput.InputStatMenu()){
				Debug.Log("Open stats menu");
			}
			else
			{
				GetMovementInput();
			}
		}
	}
}