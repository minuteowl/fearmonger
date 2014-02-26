using UnityEngine;
using System.Collections;

public static class PlayerInput {

	// Functions to normalize keyboard input
	public static bool InputRight()
	{
		return (Input.GetKey("right") && !Input.GetKey("left"));
	}

	public static bool InputRightOnce()
	{
		return (Input.GetKeyDown("right") && !Input.GetKey("left"));
	}

	public static bool InputQuit()
	{
		return (Input.GetKeyDown("escape"));
	}

	public static bool InputLeft()
	{
		return (Input.GetKey("left") && !Input.GetKey("right"));
	}

	public static bool InputLeftOnce()
	{
		return (Input.GetKeyDown("left") && !Input.GetKey("right"));
	}

	public static bool InputUp()
	{
		return (Input.GetKey("up") && !Input.GetKey("down"));
	}

	public static bool InputUpOnce()
	{
		return (Input.GetKeyDown("up") && !Input.GetKey("down"));
	}

	public static bool InputDown()
	{
		return (Input.GetKey("down") && !Input.GetKey("up"));
	}

	public static bool InputDownOnce()
	{
		return (Input.GetKeyDown("down") && !Input.GetKey("up"));
	}
	
	public static bool InputAction()
	{
		return (Input.GetKeyDown ("space"));
	}

	public static bool InputInvisible()
	{
		return (Input.GetKeyDown("c"));
	}

	public static bool InputMapMenu()
	{
		return (Input.GetKeyDown("x") && !Input.GetKeyDown("z"));
	}

	public static bool InputStatMenu()
	{
		return (Input.GetKeyDown("z"));
	}
}
