using UnityEngine;
using System.Collections;

public class Hazard_Monster : Hazard {

	// Monster chases people around

	private Transform nearestPersonTransform;
	private float distMin, dist, huntTimer=0, huntTimerMax=0.5f;
	private float soundTimer=0, soundTimerMax=2.5f;
	private Vector2 moveDirection;
	private const float runSpeed=4f;
	private AudioClip roarSound;

	// Use this for initialization
	protected override void Start () {
		duration = 8f;
		base.Start ();
		roarSound = Resources.Load<AudioClip>("Sounds/PLACEHOLDER-monstersound");
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		// Should find a new target?
		if(soundTimer<soundTimerMax)
			soundTimer+=GameVars.Tick*Time.deltaTime;
		else {
			soundTimer=0f;
			AudioSource.PlayClipAtPoint (roarSound,transform.position);
		}
		if (huntTimer<huntTimerMax)
			huntTimer+=GameVars.Tick*Time.deltaTime;
		else {
			huntTimer=0;
			distMin = 999f;
			foreach (Person p in currentRoom.occupants){
				if (p!=null) {
					dist = (p.transform.position-this.transform.position).magnitude;
					if (dist<distMin){
						distMin=dist;
						nearestPersonTransform=p.transform;
					}
				}
			}
		}
		if (nearestPersonTransform!=null){
			// A target is chosen -> chase them!
			moveDirection=((Vector2)nearestPersonTransform.position-(Vector2)transform.position).normalized;
			rigidbody2D.velocity=moveDirection*runSpeed;
		}
		else {
		//	Finish ();
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if (other.transform.CompareTag ("Person")){
			print ("collision with person!");
			Person p = other.transform.GetComponent<Person>();
			p.Threaten (this);
			p.Damage (damage);
			Finish ();//
		}
	}

	// Scaring other people (not touching)
	private void OnTriggerEnter2D(Collider2D other){
		if (other.transform.CompareTag ("Person")){
			print ("triggered with person!");
			Person p = other.transform.GetComponent<Person>();
			p.Threaten (this);
			p.Damage (1);
		}
	}
}
