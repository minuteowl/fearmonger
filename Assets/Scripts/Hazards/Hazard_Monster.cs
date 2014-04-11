using UnityEngine;
using System.Collections;

public class Hazard_Monster : Hazard {

	public bool IS_FACING_UP=false, IS_FACING_DOWN=false, IS_FACING_RIGHT=false, IS_FACING_LEFT=false;
	private Animator anim;

	// Monster chases people around

	private Transform nearestPersonTransform;
	public float distMin, dist, huntTimer=0, huntTimerMax=0.5f;
	private float soundTimer=0, soundTimerMax=3.5f;
	public Vector2 moveDirection;
	private const float runSpeed=3f;
	private AudioClip roarSound;

	//public bool IS_FACING_UP=false, IS_FACING_DOWN=false, IS_FACING_RIGHT=false, IS_FACING_LEFT=false;

	// HEY ANIMATION PEOPLE!!! USE THIS!!!
	public void Animate(){
		SetFacingDirection (); // -> Sets IS_FACING_UP, etc, for you -> don't recalculate them.
		if (anim!=null){
			anim.SetBool ("walkUp", IS_FACING_UP);
			anim.SetBool ("walkDown", IS_FACING_DOWN);
			anim.SetBool ("walkRight", IS_FACING_RIGHT);
			anim.SetBool ("walkLeft", IS_FACING_LEFT);
		}
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		anim = transform.GetComponent<Animator>();
		duration = GameVars.duration_monster;
		damage=GameVars.damage_monster;
		duration = 8f;
		roarSound = Resources.Load<AudioClip>("Sounds/ghost_giggle_3");
	}

	// Update is called once per frame
	protected override void Update () {
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
				if (p!=null && p.Sanity>0) {
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
		else { // there's nobody else in the room
			//Debug.LogError("nobody in the room!");
			//Finish ();
		}
		Animate ();
		base.Update ();
	}

	// use this to determine which sprite(s) to use
	private void SetFacingDirection()
	{
		if (Mathf.Abs(rigidbody2D.velocity.y)>Mathf.Abs(rigidbody2D.velocity.x)){
			if (rigidbody2D.velocity.y>0) {
				IS_FACING_UP=true; IS_FACING_DOWN=false;
				IS_FACING_LEFT=false; IS_FACING_RIGHT=false;
			}
			else {
				IS_FACING_UP=false; IS_FACING_DOWN=true;
				IS_FACING_LEFT=false; IS_FACING_RIGHT=false;
			}
		}
		else {
			if (rigidbody2D.velocity.x>0){
				IS_FACING_UP=false; IS_FACING_DOWN=false;
				IS_FACING_LEFT=false; IS_FACING_RIGHT=true;
			}
			else {
				IS_FACING_UP=false; IS_FACING_DOWN=false;
				IS_FACING_LEFT=true; IS_FACING_RIGHT=false;
			}
		}
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if (other.transform.CompareTag ("Person")){
			//print ("collision with person!");
			Person p = other.transform.GetComponent<Person>();
			p.Threaten (this);
			p.Damage (damage);
			//Finish ();//
		}
	}

	// Scaring other people (not touching)
	private void OnTriggerEnter2D(Collider2D other){
		if (other.transform.CompareTag ("Person")){
			//print ("triggered with person!");
			Person p = other.transform.GetComponent<Person>();
			p.Threaten (this);
			//p.Damage (1);
		}
	}


}
