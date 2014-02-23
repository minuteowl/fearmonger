using UnityEngine;
using System.Collections;
using System;

public class Ability_Push : Ability {

	GameObject tentacleObj, instance;
	PushTentacle tentacle;

	public Ability_Push () {
		Name = "Push";
		Description = "Extend a short tentacle to push victims.";
		Cost = 1;
		Level=0;
		Duration = 1f;
	}

	public override void UseAbility(Leveling lvl, MonoBehaviour[] args) {
		tentacleObj = Resources.Load<GameObject>("Generated/AbilityFX/Tentacle_short");
		Selector grab = (Selector)args[0];
		//grab.ChangeSprite("Tentacle",0.5f);
		instance = GameObject.Instantiate(tentacleObj,grab.transform.position,Quaternion.identity) as GameObject;
		tentacle = instance.transform.GetComponent<PushTentacle>();
	}

}
