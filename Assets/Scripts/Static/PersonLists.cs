using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// SOMEHOW THIS FILE WILL DETERMINE HOW TO GENERATE
// THE COMBINATIONS OF PEOPLE WHO CAN CHECK INTO A ROOM

public class PersonLists {
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
	 * 0 = none
	 * 1 = boy
	 * 2 = girl
	 * 3 = man
	 * 4 = woman
	 * 5 = man w/ candle
	 * 6 = woman w/ candle
	 * 7 = priest
	 */
	public static Transform[] PeoplePrefabs = new Transform[7];



	// Initial list: 2-person combos, at least 1 adult
	static List<int[]> ListPersonCombos = new List<int[]>(){
		new int[]{0,0,3}, new int[]{0,0,4}, new int[]{0,3,3},
		new int[]{0,1,3}, new int[]{0,1,4}, new int[]{0,3,4},
		new int[]{0,2,3}, new int[]{0,2,4}, new int[]{0,4,4},
	};
	// Next list: 3-person combos
	public static List<int[]> ListCombos_Triples = new List<int[]>(){
		new int[]{1,1,3}, new int[]{1,1,4}, new int[]{1,2,3}, new int[]{1,2,4},
		new int[]{1,3,3}, new int[]{1,3,4}, new int[]{1,4,4}, new int[]{2,2,3},
		new int[]{2,2,4}, new int[]{2,3,3}, new int[]{2,3,4}, new int[]{2,4,4},
		new int[]{3,3,3}, new int[]{3,3,4}, new int[]{3,4,4}, new int[]{4,4,4},
	};
	// Then you get rooms with a person with a candle
	public static List<int[]> ListCombos_1Candle = new List<int[]>(){
	};
	// And then you can have 2 candle people
	public static List<int[]> ListCombos_2Candle = new List<int[]>(){
	};
	// Then you can get a priest in a room
	static List<int[]> ListCombos_1Priest = new List<int[]>(){
	};
	// Then you can get 2 priests in a room (most difficult)
	static List<int[]> ListCombos_2Priest = new List<int[]>(){
	};
	
	List<int[]> joinLists(List<int[]> listTo, List<int[]> listFrom){
		foreach (int[] i in listFrom) {
			listTo.Add (i);
		}
		return listTo;
	}
	
	

}
