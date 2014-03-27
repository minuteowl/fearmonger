using UnityEngine;
using System.Collections;


public class AbilityMan : MonoBehaviour {
	
	//ability variables
	public static bool ability_spider = true;
	public GameObject spiderAbility;
	
	void Update () {
		
		//when the user clicks
		if(Input.GetMouseButtonDown(0)){
			Debug.Log("Pressed left click.");
			//find click location
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);    
			Vector3 point = ray.origin + (ray.direction);    
			float x = point.x;
			float y = point.y;
			
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			
			GameObject gameM = GameObject.Find("GameManager");
			GameManager gameS = gameM.GetComponent<GameManager>(); 
			
			if (hit){
				if(hit.collider.gameObject.CompareTag("Door"))
				{
					gameS.GoToMap();
					GameManager.atMap = true;
					Debug.Log("Go to map");
					//Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
				}
			}
			else if(GameManager.atMap == false){
				if(ability_spider){
					//create spider spell at mouse location
					GameObject spider = (GameObject)Instantiate(spiderAbility, new Vector3(x,y,-1), Quaternion.identity);
					//destroy spell after completion
					Destroy (spider, 10.0f); 
				}
			}
		}
		
	}
}