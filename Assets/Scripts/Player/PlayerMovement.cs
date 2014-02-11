using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	Vector2 facingDirection;

	Vector2 xAxis, yAxis;

	public bool IsInvisible = true;

	// Use this for initialization
	void Start () {
		xAxis = new Vector2(1,0);
		yAxis = new Vector2(0,1);
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
	}
}
