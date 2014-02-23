using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Selector))]
public class PlayerActivity : MonoBehaviour {

	GameManager game;
	Leveling level;

	// Attached scripts
	public Selector grab;
	private Transform grabTransform;
	private float grabDistance;
	public Vector3 zDistanceVector = Vector3.zero;//new Vector3(0,0,-2);

	// Actions
	MonoBehaviour[] args = new MonoBehaviour[10];
	Ability currentAbility;
	public bool UsingAbility=false;
	int currentAblilityIndex = 0;

	// Sprites
	Sprite[] sprites;
	public bool IsInvisible = true;

	// Motion
	private Vector3 facingDirection;
	public BoxCollider2D bodyCollider;
	private Vector3 xAxis = new Vector3(1,0,0);
	private Vector3 yAxis = new Vector3(0,1,0);
	public float Speed = 10f;
	public Vector3 FacingDirection {
		get { return facingDirection;}
	}




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
		game = GameObject.Find ("GameManager").GetComponent<GameManager>();
		level = GameObject.Find("GameManager").GetComponent<Leveling>();
		game.currentView = GameManager.View.Game;
		game.SetUpAbilities();
		grabDistance = bodyCollider.size.x/2 + grab.box.size.x/2;
		currentAbility = game.listAbilities[0];
		sprites = Resources.LoadAll<Sprite>("Sprites/Player");
	}

	void EnterRoom()
	{
		IsInvisible = true;
		facingDirection = yAxis;
	}

	//int spriteIndex=0;
	void ToggleInvisible()
	{
		if (IsInvisible){
			IsInvisible=false;
			transform.GetComponent<SpriteRenderer>().sprite=sprites[1];
		}
		else {
			IsInvisible = true;
			transform.GetComponent<SpriteRenderer>().sprite=sprites[2];
		}
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
			if (UsingAbility)
			{
				UsingAbility=false;
			}
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
					if (UsingAbility)
					{
						UsingAbility = false;
					}
					else if (grab.currentFocus==Selector.FocusType.Door) {
						game.GoToMap();
					}
					else if (grab.currentFocus==Selector.FocusType.Movable) {
						grab.Pickup();
					}
					else if (!UsingAbility) {
						UsingAbility=true; // make sure that the ability is only called once at a time
						if (currentAbility is Ability_Tentacle)
						{
							if (grab.currentFocus==Selector.FocusType.Person) {
								args[0] = grab.focusTransform.GetComponent<PersonObject>();
								currentAbility.Use (level, args);
							}
						}
						else if (currentAbility is Ability_Flash)
						{
							args[0] = game.currentRoom;
							currentAbility.Use (level, args);
						}
					}
				}
			}
			else if (PlayerInput.InputInvisible()) {
				ToggleInvisible();
			}
			else if (PlayerInput.InputStatMenu()){
				currentAblilityIndex = (currentAblilityIndex+1)%game.listAbilities.Count;
				currentAbility = game.listAbilities[currentAblilityIndex];
				Debug.Log ("Current Ability is: "+ game.listAbilities[currentAblilityIndex].Name);
				//Debug.Log("Open stats menu");
			}
			else
			{
				GetMovementInput();
			}
		}
	}
}