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
		return (Input.GetKey("v") || Input.GetKey("V") || Input.GetKey ("space"));
	}

	public static bool InputInvisible()
	{
		return (Input.GetKey("c") || Input.GetKey("C"));
	}

	public static bool InputMap()
	{
		return (Input.GetKey("x") && !Input.GetKey("z") && !Input.GetKey ("enter"));
	}

	public static bool InputMenu()
	{
		return ((Input.GetKey("z")||Input.GetKey ("enter")) && !Input.GetKey("x"));
	}
}
