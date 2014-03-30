using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// GLOBAL VARIABLES
public static class GameVars {

	public const float DepthCursor = -8f;
	public const float DepthPeopleHazards=-1f;

	public const float Tick = 1f;

	// prevent multiple scripts from registering the same input simultaneously
	public static bool JustClicked=false;
	public static bool JustPressedKey=false;

	public static bool IsPaused=false;
	
}
