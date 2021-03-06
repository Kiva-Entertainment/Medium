﻿public class Dash : BasicSkill {
	
	public override int getCost () { return 0; }
	public override string getName () { return "Dash"; }
	
	public override void perform (params Loc[] locs) {
		actor.playAnim ("run");
		actor.mvCur += 2;
	}
	
	public override Loc[] getRange (bool onlyValid) {
		return BasicRange.self (actor).ToArray();
	}
	
}
