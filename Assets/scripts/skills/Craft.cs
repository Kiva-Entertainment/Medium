using System.Collections.Generic;

public class Craft : BasicSkill {
	
	public override int getCost () { return 0; }
	public override string getName () { return "Craft"; }
	
	public override void perform (params Loc[] locs) {
		actor.playAnim ("run");
		new Rock (team: 0).deploy (locs [0]);
	}
	
	public override Loc[] getRange (bool onlyValid) {
		List<Loc> fullRange = BasicRange.sightline (actor, 1);

		if (onlyValid)
			return SkillValidCheck.check(fullRange, unoccupied: true); 
		else
			return SkillValidCheck.check(fullRange, unoccupied: false);
	}
	
}
