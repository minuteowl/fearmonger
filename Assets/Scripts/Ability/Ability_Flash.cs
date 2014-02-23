using UnityEngine;
using System.Collections;

public class Ability_Flash : Ability {

	LampObject lamp1, lamp2;
	RoomObject Room;

	public Ability_Flash () {
		Name = "Flicker Lights";
		Description = "Flicker the lamps in the room.";
		Cost = 2;
		Level=2;
		Duration = 0f;
	}

	public override void UseAbility(Leveling lvl, MonoBehaviour[] room) {
		this.Room = (RoomObject)room[0];
		lamp1 = Room.lamp1.GetComponent<LampObject>();
		lamp2 = Room.lamp2.GetComponent<LampObject>();
		lamp1.FlickerTimer(7);
		lamp2.FlickerTimer(7);

	}

}
