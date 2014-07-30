using UnityEngine;
using System.Collections.Generic;

public class Punch : BasicSkill {

	public override int getCost () { return 0; }
	public override string getName () { return "Punch"; }
	
	public override void perform (params Loc[] locs) {
		actor.playAnim ("punch");

		Unit u = World.current.getUnit (locs [0]);
		if (u != null) {
			u.takeDamage (100);
		}
	}
	
	public override Loc[] getRange (bool onlyValid) {
		List<Loc> result = new List<Loc> ();
		
		foreach (Loc offset in Loc.cardinals) {
			Loc potLoc = actor.loc.plus (offset);

			if (!World.current.isInBounds(potLoc))
				continue;

			// These are requirements for space range
			// When checking if valid, must also ensure that space is occupied
			if (!onlyValid) {
				result.Add(potLoc);
				continue;
			}

			if (World.current.getUnit(potLoc) == null)
				continue;
				
			result.Add(potLoc);
		}
		
		return result.ToArray ();
	}
	
}
