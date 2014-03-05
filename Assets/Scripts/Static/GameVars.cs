using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameVars {

	public static float Tick = 1f;

	public static bool InputLock=false;
	// can prevent multiple scripts from using the same input

	public static bool IsPaused=false;

	public static List<int[]> SpawnLists = new List<int[]>();
	
}
