using UnityEngine;
using System.Collections.Generic;

public class Punch : Skill {

	private Unit actor;

	public int getCost () { return 0; }
	public string getName () { return "Punch"; }
	public int getNumTargets () { return 1; }
	public int getRange () { return 1; }

	public Punch (Unit actor) {
		this.actor = actor;
	}
	
	public void perform (params Loc[] locs) {
		actor.playAnim ("punch");

		// exception handling here
		Unit u = World.current.getUnit (locs [0]);
		if (u != null) {
			u.takeDamage (10);
		}
	}
	
	public Loc[] getValidTargets (Unit actor) {
		List<Loc> result = new List<Loc> ();
		
		foreach (Loc offset in Loc.cardinals) {
			Loc potLoc = actor.loc.plus (offset);
			if (World.current.isInBounds(potLoc))
				result.Add (potLoc);
		}
		
		return result.ToArray ();
	}
	
}
