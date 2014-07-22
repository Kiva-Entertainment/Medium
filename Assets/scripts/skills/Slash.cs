using UnityEngine;
using System.Collections.Generic;

public class Slash : Skill {
	public int getCost () { return 0; }
	public string getName () { return "Slash"; }
	public int getNumTargets () { return 1; }
	public int getRange () { return 1; }

	public void perform (params Loc[] locs) {
		// exception handling here
		Unit u = World.current.getUnit (locs [0]);
		if (u != null) {
			u.takeDamage (10);
			Debug.Log (u.hpCur);
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

	// TODO remove maybe idk
	public void setTarget (Loc loc) {

	}
}
