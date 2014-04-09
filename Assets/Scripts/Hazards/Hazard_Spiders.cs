using UnityEngine;
using System.Collections;

public class Hazard_Spiders : Hazard {

	private ParticleSystem particles;
	private CircleCollider2D circle;

	// Use this for initialization
	protected override void Start () {
		damage=GameVars.damage_spiders;
		duration = GameVars.duration_spiders;
		base.Start ();
		circle = transform.GetComponent<CircleCollider2D>();
		particles = transform.GetComponent<ParticleSystem>();
		if (particles.duration != this.duration){
			Debug.LogWarning("Spider hazard particle effect has wrong duration!");
		}
	}

	private void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("Person")){
			//Debug.Log ("Spider collided with person");
			Person p = other.transform.GetComponent<Person>();
			p.Damage (damage);
		}// else {
			//Debug.Log ("Spider collided with non-person");
		//}
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		circle.radius += 2f*(Time.deltaTime*GameVars.Tick);
	}
}
