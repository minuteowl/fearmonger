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

	void OnCollisionEnter2D(Collision2D other)  {
		if (isactive) {
			if (p=other.transform.GetComponent<PersonObject>()) {
			xp = p.DecreaseSanity(1);
			level.AddExperience(xp);
			isactive=false;
		}}
	}
}
