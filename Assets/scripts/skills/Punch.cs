using UnityEngine;
using System.Collections.Generic;

public class Punch : BasicSkill {

	public override int getCost () { return 0 + 3 * extent; }
	public override string getName () { return "Punch"; }
	
	public override void perform (params Loc[] locs) {
		actor.playAnim ("punch");

		Unit u = Unit.get (locs [0]);
		if (u != null) {
			u.takeDamage (100 + 10 * extent);
		}
	}
	
	public override Loc[] getRange (bool onlyValid) {
		List<Loc> fullRange = BasicRange.sightline (actor, 1);
		
		if (onlyValid)
			return SkillValidCheck.check(fullRange, occupied: true); 
		else
			return SkillValidCheck.check(fullRange, occupied: false);
	}
	
}
