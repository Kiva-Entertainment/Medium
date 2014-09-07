﻿using UnityEngine;
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
		List<Loc> result = new List<Loc> ();

		// TODO make this exist in a single place. Make a range script
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

			if (Unit.get (potLoc) == null)
				continue;
				
			result.Add(potLoc);
		}
		
		return result.ToArray ();
	}
	
}
