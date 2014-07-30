using UnityEngine;
using System.Collections.Generic;

public class Dash : BasicSkill {
	
	public override int getCost () { return 0; }
	public override string getName () { return "Dash"; }
	
	public override void perform (params Loc[] locs) {
		actor.playAnim ("run");
		actor.mvCur += 2;
	}
	
	public override Loc[] getRange (bool onlyValid) {
		Loc[] result = new Loc[] { actor.loc };
		return result;
	}
	
}
