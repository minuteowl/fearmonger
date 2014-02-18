using UnityEngine;
using System.Collections;

public class LampObject : MonoBehaviour {

	public bool LightOn;
	public Light lightSource;

	// Use this for initialization
	void Start () {
		LightOn = true;
		lightSource = (Light)transform.GetComponent("Light");
	}

	void TurnOn()
	{
		LightOn = true;
		lightSource.enabled = true;
	}

	void TurnOff()
	{
		LightOn = false;
		lightSource.enabled = false;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
