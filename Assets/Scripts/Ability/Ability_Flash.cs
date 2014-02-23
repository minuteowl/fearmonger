using UnityEngine;
using System.Collections;

public class Ability_Flash : Ability {

	LampObject lamp1, lamp2;
	RoomObject Room;

	public Ability_Flash () {
		this.Name = "Flicker Lights";
		this.Description = "Flicker the lamps in the room.";
		this.Damage = 0;
		this.Multiplier = 1;
		this.Cost = 2;
	}

	public override void Use(Leveling lvl, MonoBehaviour[] room) {
		this.Room = (RoomObject)room[0];
		lamp1 = Room.lamp1.GetComponent<LampObject>();
		lamp2 = Room.lamp2.GetComponent<LampObject>();
		lamp1.FlickerTimer(7);
		lamp2.FlickerTimer(7);
		lvl.UseAbility(this,0);
	}

}
