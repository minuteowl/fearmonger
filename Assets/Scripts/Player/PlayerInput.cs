using UnityEngine;
using System.Collections;

public static class PlayerInput {

	// Functions to normalize keyboard input
	public static bool InputRight()
	{
		return (Input.GetKey("right") && !Input.GetKey("left"));
	}

	public static bool InputLeft()
	{
		return (Input.GetKey("left") && !Input.GetKey("right"));
	}

	public static bool InputUp()
	{
		return (Input.GetKey("up") && !Input.GetKey("down"));
	}

	public static bool InputDown()
	{
		return (Input.GetKey("down") && !Input.GetKey("up"));
	}

	public static bool InputActivate()
	{
		return (Input.GetKeyDown("v") || Input.GetKeyDown ("space"));
	}

	public static bool InputInvisible()
	{
		return (Input.GetKeyDown("c"));
	}

	public static bool InputMap()
	{
		return (Input.GetKeyDown("x") && !Input.GetKeyDown("z") && !Input.GetKeyDown ("enter"));
	}

	public static bool InputMenu()
	{
		return ((Input.GetKeyDown("z")||Input.GetKeyDown("enter")) && !Input.GetKeyDown("x"));
	}
}
