using UnityEngine;
using System.Collections.Generic;

public class Deploy : BasicSkill {

	public override int getCost () { return 0; }
	public override string getName () { return "Deploy"; }
	
	public override void perform (params Loc[] locs) {
		Generator.getGolem(team: actor.team).deploy(locs[0]);
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

			result.Add(potLoc);
		}
		
		return result.ToArray ();

	}
}
