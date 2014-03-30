using UnityEngine;
using System.Collections;

// This is in charge of the LIST of abilities
// and the toolbar GUI
public class AbilityManager : MonoBehaviour {

	private GameManager gameManager;
	public Ability[] listAbilities;
	public Ability currentAbility;
	private Vector2 clickLocation2D;
	private RaycastHit2D hit2d;
	private Ray2D ray2d;
	private bool guiClick=false;

	private void Start()
	{
		listAbilities = new Ability[5];
		listAbilities[0] = new Ability_Spiders(); 
		listAbilities[1] = new Ability_Darkness();
		listAbilities[2] = new Ability_Grab();
		listAbilities[3] = new Ability_Monster();
		listAbilities[4] = new Ability_Possess();
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
		//currentAbility = listAbilities[0]; // assuming that you begin with an ability
	}

	private void Update () {
		hit2d = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		Ray ray3d = Camera.main.ScreenPointToRay(Input.mousePosition);    
		clickLocation2D = (Vector2)(ray3d.origin + ray3d.direction);
		if (gameManager.currentView==GameManager.View.Game){
			if (!guiClick && Input.GetMouseButtonDown (0)) {
				if (currentAbility!=null){
					// assume 3rd arguments are the same for now
					currentAbility.UseAbility(gameManager.currentRoom, clickLocation2D, null);
				}
				else {
					Debug.Log ("No ability selected.");
				}
			}
		}
		
	}

	private void SelectAbility(int index){
		guiClick = true;
		currentAbility = listAbilities [index];
		Debug.Log ("Selected ability " + listAbilities [index].Name);
	}

	private void OnGUI(){
		guiClick=false;
		if (GUI.Button (new Rect (1, 61, 125, 30), listAbilities [0].Name)) {
			SelectAbility(0);
		}
		else if (GUI.Button (new Rect (1, 91, 125, 30), listAbilities [1].Name)) {
			SelectAbility(1);
		}
		else if (GUI.Button (new Rect (1, 121, 125, 30), listAbilities [2].Name)) {
			SelectAbility(2);
		}
		else if (GUI.Button (new Rect (1, 151, 125, 30), listAbilities [3].Name)) {
			SelectAbility(3);
		}
		else if (GUI.Button (new Rect (1, 181, 125, 30), listAbilities [4].Name)) {
			SelectAbility(4);
		}
	}

}