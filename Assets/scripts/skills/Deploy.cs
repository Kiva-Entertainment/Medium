using System.Collections.Generic;

public class Deploy : BasicSkill {

	public override int getCost () { return 0; }
	public override string getName () { return "Deploy"; }
	
	public override void perform (params Loc[] locs) {
		new Golem (team: actor.team).deploy (locs [0]);
	}
	
	public override Loc[] getRange (bool onlyValid) {
		List<Loc> fullRange = BasicRange.sightline (actor, 1);

		if (onlyValid)
			return SkillValidCheck.check(fullRange, unoccupied: true); 
		else
			return SkillValidCheck.check(fullRange, unoccupied: false); 
	}
}
