using UnityEngine;
using System.Collections;

public class PushTentacle : MonoBehaviour {

	Transform selectorTransform;
	Leveling level;
	PersonObject p;
	float countdown;
	bool isactive=true;
	[HideInInspector] public int xp=0;

	// Use this for initialization
	void Start () {
		selectorTransform = GameObject.Find("Selector").GetComponent<Selector>().transform;
		level = GameObject.Find ("Player").GetComponent<Leveling>();
		countdown = 1f;
		transform.position = selectorTransform.position;
		transform.rotation = selectorTransform.rotation;
	}
	
	void Update()
	{
		if (countdown>0){
			transform.position = selectorTransform.position;
			transform.rotation = selectorTransform.rotation;
			countdown -= Statics.Tick*Time.deltaTime;
		}
		else {
			isactive=false;
			GameObject.Destroy(gameObject);
			GameObject.Destroy(this);
		}
	}

	void OnTriggerEnter2D(Collider2D other)  {
		if (isactive) {
			if (p=other.transform.GetComponent<PersonObject>()) {
				p.rigidbody2D.velocity = level.transform.GetComponent<PlayerActivity>().FacingDirection*10;
				xp = p.DecreaseSanity(1);
				level.AddExperience(xp);
				if (countdown>0.25f) { countdown = 0.25f; }
				isactive=false;
			}
		}
	}
}
