using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Selector))]
public class PlayerActivity : MonoBehaviour {

	GameManager game;
	Leveling level;

	// Attached scripts
	[HideInInspector] public Selector grab;
	private Transform grabTransform;

	// Actions
	MonoBehaviour[] args = new MonoBehaviour[10];
	[HideInInspector] public Ability currentAbility;
	AbilityMenu abilityMenu;

	// Sprites
	Sprite[] sprites;
	int spriteIndex=0;
	public bool IsInvisible = false;

	// Motion
	[HideInInspector] public BoxCollider2D bodyCollider;
	private Vector3 xAxis = new Vector3(1,0,0);
	private Vector3 yAxis = new Vector3(0,1,0);
	float Speed = 900f;
	private Vector3 facingDirection;
	[HideInInspector] public Vector3 FacingDirection {
		get { return facingDirection;}
	}

	public void SetAbility(Ability a) {
		currentAbility = a;
	}

	public void FaceUp()
	{
		Debug.Log("Facing up.");
		game.currentView = GameManager.View.Game;
		SetSprite();
		facingDirection = Vector3.up;
	}


	public void MoveTo(Vector3 pos) {
		Debug.Log("Move to ("+pos.x+", "+pos.y+")");
		transform.position = new Vector3(pos.x,pos.y,transform.position.z);
	}

	// Use this for initialization
	void Start () {
		facingDirection = Vector3.up;
		bodyCollider = (BoxCollider2D)transform.GetComponent("BoxCollider2D");
		grabTransform = transform.FindChild("Selector");
		grab = grabTransform.GetComponent<Selector>();
		game = GameObject.Find ("GameManager").GetComponent<GameManager>();
		level = transform.GetComponent<Leveling>();
		abilityMenu = transform.GetComponent<AbilityMenu>();
		game.currentView = GameManager.View.Game;
		sprites = Resources.LoadAll<Sprite>("Sprites/Player");
	}

	void EnterRoom()
	{
		Statics.LockInput=true;
		facingDirection = yAxis;
	}

	void SetSprite(){
		if (game.currentView==GameManager.View.Map) {
			spriteIndex=8; // giant sprite
		}
		else if (facingDirection==Vector3.down) {
			spriteIndex = 0;
		}
		else if (facingDirection==Vector3.up) {
			spriteIndex=1;
		}
		else if (facingDirection==Vector3.right) {
			spriteIndex=2;
		}
		else {
			spriteIndex=3;
		}
		if (IsInvisible) {
			spriteIndex += 4;
		}
		transform.GetComponent<SpriteRenderer>().sprite=sprites[spriteIndex];
	}

	void ToggleInvisible()
	{
		IsInvisible = !IsInvisible;
		SetSprite();
	}

	void Move()
	{
		rigidbody2D.velocity = facingDirection*Speed*Time.deltaTime;
		SetSprite();
	}

	void GetMovementInput() {
		if (PlayerInput.InputRight())
		{
			facingDirection = xAxis;
			Move ();
			Statics.LockInput=true;
		}
		else if (PlayerInput.InputLeft())
		{
			facingDirection = -xAxis;
			Move ();
			Statics.LockInput=true;
		}
		if (PlayerInput.InputUp())
		{
			facingDirection = yAxis;
			Move ();	
			Statics.LockInput=true;
			
		}
		else if (PlayerInput.InputDown())
		{
			facingDirection = -yAxis;
			Move ();
			Statics.LockInput=true;
		}

	}

	void Update() {
		if (!Statics.LockInput) {
			UpdateInput();
		}
	}

	void UpdateAbilities() {
		Statics.LockInput=true; // make sure that the ability is only called once at a time
		if (level.CanUse(currentAbility)) {
			level.UseAbility(currentAbility);
			grab.SetWait(currentAbility.Duration);
			if (currentAbility is Ability_Push)
			{
				args[0]=grab;
				currentAbility.UseAbility (level, args);
			}
			else if (currentAbility is Ability_Flash)
			{
				args[0] = game.currentRoom;
				currentAbility.UseAbility (level, args);
			}
			else if (currentAbility is Ability_Tentacle)
			{
				// set args
				currentAbility.UseAbility(level,args);
			}
			else if (currentAbility is Ability_ShadowStorm)
			{
				// set args
				currentAbility.UseAbility(level,args);
			}
		}
	}
	
	// Update is called once per frame
	void UpdateInput () {
		if (game.currentView==GameManager.View.Game){
			//UpdateSelector();
			if (PlayerInput.InputAction()) {
				Statics.LockInput=true;
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
						SetSprite();
					}
					else if (grab.currentFocus==Selector.FocusType.Movable) {
						grab.Pickup();
					}
					else {
						UpdateAbilities();
					}
				}
			}
			else if (PlayerInput.InputInvisible()) {
				Statics.LockInput=true;
				ToggleInvisible();
			}
			else if (PlayerInput.InputStatMenu()){
				Statics.LockInput=true;
				game.Pause ();
				abilityMenu.Prepare();
				game.currentView=GameManager.View.Stats;
			}
			else
			{
				Statics.LockInput=true;
				GetMovementInput();
			}
		}
		else if (game.currentView==GameManager.View.Stats) {
			// this will go in the AbilityMenu class
		}
	}
}