using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private Vector3 facingDirection;
	public Vector3 FacingDirection {
		get { return facingDirection;}
	}

	Vector3 front1, front2, frontCheck1, frontCheck2;
	float epsilon = 0.03f;
	float radius;

	BoxCollider2D bodyCollider;

	Vector3 xAxis = new Vector3(1,0,0);
	Vector3 yAxis = new Vector3(0,1,0);

	public bool IsInvisible = true;
	public float Speed = 10f;

	// Use this for initialization
	void Start () {
		radius = 0.5f - epsilon;
		bodyCollider = (BoxCollider2D)this.transform.GetComponent("BoxCollider2D");
		facingDirection = yAxis;

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
		if (PlayerInput.InputMenu()){
			Debug.Log("Open menu");
		}
		else if (PlayerInput.InputMap())
		{
			Debug.Log("Open map");
		}
		else if (PlayerInput.InputInvisible())
		{
			Debug.Log("Turn invisible");
			ToggleInvisible();
		}
		else
		{
			GetMovementInput();
		}
	}
	
}

