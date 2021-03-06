﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// GLOBAL VARIABLES
public static class GameVars {

	public const float DepthCursor = -9f;
	public const float DepthPeopleHazards=-1f;

	public const float Tick = 1f;

	public static float Difficulty = 7.5f;
	// higher numbers = easier (lower exp drain)

	// prevent multiple scripts from registering the same input simultaneously
	public static bool JustClicked=false;
	public static bool JustPressedKey=false;

	public static bool IsPaused=false;

	public static float duration_spiders=7.5f;
	public static int damage_spiders=2;
	public static float duration_darkness=8f;
	public static int damage_darkness=3;
	public static float duration_claw=8f;
	public static int damage_claw=6;
	public static float duration_monster=10f;
	public static int damage_monster=7;
	public static float duration_possession=20f;
	public static int damage_possession=15;
}
