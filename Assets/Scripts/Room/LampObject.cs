using UnityEngine;
using System.Collections;

public class LampObject : MovableObject {

	public bool IsOn;
	public Light lightSource;
	public RoomObject room;
	// By convention, timers go from zero to max, then reset to zero
	public float flickerTimer = 0f, flickerTimerMax = 0.1f;
	public int flickersRemaining=0;

	// Use this for initialization
	void Start () {
		IsOn = true;
		lightSource = (Light)transform.GetComponent("Light");
		room = transform.parent.GetComponent<RoomObject>();
	}

	public void StartFlicker(int flickers)
	{
		flickersRemaining = flickers;
		flickerTimer = 0;
	}

	public void Switch()
	{
		if (IsOn) TurnOff();
		else TurnOn();
	}

	public void TurnOn()
	{
		IsOn = true;
		lightSource.enabled = true;
		room.lampsOn++;
		/*
		foreach (PersonObject p in room.members) {
			if (p!=null && p.currentSanity<p.MaxSanity)
			{
				p.currentSanity++;
			}
		}
		*/
	}

	public void TurnOff()
	{
		IsOn = false;
		lightSource.enabled = false;
		room.lampsOn--;
		foreach (Person p in room.occupants) {
			if (p.isAdult && p.targetLamp!=null){
				p.AssignLamp(this);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (flickersRemaining>0) {
			if (flickerTimer<flickerTimerMax)
			{
				flickerTimer += GameVars.Tick*Time.deltaTime;
			}
			else {
				flickerTimer =0;
				flickersRemaining--;
				if (IsOn) TurnOff ();
				else TurnOn();
			}
		}
	}
}
