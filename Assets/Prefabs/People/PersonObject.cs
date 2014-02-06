using UnityEngine;
using System.Collections;

public class PersonObject : MonoBehaviour {

	PlayerLevel plevel;
	public int Sanity;
	int MaxSanity = 10;
	public int RoomNumber;

	// Use this for initialization
	void Start () {
		plevel = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLevel>();
		Sanity = MaxSanity;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void Leave(){
		Destroy(this);
	}
}
