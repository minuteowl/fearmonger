using UnityEngine;
using System.Collections;

public class startgame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.anyKey){
			Debug.Log ("Loading level");
			Application.LoadLevel (1);
		}
	}
}
