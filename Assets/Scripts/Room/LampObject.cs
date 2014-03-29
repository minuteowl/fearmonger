using UnityEngine;
using System.Collections;

public class LampObject : MovableObject {

	public bool IsOn;
	private Light lightSource;
	protected RoomObject room;
	// By convention, timers go from zero to max, then reset to zero
	private float flickerTimer = 0f, flickerTimerMax = 0.1f;
	private int flickersRemaining=0;

	// Use this for initialization
	private void Start () {
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
		room.TurnLightOn();
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
		room.TurnLightOff();
		foreach (Person p in room.occupants) {
			if (p.isAdult && p.targetLamp!=null){
				p.AssignLamp(this);
			}
		}
	}

	// Update is called once per frame
	private void Update () {
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
