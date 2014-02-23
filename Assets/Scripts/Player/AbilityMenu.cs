using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AbilityMenu: MonoBehaviour
{
	GameManager game;
	Rect menuBackground;
	int menux=155, menuy=100, menuw=520, menuh=410;
	Color menuBackgroundColor;
	PlayerActivity player;
	Transform camTransform;

	int selectedIndex = 0, scrollIndex;

	[HideInInspector] public List<Ability> listAbilities;
	
	AbilityMenu()
	{
		listAbilities = new List<Ability>();
		listAbilities.Add(new Ability_Push());
		listAbilities.Add(new Ability_Flash());
		listAbilities.Add(new Ability_ShadowStorm());
	}

	void Start()
	{
		game = GameObject.Find("GameManager").GetComponent<GameManager>();
		player = transform.GetComponent<PlayerActivity>();
		camTransform  = GameObject.Find("MainCamera").transform;
		menuBackground = new Rect(menux,menuy, menuw, menuh);
		selectedIndex=0;
		player.SetAbility(listAbilities[selectedIndex]);
		listAbilities[selectedIndex].Locked=false; // start at level 1
	}

	void Update() {
		if (!Statics.LockInput){
			UpdateInput();
		}
	}

	public void Prepare(){
		scrollIndex = selectedIndex;
	}

	void UpdateInput()
	{
		if (game.currentView==GameManager.View.Stats) {
			if (PlayerInput.InputStatMenu()) {
				Statics.LockInput=true;
				game.currentView=GameManager.View.Game;
				game.Unpause();
			}else if (scrollIndex>0 && PlayerInput.InputUpOnce()){
				scrollIndex--;
			} else if ((scrollIndex<listAbilities.Count-1) && PlayerInput.InputDownOnce()){
				scrollIndex++;
			} else if (PlayerInput.InputAction()) {
				if (!listAbilities[scrollIndex].Locked) {
					selectedIndex = scrollIndex;
					player.currentAbility = listAbilities[selectedIndex];
				}
			}
		}
	}

	int rowHeight=30,
		left, width;
	void OnGUI()
	{
		if (game.currentView==GameManager.View.Stats)
		{
			GUI.color = Color.green;
			GUI.Box (menuBackground, "Ability Menu");
			left = menux+50; width = 130; GUI.color = Color.green; // column 1
			GUI.Box (new Rect(left,menuy+rowHeight,width,rowHeight),"Ability");
			for (int i=0; i<listAbilities.Count ; i++) {
				GUI.color = (i==scrollIndex)? Color.cyan : Color.white;
				GUI.Box(new Rect(left, menuy+(i+2)*rowHeight, width, rowHeight),listAbilities[i].Name);
			}
			left += width+10; width = 40; GUI.color = Color.green; // column 2
			GUI.Box (new Rect(left,menuy+rowHeight,width,rowHeight),"Cost");
			for (int i=0; i<listAbilities.Count ; i++) {
				GUI.color = (i==scrollIndex)? Color.cyan : Color.white;
				GUI.Box(new Rect(left, menuy+(i+2)*rowHeight, width, rowHeight),listAbilities[i].Cost.ToString());
			}
			left += width+10; width = 125; // column 3
			for (int i=0; i<listAbilities.Count ; i++) {
				if (i==selectedIndex) {
					GUI.color = Color.cyan;
					GUI.Box(new Rect(left, menuy+(i+2)*rowHeight, width, rowHeight),"Equipped");
				}
				else if (listAbilities[i].Locked) {
					GUI.color = Color.red;
					GUI.Box(new Rect(left, menuy+(i+2)*rowHeight, width, rowHeight),"(Locked)");
				}
			}
			GUI.color=Color.white;
			GUI.Box (new Rect(menux+10, menuy+menuh-rowHeight*2,menuw-20,rowHeight), listAbilities[scrollIndex].Description);			                                                   
		}
	}
}