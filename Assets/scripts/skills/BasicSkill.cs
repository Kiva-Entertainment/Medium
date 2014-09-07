using UnityEngine;
using System.Collections;

public abstract class BasicSkill : Skill {
	protected Unit actor;

	public Skill setActor (Unit u) {
		actor = u;
		return this;
	}

	protected int extent;

	public int getExtent () { return extent; }
	public void raiseExtent () { extent++; }
	public void lowerExtent () { if(extent > 0) extent--; }

	public abstract int getCost ();
	public abstract string getName ();
	//public abstract int getNumTargets ();
	//public abstract int getRange ();
	public abstract Loc[] getRange (bool onlyValid = false);
	public abstract void perform (params Loc[] locs);
}
