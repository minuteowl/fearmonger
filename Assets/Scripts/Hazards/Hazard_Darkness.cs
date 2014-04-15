using UnityEngine;
using System.Collections;

public class Hazard_Darkness : Hazard {

	CircleCollider2D circle;
	bool spent = false;
	//SpriteRenderer renderer;
	Vector3 dtscale, rotvector;

	// Use this for initialization
	protected override void Start () {
		duration = GameVars.duration_darkness;
		damage=GameVars.damage_darkness;
		base.Start ();
		circle = transform.GetComponent<CircleCollider2D>();
		//renderer=transform.GetComponent<SpriteRenderer>();
		//dtCollider = circle.radius/duration;
		dtscale = transform.localScale/duration;
		rotvector = Vector3.back*300f;
	}

	// The darkness turns off lamps and lanterns
	private void OnTriggerEnter2D(Collider2D other){
		if (!spent && other.transform.CompareTag ("Lamp")){
		//	print ("i love lamp");
			LampObject l = other.GetComponent<LampObject>();
			l.TurnOff ();
			spent = true;
		}
		else if (other.transform.CompareTag("Person")){
		//	print ("hello person");
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
		if (transform.localScale.x>0f){
			transform.localScale -= dtscale*GameVars.Tick*Time.deltaTime;
			transform.Rotate (rotvector*GameVars.Tick*Time.deltaTime);
		}

		base.Update();
	}
}
