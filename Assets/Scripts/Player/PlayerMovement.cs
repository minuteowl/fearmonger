using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	Vector3 facingDirection;
	Vector3 front, frontCheck;
	float epsilon = 0.03f;
	Transform TargetArea;
	float targetAreaOffset;
	BoxCollider2D bodyCollider;
	CircleCollider2D targetCollider;

	[HideInInspector]
	public bool IsHoldingSomething = false;

	Vector3 xAxis = new Vector3(1,0,0);
	Vector3 yAxis = new Vector3(0,1,0);

	public bool IsInvisible = true;
	public float Speed = 10f;

	// Use this for initialization
	void Start () {
		TargetArea = transform.FindChild("TargetArea");
		bodyCollider = (BoxCollider2D)this.transform.GetComponent("BoxCollider2D");
		targetCollider = (CircleCollider2D)TargetArea.GetComponent("CircleCollider2D");
		targetAreaOffset = bodyCollider.size.x + targetCollider.radius;
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
		if (!Physics2D.Linecast(front, frontCheck, 1 << LayerMask.NameToLayer("Solid"))){
			this.transform.position += facingDirection*Speed*Time.deltaTime;
		}
	}

	// Update is called once per frame
	void Update () {
		if (PlayerInput.InputMenu()){
		}
		else if (PlayerInput.InputMap())
		{
		}
		else if (PlayerInput.InputInvisible())
		{
			ToggleInvisible();
		}
		else
		{
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
		TargetArea.position = transform.position + facingDirection*targetAreaOffset;
		front = transform.position + facingDirection*bodyCollider.size.x;
		frontCheck = front + facingDirection*epsilon;
	}
	
}

