#pragma strict
//Steve Hart

//ability variables
static var ability_spider = true;
var spiderAbility : GameObject;
		


function Update () {

	//when the user clicks
	if(Input.GetMouseButtonDown(0)){
		Debug.Log("Pressed left click.");
		//find click location
			var ray : Ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
    		var point : Vector3 = ray.origin + (ray.direction);    
			var x = point.x;
			var y = point.y;
			
		//determine if cursor is inside room
		var mousePos = Input.mousePosition;
		mousePos.z = 1.0;
		var pz = Camera.main.ScreenToWorldPoint(mousePos);
		if (pz.x > -10 && pz.x < 10){
		
			//---------------------------SPIDER ABILITY
			if(ability_spider){
				//create spider spell at mouse location
				var spider = Instantiate(spiderAbility, new Vector3(x,y,-1), Quaternion.identity) as GameObject;
				//push all characters away
				
				//destroy spell after completion
				Destroy (spider, 10.0); 
				}
		}
		else{
			Debug.Log("Pressed left click.");	
		}
	}
}
