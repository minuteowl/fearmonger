#pragma strict

static public ability_spider = false;
var spiderAbility : GameObject
		
    	 
function Start () {

}

function Update () {

}


//on click --> if any powers are active, put the object there
function onClick(){

	if(ability_spider){
		var x = Camera.main.ScreenToWorldPoint(Input.mousePosition.x);
		var y = Camera.main.ScreenToWorldPoint(Input.mousePosition.y);
		var spider = Instantiate(spiderAbility, new Vector3(x,y,-1), Quaternion.identity) as GameObject;

	}
}
