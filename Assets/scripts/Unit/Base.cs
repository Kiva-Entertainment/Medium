using UnityEngine;
using System.Collections;

public class Base : Unit {
	public Base(int team) : base(name: "Base",
                                  type: "Base",
                                  hpMax: 100,
                                  hpCur: 100,
                                  spMax: 0,
                                  spCur: 0,
                                  actMax: 1,
                                  actCur: 1,
                                  mvMax: 0,
                                  mvCur: 0,
                                  jump: 0.0f,
                                  strength: 0,
                                  toughness: 0,
                                  intelligence: 0,
                                  willpower: 0,
                                  regen: 0.0f,
                                  team: team) {
		addSkill (new Deploy ().setActor(this) );
	}
}
