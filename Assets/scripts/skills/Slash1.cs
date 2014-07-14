using UnityEngine;
using System.Collections;

public class Slash1 : Skill {
	public int getCost () { return 0; }
	public string getName () { return "Slash1"; }
	public int getNumTargets () { return 1; }
	public int getRange () { return 1; }

	public void perform (params Loc[] locs) {
		// exception handling here
		Unit u = World.current.getUnit (locs [0]);
		u.takeDamage (10);
	}
}
