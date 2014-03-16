using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// SOMEHOW THIS FILE WILL DETERMINE HOW TO GENERATE
// THE COMBINATIONS OF PEOPLE WHO CAN CHECK INTO A ROOM

public static class PersonLists {
	/*====== PEOPLE GENERATION LISTS ======
	 * As you level up, there are more possible combinations of visitors
	 * More difficult combinations become possible at higher levels
	 * 
	 * Rules: Any occupied rooms must have...
	 * 	...at least 1 adult per room
	 *  ...no more than 2 candle people
	 *  ...no more than 2 priests
	 * 
	 * Index:
	 * 0 = boy
	 * 1 = girl
	 * 2 = man
	 * 3 = woman
	 * 4 = man w/ candle
	 * 5 = woman w/ candle
	 * 6 = priest
	 */
	// Initial list: 2-person combos, at least 1 adult
	public static List<int[]> Combinations = new List<int[]>(){
		new int[]{  2}, new int[]{  3}, new int[]{2,2},
		new int[]{0,2}, new int[]{0,3}, new int[]{2,3},
		new int[]{1,2}, new int[]{1,3}, new int[]{3,3},
	};
	// Next list: 3-person combos
	public static List<int[]> ListCombos_Triples = new List<int[]>(){
		new int[]{0,0,2}, new int[]{0,0,3}, new int[]{0,1,2}, new int[]{0,1,3},
		new int[]{0,2,2}, new int[]{0,2,3}, new int[]{0,3,3}, new int[]{1,1,2},
		new int[]{1,1,3}, new int[]{1,2,2}, new int[]{1,2,3}, new int[]{1,3,3},
		new int[]{2,2,2}, new int[]{2,2,3}, new int[]{2,3,3}, new int[]{3,3,3},
	};
	// Then you get rooms with a person with a candle
	public static List<int[]> ListCombos_0Candle = new List<int[]>(){
	};
	// And then you can have 1 candle people
	public static List<int[]> ListCombos_1Candle = new List<int[]>(){
	};
	// Then you can get a priest in a room
	public static List<int[]> ListCombos_0Priest = new List<int[]>(){
	};
	// Then you can get 1 priests in a room (most difficult)
	public static List<int[]> ListCombos_1Priest = new List<int[]>(){
	};
	/*
	List<int[]> joinLists(List<int[]> listTo, List<int[]> listFrom){
		foreach (int[] i in listFrom) {
			listTo.Add (i);
		}
		return listTo;
	}
	
	*/

}
