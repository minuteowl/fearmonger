using UnityEngine;
using System.Collections;

public class Hazard_Darkness : Hazard {

	// Use this for initialization
	protected override void Start () {
		duration = 2.2f;
		base.Start ();
	}

	// The darkness turns off lamps and lanterns
	private void OnTriggerEnter2D(Collider2D other){
		if (other.transform.CompareTag ("Lamp")){
			print ("i love lamp");
			LampObject l = other.GetComponent<LampObject>();
			l.TurnOff ();
		}
		else if (other.transform.CompareTag("Person")){
			print ("hello person");
			Person p = other.GetComponent<Person>();
			p.Damage (damage);
			p.Threaten(this);
			if (p is Person_Candle){
				((Person_Candle)p).TurnOff();
			}
		}
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update();
	}
}
