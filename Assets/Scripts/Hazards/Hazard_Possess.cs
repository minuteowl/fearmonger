using UnityEngine;
using System.Collections;

public class Hazard_Possess : Hazard {

	// Use this for initialization
	protected override void Start () {
		duration = GameVars.duration_possession;
		damage = GameVars.damage_possession;
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}
}
