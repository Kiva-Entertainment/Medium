using UnityEngine;
using System.Collections;

public class Golem : Unit {
	public Golem(int team) : base(name: "Golem",
	                        type: "Golem",
	                        hpMax: 120,
	                        hpCur: 120,
	                        spMax: 80,
	                        spCur: 0,
	                        mvMax: 4,
	                        mvCur: 4,
	                        jump: 0.4f,
	                        strength: 120,
	                        toughness: 140,
	                        intelligence: 60,
	                        willpower: 80,
	                        regen: 0.12f,
	                        team: team)
	{
		addSkill (new Punch ().setActor(this) );
		addSkill (new Dash ().setActor(this) );
	}
}