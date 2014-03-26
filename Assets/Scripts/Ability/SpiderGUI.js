#pragma strict
//Steve Hart


function Start () {

}

function Update () {

}

function OnMouseDown()

{
	if(AbilityManager.ability_spider == true){
		AbilityManager.ability_spider = false;
		Debug.Log("spider false");

	}
	else{
		AbilityManager.ability_spider = true;
		Debug.Log("spider true");
	}
}