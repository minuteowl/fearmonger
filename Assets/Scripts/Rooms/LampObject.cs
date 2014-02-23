using UnityEngine;
using System.Collections;

public class LampObject : MovableObject {

	public bool LightOn;
	public Light lightSource;
	public RoomObject room;
	public float flickerCountdown = 0f;
	float flickerCountdownMax = 0.1f;
	public int flickersRemaining=0;
	public bool IsFlickering=false;

	// Use this for initialization
	void Start () {
		LightOn = true;
		lightSource = (Light)transform.GetComponent("Light");
		room = transform.parent.GetComponent<RoomObject>();
	}

	public void FlickerTimer(int flickers)
	{
		flickersRemaining = flickers;
		flickerCountdown = flickerCountdownMax;
	}

	public void Switch()
	{
		if (LightOn) TurnOff();
		else TurnOn();
	}

	public void TurnOn()
	{
		LightOn = true;
		lightSource.enabled = true;
		room.lampsOn++;
		foreach (PersonObject p in room.members) {
			if (p!=null && p.currentSanity<p.MaxSanity)
			{
				p.currentSanity++;
			}
		}
	}

	public void TurnOff()
	{
		LightOn = false;
		lightSource.enabled = false;
		room.lampsOn--;
		foreach (PersonObject p in room.members) {
			if (p!=null && p.currentSanity>p.MaxSanity/2)
			{
				p.currentSanity--;
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (flickersRemaining>0) {
			IsFlickering = true;
			if (flickerCountdown<=0)
			{
				flickerCountdown=flickerCountdownMax;
				flickersRemaining--;
				if (LightOn) TurnOff ();
				else TurnOn();
			}
			else {
				flickerCountdown -= Statics.Tick*Time.deltaTime;
			}
		}
		else
		{
			IsFlickering = false;
			flickerCountdown=0;
		}
	}
}
