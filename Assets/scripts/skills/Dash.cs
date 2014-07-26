using UnityEngine;
using System.Collections;

public class Dash : Skill {
	public int getCost () { return 0; }
	public string getName () { return "Dash"; }
	public int getNumTargets () { return 1; }
	public int getRange () { return 0; }

	public Loc[] getValidTargets (Unit actor)
	{
		Loc[] result = new Loc[] { actor.loc };
		return result;
	}
	
	public void perform (params Loc[] locs) {
		// exception handling here
		Unit u = World.current.getUnit (locs [0]);
		if (u != null) {
			u.mvCur += 2;
		}
	}
}
